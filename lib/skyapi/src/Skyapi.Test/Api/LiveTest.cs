using System.Collections.Generic;
using System.Globalization;
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
            Assert.True(result.Count > 5000);
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
                    Assert.IsEmpty(result, tc.name);
                }
            }
        }

        internal static void Balance(Method method, DefaultApi instance, bool useCsrf)
        {
// Genesis address check, should not have a balance
            var result = new Balance();
            Assert.DoesNotThrow(() =>
            {
                result = (Balance) Utils.BalanceWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                    addrs: "2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6");
            });
            Assert.AreEqual(result, new Balance
            {
                Addresses = new Dictionary<string, BalancePair>
                {
                    ["2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6"] = new BalancePair()
                }
            });

            // Balance of final distribution address. Should have the same coins balance
            // for the next 15-20 years.

            Assert.DoesNotThrow(() =>
            {
                result = (Balance) Utils.BalanceWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                    addrs: "ejJjiCwp86ykmFr5iTJ8LxQXJ2wJPTYmkm");
            });
            Assert.AreEqual(result.Confirmed.coins, result.Predicted.coins);
            Assert.AreEqual(result.Confirmed.hours, result.Predicted.hours);
            Assert.AreNotEqual(0, result.Confirmed.hours);

            // Add 1e4 because someone sent 0.01 coins to it
            var expectedBalance = decimal.Parse("1E6", NumberStyles.Any) * decimal.Parse("1E6", NumberStyles.Any) +
                                  decimal.Parse("1E4", NumberStyles.Any);
            Assert.AreEqual(expectedBalance, result.Confirmed.coins);
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
                    () => Utils.BalanceWithMethod(method: method, instance: instance, useCsrf: useCsrf, addrs: s),
                    $"Failed to get balance of address {s}");
            }

            Assert.DoesNotThrow(() => Utils.BalanceWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                addrs: string.Join(",", address)));
        }

        internal static void BlockchainMetadata(DefaultApi instance)
        {
            var result = JsonConvert.DeserializeObject<BlockchainMetadata>(instance.BlockchainMetadata().ToString());
            Assert.AreNotEqual(0, result.Head.Seq);
        }

        internal static void BlockChainProgress(DefaultApi instance, bool liveDisableNetworking)
        {
            var result = JsonConvert.DeserializeObject<Progress>(instance.BlockchainProgress().ToString());
            Assert.AreNotEqual(0, result.Current);

            if (liveDisableNetworking)
            {
                Assert.IsEmpty(result.Peer);
                Assert.AreEqual(result.Current, result.Highest);
            }
            else
            {
                Assert.IsNotEmpty(result.Peer);
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

            foreach (var seq in knownBadBlockSeqs)
            {
                var b = instance.Block(seq: seq);
                Assert.AreEqual(seq, b.Header.Seq);
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

        internal static void Transactions(Method method, DefaultApi instance, bool useCsrf)
        {
            var simpleaddrs = new[]
            {
                "2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKt"
            };
            var sresult =
                JsonConvert.DeserializeObject<List<Transaction>>((string) Utils.TransactionsWithMethod(method: method,
                    instance: instance, useCsrf: useCsrf, addrs: string.Join(",", simpleaddrs)));
            Assert.True(sresult.Count >= 0);
            Utils.AssertNoTransactionsDupes(sresult);
            var multiaddrs = new[]
            {
                "7cpQ7t3PZZXvjTst8G7Uvs7XH4LeM8fBPD",
                "2K6NuLBBapWndAssUtkxKfCtyjDQDHrEhhT"
            };
            var mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                        addrs: string.Join(",", multiaddrs)).ToString());
            Assert.True(mresult.Count >= 4);
            Utils.AssertNoTransactionsDupes(mresult);
            //Unconfirmedtransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                            addrs: string.Join(",", simpleaddrs), confirmed: "false")
                        .ToString());
            Assert.True(sresult.Count >= 0);
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                            addrs: string.Join(",", multiaddrs), confirmed: "false")
                        .ToString());
            Assert.True(mresult.Count >= 0);
            Assert.True(mresult.Count >= sresult.Count);
            Utils.AssertNoTransactionsDupes(mresult);
            //ConfirmedTransactions
            sresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                            addrs: string.Join(",", simpleaddrs), confirmed: "true")
                        .ToString());
            Assert.True(sresult.Count >= 0);
            Utils.AssertNoTransactionsDupes(sresult);
            mresult =
                JsonConvert.DeserializeObject<List<Transaction>>(
                    Utils.TransactionsWithMethod(method: method, instance: instance, useCsrf: useCsrf,
                            addrs: string.Join(",", multiaddrs), confirmed: "true")
                        .ToString());
            Assert.True(mresult.Count >= 0);
            Assert.True(mresult.Count >= sresult.Count);
            Utils.AssertNoTransactionsDupes(mresult);
        }

        internal static void Health(DefaultApi instance, bool liveDisableNetworking)
        {
            var result = JsonConvert.DeserializeObject<Health>(instance.Health().ToString());
            Utils.CheckHealthResponse(result);
            if (liveDisableNetworking)
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

        internal static void NetworkConnection(DefaultApi instance, bool liveDisableNetworking)
        {
            var connections = instance.NetworkConnections();
            if (liveDisableNetworking)
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

        internal static void Outputs(Method method, DefaultApi instance, bool useCsrf)
        {
            dynamic outputs = Utils.OutputsWithMethod(method: method, instance: instance, useCsrf: useCsrf);
            Assert.IsNotEmpty(outputs.head_outputs);
        }


        internal static void PendingTxs(DefaultApi instance)
        {
            Assert.DoesNotThrow(() => instance.PendingTxs());
        }

        internal static void ResendUnconfirmedTxns(DefaultApi instance, bool liveDisableNetworking, bool useCsrf)
        {
            if (!liveDisableNetworking) return;
            try
            {
                if (useCsrf) instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
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
                    var result = instance.ApiV1RawtxGet(tc.txid);
                    Assert.AreEqual(tc.rawtxid, result, tc.name);
                }
            });
        }

        internal static void NetworkConnectionDisconnect(DefaultApi instance, bool liveDisableNetworking)
        {
            var connections = instance.NetworkConnections();
            if (liveDisableNetworking)
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
    }
}