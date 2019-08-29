using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Skyapi.Test.Api
{
    internal static class StableTest
    {
        internal static void AddressCount(DefaultApi instance)
        {
            var result = instance.AddressCount();
            Assert.AreEqual(155, result.Count);
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
                    golden = "",
                    address = ""
                },
                new
                {
                    name = "unknown address",
                    errCode = 200,
                    errMsg = "",
                    golden = "uxout-noaddr.golden",
                    address = "prRXwTcDK24hs6AFxj69UuWae3LzhrsPW9"
                },
                new
                {
                    name = "one address",
                    errCode = 200,
                    errMsg = "",
                    golden = "uxout-addr.golden",
                    address = "2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf"
                }
            };
            foreach (var tc in testCases)
            {
                if (tc.errCode != 200 && tc.errCode != 0)
                {
                    var err = Assert.Throws<ApiException>(() => instance.AddressUxouts(tc.address));
                    Assert.AreEqual(err.ErrorCode, tc.errCode);
                    Assert.AreEqual(err.Message, tc.errMsg);
                }
                else
                {
                    var result = instance.AddressUxouts(tc.address);
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                }
            }
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
                    name = "not found",
                    txid = "540582ee4128b733f810f149e908d984a5f403ad2865108e6c1c5423aeefc759",
                    errCode = 404,
                    errMsg = "Error calling ApiV1RawtxGet: 404 Not Found\n",
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
                    name = "OK",
                    txid = "d556c1c7abf1e86138316b8c17183665512dc67633c04cf236a8b7f332cb4add",
                    errCode = 200,
                    errMsg = "",
                    rawtxid =
                        "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                        "000100000000f8f9c644772dc5373d85e11094e438df707a42c900407a10f35a000000407a10f35a0000"
                }
            };
            if (!Utils.DbNoUnconfirmed())
            {
                testcases.Add(new
                {
                    name = "unconfirmed",
                    txid = "701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947",
                    errCode = 200,
                    errMsg = "",
                    rawtxid = "dc00000000f8293dbfdddcc56a97664655ceee650715d35a0dda32a9f0ce0e2e99d48991240100000" +
                              "03981061c7275ae9cc936e902a5367fdd87ef779bbdb31e1e10d325d17a129abb34f6e597ceeaf67b" +
                              "b051774b41c58276004f6a63cb81de61d4693bc7a5536f320001000000fe6762d753d626115c8dd3a" +
                              "053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f2002000000003be2537f8c0893fddcddc878518f38" +
                              "ea493d949e008988068d0000002739570000000000009037ff169fbec6db95e2537e4ff79396c050a" +
                              "eeb00e40b54020000002739570000000000"
                });
            }

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

        internal static void Balance(DefaultApi instance, Method method)
        {
            var testCase = new List<dynamic>
            {
                new
                {
                    name = "unknown address",
                    addrs = new[] {"prRXwTcDK24hs6AFxj69UuWae3LzhrsPW9"},
                    file = "balance-noaddrs.golden"
                },
                new
                {
                    name = "one address",
                    addrs = new[] {"2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf"},
                    file = "balance-2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf.golden"
                },
                new
                {
                    name = "duplicate address",
                    addrs = new[] {"2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf", "2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf"},
                    file = "balance-2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf.golden"
                },
                new
                {
                    name = "two address",
                    addrs = new[] {"2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf", "qxmeHkwgAMfwXyaQrwv9jq3qt228xMuoT5"},
                    file = "balance-two-addrs.golden"
                }
            };
            if (!Utils.DbNoUnconfirmed())
            {
// Trying to append any value of the same type
                testCase.Add(new
                {
                    name = "balance affected by unconfirmed transaction",
                    addrs = new[] {"R6aHqKWSQfvpdo2fGSrq4F1RYXkBWR9HHJ", "212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"},
                    file = "balance-affected-by-unconfirmed-txns.golden"
                });
            }

            testCase.ForEach(
                tc => Utils.BalanceWithMethod(method: method, instance: instance,
                    addrs: string.Join(",", tc.addrs), golden: tc.file));
        }

        internal static void Block(DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "unknown hash",
                    golden = "",
                    hash = "80744ec25e6233f40074d35bf0bfdbddfac777869b954a96833cb89f44204444",
                    seq = -1,
                    errCode = 404,
                    errMsg = "Error calling Block: 404 Not Found\n"
                },
                new
                {
                    name = "valid hash",
                    golden = "block-hash.golden",
                    hash = "70584db7fb8ab88b8dbcfed72ddc42a1aeb8c4882266dbb78439ba3efcd0458d",
                    seq = -1,
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "genesis hash",
                    golden = "block-hash-genesis.golden",
                    hash = "0551a1e5af999fe8fff529f6f2ab341e1e33db95135eef1b2be44fe6981349f3",
                    seq = -1,
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "genesis seq",
                    golden = "block-seq-0.golden",
                    hash = "",
                    seq = 0,
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "seq 1",
                    golden = "block-seq-1.golden",
                    hash = "",
                    seq = 1,
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "seq 100",
                    golden = "block-seq-100.golden",
                    hash = "",
                    seq = 100,
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "unknown seq",
                    golden = "",
                    hash = "",
                    seq = 999999999,
                    errCode = 404,
                    errMsg = "Error calling Block: 404 Not Found\n"
                }
            };
            foreach (var tc in testCases)
            {
                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() =>
                    {
                        if (tc.seq >= 0)
                        {
                            instance.Block(seq: tc.seq);
                        }
                        else
                        {
                            instance.Block(hash: tc.hash);
                        }
                    });
                    Assert.AreEqual(err.ErrorCode, tc.errCode);
                    Assert.AreEqual(err.Message, tc.errMsg);
                }
                else
                {
                    if (tc.seq >= 0)
                    {
                        var result = instance.Block(seq: tc.seq);
                        Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                    }
                    else
                    {
                        var result = instance.Block(hash: tc.hash);
                        Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                    }
                }
            }
        }

        internal static void BlockchainMetadata(DefaultApi instance)
        {
            var result = instance.BlockchainMetadata();
            var goldenfile = "blockchain-metadata.golden";
            if (Utils.DbNoUnconfirmed())
            {
                goldenfile = "blockchain-metadata-no-unconfirmed.golden";
            }

            Utils.CheckGoldenFile(goldenfile, result, result.GetType());
        }

        internal static void BlockChainProgress(DefaultApi instance)
        {
            var result = instance.BlockchainProgress();
            Utils.CheckGoldenFile("blockchain-progress.golden", result, result.GetType());
        }

        internal static void Blocks(DefaultApi instance)
        {
            Progress p = JsonConvert.DeserializeObject<Progress>(
                instance.BlockchainProgress().ToString());

            var testCases = new[]
            {
                new
                {
                    name = "multiple sequences",
                    golden = "blocks-3-5-7.golden",
                    start = 0,
                    end = 0,
                    seqs = new List<int?> {3, 5, 7},
                    errCode = 200,
                    isRange = false,
                    errMsg = ""
                },
                new
                {
                    name = "block seq not found",
                    golden = "",
                    start = 0,
                    end = 0,
                    seqs = new List<int?> {3, 5, 7, 99999},
                    errCode = 404,
                    isRange = false,
                    errMsg = "Error calling Blocks: 404 Not Found - block does not exist seq=99999\n"
                },
                new
                {
                    name = "first 10",
                    golden = "blocks-first-10.golden",
                    start = 1,
                    end = 10,
                    seqs = new List<int?>(),
                    errCode = 200,
                    isRange = true,
                    errMsg = ""
                },
                new
                {
                    name = "last 10",
                    golden = "blocks-last-10.golden",
                    start = p.Current - 10,
                    end = p.Current,
                    seqs = new List<int?>(),
                    errCode = 200,
                    isRange = true,
                    errMsg = ""
                },
                new
                {
                    name = "first block",
                    golden = "blocks-first-1.golden",
                    start = 1,
                    end = 1,
                    seqs = new List<int?>(),
                    errCode = 200,
                    isRange = true,
                    errMsg = ""
                },
                new
                {
                    name = "all blocks",
                    golden = "blocks-all.golden",
                    start = 0,
                    end = p.Current,
                    seqs = new List<int?>(),
                    errCode = 200,
                    isRange = true,
                    errMsg = ""
                },
                new
                {
                    name = "start > end",
                    golden = "blocks-end-less-than-start.golden",
                    start = 10,
                    end = 9,
                    seqs = new List<int?>(),
                    errCode = 200,
                    isRange = true,
                    errMsg = ""
                }
            };
            foreach (var tc in testCases)
            {
                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() => instance.Blocks(seqs: tc.seqs));
                    Assert.AreEqual(err.ErrorCode, tc.errCode);
                    Assert.AreEqual(err.Message, tc.errMsg);
                }
                else
                {
                    if (tc.isRange)
                    {
                        Utils.BlockInRangeTest(instance: instance, start: tc.start, end: tc.end);
                        var result = instance.Blocks(start: tc.start, end: tc.end);
                        Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                    }
                    else
                    {
                        Utils.BlocksTest(instance: instance, seqs: tc.seqs);
                        var result = instance.Blocks(seqs: tc.seqs);
                        Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                    }
                }
            }
        }

        internal static void CoinSupply(DefaultApi instance)
        {
            var result = instance.CoinSupply();
            Utils.CheckGoldenFile("coinsupply.golden", result, result.GetType());
        }

        internal static void Transactions(DefaultApi instance, Method method)
        {
            var testCases = new List<dynamic>
                {
                    //Simple
                    new
                    {
                        name = "invalid addr length",
                        addrs = new[] {"abcd"},
                        errorCode = 400,
                        errMsg = "Error calling TransactionsGet: 400 Bad Request - parse parameter: 'addrs'" +
                                 " failed: address \"abcd\" is invalid: Invalid address length\n",
                        goldenFile = "",
                        confirmed = ""
                    },
                    new
                    {
                        name = "invalid addr character",
                        addrs = new[] {"701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947"},
                        errorCode = 400,
                        errMsg = "Error calling TransactionsGet: 400 Bad Request - parse parameter: 'addrs'" +
                                 " failed: address \"701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947\"" +
                                 " is invalid: Invalid base58 character\n",
                        goldenFile = "",
                        confirmed = ""
                    },
                    new
                    {
                        name = "invalid checksum",
                        addrs = new[] {"2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKk"},
                        errorCode = 400,
                        errMsg = "Error calling TransactionsGet: 400 Bad Request - parse parameter: 'addrs'" +
                                 " failed: address \"2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKk\" is invalid: Invalid checksum\n",
                        goldenFile = "",
                        confirmed = ""
                    },
                    new
                    {
                        name = "empty addrs",
                        addrs = new[] {""},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "empty-addrs-transactions.golden",
                        confirmed = ""
                    },
                    new
                    {
                        name = "single addr",
                        addrs = new[] {"2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKt"},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "single-addr-transactions.golden",
                        confirmed = ""
                    },
                    new
                    {
                        name = "genesis",
                        addrs = new[] {"2jBbGxZRGoQG1mqhPBnXnLTxK6oxsTf8os6"},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "genesis-addr-transactions.golden",
                        confirmed = ""
                    },
                    new
                    {
                        name = "multiple addrs",
                        addrs = new[] {"2kvLEyXwAYvHfJuFCkjnYNRTUfHPyWgVwKt", "2JJ8pgq8EDAnrzf9xxBJapE2qkYLefW4uF8"},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "multiple-addr-transactions.golden",
                        confirmed = ""
                    },
                    //Confirmed=true
                    new
                    {
                        name = "all confirmed",
                        addrs = new[] {""},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "all-confirmed-transactions.golden",
                        confirmed = "true"
                    },
                    new
                    {
                        name = "unconfirmed should be excluded",
                        addrs = new[] {"212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "unconfirmed-excluded-from-transactions.golden",
                        confirmed = "true"
                    },
                    //Confirmed=false
                    new
                    {
                        name = "all unconfirmed",
                        addrs = new[] {""},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "all-unconfirmed-transactions.golden",
                        confirmed = "false"
                    },
                    new
                    {
                        name = "confirmed should be excluded",
                        addrs = new[] {"212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"},
                        errorCode = 200,
                        errMsg = "",
                        goldenFile = "confirmed-excluded-from-transactions.golden",
                        confirmed = "false"
                    }
                }
                ;

            if (!Utils.DbNoUnconfirmed())
            {
                testCases.Add(new
                {
                    name = "confirmed and unconfirmed transactions",
                    addrs = new[] {"212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"},
                    errorCode = 200,
                    errMsg = "",
                    goldenFile = "confirmed-and-unconfirmed-transactions.golden",
                    confirmed = ""
                });
                testCases.Add(new
                {
                    name = "empty addrs (all unconfirmed txns)",
                    addrs = new[] {""},
                    errorCode = 200,
                    errMsg = "",
                    goldenFile = "all-unconfirmed-txns.golden",
                    confirmed = "false"
                });
            }
            else
            {
                testCases.Add(new
                {
                    name = "empty addrs",
                    addrs = new[] {""},
                    errorCode = 200,
                    errMsg = "",
                    goldenFile = "no-unconfirmed-txns.golden",
                    confirmed = "false"
                });
            }

            foreach (var tc in testCases)
            {
                if (tc.errorCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() => Utils.TransactionsWithMethod(
                        method: method, instance: instance, addrs: string.Join(",", tc.addrs)));
                    if (method == Method.POST)
                    {
                        Assert.AreEqual(tc.errorCode, err.ErrorCode, tc.name);
                        Assert.AreEqual(tc.errMsg.Replace("Get", "Post"), err.Message, tc.name);
                    }
                    else
                    {
                        Assert.AreEqual(tc.errorCode, err.ErrorCode, tc.name);
                        Assert.AreEqual(tc.errMsg, err.Message, tc.name);
                    }
                }
                else
                {
                    if (!tc.confirmed.Equals(""))
                    {
                        Utils.TransactionsWithMethod(method: method, instance: instance,
                            addrs: string.Join(",", tc.addrs),
                            confirmed: tc.confirmed, golden: tc.goldenFile);
                    }
                    else
                    {
                        Utils.TransactionsWithMethod(method: method, instance: instance,
                            addrs: string.Join(",", tc.addrs),
                            confirmed: null, golden: tc.goldenFile);
                    }
                }
            }
        }

        internal static void Health(DefaultApi instance)
        {
            var result = JsonConvert.DeserializeObject<Health>(instance.Health().ToString());
            Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
            Utils.CheckHealthResponse(result);
            Assert.AreEqual(0, result.Open_Connections, "Open Connections");
            Assert.AreEqual(0, result.Incoming_Connections, "Incoming Connections");
            Assert.AreEqual(0, result.Outgoing_Connections, "Outgoing Connections");
            Utils.CompareTime(result.Blockchain.Time_Since_Last_Block);
            Assert.NotNull(result.Version.Commit, "version.Commit");
            Assert.NotNull(result.Version.Branch, "Version.Bransch");
            Assert.AreEqual(Utils.GetCoin(), result.Coin, "Coin");
            Assert.AreEqual($"{result.Coin}:{result.Version.Version}", result.User_Agent, "User Agent");
            Assert.AreEqual(Utils.UseCsrf(), result.CSRF_Enabled, "CSRF_Enabled");
            Assert.True(result.Csp_Enabled, "Csp_Enabled");
            Assert.True(result.Wallet_API_Enabled, "Wallet_API_Enabled");
            Assert.False(result.GUI_Enabled, "GUI_Enabled");
        }

        internal static void LastBlocks(DefaultApi instance)
        {
            //InlineResponse references a list of BlocksSchema.
            var result1 = JsonConvert.DeserializeObject<InlineResponse2001>(instance.LastBlocks(1).ToString());
            Utils.CheckGoldenFile("block-last.golden", result1, result1.GetType());
            var result2 = JsonConvert.DeserializeObject<InlineResponse2001>(instance.LastBlocks(10).ToString());
            Assert.AreEqual(10, result2.Blocks.Count);
            BlockSchema prevBlock = null;
            result2.Blocks.ForEach(block =>
            {
                if (prevBlock != null)
                {
                    Assert.AreNotEqual(prevBlock.Header.BlockHash, block.Header.BlockHash);
                }

                var bh = instance.Block(hash: block.Header.BlockHash);
                Assert.NotNull(bh);
                Assert.AreEqual(block.ToJson(), bh.ToJson());
                prevBlock = block;
            });
        }

        internal static void NetworkConnection(DefaultApi instance)
        {
            NetworkConnectionSchema connectionSchema = null;
            var connections = instance.NetworkConnections();
            Assert.IsEmpty(connections.Connections);
            var err404 =
                Assert.Throws<ApiException>(() => connectionSchema = instance.NetworkConnection("127.0.0.1:4444"));
            Assert.AreEqual(404, err404.ErrorCode);
            Assert.AreEqual("Error calling NetworkConnection: 404 Not Found\n", err404.Message);
            Assert.Null(connectionSchema);
        }

        internal static void NetworkConnectionDisconnect(DefaultApi instance)
        {
            if (Utils.UseCsrf())
            {
                instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: instance));
            }

            var err404 =
                Assert.Throws<ApiException>(() => instance.NetworkConnectionsDisconnect("999"));
            Assert.AreEqual(404, err404.ErrorCode);
            Assert.AreEqual("Error calling NetworkConnectionsDisconnect: 404 Not Found\n", err404.Message);
        }

        internal static void NetworkConnectionExchange(DefaultApi instance)
        {
            var conenctions = instance.NetworkConnectionsExchange();
            Utils.CheckGoldenFile("network-exchanged-peers.golden", conenctions, conenctions.GetType());
        }

        internal static void NoUnconfirmedOutputs(Method method, DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "no addrs or hashes",
                    golden = "no-unconfirmed-outputs-noargs.golden",
                    addrs = new List<string>(),
                    hashes = new List<string>(),
                },
                new
                {
                    name = "only addrs",
                    golden = "no-unconfirmed-outputs-addrs.golden",
                    addrs = new List<string>
                    {
                        "ALJVNKYL7WGxFBSriiZuwZKWD4b7fbV1od",
                        "2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf",
                        "qxmeHkwgAMfwXyaQrwv9jq3qt228xMuoT5",
                        "212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"
                    },
                    hashes = new List<string>(),
                },
                new
                {
                    name = "only hashes",
                    golden = "no-unconfirmed-outputs-hashes.golden",
                    addrs = new List<string>(),
                    hashes = new List<string>
                    {
                        "9e53268a18f8d32a44b4fb183033b49bebfe9d0da3bf3ef2ad1d560500aa54c6",
                        "d91e07318227651129b715d2db448ae245b442acd08c8b4525a934f0e87efce9",
                        "01f9c1d6c83dbc1c993357436cdf7f214acd0bfa107ff7f1466d1b18ec03563e",
                        "fe6762d753d626115c8dd3a053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f20",
                        "701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947",
                        "540582ee4128b733f810f149e908d984a5f403ad2865108e6c1c5423aeefc759"
                    }
                }
            };
            foreach (var tc in testCases)
            {
                Assert.False(tc.addrs.Count > 0 && tc.hashes.Count > 0);
                if (tc.addrs.Count == 0 && tc.hashes.Count == 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, golden: tc.golden);
                }
                else if (tc.addrs.Count > 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, addrs: tc.addrs,
                        golden: tc.golden);
                }
                else if (tc.hashes.Count > 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, hashes: tc.hashes,
                        golden: tc.golden);
                }
            }
        }

        internal static void Outputs(Method method, DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "no addrs or hashes",
                    golden = "outputs-noargs.golden",
                    addrs = new List<string>(),
                    hashes = new List<string>(),
                },
                new
                {
                    name = "only addrs",
                    golden = "outputs-addrs.golden",
                    addrs = new List<string>
                    {
                        "ALJVNKYL7WGxFBSriiZuwZKWD4b7fbV1od",
                        "2THDupTBEo7UqB6dsVizkYUvkKq82Qn4gjf",
                        "qxmeHkwgAMfwXyaQrwv9jq3qt228xMuoT5",
                        "212mwY3Dmey6vwnWpiph99zzCmopXTqeVEN"
                    },
                    hashes = new List<string>(),
                },
                new
                {
                    name = "only hashes",
                    golden = "outputs-hashes.golden",
                    addrs = new List<string>(),
                    hashes = new List<string>
                    {
                        "9e53268a18f8d32a44b4fb183033b49bebfe9d0da3bf3ef2ad1d560500aa54c6",
                        "d91e07318227651129b715d2db448ae245b442acd08c8b4525a934f0e87efce9",
                        "01f9c1d6c83dbc1c993357436cdf7f214acd0bfa107ff7f1466d1b18ec03563e",
                        "fe6762d753d626115c8dd3a053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f20",
                        "701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947",
                        "540582ee4128b733f810f149e908d984a5f403ad2865108e6c1c5423aeefc759"
                    }
                }
            };
            foreach (var tc in testCases)
            {
                Assert.False(tc.addrs.Count > 0 && tc.hashes.Count > 0);
                if (tc.addrs.Count == 0 && tc.hashes.Count == 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, golden: tc.golden);
                }
                else if (tc.addrs.Count > 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, addrs: tc.addrs,
                        golden: tc.golden);
                }
                else if (tc.hashes.Count > 0)
                {
                    Utils.OutputsWithMethod(method: method, instance: instance, hashes: tc.hashes,
                        golden: tc.golden);
                }
            }
        }

        internal static void NoUnconfirmedPendingTxs(DefaultApi instance)
        {
            var txns = instance.PendingTxs();
            Assert.IsEmpty(txns);
        }

        internal static void PendingTxs(DefaultApi instance)
        {
            var txns = instance.PendingTxs();
            DateTime txnRecive;
            DateTime txnChecked;
            txns.ForEach(txn =>
            {
                txnRecive = DateTime.Parse(txn.Received);
                txnChecked = DateTime.Parse(txn.Received);
                Assert.AreNotEqual(txnRecive, new DateTime());
                Assert.AreNotEqual(txnChecked, new DateTime());
            });
            Utils.CheckGoldenFile("pending-transactions.golden", txns, txns.GetType());
        }

        internal static void ResendUnconfirmedTxns(DefaultApi instance)
        {
            var err = Assert.Throws<ApiException>(() =>
            {
                if (Utils.UseCsrf()) instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                instance.ResendUnconfirmedTxns();
            });
            Assert.AreEqual(503, err.ErrorCode);
            Assert.AreEqual("Error calling ResendUnconfirmedTxns: 503 Service Unavailable - Networking " +
                            "is disabled\n", err.Message);
        }

        internal static void RichList(DefaultApi instance)
        {
            var richlist = instance.Richlist();
            Utils.CheckGoldenFile("richlist-default.golden", richlist, richlist.GetType());
            richlist = instance.Richlist(includeDistribution: false, n: "0");
            Utils.CheckGoldenFile("richlist-all.golden", richlist, richlist.GetType());
            richlist = instance.Richlist(includeDistribution: true, n: "0");
            Utils.CheckGoldenFile("richlist-all-include-distribution.golden", richlist, richlist.GetType());
            richlist = instance.Richlist(includeDistribution: false, n: "8");
            Utils.CheckGoldenFile("richlist-8.golden", richlist, richlist.GetType());
            richlist = instance.Richlist(includeDistribution: true, n: "150");
            Utils.CheckGoldenFile("richlist-150-include-distribution.golden", richlist, richlist.GetType());
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
                    name = "not exist",
                    txid = "540582ee4128b733f810f149e908d984a5f403ad2865108e6c1c5423aeefc759",
                    errCode = 404,
                    errMsg = "Error calling Transaction: 404 Not Found\n",
                    golden = ""
                },
                new
                {
                    name = "genesis transaction",
                    txid = "d556c1c7abf1e86138316b8c17183665512dc67633c04cf236a8b7f332cb4add",
                    errCode = 200,
                    errMsg = "",
                    golden = "genesis-transaction.golden"
                },
                new
                {
                    name = "transaction in block 101",
                    txid = "e8fe5290afba3933389fd5860dca2cbcc81821028be9c65d0bb7cf4e8d2c4c18",
                    errCode = 200,
                    errMsg = "",
                    golden = "transaction-block-101.golden"
                }
            };
            if (!Utils.DbNoUnconfirmed())
            {
                testCases.Add(new
                {
                    name = "unconfirmed",
                    txid = "701d23fd513bad325938ba56869f9faba19384a8ec3dd41833aff147eac53947",
                    errCode = 200,
                    errMsg = "",
                    golden = "transaction-unconfirmed.golden"
                });
            }

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
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                }
            });
        }

        internal static void TransactionInject(DefaultApi instance)
        {
            var testCases = new List<dynamic>
            {
                new
                {
                    name = "database is read only",
                    txn = "00000000000000000000000000000000000000000000000000000000000000000000000000000" +
                          "00000000000000100000000f8f9c644772dc5373d85e11094e438df707a42c900407a10f35a00" +
                          "0000407a10f35a0000",
                    errCode = 500,
                    errMsg = "Error calling TransactionInject: 500 Internal Server Error - database is in " +
                             "read-only mode\n"
                },
                new
                {
                    name = "empty txn",
                    txn = "",
                    errCode = 400,
                    errMsg = "Missing required parameter 'rawtx' when calling DefaultApi->TransactionInject"
                },
                new
                {
                    name = "invalid txn",
                    txn = "abcd",
                    errCode = 400,
                    errMsg = "Error calling TransactionInject: 400 Bad Request - Invalid transaction: Not enough " +
                             "buffer data to deserialize\n"
                },
            };

            testCases.ForEach(tc =>
            {
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: instance));
                }

                var err = Assert.Throws<ApiException>(() => instance.TransactionInject(tc.txn));
                Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                Assert.AreEqual(tc.errMsg, err.Message, tc.name);
            });
        }

        internal static void TransactionPost(DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "invalid no uxouts for addresses",
                    req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = new List<string>
                        {
                            "2M755W9o7933roLASK9PZTmqRsjQUsVen9y"
                        },
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = "2M755W9o7933roLASK9PZTmqRsjQUsVen9y",
                                Coins = "1.000000",
                                Hours = "100"
                            }
                        }
                    },
                    errCode = 400,
                    errMsg = "no unspents to spend"
                },
                new
                {
                    name = "OK",
                    req = new TransactionV2ParamsAddress
                    {
                        HoursSelection = new TransactionV2ParamsHoursSelection
                        {
                            Type = "manual"
                        },
                        Address = new List<string>
                        {
                            "qxmeHkwgAMfwXyaQrwv9jq3qt228xMuoT5"
                        },
                        To = new List<TransactionV2ParamsTo>
                        {
                            new TransactionV2ParamsTo
                            {
                                Address = "7cpQ7t3PZZXvjTst8G7Uvs7XH4LeM8fBPD",
                                Coins = "0.003000",
                                Hours = "10"
                            }
                        }
                    },
                    errCode = 200,
                    errMsg = ""
                }
            };
            foreach (var tc in testCases)
            {
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: instance));
                }

                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(
                        () => { Console.WriteLine(instance.TransactionPost(tc.req)); });
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.True(err.Message.Contains(tc.errMsg), tc.name);
                }
                else
                {
                    var result = instance.TransactionPost(tc.req);
                    Utils.CheckGoldenFile("Created-Transaction.golden", result.Data.Transaction,
                        result.Data.Transaction.GetType());
                }
            }
        }

        internal static void TransactionPostUnspents(DefaultApi instance)
        {
            var withunspent = new
            {
                name = "invalid uxouts do not exist",
                req = new TransactionV2ParamsUnspent
                {
                    HoursSelection = new TransactionV2ParamsHoursSelection
                    {
                        Type = "manual"
                    },
                    Unspents = new List<string>
                    {
                        "519c069a0593e179f226e87b528f60aea72826ec7f99d51279dd8854889ed7e2"
                    },
                    To = new List<TransactionV2ParamsTo>
                    {
                        new TransactionV2ParamsTo
                        {
                            Address = "2M755W9o7933roLASK9PZTmqRsjQUsVen9y",
                            Coins = "1.000000",
                            Hours = "100"
                        }
                    }
                },
                errCode = 400,
                errMsg =
                    "unspent output of 519c069a0593e179f226e87b528f60aea72826ec7f99d51279dd8854889ed7e2 does not exist"
            };
            if (Utils.UseCsrf())
            {
                instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance: instance));
            }

            var err = Assert.Throws<ApiException>(() => instance.TransactionPostUnspent(withunspent.req));
            Assert.AreEqual(withunspent.errCode, err.ErrorCode, withunspent.name);
            Assert.True(err.Message.Contains(withunspent.errMsg), withunspent.name);
        }

        internal static void TransactionVerify(DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "unsigned transaction",
                    txn =
                        "dc0000000039c88c7a944ac5f8b191fcbfce621a2ba173a59660590256c798f554d16ba0bc010000000000000" +
                        "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                        "00000000000000000000000000000000000100000075692aeff988ce0da734c474dbef3a1ce19a5a6823bbcd3" +
                        "6acb856c83262261e020000000010722f061aa262381dce35193d43eceb112373c3b80b0000000000000a0000" +
                        "00000000000079ae1e8799c65b4c01e18b3b4135410c5b711f364831432505000000422f050000000000",
                    unsigned = true,
                    golden = "verify-transaction-unsigned.golden",
                    errCode = 200,
                    errMsg = ""
                },
                new
                {
                    name = "violates soft constraint",
                    txn = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                          "00100000000f8f9c644772dc5373d85e11094e438df707a42c900407a10f35a000000407a10f35a0000",
                    unsigned = false,
                    golden = "verify-genesis-transaction.golden",
                    errCode = 422,
                    errMsg = "Transaction violates soft constraint: Insufficient coinhours for transaction outputs"
                }
            };
            foreach (var tc in testCases)
            {
                if (Utils.UseCsrf())
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", Utils.GetCsrf(instance));
                }

                if (tc.errCode != 200)
                {
                    var err = Assert.Throws<ApiException>(() =>
                        instance.TransactionVerify(encodedtransaction: tc.txn, unsigned: tc.unsigned));
                    var msg = JsonConvert.DeserializeObject<dynamic>(err.Message.Substring(33)).error.message;
                    var data = JsonConvert.DeserializeObject<dynamic>(err.Message.Substring(33)).data;
                    Assert.AreEqual(tc.errCode, err.ErrorCode, tc.name);
                    Assert.AreEqual(tc.errMsg, msg.ToString(), tc.name);
                    Utils.CheckGoldenFile(tc.golden, data, data.GetType());
                }
                else
                {
                    var result = instance.TransactionVerify(encodedtransaction: tc.txn, unsigned: tc.unsigned);
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                }
            }
        }

        internal static void Uxouts(DefaultApi instance)
        {
            var testCases = new[]
            {
                new
                {
                    name = "valid uxID",
                    golden = "uxout.golden",
                    uxID = "fe6762d753d626115c8dd3a053b5fb75d6d419a8d0fb1478c5fffc1fe41c5f20",
                }
            };

            foreach (var tc in testCases)
            {
                Assert.DoesNotThrow(() =>
                {
                    var result = instance.Uxout(tc.uxID);
                    Utils.CheckGoldenFile(tc.golden, result, result.GetType());
                });
            }

            Utils.ScanUxouts(instance);
        }
    }
}