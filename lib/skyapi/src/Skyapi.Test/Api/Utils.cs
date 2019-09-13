using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Skyapi.Api;
using Skyapi.Model;
using skycoin;

namespace Skyapi.Test.Api
{
    internal static class Utils
    {
        /// <summary>
        /// Accept a BalanceGet and BalancePost methods.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instance"></param>
        /// <param name="addrs"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static Balance BalanceWithMethod(Method method, DefaultApi instance, string addrs = null,
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
                if (UseCsrf())
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
        /// <param name="addrs"></param>
        /// <param name="confirmed"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static object TransactionsWithMethod(Method method, DefaultApi instance,
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
                if (UseCsrf())
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
        /// <param name="addrs"></param>
        /// <param name="hashes"></param>
        /// <param name="golden"></param>
        /// <returns>object</returns>
        internal static Outputs OutputsWithMethod(Method method, DefaultApi instance,
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
                if (UseCsrf())
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
            Assert.True(h != 0 || m != 0 || s != 0);
        }

        /// <summary>
        /// Obtains a CSRF token.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>token</returns>
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

        internal static string GetWalletName()
        {
            var walletname = Environment.GetEnvironmentVariable("API_WALLET_ID");
            Assert.NotNull(walletname, "Missing WALLET_NAME environment value");
            return walletname;
        }

        internal static bool DoLiveWallet()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("LIVE_WALLET") ?? "false");
        }

        internal static string GetWalletDir(DefaultApi instance)
        {
            return instance.WalletFolder().Address;
        }

        internal static void RequireWalletEnv()
        {
            if (!DoLiveWallet())
            {
                Assert.Ignore("DoLiveWallet is false.");
            }

            var walletName = GetWalletName() ?? "";
            if (walletName == "")
            {
                Assert.Fail("missing API_WALLET_ID environment value");
            }
        }

        internal static string GetWalletPassword()
        {
            return Environment.GetEnvironmentVariable("WALLET_PASSWORD") ?? "";
        }

        internal static string GetTestMode()
        {
            return Environment.GetEnvironmentVariable("TEST_MODE") ?? "stable";
        }

        internal static string GetCoin()
        {
            return Environment.GetEnvironmentVariable("COIN") ?? "skycoin";
        }

        internal static bool Enabled()
        {
            return Environment.GetEnvironmentVariable("SKYCOIN_INTEGRATION_TESTS") == "1";
        }

        internal static bool SkipWalletIfLive()
        {
            var skip = Enabled() && GetTestMode() == "live" && !DoLiveWallet();
            if (skip)
            {
                Assert.Ignore("live wallet tests disabled");
            }

            return false;
        }

        internal static bool UseCsrf()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("USE_CSRF") ?? "true");
        }

        internal static string GetNodeHost()
        {
            return Environment.GetEnvironmentVariable("SKYCOIN_NODE_HOST") ?? "http://localhost:6420";
        }

        internal static bool DbNoUnconfirmed()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("DB_NO_UNCONFIRMED") ?? "false");
        }

        internal static bool LiveDisableNetworking()
        {
            return Convert.ToBoolean(Environment.GetEnvironmentVariable("LIVE_DISABLE_NETWORKING") ?? "true");
        }

        internal static Tuple<SWIGTYPE_p_Wallet__Handle, long, long, string> PrepareAndCheckWallet(
            DefaultApi instance, double minCoins,
            double minHours)
        {
            var walletName = GetWalletName();
            var walletDir = GetWalletDir(instance);
            var walletPass = GetWalletPassword();
            if (!File.Exists($"{walletDir}/{walletName}"))
            {
                Assert.Fail($"Wallet {walletDir}/{walletName} doesn't exist");
            }

            var walletHandle = skycoin.skycoin.new_Wallet__HandlePtr();
            var err = skycoin.skycoin.SKY_wallet_Load($"{walletDir}/{walletName}", walletHandle);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var encryptCharPtr = skycoin.skycoin.new_CharPtr();
            err = skycoin.skycoin.SKY_wallet_Wallet_IsEncrypted(walletHandle, encryptCharPtr);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var encrypt = skycoin.skycoin.CharPtr_value(encryptCharPtr);
            if (encrypt == 1 && walletPass.Equals(""))
            {
                Assert.Fail("Wallet is encrypted, must set WALLET_PASSWORD env var");
            }

            var entryLenGoUint32Ptr = skycoin.skycoin.new_GoUint32Ptr();
            err = skycoin.skycoin.SKY_api_Handle_GetWalletEntriesCount(walletHandle, entryLenGoUint32Ptr);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var entryLen = skycoin.skycoin.GoUint32Ptr_value(entryLenGoUint32Ptr);
            if (entryLen < 2)
            {
                Assert.DoesNotThrow(() =>
                {
                    if (UseCsrf())
                    {
                        instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", GetCsrf(instance));
                    }

                    instance.WalletNewAddress(walletName, 2, walletPass);
                    err = skycoin.skycoin.SKY_wallet_Load($"{walletDir}/{walletName}", walletHandle);
                    Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                });
            }

            var balanceTuple = GetBalanceWallet(instance, walletName);
            if (balanceTuple.Item1 < minCoins)
            {
                //  Assert.Fail($"Wallet must have at least {minCoins} coins");
            }

            if (balanceTuple.Item2 < minHours)
            {
                //Assert.Fail($"Wallet must have at least {minHours} coins");
            }

            skycoin.skycoin.SKY_wallet_Wallet_Save(walletHandle, $"{walletDir}/{walletName}");

            return new Tuple<SWIGTYPE_p_Wallet__Handle, long, long, string>(
                walletHandle,
                balanceTuple.Item1,
                balanceTuple.Item2,
                walletPass);
        }

        internal static bool IsNullAddress(string a)
        {
            if (a.Equals(""))
            {
                return true;
            }

            var cipherAddress = skycoin.skycoin.new_cipher__Addressp();
            var err = skycoin.skycoin.SKY_cipher_DecodeBase58Address(a, cipherAddress);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var unsignedChar = skycoin.skycoin.new_CharPtr();
            err = skycoin.skycoin.SKY_cipher_Address_Null(cipherAddress, unsignedChar);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            return skycoin.skycoin.CharPtr_value(unsignedChar) == 1;
        }

        internal static string ToDropletString(ulong i)
        {
            var goString = new _GoString_();
            skycoin.skycoin.SKY_droplet_ToString(i, goString);

            return goString.p;
        }

        internal static ulong FromDropletString(String i)
        {
            var goUint64P = skycoin.skycoin.new_GoUint64p();
            skycoin.skycoin.SKY_droplet_FromString(i, goUint64P);

            return skycoin.skycoin.GoUint64p_value(goUint64P);
        }

        internal static Tuple<long, long> GetBalanceWallet(DefaultApi instance, string walletId)
        {
            var balance = instance.WalletBalance(walletId);
            return new Tuple<long, long>(balance.Confirmed.Coins, balance.Confirmed.Hours);
        }

        internal static void ScanUxouts(DefaultApi instance)
        {
            var outputs = instance.OutputsGet();

            foreach (var oh in outputs.HeaderOutputs)
            {
                dynamic foundUx = instance.Uxout(oh.Hash);
                Assert.AreEqual(oh.Hash, foundUx.uxid.ToString(), "uxid");
                Assert.AreEqual(oh.Time, (ulong) foundUx.time, "time");
                Assert.AreEqual(oh.BlockSeq, (ulong) foundUx.src_block_seq, "src_block_seq");
                Assert.AreEqual(oh.SrcTx, foundUx.src_tx.ToString(), "src_tx");
                Assert.AreEqual(oh.Address, foundUx.owner_address.ToString(), "owner_address");

                if (foundUx.spent_block_seq == 0)
                {
                    Assert.AreEqual("0000000000000000000000000000000000000000000000000000000000000000",
                        foundUx.spent_tx.ToString());
                }
                else
                {
                    Assert.AreNotEqual("0000000000000000000000000000000000000000000000000000000000000000",
                        foundUx.spent_tx.ToString());
                }
            }
        }

        internal static string GenString()
        {
            var lon = 15;
            var miGuid = Guid.NewGuid();
            var token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "");
            return token.Substring(0, lon);
        }

        internal static Tuple<Wallet, Action> CreateWallet(DefaultApi instance, string type = "deterministic",
            string label = null, string seed = null, string xpub = null, string seedPassphase = null,
            string bip44Coins = null, int? scan = null, bool encrypt = false, string pass = null)
        {
            switch (type)
            {
                case "deterministic":
                    if (seed == null || seed == "")
                    {
                        var goString = new _GoString_();
                        skycoin.skycoin.SKY_bip39_NewDefaultMnemomic(goString);
                        seed = goString.p;
                    }

                    break;
                case "bip44":
                    if (seed == null || seed == "")
                    {
                        var goString = new _GoString_();
                        skycoin.skycoin.SKY_bip39_NewDefaultMnemomic(goString);
                        seed = goString.p;
                    }

                    break;
                case "xpub":
                    if (xpub == null || xpub == "")
                    {
                        //  var goString = new _GoString_();
                        //skycoin.skycoin.SKY_bip39_NewDefaultMnemomic(goString);
                        //seed = goString.p;
                    }

                    break;
            }

            if (label == null || label == "")
            {
                label = GenString();
            }

            if (UseCsrf())
            {
                instance.Configuration.AddApiKeyPrefix("X-CSRF-TOKEN", GetCsrf(instance));
            }

            var w = instance.WalletCreate(type, label, seed, seedPassphase,
                bip44Coins, xpub, scan, encrypt, pass);
            var walletDir = GetWalletDir(instance);
            return new Tuple<Wallet, Action>(w, () =>
            {
                // Cleaner function to delete the wallet and bak wallet
                var walletpath = $"{walletDir}/{w.Meta.Id}";
                if (!File.Exists(walletpath))
                {
                    Assert.Fail("Wallet file not found");
                }

                File.Delete(walletpath);
                if (File.Exists(walletpath + ".bak"))
                {
                    File.Delete(walletpath + ".bak");
                }

                instance.WalletUnload(w.Meta.Id);
            });
        }

        internal static cipher_SHA256 RandSha256()
        {
            var b = new GoSlice();
            skycoin.skycoin.SKY_cipher_RandByte(128, b);
            var cipherSha256 = new cipher_SHA256();
            skycoin.skycoin.SKY_cipher_SumSHA256(b, cipherSha256);
            return cipherSha256;
        }

        internal static string GetAddressOfWalletEntries(int pos, SWIGTYPE_p_Wallet__Handle wallet)
        {
            var cipherAddress = new cipher__Address();
            var err = skycoin.skycoin.SKY_api_Handle_WalletGetEntry(wallet, (uint) pos, cipherAddress,
                new cipher_PubKey());
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            var addressGoString = new _GoString_();
            err = skycoin.skycoin.SKY_cipher_Address_String(cipherAddress, addressGoString);
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            return addressGoString.p;
        }

        internal static cipher__Address GetCipherAddressOfWalletEntries(int pos, SWIGTYPE_p_Wallet__Handle wallet)
        {
            var cipherAddress = new cipher__Address();
            var err = skycoin.skycoin.SKY_api_Handle_WalletGetEntry(wallet, (uint) pos, cipherAddress,
                new cipher_PubKey());
            Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
            return cipherAddress;
        }

        internal static string Sha256Hex(cipher_SHA256 unknownOutputs)
        {
            var goString = new _GoString_();
            skycoin.skycoin.SKY_cipher_SHA256_Hex(unknownOutputs, goString);
            return goString.p;
        }

        internal static WalletEntry GetLastChangeEntry(Wallet w2)
        {
            Assert.AreEqual("bip44", w2.Meta.Type);
            w2.Entries.Sort((e1, e2) =>
            {
                if (e1.Change == e2.Change)
                {
                    return e1.ChildNumber - e2.ChildNumber;
                }

                return e1.Change - e2.Change;
            });
            var lastChangeEntry = w2.Entries[0];
//            if (lastChangeEntry.Change!=bip44.ChangeChainIndex)
//            {
//                return null;
//            }
            return lastChangeEntry;
        }

        internal static coin_UxOutArray ToUxArray(List<UnspentOutput> outputs)
        {
            var uxArr = new coin_UxOutArray();
            outputs.ForEach(o =>
            {
                var uxOut = new coin__UxOut();
                uxOut.Head.Time = o.Time;
                uxOut.Head.BkSeq = o.BlockSeq;
                var cipherAddress = skycoin.skycoin.new_cipher__Addressp();
                var err = skycoin.skycoin.SKY_cipher_DecodeBase58Address(o.Address, cipherAddress);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                uxOut.Body.Address = cipherAddress;
                uxOut.Body.Coins = FromDropletString(o.Coins);
                uxOut.Body.Hours = o.Hours;
                var cipherSha256 = new cipher_SHA256();
                err = skycoin.skycoin.SKY_cipher_SHA256FromHex(o.SrcTx, cipherSha256);
                Assert.AreEqual(skycoin.skycoin.SKY_OK, err);
                uxOut.Body.SetSrcTransaction(cipherSha256);
                uxArr.append(uxOut);
            });
            return uxArr;
        }
    }
}