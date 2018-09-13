using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_address {
        [Test ()]
        public void TestDecodeBase58Address () {
            uint err = 0;
            var s = new skycoin.cipher_SecKey ();
            var p = new skycoin.cipher_PubKey ();
            Assert.AreEqual (skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s), skycoin.skycoin.SKY_OK, "Failed SKY_cipher_GenerateKeyPair");
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_SecKey_Verify ");
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_PubKey_Verify");
            var a = new skycoin.cipher__Address ();
            var a1 = new skycoin.cipher__Address ();
            var a2 = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            err = skycoin.skycoin.SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, " Failed SKY_cipher_Address_Verify");
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address ("", a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58String, "Failed SKY_cipher_DecodeBase58Address 1");
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address ("asa", a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength, "Failed SKY_cipher_DecodeBase58Address 2");

            var sa = new skycoin._GoString_ ();
            err = skycoin.skycoin.SKY_cipher_Address_String (a, sa);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }
    }
}