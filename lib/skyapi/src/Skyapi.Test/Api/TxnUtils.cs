using System;
using System.Collections.Generic;
using NUnit.Framework;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;
using skycoin;

namespace Skyapi.Test.Api
{
    internal class TxnUtils
    {
        internal static void AssertCreateTransactionResult(DefaultApi instance, LiveCreateTxnTestCase tc,
            InlineResponse2008 result, bool unsigned, Wallet w)
        {
            if (tc.OutputsSubset.Length == 0)
            {
                Assert.AreEqual(tc.Outputs.Length, result.Data.Transaction.Outputs.Count);
            }

            for (var i = 0; i < tc.Outputs.Length; i++)
            {
                var coins = Utils.FromDropletString(result.Data.Transaction.Outputs[i].Coins);
                Assert.AreEqual(tc.Outputs[i].Coins, coins);
                if (!tc.IgnoreHours)
                {
                    var hours = uint.Parse(result.Data.Transaction.Outputs[i].Hours);
                    Assert.AreEqual(tc.Outputs[i].Hours, hours);
                }

                var unsignedChar = skycoin.skycoin.new_CharPtr();
                skycoin.skycoin.SKY_cipher_Address_Null(tc.Outputs[i].Address, unsignedChar);
                if (skycoin.skycoin.CharPtr_value(unsignedChar) == 1)
                {
// The final change output may not have the address specified,
// if the ChangeAddress was not specified in the wallet params.
                    Assert.AreEqual(i, tc.Outputs.Length - 1);
                    Assert.Null(tc.Req.ChangeAddress);
                    var changeAddress = result.Data.Transaction.Outputs[i].Address;
                    Assert.False(Utils.IsNullAddress(changeAddress));
                    if (w != null && w.Meta.Type == "bip44")
                    {
// Check that the change address was a new address generated
// from the wallet's change path

// Get the update wallet from the API.
// Look for the last change address.
// It should match the change address that was used.
// Compare it to the previous wallet
                        var w2 = instance.Wallet(w.Meta.Id);
                        var lastChangeEntry = Utils.GetLastChangeEntry(w2);
// Compare it to the initial wallet state.
// It should be a new address with an incremented child number
                        var prevLastChangeEntry = Utils.GetLastChangeEntry(w);
                        Assert.AreNotEqual(prevLastChangeEntry, lastChangeEntry);
                        if (prevLastChangeEntry == null)
                        {
                            Assert.AreEqual(0, lastChangeEntry.ChildNumber);
                        }
                        else
                        {
                            Assert.AreEqual(prevLastChangeEntry.ChildNumber + 1, lastChangeEntry.ChildNumber);
                        }

// Make sure that the last change address in the wallet was used
                        Assert.False(Utils.IsNullAddress(lastChangeEntry.Address));
                        Assert.AreEqual(changeAddress, lastChangeEntry.Address);
                    }
                    else
                    {
// Check that the automatically-selected change address was one
// of the addresses for the UTXOs spent by the transaction
                        var changeAddrFound = false;
                        foreach (var inp in result.Data.Transaction.Inputs)
                        {
                            Assert.False(Utils.IsNullAddress(inp.Address));
                            if (changeAddress == inp.Address)
                            {
                                changeAddrFound = true;
                                break;
                            }
                        }

                        Assert.True(changeAddrFound);
                    }
                }
                else
                {
                    var address = new _GoString_();
                    skycoin.skycoin.SKY_cipher_Address_String(tc.Outputs[i].Address, address);
                    Assert.AreEqual(address.p, result.Data.Transaction.Outputs[i].Address);
                }
            }

// The wallet should be unmodified if the wallet type is not bip44
            if (w != null && w.Meta.Type != "bip44")
            {
                var w2 = instance.Wallet(w.Meta.Id);
                Assert.AreEqual(w, w2);
            }

            AssertEncodedTxnMatchesTxn(instance, result.Data);
            AssertRequestedCoins(tc.Req.To, result.Data.Transaction.Outputs);
            AssertCreateTransactionValid(result.Data.Transaction, unsigned);
            if (tc.Req.HoursSelection.Type == "manual")
            {
                AssertRequestedHours(tc.Req.To, result.Data.Transaction.Outputs);
            }

            tc.AdditionalRespVerify?.Invoke(result.Data.Transaction);
            AssertVerifyTransaction(instance, result.Data.EncodedTransaction, unsigned);
        }

        internal static void AssertVerifyTransaction(DefaultApi instance, string dataEncodedTransaction, bool unsigned)
        {
            if (unsigned)
            {
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                }
                Assert.DoesNotThrow(() => instance.TransactionVerify(dataEncodedTransaction, true));
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                }
                var err = Assert.Throws<ApiException>(() => instance.TransactionVerify(dataEncodedTransaction));
                Assert.AreEqual(422, err.ErrorCode);
                Assert.True(
                    err.Message.Contains("Transaction violates hard constraint: Unsigned input in transaction"));
            }
            else
            {
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                }
                Assert.DoesNotThrow(() => instance.TransactionVerify(dataEncodedTransaction));
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                }
                var err = Assert.Throws<ApiException>(() => instance.TransactionVerify(dataEncodedTransaction,
                    true));
                Assert.AreEqual(422, err.ErrorCode);
                Assert.True(
                    err.Message.Contains("Transaction violates hard constraint: Unsigned input in transaction"));
            }
        }

        internal static void AssertRequestedHours(IReadOnlyList<TransactionV2ParamsTo> reqTo,
            IReadOnlyList<TransactionOutput> transactionOutputs)
        {
            for (var i = 0; i < reqTo.Count; i++)
            {
                var toHours = ulong.Parse(reqTo[i].Hours);
                var outHours = ulong.Parse(transactionOutputs[i].Hours);
                Assert.AreEqual(toHours, outHours);
            }
        }

        internal static void AssertCreateTransactionValid(CreatedTransaction dataTransaction, bool unsigned)
        {
            Assert.NotNull(dataTransaction.Inputs);
            Assert.NotNull(dataTransaction.Outputs);
            Assert.AreEqual(dataTransaction.Inputs.Count, dataTransaction.Sigs.Count);
            if (unsigned)
            {
                foreach (var s in dataTransaction.Sigs)
                {
                    var cipherSig = new cipher_Sig();
                    skycoin.skycoin.SKY_cipher_SigFromHex(s, cipherSig);
                    Assert.AreEqual(new cipher_Sig(), cipherSig);
                }
            }

            var fee = ulong.Parse(dataTransaction.Fee);
            Assert.AreNotEqual(0, fee);
            ulong inputHours = 0;
            ulong inputCoins = 0;
            foreach (var inp in dataTransaction.Inputs)
            {
                Assert.NotNull(inp.CalculatedHours);
                var calculateHours = ulong.Parse(inp.CalculatedHours);
                var unsignedLongLong = skycoin.skycoin.new_GoUint64p();
                var err = skycoin.skycoin.SKY_util_AddUint64(inputHours, calculateHours, unsignedLongLong);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                inputHours = skycoin.skycoin.GoUint64p_value(unsignedLongLong);
                Assert.NotNull(inp.Hours);
                var hours = ulong.Parse(inp.Hours);
                Assert.True(hours <= calculateHours);
                Assert.NotNull(inp.Coins);
                var coins = Utils.FromDropletString(inp.Coins);
                err = skycoin.skycoin.SKY_util_AddUint64(inputCoins, coins, unsignedLongLong);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                inputCoins = skycoin.skycoin.GoUint64p_value(unsignedLongLong);
            }

            ulong outputHours = 0;
            ulong outputCoins = 0;
            foreach (var outp in dataTransaction.Outputs)
            {
                var hours = ulong.Parse(outp.Hours);
                var unsignedLongLong = skycoin.skycoin.new_GoUint64p();
                var err = skycoin.skycoin.SKY_util_AddUint64(outputHours, hours, unsignedLongLong);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                outputHours = skycoin.skycoin.GoUint64p_value(unsignedLongLong);
                var coins = ulong.Parse(outp.Coins);
                err = skycoin.skycoin.SKY_util_AddUint64(outputCoins, coins, unsignedLongLong);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                outputCoins = skycoin.skycoin.GoUint64p_value(unsignedLongLong);
            }

            Assert.True(inputHours > outputHours);
            Assert.AreEqual(inputHours - outputHours, fee);
            Assert.AreEqual(inputCoins, outputCoins);
            Assert.AreEqual(0, dataTransaction.Type);
            Assert.AreEqual(0, dataTransaction.Type);
            Assert.NotNull(dataTransaction.Length);
        }

        internal static void AssertRequestedCoins(List<TransactionV2ParamsTo> reqTo,
            List<TransactionOutput> transactionOutputs)
        {
            ulong requestCoins = 0;
            foreach (var o in reqTo)
            {
                var c = Utils.FromDropletString(o.Coins);
                requestCoins += c;
            }

            ulong sentCoins = 0;
            for (var i = 0; i < reqTo.Count; i++)
            {
                var c = Utils.FromDropletString(transactionOutputs[i].Coins);
                sentCoins += c;
            }

            Assert.AreEqual(requestCoins, sentCoins);
        }

        internal static void AssertEncodedTxnMatchesTxn(DefaultApi instance, InlineResponse2008Data result)
        {
            Assert.NotNull(result.EncodedTransaction);
            var emptyTxn =
                "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Assert.AreNotEqual(emptyTxn, result.EncodedTransaction);
            var txnEncode = instance.ApiV1RawtxGet(result.Transaction.Txid);
            Assert.AreEqual(result.EncodedTransaction, txnEncode);
        }

        internal struct LiveCreateTxnTestCase
        {
            public string Name;
            public TransactionV2ParamsAddress Req;
            public string ErrMsg;
            public int ErrCode;
            public coin__TransactionOutput[] Outputs;
            public coin__TransactionOutput[] OutputsSubset;
            public bool IgnoreHours;
            public Action<CreatedTransaction> AdditionalRespVerify;
        }

        internal static LiveCreateTxnTestCase[] MakeLiveCreateTxnTestCases(DefaultApi instance,
            SWIGTYPE_p_Wallet__Handle wallet, long totalCoins, long totalHours)
        {
            var remainingHoursUint64P = skycoin.skycoin.new_GoUint64p();
            var err = skycoin.skycoin.SKY_fee_RemainingHours((ulong) totalHours, 10, remainingHoursUint64P);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var remainingHours = skycoin.skycoin.GoUint64p_value(remainingHoursUint64P);
            Assert.True(remainingHours > 1);
            var unknownOutputs = Utils.RandSha256();
            var defaultChangeAddress = Utils.GetAddressOfWalletEntries(0, wallet);
//Get all outputs
            var outputs = instance.OutputsGet();
//Split outputs into those held by the wallet and those not
            var entryLenGoUint32Ptr = skycoin.skycoin.new_GoUint32Ptr();
            err = skycoin.skycoin.SKY_api_Handle_GetWalletEntriesCount(wallet, entryLenGoUint32Ptr);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var entryLen = skycoin.skycoin.GoUint32Ptr_value(entryLenGoUint32Ptr);
            var addresses = new List<string>();
            for (var i = 0; i < entryLen; i++)
            {
                addresses.Add(Utils.GetAddressOfWalletEntries(i, wallet));
            }

            var walletOutputHashes = new List<string>();
            var walletOutputs = new List<UnspentOutput>();
            var walletAuxs = new Dictionary<string, List<string>>();
            foreach (var o in outputs.HeaderOutputs)
            {
                if (addresses.Exists(addrs => addrs.Equals(o.Address)))
                {
                    walletOutputs.Add(o);
                    walletOutputHashes.Add(o.Hash);
                    if (!walletAuxs.ContainsKey(o.Address))
                    {
                        walletAuxs.Add(o.Address, new List<string>());
                    }

                    walletAuxs[o.Address].Add(o.Hash);
                }
            }

            Assert.IsNotEmpty(walletOutputs);
            return new[]
            {
                new LiveCreateTxnTestCase
                {
                    Name = "invalid decimal",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = "0.0001",
                                Hours = "1"
                            }
                        }
                    },
                    ErrMsg = "to[0].coins has too many decimal places",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "overflowing hours",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = "0.001",
                                Hours = "1"
                            },
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = "0.001",
                                Hours = long.MaxValue.ToString()
                            },
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = "0.001",
                                Hours = (long.MaxValue - 1).ToString()
                            }
                        }
                    },
                    ErrMsg = "total output hours error: uint64 addition overflow",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "insufficient coins",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = (totalCoins + 1).ToString(),
                                Hours = "1"
                            }
                        }
                    },
                    ErrMsg = "balance is not sufficient",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "insufficient hours",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(0, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins),
                                Hours = (totalHours + 1).ToString()
                            }
                        }
                    },
                    ErrMsg = "hours is not sufficient",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
// NOTE: this test will fail if "totalCoins - 1e3" does not require
// all of the outputs to be spent, e.g. if there is an output with
// "totalCoins - 1e3" coins in it.
// TODO -- Check that the wallet does not have an output of 0.001,
// because then this test cannot be performed, since there is no
// way to use all outputs and produce change in that case.
                    Name = "valid request, manual one output with change, spend all",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins - (ulong) 1e3),
                                Hours = "1"
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) (totalCoins - 1e3),
                            Hours = 1
                        },
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(0, wallet),
                            Coins = (ulong) (1e3),
                            Hours = remainingHours - 1
                        }
                    }
                },
                new LiveCreateTxnTestCase
                {
// NOTE: this test will fail if "totalCoins - 1e3" does not require
// all of the outputs to be spent, e.g. if there is an output with
// "totalCoins - 1e3" coins in it.
// TODO -- Check that the wallet does not have an output of 0.001,
// because then this test cannot be performed, since there is no
// way to use all outputs and produce change in that case.
                    Name = "valid request, manual one output with change, spend all, unspecified change address",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) (totalCoins - 1e3)),
                                Hours = "1"
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) (totalCoins - 1e3),
                            Hours = 1
                        },
                        new coin__TransactionOutput
                        {
// Address omitted -- will be checked later in the test body
                            Coins = (ulong) (1e3),
                            Hours = remainingHours - 1
                        }
                    }
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, manual one output with change, don't spend all",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) 1e3),
                                Hours = "1"
                            }
                        }
                    },
                    OutputsSubset = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) 1e3,
                            Hours = 1
                        }
// NOTE: change omitted,
// change is too difficult to predict in this case, we are
// just checking that not all uxouts get spent in the transaction
                    }
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, manual one output no change",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins),
                                Hours = "1"
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) totalCoins,
                            Hours = 1
                        }
                    }
                },
                new LiveCreateTxnTestCase
                {
// NOTE: no reliable way to test the ignore unconfirmed behavior,
// this test only checks that if IgnoreUnconfirmed is specified,
// the API doesn't throw up some parsing error
                    Name = "valid request, manual one output no change, ignore unconfirmed",
                    Req = new TransactionV2ParamsAddress
                    {
                        IgnoreUnconfirmed = true,
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins),
                                Hours = "1"
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) totalCoins,
                            Hours = 1
                        }
                    }
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, auto one output no change, share factor recalculates to 1.0",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "auto",
                            Mode = "share",
                            ShareFactor = "0.5"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins),
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) totalCoins,
                            Hours = remainingHours
                        }
                    }
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, auto two outputs with change",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "auto",
                            Mode = "share",
                            ShareFactor = "0.5"
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) 1e3),
                            },
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) (totalCoins - 2e3)),
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) 1e3,
                        },
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) (totalCoins - 2e3),
                        },
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(0, wallet),
                            Coins = (ulong) 1e3,
                        }
                    },
                    IgnoreHours = true // the hours are too unpredictable
                },
                new LiveCreateTxnTestCase
                {
                    Name = "uxout does not exist",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual",
                        },
                        UxOuts = new List<string> {Utils.Sha256Hex(unknownOutputs)},
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) totalCoins),
                                Hours = "1"
                            }
                        }
                    },
                    ErrMsg = $"unspent output of {Utils.Sha256Hex(unknownOutputs)} does not exist",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "insufficient balance with uxouts",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual",
                        },
                        UxOuts = new List<string> {walletOutputs[0].Hash},
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) (totalCoins + 1e3)),
                                Hours = "1"
                            }
                        }
                    },
                    ErrMsg = "balance is not sufficient",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "insufficient hours with uxouts",
                    Req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual",
                        },
                        UxOuts = new List<string> {walletOutputs[0].Hash},
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) 1e3),
                                Hours = (totalHours + 1).ToString()
                            }
                        }
                    },
                    ErrMsg = "hours are not sufficient",
                    ErrCode = 400
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, uxouts specified",
                    Req = new TransactionV2ParamsAddress
                    {
// NOTE: all uxouts are provided, which has the same behavior as
// not providing any uxouts or addresses.
// Using a subset of uxouts makes the wallet setup very
// difficult, especially to make deterministic, in the live test
// More complex cases should be covered by unit tests
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual",
                        },
                        UxOuts = walletOutputHashes,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) 1e3),
                                Hours = (totalHours + 1).ToString()
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) (totalCoins - 1e3),
                            Hours = 1
                        },
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) 1e3,
                            Hours = remainingHours - 1
                        }
                    },
                    AdditionalRespVerify = txnResp =>
                        Assert.AreEqual(walletOutputHashes.Count, txnResp.Inputs.Count)
                },
                new LiveCreateTxnTestCase
                {
                    Name = "valid request, addresses specified",
                    Req = new TransactionV2ParamsAddress
                    {
// NOTE: all addresses are provided, which has the same behavior as
// not providing any addresses.
// Using a subset of addresses makes the wallet setup very
// difficult, especially to make deterministic, in the live test
// More complex cases should be covered by unit tests
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual",
                        },
                        Address = addresses,
                        ChangeAddress = defaultChangeAddress,
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = Utils.GetAddressOfWalletEntries(1, wallet),
                                Coins = Utils.ToDropletString((ulong) (totalCoins - 1e3)),
                                Hours = "1"
                            }
                        }
                    },
                    Outputs = new[]
                    {
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(1, wallet),
                            Coins = (ulong) (totalCoins - 1e3),
                            Hours = 1
                        },
                        new coin__TransactionOutput
                        {
                            Address = Utils.GetCipherAddressOfWalletEntries(0, wallet),
                            Coins = (ulong) 1e3,
                            Hours = remainingHours - 1
                        }
                    }
                }
            };
        }
    }
}