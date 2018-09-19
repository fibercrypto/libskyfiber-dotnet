using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_hash {
        public void freshSumRipemd160 (skycoin.GoSlice bytes, cipher_Ripemd160 rp160) {
            skycoin.skycoin.SKY_cipher_HashRipemd160 (bytes, rp160);
        }

        void freshSumSHA256 (GoSlice bytes, cipher_SHA256 sha256) {
            skycoin.skycoin.SKY_cipher_SumSHA256 (bytes, sha256);
        }

        [Test]
        public void TestHashRipemd160 () {
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (128, b);
            var hr = new cipher_Ripemd160 ();
            err = skycoin.skycoin.SKY_cipher_HashRipemd160 (b, hr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (160, b);
            var r = new cipher_Ripemd160 ();
            err = skycoin.skycoin.SKY_cipher_HashRipemd160 (b, r);
            Assert.AreEqual (r.isEqual (new cipher_Ripemd160 ()), 0);
            // 2nd hash should not be affected by previous
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (256, b);
            var r2 = new cipher_Ripemd160 ();
            err = skycoin.skycoin.SKY_cipher_HashRipemd160 (b, r2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (r2.isEqual (new cipher_Ripemd160 ()), 0);
            var r3 = new cipher_Ripemd160 ();
            freshSumRipemd160 (b, r3);
            Assert.AreEqual (r2.isEqual (r3), 1);
        }

        [Test]
        public void TestRipemd160Set () {
            var h = new cipher_Ripemd160 ();
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (21, b);
            err = skycoin.skycoin.SKY_cipher_Ripemd160_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthRipemd160);
            b = new GoSlice ();
            h = new cipher_Ripemd160 ();
            skycoin.skycoin.SKY_cipher_RandByte (100, b);
            err = skycoin.skycoin.SKY_cipher_Ripemd160_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice ();
            h = new cipher_Ripemd160 ();
            skycoin.skycoin.SKY_cipher_RandByte (19, b);
            err = skycoin.skycoin.SKY_cipher_Ripemd160_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice ();
            h = new cipher_Ripemd160 ();
            skycoin.skycoin.SKY_cipher_RandByte (0, b);
            err = skycoin.skycoin.SKY_cipher_Ripemd160_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice ();
            h = new cipher_Ripemd160 ();
            skycoin.skycoin.SKY_cipher_RandByte (20, b);
            err = skycoin.skycoin.SKY_cipher_Ripemd160_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestSHA256Set () {
            var h = new cipher_SHA256 ();
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (33, b);
            err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256 ();
            b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (100, b);
            err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256 ();
            b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (31, b);
            err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256 ();
            b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (0, b);
            err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256 ();
            b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (32, b);
            err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestSHA256Hex () {
            var h = new cipher_SHA256 ();
            var b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (32, b);
            var err = skycoin.skycoin.SKY_cipher_SHA256_Set (h, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var s = new _GoString_ ();
            err = skycoin.skycoin.SKY_cipher_SHA256_Hex (h, s);
            var h2 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_cipher_SHA256FromHex (s.p, h2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (h.isEqual (h2), 1);
            var s1 = new _GoString_ ();
            err = skycoin.skycoin.SKY_cipher_SHA256_Hex (h, s1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (s1.p == s.p, true);

        }
    }
}