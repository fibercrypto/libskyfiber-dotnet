using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_address {
        [Test ()]
        public void TestDecodeBase58Address () {
            uint err = 0;
            cipher_SecKey s = new cipher_SecKey();
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Faild SKY_cipher_SecKey_Verify ");
            cipher_PubKey p = new skycoin.cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Faild SKY_cipher_PubKey_Verify");

            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Faild SKY_cipher_GenerateKeyPair");

        }
    }
}