using System;
using NUnit.Framework;
using skycoin;
namespace LibSkycoinNetTest {
    [TestFixture ()]
    public class check_cipher_address : skycoin.skycoin {
        [Test ()]
        public void TestDecodeBase58Address () {
            uint err = 0;
            var s = new cipher_SecKey ();
            var p = new cipher_PubKey ();
            Assert.AreEqual (SKY_cipher_GenerateKeyPair (p, s), SKY_OK, "Failed SKY_cipher_GenerateKeyPair");
            err = SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, SKY_OK, "Failed SKY_cipher_SecKey_Verify ");
            err = SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, SKY_OK, "Failed SKY_cipher_PubKey_Verify");
            var a = new cipher__Address ();
            var a1 = new cipher__Address ();
            var a2 = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            Assert.AreEqual (err, SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            err = SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, SKY_OK, " Failed SKY_cipher_Address_Verify");
            err = SKY_cipher_DecodeBase58Address ("", a1);
            Assert.AreEqual (err, SKY_ERROR, "Failed SKY_cipher_DecodeBase58Address 1");
            err = SKY_cipher_DecodeBase58Address ("asa", a1);
            Assert.AreEqual (err, SKY_ErrAddressInvalidLength, "Failed SKY_cipher_DecodeBase58Address 2");

            var addrStr = new _GoString_ ();
            err = SKY_cipher_Address_String (a, addrStr);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_DecodeBase58Address (addrStr.p, a2);
            Assert.AreEqual (err, SKY_OK, "Failed SKY_cipher_DecodeBase58Address");
            Assert.IsTrue (a.isEqual (a2) == 1);
            var as2 = " " + addrStr.p;
            err = SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, SKY_ERROR, "Failed SKY_cipher_DecodeBase58Address 31");

            as2 = "000" + addrStr.p;
            err = SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, SKY_ERROR, "Failed SKY_cipher_DecodeBase58Address 3");

            as2 = addrStr.p + "000";
            err = SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, SKY_ERROR, "Failed SKY_cipher_DecodeBase58Address 30");
        }

        [Test]
        public void TestAddressFromBytes () {
            var s = new cipher_SecKey ();
            var p = new cipher_PubKey ();
            uint err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            var a = new cipher__Address ();
            var a2 = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            var b = new GoSlice ();
            var b2 = new GoSlice ();

            err = SKY_cipher_Address_Bytes (a, b);
            err = SKY_cipher_Address_Bytes (a, b2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromBytes (b2, a2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (a2.isEqual (a), 1);
            // Invalid number of bytes
            long len_b = b.len;
            b.len = b.len - 2;
            err = SKY_cipher_AddressFromBytes (b, a2);
            Assert.AreEqual (SKY_ErrAddressInvalidLength, err, "Failed SKY_cipher_AddressFromBytes 2");
            b.len = len_b;
            a.setVersion ('2');
            err = SKY_cipher_Address_Bytes (a, b);
            Assert.AreEqual (err, SKY_OK, "SKY_cipher_Address_Bytes SKY_ErrAddressInvalidVersion");
            err = SKY_cipher_AddressFromBytes (b, a);
            Assert.AreEqual (SKY_ErrAddressInvalidVersion, err);
        }

        [Test]
        public void TestAddressRoundtrip () {
            // Tests encode and decode
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            var a = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            var b = new GoSlice ();
            err = SKY_cipher_Address_Bytes (a, b);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromBytes (b, a);
            Assert.AreEqual (err, SKY_OK);
            var a2 = new cipher__Address ();
            err = SKY_cipher_AddressFromBytes (b, a2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (a.isEqual (a2), 1);
            var str = new _GoString_ ();
            var str2 = new _GoString_ ();
            err = SKY_cipher_Address_String (a, str);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_Address_String (a2, str2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (str.p == str2.p, true);
        }

        [Test]
        public void TestAddressVerify () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            var a = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            // Valid pubkey+address
            err = SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, SKY_OK);
            // Invalid pubkey
            err = SKY_cipher_Address_Verify (a, new cipher_PubKey ());
            Assert.AreEqual (err, SKY_ErrAddressInvalidPubKey);
            var p2 = new cipher_PubKey ();
            var s2 = new cipher_SecKey ();
            err = SKY_cipher_GenerateKeyPair (p2, s2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_Address_Verify (a, p2);
            Assert.AreEqual (err, SKY_ErrAddressInvalidPubKey);
            // Bad version
            a.setVersion ('1');
            err = SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, SKY_ErrAddressInvalidVersion);
        }

        [Test]
        public void TestAddressBulk () {

            for (int i = 0; i < 1024; i++) {
                var pub = new cipher_PubKey ();
                var sec = new cipher_SecKey ();
                var b = new GoSlice ();
                var err = SKY_cipher_RandByte (32, b);
                Assert.AreEqual (err, SKY_OK);
                err = SKY_cipher_GenerateDeterministicKeyPair (b, pub, sec);
                Assert.AreEqual (err, SKY_OK);
                var a = new cipher__Address ();
                err = SKY_cipher_AddressFromPubKey (pub, a);
                Assert.AreEqual (err, SKY_OK);
                err = SKY_cipher_Address_Verify (a, pub);
                Assert.AreEqual (err, SKY_OK);
                var s = new _GoString_ ();
                err = SKY_cipher_Address_String (a, s);
                Assert.AreEqual (err, SKY_OK);
                var a2 = new cipher__Address ();
                err = SKY_cipher_DecodeBase58Address (s.p, a2);
                Assert.AreEqual (err, SKY_OK);
                Assert.AreEqual (a2.isEqual (a), 1);
            }
        }

        [Test]
        public void TestAddressNull () {
            var a = new cipher__Address ();
            Assert.AreEqual (a.isEqual (new cipher__Address ()), 1);
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (a.isEqual (new cipher__Address ()), 0);

        }
    }
}