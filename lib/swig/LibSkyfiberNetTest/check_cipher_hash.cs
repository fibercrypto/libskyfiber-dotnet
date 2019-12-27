using System;
using NUnit.Framework;
using skycoin;
namespace LibskyfiberNetTest
{
    [TestFixture()]
    public class check_cipher_hash : skycoin.skycoin
    {
        public void freshSumRipemd160(skycoin.GoSlice bytes, cipher_Ripemd160 rp160)
        {
            SKY_cipher_HashRipemd160(bytes, rp160);
        }

        void freshSumSHA256(GoSlice bytes, cipher_SHA256 sha256)
        {
            SKY_cipher_SumSHA256(bytes, sha256);
        }

        [Test]
        public void TestHashRipemd160()
        {
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(128, b);
            var hr = new cipher_Ripemd160();
            err = SKY_cipher_HashRipemd160(b, hr);
            Assert.AreEqual(err, SKY_OK);
            b = new GoSlice();
            err = SKY_cipher_RandByte(160, b);
            var r = new cipher_Ripemd160();
            err = SKY_cipher_HashRipemd160(b, r);
            Assert.AreEqual(r.isEqual(new cipher_Ripemd160()), 0);
            // 2nd hash should not be affected by previous
            b = new GoSlice();
            err = SKY_cipher_RandByte(256, b);
            var r2 = new cipher_Ripemd160();
            err = SKY_cipher_HashRipemd160(b, r2);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(r2.isEqual(new cipher_Ripemd160()), 0);
            var r3 = new cipher_Ripemd160();
            freshSumRipemd160(b, r3);
            Assert.AreEqual(r2.isEqual(r3), 1);
        }

        [Test]
        public void TestRipemd160Set()
        {
            var h = new cipher_Ripemd160();
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(21, b);
            err = SKY_cipher_Ripemd160_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthRipemd160);
            b = new GoSlice();
            h = new cipher_Ripemd160();
            SKY_cipher_RandByte(100, b);
            err = SKY_cipher_Ripemd160_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice();
            h = new cipher_Ripemd160();
            SKY_cipher_RandByte(19, b);
            err = SKY_cipher_Ripemd160_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice();
            h = new cipher_Ripemd160();
            SKY_cipher_RandByte(0, b);
            err = SKY_cipher_Ripemd160_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthRipemd160);

            b = new GoSlice();
            h = new cipher_Ripemd160();
            SKY_cipher_RandByte(20, b);
            err = SKY_cipher_Ripemd160_Set(h, b);
            Assert.AreEqual(err, SKY_OK);
        }

        [Test]
        public void TestSHA256Set()
        {
            var h = new cipher_SHA256();
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(33, b);
            err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256();
            b = new GoSlice();
            SKY_cipher_RandByte(100, b);
            err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256();
            b = new GoSlice();
            SKY_cipher_RandByte(31, b);
            err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256();
            b = new GoSlice();
            SKY_cipher_RandByte(0, b);
            err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_ErrInvalidLengthSHA256);
            h = new cipher_SHA256();
            b = new GoSlice();
            SKY_cipher_RandByte(32, b);
            err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_OK);
        }

        [Test]
        public void TestSHA256Hex()
        {
            var h = new cipher_SHA256();
            var b = new GoSlice();
            SKY_cipher_RandByte(32, b);
            var err = SKY_cipher_SHA256_Set(h, b);
            Assert.AreEqual(err, SKY_OK);
            var s = new _GoString_();
            err = SKY_cipher_SHA256_Hex(h, s);
            var h2 = new cipher_SHA256();
            err = SKY_cipher_SHA256FromHex(s.p, h2);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(h.isEqual(h2), 1);
            var s1 = new _GoString_();
            err = SKY_cipher_SHA256_Hex(h, s1);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(s1.p == s.p, true);
        }

        struct StrTest
        {
            public string input;
            public string output;
        }

        [Test]
        public void TestSHA256KnownValue()
        {

            StrTest caseval;
            caseval.input = "skycoin";
            caseval.output = "5a42c0643bdb465d90bf673b99c14f5fa02db71513249d904573d2b8b63d353d";
            StrTest caseval1;
            caseval1.input = "hello world";
            caseval1.output = "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9";
            StrTest caseval2;
            caseval2.input = "hello world asd awd awd awdapodawpokawpod ";
            caseval2.output = "99d71f95cafe05ea2dddebc35b6083bd5af0e44850c9dc5139b4476c99950be4";
            StrTest[] val = { caseval, caseval1, caseval2 };

            for (int i = 0; i < val.Length; i++)
            {
                var io = val[i];
                var b = new GoSlice();
                var h = new cipher_SHA256();
                var input = new _GoString_();
                var output = new _GoString_();
                input.p = io.input;
                b.convertString(input);
                var err = SKY_cipher_SumSHA256(b, h);
                Assert.AreEqual(err, SKY_OK);
                err = SKY_cipher_SHA256_Hex(h, output);
                Assert.AreEqual(err, SKY_OK);
                Assert.AreEqual(output.p == io.output, true);
            }
        }

        [Test]
        public void TestSumSHA256()
        {
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(256, b);
            var h1 = new cipher_SHA256();
            err = SKY_cipher_SumSHA256(b, h1);
            Assert.AreEqual(h1.isEqual(new cipher_SHA256()), 0);
            // A second call to Sum should not be influenced by the original
            var c = new GoSlice();
            err = SKY_cipher_RandByte(256, c);
            var h2 = new cipher_SHA256();
            err = SKY_cipher_SumSHA256(c, h2);
            Assert.AreEqual(h2.isEqual(new cipher_SHA256()), 0);
            var h3 = new cipher_SHA256();
            freshSumSHA256(c, h3);
            Assert.AreEqual(h2.isEqual(h3), 1);
        }

    }
}