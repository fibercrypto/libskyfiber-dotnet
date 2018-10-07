using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_outputs {

        utils.transutils transutils = new utils.transutils ();

        public void TestUxBodyHash () {
            var uxb = transutils.makeUxBody ();
            var hasx = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_coin_UxBody_Hash (uxb, hasx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hasx.isEqual (new cipher_SHA256 ()), 0);
        }

        [Test]
        public void TestUxOutHash () {
            var uxb = transutils.makeUxBody ();
            var uxo = transutils.makeUxOut ();
            uxo.Body = uxb;
            var hash_body = new cipher_SHA256 ();
            var hash_out = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_coin_UxBody_Hash (uxb, hash_body);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxo, hash_out);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hash_body.isEqual (hash_out), 1);
            //  Head should not affect hash
            var uxh = new coin__UxHead ();
            uxh.Time = 0;
            uxh.BkSeq = 1;
            uxo.Head = uxh;
            err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxo, hash_out);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hash_body.isEqual (hash_out), 1);
        }

        [Test]
        public void TestUxOutSnapshotHash () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var uxb = new coin__UxBody ();
            var b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (128, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var h = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxb.SetSrcTransaction (h);
            var a = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxb.Address = a;
            uxb.Coins = (ulong) 1e6;
            uxb.Hours = (ulong) 100;
            var uxo = new coin__UxOut ();
            var uxh = new coin__UxHead ();
            uxh.Time = 100;
            uxh.BkSeq = 2;
            uxo.Head = uxh;
            uxo.Body = uxb;
            var hn = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo, hn);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // snapshot hash should be dependent on every field in body and head
            // Head Time
            var uxo_2 = uxo;
            uxh.Time = 20;
            uxo_2.Head = uxh;
            var hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
            // Head BkSeq
            uxo_2 = uxo;
            uxh.BkSeq = 4;
            uxo_2.Head = uxh;
            hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
            // Body SetSrcTransaction
            uxo_2 = uxo;
            uxb = new coin__UxBody ();
            err = skycoin.skycoin.SKY_cipher_RandByte (128, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            h = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxb.SetSrcTransaction (h);
            uxo_2.Body = uxb;
            hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
            // Body Address
            var p_2 = new cipher_PubKey ();
            var s_2 = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p_2, s_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a_2 = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p_2, a_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxo_2 = uxo;
            uxb = new coin__UxBody ();
            uxb.Address = a_2;
            uxo_2.Body = uxb;
            hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
            // Body Coins
            uxo_2 = uxo;
            uxb = new coin__UxBody ();
            uxb.Coins = 2;
            uxo_2.Body = uxb;
            hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
            // Body Hours
            uxo_2 = uxo;
            uxb = new coin__UxBody ();
            uxb.Hours = 2;
            uxo_2.Body = uxb;
            hn_2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_UxOut_SnapshotHash (uxo_2, hn_2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hn.isEqual (hn_2), 0);
        }

        [Test]
        public void TestUxOutCoinHours () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            var uxb = new coin__UxBody ();
            var b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (128, b);
            var h = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxb.SetSrcTransaction (h);
            var a = new cipher__Address ();
            skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            uxb.Address = a;
            uxb.Coins = (ulong) 1e6;
            uxb.Hours = 100;
            var uxo = new coin__UxOut ();
            var uxh = new coin__UxHead ();
            uxh.Time = 100;
            uxh.BkSeq = 2;
            uxo.Head = uxh;
            uxo.Body = uxb;

            // Less than an hour passed
            var now = uxh.Time + 100;
            var hours = skycoin.skycoin.new_GoUint64p ();
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxh.Time, skycoin.skycoin.GoUint64p_value (hours), "Less than an hour passed");
            // 1 hours passed
            now = uxh.Time + 3600;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxh.Time + (uxb.Coins / 1000000), "1 hours passed");
            // 6 hours passed
            now = uxh.Time + 3600 * 6;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxh.Time + (uxb.Coins / 1000000) * 6, "1 hours passed");
            // Time is backwards (treated as no hours passed)
            now = uxh.Time / 2;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxh.Time);
            // 1 hour has passed, output has 1.5 coins, should gain 1 coinhour
            uxb.Coins = 1500000;
            now = uxh.Time + 3600;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours + 1);
            // 2 hours have passed, output has 1.5 coins, should gain 3 coin hours
            uxb.Coins = 1500000;
            uxo.Body = uxb;
            now = uxh.Time + 3600 * 2;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // 1 second has passed, output has 3600 coins, should gain 1 coin hour
            uxb.Coins = 3600000000;
            uxo.Body = uxb;
            now = uxh.Time + 1;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours + 1);
            // 1000000 hours minus 1 second have passed, output has 1 droplet, should gain 0 coin hour
            uxb.Coins = 1;
            uxo.Body = uxb;
            now = (ulong) (uxh.Time + (ulong) Convert.ToInt64 (1e6 * 3600) - 1);
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours);
            // 1000000 hours have passed, output has 1 droplet, should gain 1 coin hour
            uxb.Coins = 1;
            uxo.Body = uxb;
            now = uxh.Time + Convert.ToUInt64 (10e5 * 3600);
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours + 1);
            // No hours passed, using initial coin hours
            uxb.Coins = (ulong) 10e8;
            uxb.Hours = 1000 * 1000;
            uxo.Body = uxb;
            now = uxh.Time;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours);
            // One hour passed, using initial coin hours
            now = uxh.Time + 3600;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), uxb.Hours + (10e8 / 10e5));
            // No hours passed and no hours to begin with 0
            uxb.Hours = 0;
            uxo.Body = uxb;
            now = uxh.Time;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), 0);
            // Centuries have passed, time-based calculation overflows uint64 when calculating the whole coin seconds
            uxb.Coins = (ulong) 20e5;
            uxo.Body = uxb;
            now = 0xFFFFFFFFFFFFFFFF;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);
            // Centuries have passed, time-based calculation overflows uint64 when calculating the droplet seconds
            uxb.Coins = (ulong) 15e5;
            uxo.Body = uxb;
            now = 0xFFFFFFFFFFFFFFFF;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);
            // Output would overflow if given more hours, has reached its limit
            uxb.Coins = (ulong) 36e8;
            uxo.Body = uxb;
            now = 0xFFFFFFFFFFFFFFFF;
            err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (uxo, now, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);
        }

        [Test]
        public void TestUxArrayCoins () {
            var uxa = transutils.makeUxOutArray (4);
            for (int i = 0; i < uxa.count; i++) {
                var x = uxa.getAt (i);
                x.Body.Coins = (ulong) 1e6;
            }
            var coins = skycoin.skycoin.new_GoUint64p ();
            var err = skycoin.skycoin.SKY_coin_UxArray_Coins (uxa, coins);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (coins), (ulong) 4e6);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var uxo = uxa.getAt (2);
            uxo.Body.Coins = (ulong) (ulong.MaxValue - 1e6);
            uxa.setAt (2, uxo);
            err = skycoin.skycoin.SKY_coin_UxArray_Coins (uxa, coins);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);
        }
        public ulong ux_Array_CoinsHours (coin_UxOutArray uxa, ulong now = 0, ulong slic = 0) {
            ulong result = 0;
            for (var i = slic; i < (ulong) uxa.count; i++) {
                var x = uxa.getAt ((int) i);
                var time = skycoin.skycoin.new_GoUint64p ();
                var err = skycoin.skycoin.SKY_coin_UxOut_CoinHours (x, now, time);
                result += skycoin.skycoin.GoUint64p_value (time);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            }
            return result;
        }

        [Test]
        public void TestUxArrayCoinHours () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 4);
            var count = skycoin.skycoin.new_GoUint64p ();
            var uxo = uxa.getAt (0);
            var err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, uxo.Head.Time, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (count), 400);
            // 1 hour later
            err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, uxo.Head.Time + 3600, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (count), 404);
            // 1.5 hours later
            err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, uxo.Head.Time + 3600 + 1800, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (count), 404);
            // 2 hours later
            err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, uxo.Head.Time + 3600 + 4600, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (count), 408);

            uxo = uxa.getAt (2);
            uxo.Body.Hours = ulong.MaxValue - 100;
            uxa.setAt (2, uxo);
            uxo = uxa.getAt (0);
            err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, uxo.Head.Time, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            err = skycoin.skycoin.SKY_coin_UxArray_CoinHours (uxa, Convert.ToUInt64 (uxo.Head.Time * 10e11), count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddEarnedCoinHoursAdditionOverflow);
        }

        [Test]
        public void TestUxArrayHashArray () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 4);
            var sha = new cipher_SHA256 ();
            var hashes = new cipher_SHA256s ();
            var err = skycoin.skycoin.SKY_coin_UxArray_Hashes (uxa, hashes);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hashes.count, uxa.count);
            var uxo = uxa.getAt (0);
            err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxo, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hashes.getAt (0).isEqual (sha), 1);
            for (int i = 0; i < hashes.count; i++) {
                err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxa.getAt (i), sha);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (sha.isEqual (hashes.getAt (i)), 1);
            }
        }

        [Test]
        public void TestUxArrayHasDupes () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 4);
            var hasDupes = skycoin.skycoin.new_CharPtr();
            var err = skycoin.skycoin.SKY_coin_UxArray_HasDupes (uxa, hasDupes);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value(hasDupes), 0);
            uxa.setAt (0, uxa.getAt (1));
            err = skycoin.skycoin.SKY_coin_UxArray_HasDupes (uxa, hasDupes);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value (hasDupes), 1);
        }

        [Test]
        public void TestUxArraySub () {
            var uxa = new coin_UxOutArray ();
            var uxb = new coin_UxOutArray ();
            var uxc = new coin_UxOutArray ();
            var uxd = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 4);
            skycoin.skycoin.makeUxArray (uxb, 4);
            uxc.allocate (6);
            uxc.setAt (0, uxa.getAt (0));
            uxc.setAt (1, uxa.getAt (1));
            for (int i = 0; i < uxb.count; i++) {
                uxc.setAt (i + 2, uxb.getAt (i));
            }
            uxc.setAt (6, uxa.getAt (1));
            uxc.setAt (7, uxa.getAt (2));

            var err = skycoin.skycoin.SKY_coin_UxArray_Sub (uxc, uxa, uxd);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxd.count, uxb.count);
            Assert.AreEqual (uxd.isEqual (uxb), 1);
            err = skycoin.skycoin.SKY_coin_UxArray_Sub (uxc, uxb, uxd);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxd.count, 2);
            var test = new coin_UxOutArray ();
            test.allocate (2);
            test.setAt (0, uxa.getAt (0));
            test.setAt (1, uxa.getAt (1));
            Assert.AreEqual (test.isEqual (uxd), 1);
            // No intersection
            err = skycoin.skycoin.SKY_coin_UxArray_Sub (uxa, uxb, uxd);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxd.isEqual (uxa), 1);
            err = skycoin.skycoin.SKY_coin_UxArray_Sub (uxb, uxa, uxd);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxd.isEqual (uxb), 1);
        }
        public bool manualUxArrayIsSorted (coin_UxOutArray uxa) {
            var sha_1 = new cipher_SHA256 ();
            var sha_2 = new cipher_SHA256 ();
            var isSort = true;
            for (int i = 0; i < uxa.count - 1; i++) {
                var err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxa.getAt (i), sha_1);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_coin_UxOut_Hash (uxa.getAt (i + 1), sha_2);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                if (sha_1.isEqual (sha_2) == 0) {
                    isSort = false;
                }
            }
            return isSort;
        }

        [Test]
        public void TestUxArrayLen () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 4);
            Assert.AreEqual (uxa.count, 4);
        }

        [Test]
        public void TestUxArrayLess () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 2);
            var hashes = new cipher_SHA256s ();
            var err = skycoin.skycoin.SKY_coin_UxArray_Hashes (uxa, hashes);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (hashes.count, uxa.count);
            var less = skycoin.skycoin.new_CharPtr ();
            var less2 = skycoin.skycoin.new_CharPtr ();
            err = skycoin.skycoin.SKY_coin_UxArray_Less (uxa, 0, 1, less);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_UxArray_Less (uxa, 1, 0, less2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreNotEqual (skycoin.skycoin.CharPtr_value (less), skycoin.skycoin.CharPtr_value (less2));
        }

        [Test]
        public void TestUxArraySwap () {
            var uxa = new coin_UxOutArray ();
            uxa.allocate (2);
            var uxx = new coin__UxOut ();
            var uxy = new coin__UxOut ();
            skycoin.skycoin.makeUxOut (uxx);
            skycoin.skycoin.makeUxOut (uxy);
            uxa.setAt (0, uxx);
            uxa.setAt (1, uxy);
            var err = skycoin.skycoin.SKY_coin_UxArray_Swap (uxa, 0, 1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxa.getAt (0).isEqual (uxy), 0);
            Assert.AreEqual (uxa.getAt (1).isEqual (uxx), 0);
            err = skycoin.skycoin.SKY_coin_UxArray_Swap (uxa, 0, 1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxa.getAt (0).isEqual (uxx), 0);
            Assert.AreEqual (uxa.getAt (1).isEqual (uxy), 0);
            err = skycoin.skycoin.SKY_coin_UxArray_Swap (uxa, 1, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (uxa.getAt (0).isEqual (uxx), 0);
            Assert.AreEqual (uxa.getAt (1).isEqual (uxy), 0);
        }

        [Test]
        public void TestAddressUxOutsKeys () {
            var uxa = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxa, 3);
            var uxH = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            var err = skycoin.skycoin.SKY_coin_NewAddressUxOuts (uxa, uxH);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var keys = new cipher_Addresses ();
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Keys (uxH, keys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (keys.count, 3);
            for (int i = 0; i < keys.count; i++) {
                var k = keys.getAt (i);
                var res = uxa.getAt (0).Body.Address.isEqual (k) + uxa.getAt (1).Body.Address.isEqual (k) + uxa.getAt (2).Body.Address.isEqual (k);
                Assert.IsTrue (res > 0);
            }
        }

        [Test]
        public void TestAddressUxOutsSub () {
            var uxs = new coin_UxOutArray ();
            var empty = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxs, 4);
            skycoin.skycoin.makeUxArray (empty, 0);

            var up = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            var err = skycoin.skycoin.SKY_coin_NewAddressUxOuts (empty, up);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var up2 = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            err = skycoin.skycoin.SKY_coin_NewAddressUxOuts (empty, up2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var uxo = uxs.getAt (1);
            uxo.Body.Address = uxs.getAt (0).Body.Address;
            uxs.setAt (1, uxo);

            var arg = new coin_UxOutArray ();
            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (0));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, uxs.getAt (0).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (2));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, uxs.getAt (2).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (3));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, uxs.getAt (3).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (1));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up2, uxs.getAt (1).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (2));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up2, uxs.getAt (2).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var up3 = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Add (up, up2, up3);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var len = skycoin.skycoin.new_Gointp ();
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Length (up3, len);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (len), 3);
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Length (up2, len);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (len), 2);
        }

        [Test]
        public void TestAddressUxOutsFlatten () {
            var empty = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (empty, 0);
            var up = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            var err = skycoin.skycoin.SKY_coin_NewAddressUxOuts (empty, up);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var uxs = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxs, 3);
            var uxo = uxs.getAt (2);
            uxo.Body.Address = uxs.getAt (1).Body.Address;
            uxs.setAt (0, uxo);
            var emptyAddr = new cipher__Address ();
            skycoin.skycoin.makeAddress (emptyAddr);

            // An empty array
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, emptyAddr, empty);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // 1 element array
            var arg = new coin_UxOutArray ();
            arg.release ();
            arg.allocate (1);
            arg.setAt (0, uxs.getAt (0));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, uxs.getAt (0).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg.release ();
            arg.allocate (2);
            arg.setAt (0, uxs.getAt (1));
            arg.setAt (1, uxs.getAt (2));
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Set (up, uxs.getAt (1).Body.Address, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var flat = new coin_UxOutArray ();
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Flatten (up, flat);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (flat.count, 2);

            for (int i = 0; i < flat.count; i++) {
                var ux = flat.getAt (i);
                Assert.AreNotEqual (ux.Body.Address.isEqual (emptyAddr), 1, "Is equal " + i.ToString ());
            }
            if (flat.getAt (0).Body.Address.isEqual (uxs.getAt (0).Body.Address) == 1) {
                Assert.AreEqual (flat.getAt (0).isEqual (uxs.getAt (0)), 0);
                Assert.AreEqual (flat.getAt (0).Body.Address.isEqual (uxs.getAt (0).Body.Address), 1);
                Assert.AreEqual (flat.getAt (0 + 1).Body.Address.isEqual (uxs.getAt (0 + 1).Body.Address), 1);
                Assert.AreEqual (flat.getAt (0 + 1).isEqual (uxs.getAt (1)), 0);
            }

        }

        [Test]
        public void TestNewAddressUxOuts () {
            var uxs = new coin_UxOutArray ();
            skycoin.skycoin.makeUxArray (uxs, 6);
            var ux = uxs.getAt (1);
            ux.Body.Address = uxs.getAt (0).Body.Address;
            uxs.setAt (1, ux);
            ux = uxs.getAt (3);
            ux.Body.Address = uxs.getAt (2).Body.Address;
            uxs.setAt (3, ux);
            ux = uxs.getAt (4);
            ux.Body.Address = uxs.getAt (2).Body.Address;
            uxs.setAt (4, ux);

            var uxo = skycoin.skycoin.new_AddressUxOuts__HandlePtr ();
            var err = skycoin.skycoin.SKY_coin_NewAddressUxOuts (uxs, uxo);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var len = skycoin.skycoin.new_Gointp ();
            err = skycoin.skycoin.SKY_coin_AddressUxOuts_Length (uxo, len);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (len), 3);
            var uxa1 = new coin_UxOutArray ();
            var uxa2 = new coin_UxOutArray ();

            skycoin.skycoin.SKY_coin_AddressUxOuts_Get (uxo, uxs.getAt (0).Body.Address, uxa1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxa2.allocate (2);
            uxa2.setAt (1, uxs.getAt (4));
            uxa2.setAt (0, uxs.getAt (0));
            uxa2.setAt (1, uxs.getAt (4));
            uxa2.setAt (1, uxs.getAt (1));
            uxa2.setAt (1, uxs.getAt (4));
            Assert.AreEqual (uxa1.isEqual (uxa2), 0);

            uxa2.setAt (1, uxs.getAt (4));
            uxa1.release ();
            skycoin.skycoin.SKY_coin_AddressUxOuts_Get (uxo, uxs.getAt (3).Body.Address, uxa1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxa2.release ();
            uxa2.allocate (3);
            uxa2.setAt (0, uxs.getAt (2));
            uxa2.setAt (1, uxs.getAt (3));
            uxa2.setAt (1, uxs.getAt (4));
            Assert.AreEqual (uxa1.isEqual (uxa2), 0);

            uxa1.release ();
            skycoin.skycoin.SKY_coin_AddressUxOuts_Get (uxo, uxs.getAt (5).Body.Address, uxa1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            uxa2.release ();
            uxa2.allocate (1);
            uxa2.setAt (0, uxs.getAt (5));
            Assert.AreEqual (uxa1.isEqual (uxa2), 1);
        }
    }
}