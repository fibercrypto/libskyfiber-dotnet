using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

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
            var rawtx = "00000000000000000000000000000000000000000000000000000000000000000000000000000" +
                        "00000000000000100000000f8f9c644772dc5373d85e11094e438df707a42c900407a10f35a00" +
                        "0000407a10f35a0000";
            if (Utils.UseCsrf()) instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
            var err = Assert.Throws<ApiException>(() => instance.TransactionInject(rawtx),
                "valid request, networking disabled");
            Console.WriteLine(err.Message);
            Assert.AreEqual(400, err.ErrorCode);
            Assert.AreEqual(
                "Error calling TransactionInject: 400 Bad Request - Transaction violates hard " +
                "constraint: No inputs", err.Message);
        }

        public static void TransactionInjectEnableNetworking(DefaultApi instance)
        {
            dynamic walletTxnDat =
                Utils.PrepareAndCheckWallet(instance, (long) decimal.Parse("2E6", NumberStyles.Any), 2);

            var testcases = new[]
            {
                new
                {
                    name = "send all coins to the first address",
                    txnReq = Utils.CreateTxnReq(instance, walletTxnDat.wallet, "1", new List<object>
                    {
                        new
                        {
                            address = walletTxnDat.wallet.Entries[0].Address,
//                            coins = Utils.ToDropletString(walletTxnDat.coins)
                        }
                    }),
                    errCode = 200,
                    errMsg = "",
                    checkt = new Action<Transaction>(transaction =>
                    {
                        decimal coin = 0M;
                        transaction.Txn.Outputs.ForEach(o =>
                        {
                            var output = JsonConvert.DeserializeObject<dynamic>(o.ToString());
//                            coin += Utils.FromDropletString(output.coins);
                        });
                        Assert.AreEqual(walletTxnDat.coins, coin);
                        var coinbalance =
                            JsonConvert.DeserializeObject<Balance>(
                                instance.BalanceGet(walletTxnDat.wallet.Entries[0].Address).ToString()).Confirmed.coins;
                        Assert.AreEqual(walletTxnDat.coins, coinbalance);
                    })
                },
                new
                {
                    name = "send 0.003 coin to second address",
                    txnReq = Utils.CreateTxnReq(instance, walletTxnDat.wallet, "0.5", new List<object>
                    {
                        new
                        {
                            address = walletTxnDat.wallet.Entries[1].Address,
//                            coins = Utils.ToDropletString(decimal.Parse("3E3", NumberStyles.Any))
                        }
                    }),
                    errCode = 200,
                    errMsg = "",
                    checkt = new Action<Transaction>(transaction =>
                    {
                        Assert.AreEqual(2, transaction.Txn.Outputs.Count);
                        var addrsOutputInTxn = new Func<string, dynamic>(addrs =>
                        {
                            dynamic tOut = null;
                            transaction.Txn.Outputs.ForEach(o =>
                            {
                                var output = JsonConvert.DeserializeObject<dynamic>(o.ToString());
                                if (output.dst == addrs)
                                {
                                    tOut = output;
                                }
                            });
                            return tOut;
                        });

                        // Confirms the second address has 0.003 coin
                        var outp = addrsOutputInTxn(walletTxnDat.wallet.Entries[1].Address);
                        Assert.AreEqual("0.003000", outp.coins);
//                        var coin = Utils.FromDropletString(outp.coins);

                        // Gets the expected change coins
//                        var expectedExchange = walletTxnDat.coins - coin;

                        var changeout = addrsOutputInTxn(walletTxnDat.wallet.Entries[0].Address);
//                        var changecoins = Utils.ToDropletString(changeout.coins);
//                        Assert.AreEqual(expectedExchange, changecoins);
                    })
                },
                new
                {
                    name = "send to null address",
                    txnReq = Utils.CreateTxnReq(instance, walletTxnDat.wallet, "1", new List<object>
                    {
                        new
                        {
                            address = walletTxnDat.wallet.Entries[0].Address,
//                            coins = Utils.ToDropletString(walletTxnDat.coins)
                        }
                    }),
                    errCode = 200,
                    errMsg = "",
                    checkt = new Action<Transaction>(transaction => { })
                }
            };

            foreach (var tc in testcases)
            {
                Transaction txn = null;
                Assert.DoesNotThrow(() =>
                {
                    var transaction = instance.WalletTransaction(tc.txnReq);
                    var txid = instance.TransactionInject(transaction.encoded_transaction);
                    Assert.Equals(transaction.transaction.txid, txid);
                    var t = Task.Run(async delegate
                    {
                        await Task.Delay(45000);
                        txn = instance.Transaction(txid);
                        if (!(txn.Status.Confirmed ?? true))
                        {
                            Assert.Fail("Waiting for transaction to be confirmed timeout");
                        }
                    });
                    t.Wait();
                    tc.checkt(txn);
                }, tc.name);
            }
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
           var prepareAndCheckWallet= Utils.PrepareAndCheckWallet(instance:instance,2e6,20);
           var wallet = prepareAndCheckWallet.Item1;
           var totalCoins = prepareAndCheckWallet.Item2;
           var totalHours = prepareAndCheckWallet.Item3;
           
        }
    }
}