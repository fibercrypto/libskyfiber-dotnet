using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_encrypt_scrypt_chacha20poly1305 {
        [Test]
        public void TestScryptChacha20poly1305Encrypt () {
            for (int i = 1; i < 20; i++) {
                var name = "N=1<<" + i.ToString () + "(" + (1 << i).ToString () + ")" + ", R=8, p=1, keyLen=32";
                var crypto = new encrypt__ScryptChacha20poly1305 ();
                crypto.N = (int) (1 << i);
                crypto.R = 8;
                crypto.P = 1;
                crypto.KeyLen = 32;
                var encData = new GoSlice ();
                var plain = new GoSlice ();
                var passwd = new GoSlice ();
                var plaintext = new _GoString_ ();
                var passwdText = new _GoString_ ();
                plaintext.p = "plaintext";
                plain.convertString (plaintext);
                passwdText.p = "password";
                passwd.convertString (passwdText);
                var err = skycoin.skycoin.SKY_encrypt_ScryptChacha20poly1305_Encrypt (crypto, plain, passwd, encData);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK, name);
                Assert.AreEqual(encData.len > 2,true);
                // var str = new _GoString_();
                // str.p ="";
                // var decode_len = skycoin.skycoin.b64_decode(encData.toString(),(uint)encData.len,str);


            }
        }
    }
}