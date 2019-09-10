using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;
using skycoin;

namespace Skyapi.Test.Api
{
    internal static class LiveTest
    {
        internal static void AddressCount(DefaultApi instance)
        {
            var result = instance.AddressCount();
            // 5296 addresses as of 2018-03-06, the count could decrease but is unlikely to
            Assert.True(result.Count > 5000, "Please set disable-networking=false");
        }

        internal static void AddressUxouts(DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "no addresses",
                    errCode = 400,
                    errMsg = "Error calling AddressUxouts: 400 Bad Request - address is empty\n",
                    address = ""
                },
                new
                {
                    name = "invalid address length",
                    errCode = 400,
                    errMsg = "Error calling AddressUxouts: 400 Bad Request - Invalid address length\n",
                    address = "prRXwTcDK24hs6AFxj"
                },
                new
                {
                    name = "unknown address",
                    errCode = 200,
                    errMsg = "",
                    address = "prRXwTcDK24hs6AFxj69UuWae3LzhrsPW9"
                },
                new
                {
                    name = "one address",
                    errCode = 200,
                    errMsg = "",
                    address = "2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf"
                }
            };
            foreach (var tc in testCases)
            {
                if (tc.errCode != 0 && tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() => instance.AddressUxouts(tc.address), tc.name);
                    Assert.AreEqual(err.ErrorCode, tc.errCode, tc.name);
                    Assert.AreEqual(err.Message, tc.errMsg, tc.name);
                }
                else
                {
                    var result = instance.AddressUxouts(tc.address);
                    if (tc.name.Equals("unknown address"))
                    {
                        Assert.IsEmpty(result, tc.name);
                    }
                    else
                    {
                        Assert.IsNotEmpty(result, tc.name);
                    }
                }
            }
        }

        internal static void Balance(Method method, DefaultApi instance)
        {
// Genesis address check, should not have a balance
            var result = new Balance();
            Assert.DoesNotThrow(() =>
                result = Utils.BalanceWithMethod(method: method,
                    instance: instance, addrs: "2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6"));

            Assert.AreEqual(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(new Balance
            {
                Addresses = new Dictionary<string, BalancePair>
                {
                    ["2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6"] = new BalancePair
                    {
                        Confirmed = new BalanceConfirm(),
                        Predicted = new BalancePredict()
                    }
                },
                Confirmed = new BalanceConfirm(),
                Predicted = new BalancePredict()
            }), "Genesis address.");
            // Balance of final distribution address. Should have the same coins balance
            // for the next 15-20 years.

            Assert.DoesNotThrow(() =>
            {
                result = Utils.BalanceWithMethod(method: method,
                    instance: instance, addrs: "ejJjiCwp86ykmFr5iTJ8LxQXJ2wJPTYmkm");
            });
            Assert.AreEqual(result.Confirmed.Coins, result.Predicted.Coins);
            Assert.AreEqual(result.Confirmed.Hours, result.Predicted.Hours);
            Assert.AreNotEqual(0, result.Confirmed.Hours);

            // Add 1e4 because someone sent 0.01 coins to it
            var expectedBalance = decimal.Parse("1E6", NumberStyles.Any) * decimal.Parse("1E6", NumberStyles.Any) +
                                  decimal.Parse("1E4", NumberStyles.Any);
            Assert.AreEqual(expectedBalance, result.Confirmed.Coins);
            // Check that the balance is queryable for addresses known to be affected
            // by the coinhour overflow problem
            var address = new[]
            {
                "n7AR1VMW1pK7F9TxhYdnr3HoXEQ3g9iTNP",
                "2aTzmXi9jyiq45oTRFCP9Y7dcvnT6Rsp7u",
                "FjFLnus2ePxuaPTXFXfpw6cVAE5owT1t3P",
                "KT9vosieyWhn9yWdY8w7UZ6tk31KH4NAQK"
            };
            foreach (var s in address)
            {
                Assert.DoesNotThrow(
                    () => Utils.BalanceWithMethod(method: method, instance: instance, addrs: s),
                    $"Failed to get balance of address {s}");
            }

            Assert.DoesNotThrow(() => Utils.BalanceWithMethod(method: method, instance: instance,
                addrs: string.Join(",", address)));
        }

        internal static void BlockchainMetadata(DefaultApi instance)
        {
            var result = JsonConvert.DeserializeObject<BlockchainMetadata>(instance.BlockchainMetadata().ToString());
            Assert.AreNotEqual(0, result.Head.Seq);
        }

        internal static void BlockChainProgress(DefaultApi instance)
        {
            var result = instance.BlockchainProgress();
            Assert.AreNotEqual(0, result.Current);
            if (Utils.LiveDisableNetworking())
            {
                Assert.IsNull(result.Peer, "peer");
                Assert.AreEqual(result.Current, result.Highest);
            }
            else
            {
                Assert.IsNotNull(result.Peer);
                Assert.True(result.Highest >= result.Current);
            }
        }

        internal static void Block(DefaultApi instance)
        {
            var knownBadBlockSeqs = new[]
            {
                // coinhour fee calculation mistake, related to distribution addresses:
                297,
                741,
                743,
                749,
                796,
                4956,
                10125,
                // coinhour overflow related:
                11685,
                11707,
                11710,
                11709,
                11705,
                11708,
                11711,
                11706,
                11699,
                13277
            };
            if (Utils.LiveDisableNetworking())
            {
                Assert.Ignore("Skipping slow block tests when networking disabled");
            }

            foreach (var seq in knownBadBlockSeqs)
            {
                BlockSchema block = null;
                Assert.DoesNotThrow(() => { block = instance.Block(seq: seq); });

                Assert.AreEqual(seq, block.Header.Seq);
            }
        }

        internal static void Blocks(DefaultApi instance)
        {
            Utils.BlocksTest(instance: instance, seqs: new List<int?> {3, 5, 7});
            Utils.BlockInRangeTest(instance: instance, start: 1, end: 10);
        }

        internal static void CoinSupply(DefaultApi instance)
        {
            var cs = instance.CoinSupply();
            Assert.IsNotEmpty(cs.CurrentSupply);
            Assert.IsNotEmpty(cs.TotalSupply);
            Assert.IsNotEmpty(cs.MaxSupply);
            Assert.AreEqual("100000000.000000", cs.MaxSupply);
            Assert.IsNotEmpty(cs.CurrentCoinhourSupply);
            Assert.IsNotEmpty(cs.TotalCoinhourSupply);
            Assert.AreEqual(100, cs.UnlockedDistributionAddresses.Count + cs.LockedDistributionAddresses.Count);
        }

        internal static void Transactions(Method method, DefaultApi instance)
        {
            var simpleaddrs = new[]
            {
                "2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKt"
            };
            var sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(Utils.TransactionsWithMethod(method: method,
                    instance: instance, addrs: string.Join(",", simpleaddrs)).ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress");
            Utils.AssertNoTransactionsDupes(sresult);
            var multiaddrs = new[]
            {
                "7cpQ7t3PZZXvjTst8G7Uvs7XH4LeM8fBPD",
                "2K6NuLBBapWndAssUtkxKfCtyjDQDHrEhhT"
            };
            var mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance,
                        addrs: string.Join(",", multiaddrs)).ToString());
            Assert.True(mresult.Count >= 4, "multiaddress");
            Utils.AssertNoTransactionsDupes(mresult);
            //Unconfirmedtransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance,
                            addrs: string.Join(",", simpleaddrs), confirmed: "false")
                        .ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress, confirmed=false");
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance,
                            addrs: string.Join(",", multiaddrs), confirmed: "false")
                        .ToString());
            Assert.True(mresult.Count >= 0, "multiaddress, confirm=false");
            Assert.True(mresult.Count >= sresult.Count, "mresult.Count >= sresult.Count confirmed=false");
            Utils.AssertNoTransactionsDupes(mresult);
            //ConfirmedTransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance,
                            addrs: string.Join(",", simpleaddrs), confirmed: "true")
                        .ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress, confirm=true");
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, confirmed: "true")
                        .ToString());
            Assert.True(mresult.Count >= 0, "simpleaddress, confirm=true");
            Assert.True(mresult.Count >= sresult.Count, "mresult.Count >= sresult.Count confirmed=true");
            Utils.AssertNoTransactionsDupes(mresult);
        }

        internal static void Health(DefaultApi instance)
        {
            var result = JsonConvert.DeserializeObject<Health>(instance.Health().ToString());
            Utils.CheckHealthResponse(result);
            if (Utils.LiveDisableNetworking())
            {
                Assert.AreEqual(0, result.Open_Connections);
                Assert.AreEqual(0, result.Outgoing_Connections);
                Assert.AreEqual(0, result.Incoming_Connections);
            }
            else
            {
                Assert.AreNotEqual(0, result.Open_Connections);
            }

            Assert.AreEqual(result.Outgoing_Connections + result.Incoming_Connections, result.Open_Connections);
        }

        internal static void LastBlock(DefaultApi instance)
        {
            BlockSchema prevBlock = null;
            var results = JsonConvert.DeserializeObject<InlineResponse2001>(instance.LastBlocks(10).ToString());
            Assert.AreEqual(10, results.Blocks.Count);
            results.Blocks.ForEach(b =>
            {
                if (prevBlock != null)
                {
                    Assert.AreEqual(prevBlock.Header.BlockHash, b.Header.PreviousBlockHash);
                }

                var bh = instance.Block(hash: b.Header.BlockHash);
                Assert.NotNull(bh);
                Assert.AreEqual(b.ToJson(), bh.ToJson());
                prevBlock = b;
            });
        }

        internal static void NetworkConnection(DefaultApi instance)
        {
            var connections = instance.NetworkConnections();
            if (Utils.LiveDisableNetworking())
            {
                Assert.IsEmpty(connections.Connections);
                return;
            }

            Assert.IsNotEmpty(connections.Connections);
            var check = false;
            connections.Connections.ForEach(cc =>
            {
                NetworkConnectionSchema connection = null;
                try
                {
                    connection = instance.NetworkConnection(cc.Address);
                }
                catch (ApiException err)
                {
                    // The connection may have disconnected by now
                    if (err.ErrorCode == 404 || err.Message == "Error calling NetworkConnection: 404 Not Found\n")
                    {
                        return;
                    }
                }

                Assert.NotNull(cc.Address);
                Assert.AreEqual(cc.Address, connection?.Address);
                Assert.AreEqual(cc.Id, connection?.Id);
                Assert.AreEqual(cc.ListenPort, connection?.ListenPort);
                Assert.AreEqual(cc.Mirror, connection?.Mirror);
                switch (cc.State)
                {
                    case NetworkConnectionSchema.StateEnum.Introduced:
                        Assert.AreEqual(NetworkConnectionSchema.StateEnum.Introduced, connection?.State);
                        break;
                    case NetworkConnectionSchema.StateEnum.Connected:
                        Assert.AreNotEqual(NetworkConnectionSchema.StateEnum.Pending, connection?.State);
                        break;
                }

                if (cc.State == NetworkConnectionSchema.StateEnum.Pending)
                {
                    Assert.AreEqual(0, cc.Id);
                }
                else
                {
                    Assert.AreNotEqual(0, cc.Id);
                }

                Assert.AreEqual(cc.Outgoing, connection?.Outgoing);
                Assert.True(cc.LastReceived <= connection?.LastReceived);
                Assert.True(cc.LastSent <= connection.LastSent);
                Assert.AreEqual(cc.ConnectedAt, connection.ConnectedAt);
                check = true;
            });
            Assert.True(check,
                "Was not able to find any connection by address, despite finding connections when querying all");
            connections = instance.NetworkConnections(states: "pending");
            connections.Connections.ForEach(
                cc => { Assert.AreEqual(NetworkConnectionSchema.StateEnum.Pending, cc.State); });
            connections = instance.NetworkConnections(direction: "incoming");
            connections.Connections.ForEach(cc => Assert.False(cc.Outgoing ?? true, "Outgoing is true or empty."));
        }

        internal static void NetworkConnectionExchange(DefaultApi instance)
        {
            Assert.DoesNotThrow(() => instance.NetworkConnectionsExchange());
        }

        internal static void Outputs(Method method, DefaultApi instance)
        {
            var outputs = Utils.OutputsWithMethod(method: method, instance: instance);
            Assert.IsNotEmpty(outputs.HeaderOutputs);
        }


        internal static void PendingTxs(DefaultApi instance)
        {
            Assert.DoesNotThrow(() => instance.PendingTxs());
        }

        internal static void ResendUnconfirmedTxns(DefaultApi instance)
        {
            if (!Utils.LiveDisableNetworking()) return;
            try
            {
                if (Utils.UseCsrf()) instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                instance.ResendUnconfirmedTxns();
            }
            catch (ApiException err)
            {
                Assert.AreEqual(503, err.ErrorCode);
                Assert.AreEqual("Error calling ResendUnconfirmedTxns: 503 Service Unavailable - Networking " +
                                "is disabled\n", err.Message);
            }
        }

        internal static void RichList(DefaultApi instance)
        {
            dynamic richlist = instance.Richlist();
            Assert.IsNotEmpty(richlist.richlist);
            Assert.AreEqual(20, richlist.richlist.Count);
            richlist = instance.Richlist(includeDistribution: true, n: "150");
            Assert.AreEqual(150, richlist.richlist.Count);
        }


        internal static void ApiRawTxGet(DefaultApi instance)
        {
            var testcases = new List<dynamic>
            {
                new
                {
                    name = "invalid hex length",
                    txid = "abcd",
                    errCode = 400,
                    errMsg = "Error calling ApiV1RawtxGet: 400 Bad Request - Invalid hex length\n",
                    rawtxid = ""
                },
                new
                {
                    name = "odd length hex string",
                    txid = "abcdeffedca",
                    errCode = 400,
                    errMsg = "Error calling ApiV1RawtxGet: 400 Bad Request - encoding/hex: odd length hex string\n",
                    rawtxid = ""
                },
                new
                {
                    name = "OK - genesis tx",
                    txid = "d556c1c7abf1e86138316b8c17183665512dc67633c04cf236a8b7f332cb4add",
                    errCode = 200,
                    errMsg = "",
                    rawtxid =
                        "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                        "000100000000f8f9c644772dc5373d85e11094e438df707a42c900407a10f35a000000407a10f35a0000"
                },
                new
                {
                    name = "OK",
                    txid = "540582ee4128b733f810f149e908d984a5f403ad2865108e6c1c5423aeefc759",
                    errCode = 200,
                    errMsg = "",
                    rawtxid = "3d0100000088b4e967d77a8b7155c5378a85c199fabf94048aa84833ef5eab7818545bcda80200000071" +
                              "985c70041fe5a6408a2dfac2ea4963820bc603059521259debb114b2f6630b5658e7ff665b2db7878ce9" +
                              "b0d1d051ec66b5dea23274e52642bc7e451b273a90008afb06133958b03c4795d5a7acd001f3942cc6d3" +
                              "b19e93d357d2675fe9ba8bbf3db30b3cda779e441fced581aee88f48c8af017b30dc276b15be25d4bb44" +
                              "260c000200000050386f195b367f8261e66e3fdfbc942fbacfe25e117e554ca1c1caf8993454767afab0" +
                              "3c823346ff8b00c29df6acc05841583d90dfd451ba09e66884a48e83f70200000000ef3b60779f014b3c" +
                              "7acf27c16c9acc3ff3bea61600a8b54b06000000c2ba2400000000000037274869aaa4c2e2e5c9159502" +
                              "4c65f8f9458102404b4c0000000000c2ba240000000000"
                }
            };
            testcases.ForEach(tc =>
            {
                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() => instance.ApiV1RawtxGet(tc.txid));
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.AreEqual(tc.errMsg, err.Message, tc.name);
                }
                else
                {
                    var result = "";
                    Assert.DoesNotThrow(() => { result = instance.ApiV1RawtxGet(tc.txid); }, (string) tc.name);
                    Assert.AreEqual(tc.rawtxid, result);
                }
            });
        }

        internal static void NetworkConnectionDisconnect(DefaultApi instance)
        {
            var connections = instance.NetworkConnections();
            if (Utils.LiveDisableNetworking())
            {
                Assert.IsEmpty(connections.Connections);
                return;
            }

            Assert.IsNotEmpty(connections.Connections);
            connections.Connections.ForEach(cc =>
            {
                NetworkConnectionSchema connection = null;
                try
                {
                    connection = instance.NetworkConnection(cc.Address);
                }
                catch (ApiException err)
                {
                    // The connection may have disconnected by now
                    if (err.ErrorCode == 404 || err.Message == "Error calling NetworkConnection: 404 Not Found\n")
                    {
                        return;
                    }
                }

                Assert.AreEqual(cc.Id, connection?.Id);
                Assert.DoesNotThrow(() => instance.NetworkConnectionsDisconnect(cc.Id.ToString()));
            });
        }

        internal static void Transaction(DefaultApi instance)
        {
            var testCases = new List<dynamic>
            {
                new
                {
                    name = "invalid TxID",
                    txid = "abcd",
                    errCode = 400,
                    errMsg = "Error calling Transaction: 400 Bad Request - Invalid hex length\n",
                    golden = ""
                },
                new
                {
                    name = "empty txID",
                    txid = "",
                    errCode = 400,
                    errMsg = "Error calling Transaction: 400 Bad Request - txid is empty\n",
                    golden = ""
                },
                new
                {
                    name = "OK",
                    txid = "76ecbabc53ea2a3be46983058433dda6a3cf7ea0b86ba14d90b932fa97385de7",
                    errCode = 200,
                    errMsg = "",
                    golden = "transaction-block-517.golden"
                }
            };

            testCases.ForEach(tc =>
            {
                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() => instance.Transaction(tc.txid));
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.AreEqual(tc.errMsg, err.Message, tc.name);
                }
                else
                {
                    var result = (Transaction) instance.Transaction(tc.txid);
                    // tx.Status.Height is how many blocks are above this transaction,
                    // make sure it is past some checkpoint height
                    Assert.True(result.Status.Height > 50836);
                    // readable.TransactionWithStatus.Status.Height is not stable
                    result.Status.Height = 0;
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                }
            });
        }

        public static void TransactionInjectDisableNetworking(DefaultApi instance)
        {
            Utils.RequireWalletEnv();
            var prepareAndCheck = Utils.PrepareAndCheckWallet(instance, 2e6, 20);
            var walletname = Utils.GetWalletName();
            var wallet = instance.Wallet(walletname);
            var totalcoins = prepareAndCheck.Item2;
            var totalhours = prepareAndCheck.Item3;
            var pass = prepareAndCheck.Item4;
            var tReq = new WalletTransactionRequest
            {
                Id = walletname,
                Password = pass,
                HoursSelection = new TransactionV2ParamsHoursSelection
                {
                    Type = "manual"
                },
                ChangeAddress = wallet.Entries[0].Address,
                To = new List<TransactionV2ParamsTo>
                {
                    new TransactionV2ParamsTo
                    {
                        Address = wallet.Entries[1].Address,
                        Coins = Utils.ToDropletString((ulong) totalcoins),
                        Hours = (totalhours / 2).ToString()
                    }
                }
            };
            Assert.DoesNotThrow(() =>
            {
                var txResponse = instance.WalletTransaction(tReq);
                var err = Assert.Throws<ApiException>(() => instance.TransactionInject(txResponse.EncodedTransaction));
                Assert.AreEqual(503, err.ErrorCode);
                Assert.True(err.Message.Contains("503 Service Unavailable - Outgoing connections are disabled"));
            });
        }


        public static void TransactionInjectEnableNetworking(DefaultApi instance)
        {
//            Utils.RequireWalletEnv();
//            var prepareAndCheck = Utils.PrepareAndCheckWallet(instance, 2E6, 2);
//            var totalcoins = prepareAndCheck.Item2;
//            var totalhours = prepareAndCheck.Item3;
//            var pass = prepareAndCheck.Item4;
//            var prepareTxnFunc = new Func<string, ulong, string, Tuple<string, string>>(
//                (toAddr, coins, shareFactor) =>
//                {
//                    var walletname = Utils.GetWalletName();
//                    var wallet = instance.Wallet(walletname);
//                    var tReq = new WalletTransactionRequest
//                    {
//                        Id = walletname,
//                        Password = pass,
//                        HoursSelection = new TransactionV2ParamsHoursSelection
//                        {
//                            Type = "auto",
//                            Mode = "share",
//                            ShareFactor = shareFactor
//                        },
//                        ChangeAddress = wallet.Entries[0].Address,
//                        To = new List<TransactionV2ParamsTo>
//                        {
//                            new TransactionV2ParamsTo
//                            {
//                                Address = toAddr,
//                                Coins = Utils.ToDropletString((ulong) coins),
//                            }
//                        }
//                    };
//                    var txnResponse = instance.WalletTransaction(tReq);
     //       return new Tuple<string, string>(" ", " ");
       // });
    }

    internal static void WalletBalance(DefaultApi instance)
    {
    Utils.RequireWalletEnv();
    var walletname = Utils.GetWalletName();
    Assert.DoesNotThrow(() =>
    {
        var walletbalance = instance.WalletBalance(walletname);
        Assert.NotNull(walletbalance);
        Assert.NotNull(walletbalance.Addresses);
    });
}

internal static void WalletTransactions(DefaultApi instance)
{
Utils.RequireWalletEnv();
var walletid = Utils.GetWalletName();
var result = instance.WalletTransactions(walletid);

var bw = instance.WalletBalance(walletid);
    if (bw.Predicted.Coins != bw.Confirmed.Coins)
{
    Assert.NotNull(result.Transactions);
}
else
{
    Assert.IsNull(result.Transactions);
}
}

internal static void Uxouts(DefaultApi instance)
{
// A spent uxout should never change
Assert.DoesNotThrow(() =>
{
    dynamic ux = instance.Uxout("fe6762d753d626115c8dd3a053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f20");
    Utils.CheckGoldenFile("uxout-spent.golden", ux, ux.GetType());
    Assert.AreNotEqual(0, ux.spent_block_seq);
});
}
internal static void TransactionPost(DefaultApi instance)
{
Utils.RequireWalletEnv();
var prepareAndCheckWallet = Utils.PrepareAndCheckWallet(instance, 2e6, 20);
var wallet = prepareAndCheckWallet.Item1;
var totalCoins = prepareAndCheckWallet.Item2;
var totalHours = prepareAndCheckWallet.Item3;
var testCases = MakeLiveCreateTxnTestCases(instance, wallet, totalCoins, totalHours);
foreach (var tc in testCases)
{
Assert.DoesNotThrow(
() =>
{
Assert.False(tc.Outputs.Length != 0 && tc.OutputsSubset.Length != 0,
"outputs and outputsSubset can't both be set");
if (tc.ErrCode == 400)
{
var err = Assert.Throws<ApiException>(() => instance.TransactionsPost());
Assert.AreEqual(tc.ErrCode, err.ErrorCode);
Assert.True(err.Message.Contains(tc.ErrMsg));
return;
}
var result = instance.TransactionPost(tc.Req);
AssertCreateTransactionResult(instance, tc, result, true, null);
}, tc.Name);
}
}
private static void AssertCreateTransactionResult(DefaultApi instance, LiveCreateTxnTestCase tc,
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
private static void AssertVerifyTransaction(IDefaultApi instance, string dataEncodedTransaction, bool unsigned)
{
if (unsigned)
{
Assert.DoesNotThrow(() => instance.TransactionVerify(dataEncodedTransaction, true));
var err = Assert.Throws<ApiException>(() => instance.TransactionVerify(dataEncodedTransaction));
Assert.AreEqual(422, err.ErrorCode);
Assert.True(
err.Message.Contains("Transaction violates hard constraint: Unsigned input in transaction"));
}
else
{
Assert.DoesNotThrow(() => instance.TransactionVerify(dataEncodedTransaction));
var err = Assert.Throws<ApiException>(() => instance.TransactionVerify(dataEncodedTransaction,
true));
Assert.AreEqual(422, err.ErrorCode);
Assert.True(
err.Message.Contains("Transaction violates hard constraint: Unsigned input in transaction"));
}
}
private static void AssertRequestedHours(IReadOnlyList<TransactionV2ParamsTo> reqTo,
IReadOnlyList<TransactionOutput> transactionOutputs)
{
for (var i = 0; i < reqTo.Count; i++)
{
var toHours = ulong.Parse(reqTo[i].Hours);
var outHours = ulong.Parse(transactionOutputs[i].Hours);
Assert.AreEqual(toHours, outHours);
}
}
private static void AssertCreateTransactionValid(CreatedTransaction dataTransaction, bool unsigned)
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
private static void AssertRequestedCoins(List<TransactionV2ParamsTo> reqTo,
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
private static void AssertEncodedTxnMatchesTxn(DefaultApi instance, InlineResponse2008Data result)
{
Assert.NotNull(result.EncodedTransaction);
var emptyTxn =
"00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
Assert.AreNotEqual(emptyTxn, result.EncodedTransaction);
var txnEncode = instance.ApiV1RawtxGet(result.Transaction.Txid);
Assert.AreEqual(result.EncodedTransaction, txnEncode);
}
private struct LiveCreateTxnTestCase
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
private static LiveCreateTxnTestCase[] MakeLiveCreateTxnTestCases(DefaultApi instance,
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