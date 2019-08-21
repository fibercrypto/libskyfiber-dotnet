using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Model;

namespace Skyapi.Test.Api
{
    internal static class Utils
    {
        /// <summary>
        /// Accept a BalanceGet and BalancePost methods.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instance"></param>
        /// <param name="useCsrf"></param>
        /// <param name="addrs"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static object BalanceWithMethod(Method method, DefaultApi instance, bool useCsrf, string addrs = null,
            string golden = null)
        {
            if (method == Method.GET)
            {
                var result = instance.BalanceGet(addrs: addrs);
                if (golden != null)
                {
                    CheckGoldenFile(golden, result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            if (method == Method.POST)
            {
                if (useCsrf)
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", GetCsrf(instance: instance));
                }

                var result = instance.BalancePost(addrs: addrs);
                if (golden != null)
                {
                    CheckGoldenFile($"post-{golden}", result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Accept a TransactionsGet and TransactionsPost methods.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instance"></param>
        /// <param name="useCsrf"></param>
        /// <param name="addrs"></param>
        /// <param name="confirmed"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static object TransactionsWithMethod(Method method, DefaultApi instance, bool useCsrf,
            string addrs = null, string confirmed = null, string golden = null)
        {
            if (method == Method.GET)
            {
                var result = instance.TransactionsGet(addrs: addrs, confirmed: confirmed);
                if (golden != null)
                {
                    CheckGoldenFile(golden, result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            if (method == Method.POST)
            {
                if (useCsrf)
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", GetCsrf(instance));
                }

                var result = instance.TransactionsPost(addrs: addrs, confirmed: confirmed);
                if (golden != null)
                {
                    CheckGoldenFile($"post-{golden}", result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Accept a OutpustGet and OutputsPost methods.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instance"></param>
        /// <param name="useCsrf"></param>
        /// <param name="addrs"></param>
        /// <param name="hashes"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static object OutputsWithMethod(Method method, DefaultApi instance, bool useCsrf,
            List<string> addrs = null, List<string> hashes = null, string golden = null)
        {
            if (method == Method.GET)
            {
                var result = instance.OutputsGet(address: addrs, hash: hashes);
                if (golden != null)
                {
                    CheckGoldenFile(golden, result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            if (method == Method.POST)
            {
                if (useCsrf)
                {
                    instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", GetCsrf(instance));
                }

                var result = instance.OutputsPost(address: addrs, hash: hashes);
                if (golden != null)
                {
                    CheckGoldenFile($"post-{golden}", result, result.GetType());
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

/// <summary>
/// Compare the time of Health.
/// </summary>
/// <param name="time"></param>
        internal static void CompareTime(string time)
        {
            var x = Regex.Split(time.Replace(".", ","), @"h|s|m").Reverse().ToArray();
            int s = (int) double.Parse(x.Length >= 2 ? (x[1] != "" ? x[1] : "0") : "0"),
                m = (int) double.Parse(x.Length >= 3 ? (x[2] != "" ? x[2] : "0") : "0"),
                h = (int) double.Parse(x.Length >= 4 ? (x[3] != "" ? x[3] : "0") : "0");
            Assert.True(new TimeSpan(h, m, s) > TimeSpan.Zero);
        }

        /// <summary>
        /// Obtains a CSRF token.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static string GetCsrf(DefaultApi instance)
        {
            string token;
            try
            {
                token = instance.Csrf().CsrfToken;
            }
            catch (Exception)
            {
                return "";
            }

            return token;
        }

        /// <summary>
        /// Save the data of test into a *.golden file.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        internal static void CheckGoldenFile(string file, object obj, Type type)
        {
            var path = "../../../../TestFile/";
            //Verify directory
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = path + file;
            //Verify file
            if (!File.Exists(path)) File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
            using (var sr = File.OpenText(path))
            {
                var p = JsonConvert.DeserializeObject(sr.ReadToEnd(), type);
                sr.Close();
                Assert.AreEqual(JsonConvert.SerializeObject(p), JsonConvert.SerializeObject(obj));
            }
        }

        /// <summary>
        /// Check Blocks with start and end params.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        internal static void BlockInRangeTest(DefaultApi instance, int? start = null, int? end = null)
        {
            var result = instance.Blocks(start: start, end: end);
            if (start > end)
            {
                Assert.IsEmpty(result.Blocks);
            }
            else
            {
                Assert.AreEqual(end - start + 1, result.Blocks.Count);
            }

            BlockSchema prevblock = null;
            result.Blocks.ForEach(b =>
            {
                if (prevblock != null)
                {
                    Assert.AreEqual(prevblock.Header.BlockHash, b.Header.PreviousBlockHash);
                }

                var bh = instance.Block(hash: b.Header.BlockHash);
                Assert.AreEqual(result.Blocks.FindIndex(block => Equals(block, b)) + start, b.Header.Seq);
                Assert.NotNull(bh);
                Assert.AreEqual(bh.ToJson(), b.ToJson());
                prevblock = b;
            });
        }

        /// <summary>
        /// Check a BLocks with seqs param.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="seqs"></param>
        internal static void BlocksTest(DefaultApi instance, List<int?> seqs)
        {
            var result = instance.Blocks(seqs: seqs);
            Assert.AreEqual(seqs.Count, result.Blocks.Count);
            var seqsMap = new Dictionary<int?, BlockVerboseSchemaHeader>();
            seqs.ForEach(s =>
            {
                if (s != null) seqsMap[s] = null;
            });
            result.Blocks.ForEach(b =>
            {
                if (b.Header.Seq != null)
                {
                    Assert.True(seqsMap.ContainsKey(b.Header.Seq));
                    seqsMap.Remove(b.Header.Seq);
                }

                var bh = instance.Block(b.Header.BlockHash);
                Assert.NotNull(bh);
                Assert.AreEqual(b.ToJson(), bh.ToJson());
            });
            Assert.IsEmpty(seqsMap);
        }

        /// <summary>
        /// Check a Health Response.
        /// </summary>
        /// <param name="h"></param>
        internal static void CheckHealthResponse(Health h)
        {
            Assert.AreNotEqual(0, h.Blockchain.Unspents);
            Assert.AreNotEqual(0, h.Blockchain.Head.Seq);
            Assert.AreNotEqual(0, h.Blockchain.Head.Timestamp);
            Assert.NotNull(h.Version.Version);
            CompareTime(h.Uptime);
            Assert.NotNull(h.Coin);
            Assert.NotNull(h.User_Agent);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        internal static void AssertNoTransactionsDupes(IEnumerable<Transaction> list)
        {
            var txids = new Dictionary<string, object>();
            foreach (var transaction in list)
            {
                Assert.False(txids.ContainsKey(transaction.Txn.InnerHash));
                txids[transaction.Txn.InnerHash] = new object();
            }
        }

    }
}