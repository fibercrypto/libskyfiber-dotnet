using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_coin {

        utils.transutils transutils = new utils.transutils ();

        [Test]
        public void TestAddress1 () {
            var address_hex = "02fa939957e9fc52140e180264e621c2576a1bfe781f88792fb315ca3d1786afb8";
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_base58_String2Hex (address_hex, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var p = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestAddress2 () {
            var address_hex = "5a42c0643bdb465d90bf673b99c14f5fa02db71513249d904573d2b8b63d353d";
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_base58_String2Hex (address_hex, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSecKey (s, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestCrypto1 () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            for (int i = 0; i < 10; i++) {
                var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_cipher_TestSecKey (s);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            }
        }

        [Test]
        public void TestCrypto2 () {
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_base58_String2Hex ("5a42c0643bdb465d90bf673b99c14f5fa02db71513249d904573d2b8b63d353d", b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (b.len, 32);
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSecKey (s, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var text = "test message";
            var sha = new cipher_SHA256 ();
            b = new GoSlice ();
            var text_str = new _GoString_ ();
            text_str.p = text;
            b.convertString (text_str);
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_TestSecKeyHash (s, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

        }

    }
}