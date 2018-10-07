using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_sha256xor {
        utils.transutils utils = new transutils ();
        int sha256XorDataLengthSize = 4;
        int sha256XorBlockSize = 32;
        int sha256XorNonceSize = 32;
        int sha256XorChecksumSize = 32;

        struct StrTest {
            public string name;
            public int data;
            public GoSlice password;
            public int error;
        }

        StrTest[] cases;

        public void FullCase1 () {
            cases = new StrTest[5];
            var c = new StrTest ();
            var str = new _GoString_ ();
            c.name = "data length=1 password is empty=true";
            c.data = 1;
            c.password = new GoSlice ();
            c.error = skycoin.skycoin.SKY_ErrSHA256orMissingPassword;
            cases[0] = c;

            c = new StrTest ();
            str = new _GoString_ ();
            c.name = "data length=1  password is empty=false";
            c.data = 1;
            c.password = new GoSlice ();
            str.p = "key";
            c.password.convertString (str);
            c.error = skycoin.skycoin.SKY_OK;
            cases[1] = c;

            c = new StrTest ();
            str = new _GoString_ ();
            c.name = "data length<32  password is empty=false";
            c.data = 2;
            c.password = new GoSlice ();
            str.p = "pwd";
            c.password.convertString (str);
            c.error = skycoin.skycoin.SKY_OK;
            cases[2] = c;

            c = new StrTest ();
            str = new _GoString_ ();
            c.name = "data length=2*32  password is empty=false";
            c.data = 64;
            c.password = new GoSlice ();
            str.SetString ("9JMkCPphe73NQvGhmab");
            c.password.convertString (str);
            c.error = skycoin.skycoin.SKY_OK;
            cases[3] = c;

            c = new StrTest ();
            str = new _GoString_ ();
            c.name = "data length>2*32  password is empty=false";
            c.data = 65;
            c.password = new GoSlice ();
            str.p = "9JMkCPphe73NQvGhmab";
            c.password.convertString (str);
            c.error = skycoin.skycoin.SKY_OK;
            cases[4] = c;

        }

        [Test]
        public void TestEncrypt () {
            FullCase1 ();

            for (int i = 0; i < cases.Length; i++) {
                var t = cases[i];
                Console.WriteLine (t.name + " " + i);
                var encrypted = new GoSlice ();
                var data = new GoSlice ();
                var err = skycoin.skycoin.SKY_cipher_RandByte (t.data, data);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_encrypt_Sha256Xor_Encrypt (data, t.password, encrypted);
                Assert.AreEqual (t.error, err);
                if (t.error == skycoin.skycoin.SKY_OK) {
                    var n = (sha256XorDataLengthSize + t.data) / sha256XorBlockSize;
                    var m = (sha256XorDataLengthSize + t.data) % sha256XorBlockSize;
                    if (m > 0) {
                        n += 1;
                    }
                    var str = new _GoString_ ();
                    encrypted.getString (str);
                    Console.WriteLine (str.p);
                    Console.WriteLine (str.n);
                    if (utils.IsBase64String (str.p)) {
                        var rdata = utils.base64Decode (str.p);
                        var totalEncryptedDataLen = sha256XorBlockSize + sha256XorNonceSize + 32 + n * sha256XorBlockSize;
                        Assert.AreEqual (rdata.Length, totalEncryptedDataLen, t.name);
                    }
                }

            }
        }
        public void FullCase2 () {
            cases = new StrTest[5];
            var str = new _GoString_ ();
            var c = new StrTest ();
            c.name = "invalid data length";
            c.error = skycoin.skycoin.SKY_ERROR;
            c.data = 32;
            c.password = new GoSlice ();
            str.p = "pwd";
            c.password.convertString (str);
            cases[0] = c;

            str = new _GoString_ ();
            c = new StrTest ();
            c.name = "invalid checksum";
            c.error = skycoin.skycoin.SKY_ERROR;
            c.data = 32;
            c.password = new GoSlice ();
            str.p = "pwd";
            c.password.convertString (str);
            cases[1] = c;

            str = new _GoString_ ();
            c = new StrTest ();
            c.name = "empty password";
            c.error = skycoin.skycoin.SKY_ErrSHA256orMissingPassword;
            c.data = 32;
            c.password = new GoSlice ();
            str.p = string.Empty;
            c.password.convertString (str);
            cases[2] = c;

            str = new _GoString_ ();
            c = new StrTest ();
            c.name = "nil password";
            c.error = skycoin.skycoin.SKY_ErrSHA256orMissingPassword;
            c.data = 32;
            c.password = new GoSlice ();
            str.p = string.Empty;
            c.password.convertString (str);
            cases[3] = c;

            str = new _GoString_ ();
            c = new StrTest ();
            c.name = "invalid password";
            c.error = skycoin.skycoin.SKY_ERROR;
            c.data = 32;
            c.password = new GoSlice ();
            str.p = "wrong password";
            c.password.convertString (str);
            cases[4] = c;

        }

        [Test]
        public void TestDecrypt () {
            FullCase2 ();
            for (int i = 0; i < cases.Length; i++) {
                var tc = cases[i];
                var d = new GoSlice ();
                var data = new GoSlice ();
                var edata = new GoSlice ();
                skycoin.skycoin.SKY_cipher_RandByte (32, data);
                skycoin.skycoin.makeEncryptedData (data, 65, tc.password, edata);
                var err = skycoin.skycoin.SKY_encrypt_Sha256Xor_Decrypt (edata, tc.password, d);
                Assert.AreEqual (err, tc.error, tc.name + " " + i);
                if (err == skycoin.skycoin.SKY_OK) {
                    Assert.AreEqual (d.isEqual (data), 1);
                }
            }

            for (int i = 0; i < 64; i++) {
                var data = new GoSlice ();
                var err = skycoin.skycoin.SKY_cipher_RandByte (32, data);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (data.len, 32);
                var pwd = new GoSlice ();
                var encrypted = new GoSlice ();
                var decrypted = new GoSlice ();
                var pwdStr = new _GoString_ ();
                pwdStr.SetString ("pwd");
                pwd.convertString (pwdStr);
                err = skycoin.skycoin.SKY_encrypt_Sha256Xor_Encrypt (data, pwd, encrypted);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_encrypt_Sha256Xor_Decrypt (encrypted, pwd, decrypted);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (data.isEqual (decrypted), 1);
            }
        }
    }
}