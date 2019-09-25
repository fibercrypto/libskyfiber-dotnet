using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;
using skycoin;

namespace Skyapi.Test.Api
{
    [TestFixture]
    public class LiveTest : skycoin.skycoin
    {
        private DefaultApi _instance;

        [SetUp]
        public void Init()
        {
            _instance = new DefaultApi(Utils.GetNodeHost());
        }

        [Test]
        public void AddressCount()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var result = _instance.AddressCount();
            // 5296 addresses as of 2018-03-06, the count could decrease but is unlikely to
            Assert.True(result.Count > 5000, "Please set disable-networking=false");
        }

        [Test]
        public void AddressUxouts()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

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
                    var err = Assert.Throws<ApiException>(() => _instance.AddressUxouts(tc.address), tc.name);
                    Assert.AreEqual(err.ErrorCode, tc.errCode, tc.name);
                    Assert.AreEqual(err.Message, tc.errMsg, tc.name);
                }
                else
                {
                    var result = _instance.AddressUxouts(tc.address);
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

        [Test]
        public void BalanceGet()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Balance(Method.GET);
        }

        [Test]
        public void BalancePost()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Balance(Method.POST);
        }

        private void Balance(Method method)
        {
// Genesis address check, should not have a balance
            var result = new Balance();
            Assert.DoesNotThrow(() =>
                result = Utils.BalanceWithMethod(method: method,
                    instance: _instance, addrs: "2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6"));

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
                    instance: _instance, addrs: "ejJjiCwp86ykmFr5iTJ8LxQXJ2wJPTYmkm");
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
                    () => Utils.BalanceWithMethod(method: method, instance: _instance, addrs: s),
                    $"Failed to get balance of address {s}");
            }

            Assert.DoesNotThrow(() => Utils.BalanceWithMethod(method: method, instance: _instance,
                addrs: string.Join(",", address)));
        }

        [Test]
        public void BlockchainMetadata()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var result = JsonConvert.DeserializeObject<BlockchainMetadata>(_instance.BlockchainMetadata().ToString());
            Assert.AreNotEqual(0, result.Head.Seq);
        }

        [Test]
        public void BlockChainProgress()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var result = _instance.BlockchainProgress();
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

        [Test]
        public void Block()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

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
                Assert.DoesNotThrow(() => { block = _instance.Block(seq: seq); });

                Assert.AreEqual(seq, block.Header.Seq);
            }
        }

        [Test]
        public void Blocks()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Utils.BlocksTest(instance: _instance, seqs: new List<int?> {3, 5, 7});
            Utils.BlockInRangeTest(instance: _instance, start: 1, end: 10);
        }

        [Test]
        public void CoinSupply()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var cs = _instance.CoinSupply();
            Assert.IsNotEmpty(cs.CurrentSupply);
            Assert.IsNotEmpty(cs.TotalSupply);
            Assert.IsNotEmpty(cs.MaxSupply);
            Assert.AreEqual("100000000.000000", cs.MaxSupply);
            Assert.IsNotEmpty(cs.CurrentCoinhourSupply);
            Assert.IsNotEmpty(cs.TotalCoinhourSupply);
            Assert.AreEqual(100, cs.UnlockedDistributionAddresses.Count + cs.LockedDistributionAddresses.Count);
        }

        [Test]
        public void TransactionsGet()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Assert.Ignore();

            Transactions(Method.GET);
        }

        [Test]
        public void TransactionsPost()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Assert.Ignore();
            Transactions(Method.POST);
        }

        private void Transactions(Method method)
        {
            var simpleaddrs = new[]
            {
                "2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKt"
            };
            var sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(Utils.TransactionsWithMethod(method: method,
                    instance: _instance, addrs: string.Join(",", simpleaddrs)).ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress");
            Utils.AssertNoTransactionsDupes(sresult);
            var multiaddrs = new[]
            {
                "7cpQ7t3PZZXvjTst8G7Uvs7XH4LeM8fBPD",
                "2K6NuLBBapWndAssUtkxKfCtyjDQDHrEhhT"
            };
            var mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: _instance,
                        addrs: string.Join(",", multiaddrs)).ToString());
            Assert.True(mresult.Count >= 4, "multiaddress");
            Utils.AssertNoTransactionsDupes(mresult);
            //Unconfirmedtransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: _instance,
                            addrs: string.Join(",", simpleaddrs), confirmed: "false")
                        .ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress, confirmed=false");
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: _instance,
                            addrs: string.Join(",", multiaddrs), confirmed: "false")
                        .ToString());
            Assert.True(mresult.Count >= 0, "multiaddress, confirm=false");
            Assert.True(mresult.Count >= sresult.Count, "mresult.Count >= sresult.Count confirmed=false");
            Utils.AssertNoTransactionsDupes(mresult);
            //ConfirmedTransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: _instance,
                            addrs: string.Join(",", simpleaddrs), confirmed: "true")
                        .ToString());
            Assert.True(sresult.Count >= 0, "simpleaddress, confirm=true");
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: _instance, confirmed: "true")
                        .ToString());
            Assert.True(mresult.Count >= 0, "simpleaddress, confirm=true");
            Assert.True(mresult.Count >= sresult.Count, "mresult.Count >= sresult.Count confirmed=true");
            Utils.AssertNoTransactionsDupes(mresult);
        }

        [Test]
        public void Health()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var result = JsonConvert.DeserializeObject<Health>(_instance.Health().ToString());
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

        [Test]
        public void LastBlock()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            BlockSchema prevBlock = null;
            var results = JsonConvert.DeserializeObject<InlineResponse2001>(_instance.LastBlocks(10).ToString());
            Assert.AreEqual(10, results.Blocks.Count);
            results.Blocks.ForEach(b =>
            {
                if (prevBlock != null)
                {
                    Assert.AreEqual(prevBlock.Header.BlockHash, b.Header.PreviousBlockHash);
                }

                var bh = _instance.Block(hash: b.Header.BlockHash);
                Assert.NotNull(bh);
                Assert.AreEqual(b.ToJson(), bh.ToJson());
                prevBlock = b;
            });
        }

        [Test]
        public void NetworkConnection()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var connections = _instance.NetworkConnections();
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
                    connection = _instance.NetworkConnection(cc.Address);
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
            connections = _instance.NetworkConnections(states: "pending");
            connections.Connections.ForEach(
                cc => { Assert.AreEqual(NetworkConnectionSchema.StateEnum.Pending, cc.State); });
            connections = _instance.NetworkConnections(direction: "incoming");
            connections.Connections.ForEach(cc => Assert.False(cc.Outgoing ?? true, "Outgoing is true or empty."));
        }

        [Test]
        public void NetworkConnectionExchange()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Assert.DoesNotThrow(() => _instance.NetworkConnectionsExchange());
        }

        [Test]
        public void OutputsGet()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Outputs(Method.GET);
        }

        [Test]
        public void OutputsPost()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Outputs(Method.POST);
        }

        private void Outputs(Method method)
        {
            var outputs = Utils.OutputsWithMethod(method: method, instance: _instance);
            Assert.IsNotEmpty(outputs.HeaderOutputs);
        }

        [Test]
        public void PendingTxs()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Assert.DoesNotThrow(() => _instance.PendingTxs());
        }

        [Test]
        public void ResendUnconfirmedTxns()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            if (!Utils.LiveDisableNetworking()) return;
            try
            {
                if (Utils.UseCsrf()) _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(_instance));
                _instance.ResendUnconfirmedTxns();
            }
            catch (ApiException err)
            {
                Assert.AreEqual(503, err.ErrorCode);
                Assert.AreEqual("Error calling ResendUnconfirmedTxns: 503 Service Unavailable - Networking " +
                                "is disabled\n", err.Message);
            }
        }

        [Test]
        public void RichList()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            dynamic richlist = _instance.Richlist();
            Assert.IsNotEmpty(richlist.richlist);
            Assert.AreEqual(20, richlist.richlist.Count);
            richlist = _instance.Richlist(includeDistribution: true, n: "150");
            Assert.AreEqual(150, richlist.richlist.Count);
        }

        [Test]
        public void ApiRawTxGet()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

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
                    var err = Assert.Throws<ApiException>(() => _instance.ApiV1RawtxGet(tc.txid));
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.AreEqual(tc.errMsg, err.Message, tc.name);
                }
                else
                {
                    var result = "";
                    Assert.DoesNotThrow(() => { result = _instance.ApiV1RawtxGet(tc.txid); }, (string) tc.name);
                    Assert.AreEqual(tc.rawtxid, result);
                }
            });
        }

        [Test]
        public void NetworkConnectionDisconnect()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            var connections = _instance.NetworkConnections();
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
                    connection = _instance.NetworkConnection(cc.Address);
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

                Assert.DoesNotThrow(() =>
                {
                    if (Utils.UseCsrf())
                    {
                        _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
                    }

                    _instance.NetworkConnectionsDisconnect(cc.Id.ToString());
                });
            });
        }

        [Test]
        public void Transaction()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

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
                    var err = Assert.Throws<ApiException>(() => _instance.Transaction(tc.txid));
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.AreEqual(tc.errMsg, err.Message, tc.name);
                }
                else
                {
                    var result = (Transaction) _instance.Transaction(tc.txid);
                    // tx.Status.Height is how many blocks are above this transaction,
                    // make sure it is past some checkpoint height
                    Assert.True(result.Status.Height > 50836);
                    // readable.TransactionWithStatus.Status.Height is not stable
                    result.Status.Height = 0;
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                }
            });
        }

        [Test]
        public void TransactionInjectDisableNetworking()
        {
            if (!(Utils.GetTestMode().Equals("live") && Utils.LiveDisableNetworking()))
            {
                return;
            }

            Utils.RequireWalletEnv();
            var prepareAndCheck = Utils.PrepareAndCheckWallet(_instance, 2e6, 20);
            var walletname = Utils.GetWalletName();
            var wallet = _instance.Wallet(walletname);
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
                if (Utils.UseCsrf())
                {
                    _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
                }

                var txResponse = _instance.WalletTransaction(tReq);
                var err = Assert.Throws<ApiException>(() => _instance.TransactionInject(txResponse.EncodedTransaction));
                Assert.AreEqual(503, err.ErrorCode);
                Assert.True(err.Message.Contains("503 Service Unavailable - Outgoing connections are disabled"));
            });
        }

        [Test]
        public void TransactionInjectEnableNetworking()
        {
            if (!Utils.GetTestMode().Equals("live") || Utils.LiveDisableNetworking())
            {
                return;
            }

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

        [Test]
        public void WalletBalance()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            if (Utils.DisableWalletApi())
            {
                Assert.Ignore("Wallet API are disabled.");
            }

            Utils.RequireWalletEnv();
            var walletname = Utils.GetWalletName();
            Assert.DoesNotThrow(() =>
            {
                var walletbalance = _instance.WalletBalance(walletname);
                Assert.NotNull(walletbalance);
                Assert.NotNull(walletbalance.Addresses);
            });
        }

        [Test]
        public void WalletTransactions()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            if (Utils.DisableWalletApi())
            {
                Assert.Ignore("Wallet API are disabled.");
            }
            
            Utils.RequireWalletEnv();
            var walletid = Utils.GetWalletName();
            var result = _instance.WalletTransactions(walletid);

            var bw = _instance.WalletBalance(walletid);
            if (bw.Predicted.Coins != bw.Confirmed.Coins)
            {
                Assert.NotNull(result.Transactions);
            }
            else
            {
                Assert.IsEmpty(result.Transactions);
            }
        }

        [Test]
        public void Uxouts()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            if (Utils.LiveDisableNetworking())
            {
                Assert.Ignore("Skipping slow ux out tests when networking disabled");
            }

// A spent uxout should never change
            Assert.DoesNotThrow(() =>
            {
                dynamic ux = _instance.Uxout("fe6762d753d626115c8dd3a053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f20");
                Utils.CheckGoldenFile("uxout-spent.golden", ux, ux.GetType());
                Assert.AreNotEqual(0, ux.spent_block_seq);
            });
        }

        [Test]
        public void TransactionPost()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Utils.RequireWalletEnv();
            var prepareAndCheckWallet = Utils.PrepareAndCheckWallet(_instance, 2e6, 20);
            var wallet = prepareAndCheckWallet.Item1;
            var totalCoins = prepareAndCheckWallet.Item2;
            var totalHours = prepareAndCheckWallet.Item3;
            var testCases = TxnUtils.MakeLiveCreateTxnTestCases(_instance, wallet, totalCoins, totalHours);
            foreach (var tc in testCases)
            {
                if (Utils.UseCsrf())
                {
                    _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
                }

                Assert.DoesNotThrow(
                    () =>
                    {
                        Assert.False(tc.Outputs.Length != 0 && tc.OutputsSubset.Length != 0,
                            "outputs and outputsSubset can't both be set");
                        if (tc.ErrCode == 400)
                        {
                            var err = Assert.Throws<ApiException>(() => _instance.TransactionPost());
                            Assert.AreEqual(tc.ErrCode, err.ErrorCode);
                            Assert.True(err.Message.Contains(tc.ErrMsg));
                            return;
                        }

                        var result = _instance.TransactionPost(tc.Req);
                        TxnUtils.AssertCreateTransactionResult(_instance, tc, result.Data, true, null);
                    }, tc.Name);
            }
        }

        [Test]
        public void WalletSignTransaction()
        {
            if (!Utils.GetTestMode().Equals("live"))
            {
                return;
            }

            Utils.RequireWalletEnv();

            var prepareAndCheckWallet = Utils.PrepareAndCheckWallet(_instance, 2e6, 20);
            var wallet = prepareAndCheckWallet.Item1;
            var pass = prepareAndCheckWallet.Item4;
            var entryLenGoUint32Ptr = new_GoUint32Ptr();
            var err = SKY_api_Handle_GetWalletEntriesCount(wallet, entryLenGoUint32Ptr);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var entryLen = skycoin.skycoin.GoUint32Ptr_value(entryLenGoUint32Ptr);
            // Fetch outputs held by the wallet
            var addresses = new List<string>();
            for (var i = 0; i < entryLen; i++)
            {
                addresses.Add(Utils.GetAddressOfWalletEntries(i, wallet));
            }

            // Abort if the transaction is spending summary
            var summary = _instance.OutputsGet(addresses);

            // Need at least 2 summary for the created transaction
            Assert.True(summary.HeaderOutputs.Count > 1);

            // Use the first two outputs for a transaction
            var headOutputs = summary.HeaderOutputs.GetRange(0, 2);
            var outputs = Utils.ToUxArray(headOutputs);
            var uint64P = new_GoUint64p();
            err = SKY_coin_UxArray_Coins(outputs, uint64P);
            Assert.AreEqual(SKY_OK, err);
            var totalcoins = GoUint64p_value(uint64P);
            var totalcoinString = Utils.ToDropletString(totalcoins);
            var uxOutHashes = new List<string>();
            var hashes = new cipher_SHA256s();
            SKY_coin_UxArray_Hashes(outputs, hashes);
            for (var i = 0; i < hashes.count; i++)
            {
                uxOutHashes.Add(hashes.getAt(i).getStr().p);
            }

            // Create an unsigned transaction using two inputs
            // Ensure at least 2 inputs
            // Specify outputs in the request to create txn
            // Specify unsigned in the request to create txn

            if (Utils.UseCsrf())
            {
                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
            }

            var txnResp = _instance.WalletTransaction(new WalletTransactionRequest
            {
                Id = Utils.GetWalletName(),
                Unsigned = true,
                Password = pass,
                Unspents = uxOutHashes,
                HoursSelection = new TransactionV2ParamsHoursSelection
                {
                    Type = "auto",
                    Mode = "share",
                    ShareFactor = "0.5"
                },
                To = new List<TransactionV2ParamsTo>
                {
                    new TransactionV2ParamsTo
                    {
                        Address = Utils.GetAddressOfWalletEntries(0, wallet),
                        Coins = totalcoinString
                    }
                }
            });
//            // Create an invalid txn with an extra null sig
//            invalidTxn := coin.MustDeserializeTransactionHex(txnResp.EncodedTransaction)
//            invalidTxn.Sigs = append(invalidTxn.Sigs, cipher.Sig{})
//            require.NotEqual(t, len(invalidTxn.In), len(invalidTxn.Sigs))

            var testCases = new[]
            {
                new
                {
                    name = "sign one input",
                    req = new WalletTransactionSignRequest
                    {
                        WalletId = Utils.GetWalletName(),
                        Password = pass,
                        SignIndexes = new List<long?> {1},
                        EncodedTransaction = txnResp.EncodedTransaction
                    },
                    fullySigned = false,
                    errMg = "",
                    errCode = 200,
                },
                new
                {
                    name = "sign all input",
                    req = new WalletTransactionSignRequest
                    {
                        WalletId = Utils.GetWalletName(),
                        Password = pass,
                        SignIndexes = null,
                        EncodedTransaction = txnResp.EncodedTransaction
                    },
                    fullySigned = true,
                    errMg = "",
                    errCode = 200,
                },
                new
                {
                    name = "sign invalid input",
                    req = new WalletTransactionSignRequest
                    {
                        WalletId = Utils.GetWalletName(),
                        Password = pass,
                        SignIndexes = new List<long?> {1},
                        //       EncodedTransaction = invalid Txn
                    },
                    fullySigned = false,
                    errMg = "Transaction violates hard constraint: Invalid number of signatures",
                    errCode = 400,
                },
            };

            var doTest =
                new Action<string, WalletTransactionSignRequest, bool, string, int>(
                    (name, req, fullySigned, errMsg, errCode) =>
                    {
                        if (Utils.UseCsrf())
                        {
                            _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
                        }

                        if (!errMsg.Equals(""))
                        {
                            var errApiException = Assert.Throws<ApiException>(() =>
                                _instance.WalletTransactionSign(req));
                            Assert.AreEqual(errCode, errApiException.ErrorCode, name);
                            Assert.True(errApiException.Message.Contains(errMsg), name);
                            return;
                        }

                        var resp = _instance.WalletTransactionSign(req);

//                        txn, err := coin.DeserializeTransactionHex(tc.req.EncodedTransaction)
//                        require.NoError(t, err)
//
//                        // TxID should have changed
//                        require.NotEqual(t, txn.Hash(), resp.Transaction.TxID)
//                        // Length, InnerHash should not have changed
//                        require.Equal(t, txn.Length, resp.Transaction.Length)
//                        require.Equal(t, txn.InnerHash.Hex(), resp.Transaction.InnerHash)

                        if (fullySigned)
                        {
                            if (Utils.UseCsrf())
                            {
                                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN",
                                    Utils.GetCsrf(instance: _instance));
                            }

                            Assert.DoesNotThrow(() => _instance.TransactionVerify(resp.Data.EncodedTransaction),
                                name);
                            if (Utils.UseCsrf())
                            {
                                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN",
                                    Utils.GetCsrf(instance: _instance));
                            }

                            var errApiException = Assert.Throws<ApiException>(() =>
                                _instance.TransactionVerify(resp.Data.EncodedTransaction, true), name);
                            Assert.True(errApiException.Message.Contains(
                                    "Transaction violates hard constraint: Unsigned transaction must contain a null signature"),
                                name);
                        }
                        else
                        {
                            if (Utils.UseCsrf())
                            {
                                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN",
                                    Utils.GetCsrf(instance: _instance));
                            }

                            var errApiException = Assert.Throws<ApiException>(() =>
                                _instance.TransactionVerify(resp.Data.EncodedTransaction), name);
                            Assert.True(errApiException.Message.Contains(
                                "Transaction violates hard constraint: Unsigned input in transaction"), name);
                            if (Utils.UseCsrf())
                            {
                                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN",
                                    Utils.GetCsrf(instance: _instance));
                            }

                            Assert.DoesNotThrow(
                                () => { _instance.TransactionVerify(resp.Data.EncodedTransaction, true); }, name);
                        }
                    });
            foreach (var tc in testCases)
            {
                doTest(tc.name, tc.req, tc.fullySigned, tc.errMg, tc.errCode);
            }

            if (Utils.UseCsrf())
            {
                _instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: _instance));
            }

            // Create a partially signed transaction then sign the remainder of it
            var response = _instance.WalletTransactionSign(new WalletTransactionSignRequest
            {
                WalletId = Utils.GetWalletName(),
                Password = pass,
                SignIndexes = new List<long?> {1},
                EncodedTransaction = txnResp.EncodedTransaction
            });
            doTest(
                "sign partially signed transaction", //name
                new WalletTransactionSignRequest //req
                {
                    WalletId = Utils.GetWalletName(),
                    Password = pass,
                    EncodedTransaction = response.Data.EncodedTransaction
                },
                true, //fullySigned
                "", //errMsg
                0); //errCode
        }

        [Test]
        public void WalletCreateTransactionSpecificUnsigned()
        {
            TxnUtils.WalletCreateTransactionSpecific(_instance, true);
        }

        [Test]
        public void WalletCreateTransactionSpecificSigned()
        {
            TxnUtils.WalletCreateTransactionSpecific(_instance, false);
        }

        [Test]
        public void TestLiveWalletCreateTransactionRandomUnsigned()
        {
            TxnUtils.TestLiveWalletCreateTransactionRandom(_instance, true);
        }

        [Test]
        public void TestLiveWalletCreateTransactionRandomSigned()
        {
            TxnUtils.TestLiveWalletCreateTransactionRandom(_instance, false);
        }
    }
}