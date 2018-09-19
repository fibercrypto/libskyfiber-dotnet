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
                Console.WriteLine ("----------------Iteracion----------------- " + i.ToString () + name);
                var crypto = new encrypt__ScryptChacha20poly1305 ();
                crypto.N = ((long) (1 << i));
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
                Assert.AreEqual (encData.len > 2, true);
                string str = (string) encData.toString ().p;
                var base64 = Convert.FromBase64String (str);
                var meta = System.Text.ASCIIEncoding.ASCII.GetString (base64);
                Assert.AreEqual (base64.Length >= 2, true);
                Assert.AreEqual (base64.Length < 1024, true);

                
                var n = skycoin.skycoin.new_intp ();
                var r = skycoin.skycoin.new_intp ();
                var p = skycoin.skycoin.new_intp ();
                var keyLen = skycoin.skycoin.new_intp ();
                Console.WriteLine (meta);
                skycoin.skycoin.parseJsonMetaData (meta, n, r, p, keyLen);
                Console.WriteLine ("N =" + skycoin.skycoin.intp_value (n));
                Console.WriteLine ("R =" + skycoin.skycoin.intp_value (r));
                Console.WriteLine ("P = " + skycoin.skycoin.intp_value (p));
                Console.WriteLine ("keyLen= " + skycoin.skycoin.intp_value (keyLen));
            }
        }

        struct StructTest {
            public string name;
            public GoSlice data;
            public GoSlice encData;
            public GoSlice encPwd;
            public GoSlice decPwd;
            public long err;
        }

        [Test ()]
        public void TestScryptChacha20poly1305Decrypt () {

            var casett = new StructTest ();

            // StructTest.data
            var pData = new GoSlice ();
            var pDataText = new _GoString_ ();
            pDataText.p = "plaintext";
            pData.convertString (pDataText);

            // StructTest.encData
            var pencData = new GoSlice ();
            var pencDataText = new _GoString_ ();
            pencDataText.p = "dQB7Im4iOjUyNDI4OCwiciI6OCwicCI6MSwia2V5TGVuIjozMiwic2FsdCI6ImpiejUrSFNjTFFLWkI5T0tYblNNRmt2WDBPY3JxVGZ0ZFpDNm9KUFpaeHc9Iiwibm9uY2UiOiJLTlhOQmRQa1ZUWHZYNHdoIn3PQFmOot0ETxTuv//skTG7Q57UVamGCgG5";
            pencData.convertString (pDataText);

            // StructTest.encPwd
            var pencPwd = new GoSlice ();
            var pencPwdText = new _GoString_ ();
            pencPwdText.p = "pwd";
            pencPwd.convertString (pencPwdText);

            // StructTest.decPwd
            var pdecPwd = new GoSlice ();
            var pdecPwdText = new _GoString_ ();
            pdecPwdText.p = "pwd";
            pdecPwd.convertString (pdecPwdText);
            casett.data = pData;
            casett.decPwd = pdecPwd;
            casett.encData = pencData;
            casett.encPwd = pencPwd;
            casett.err = skycoin.skycoin.SKY_OK;
            StructTest[] tt = { casett };

            // for (int i = 0; i < tt.Length; i++) {
            //     var tc = tt[i];
            //     var name = "N=1<<19 r=8 p=1 keyLen=32 " + tc.name;
            //     var crypto = new encrypt__ScryptChacha20poly1305 ();
            //     var data = new GoSlice ();
            //     var err = skycoin.skycoin.SKY_encrypt_ScryptChacha20poly1305_Decrypt (crypto, tc.encData, tc.decPwd, data);
            //     Assert.AreEqual (err, tc.err, name);

            // }
        }
    }
}