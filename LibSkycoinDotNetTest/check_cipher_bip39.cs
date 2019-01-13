using System;
using NUnit.Framework;
using skycoin;

namespace LibSkycoinDotNetTest {
    [TestFixture ()]
    public class check_cipher_bip39 : skycoin.skycoin {
        transutils utils = new transutils ();
        [Test]
        public void TestIsMnemonicValid () {
            var m = new _GoString_ ();
            var err = SKY_bip39_NewDefaultMnemomic (m);
            Assert.AreEqual (err, SKY_OK);
            var val = new_CharPtr ();
            err = SKY_bip39_IsMnemonicValid (m.p, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsTrue (Convert.ToBoolean (CharPtr_value (val)));

            // Truncated
            var str = m.p;
            str = str.Substring (0, str.Length - 15);
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            // Trailing whitespace
            str = m.p;
            str += " ";
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            str = m.p;
            str += "/n";
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            // Preceding whitespace
            str = m.p;
            str = String.Concat (str, " ");
            str = String.Concat (str, str);
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            str = m.p;
            str += "/n" + str;
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            // Extra whitespace between words
            str = m.p;
            var ms = str.Split (' ');
            str = String.Join ("  ", ms);
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            // Contains invalid word
            str = m.p;
            ms = str.Split (' ');
            ms[2] = "foo";
            str = String.Join ("  ", ms);
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

            // Invalid number of words
            str = m.p;
            ms = str.Split (' ');
            var ms1 = new string[ms.Length - 1];
            for (int i = 0; i < ms1.Length; i++) {
                ms1[i] = ms[i];
            }
            str = String.Join ("  ", ms1);
            err = SKY_bip39_IsMnemonicValid (str, val);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsFalse (Convert.ToBoolean (CharPtr_value (val)));

        }
    }
}