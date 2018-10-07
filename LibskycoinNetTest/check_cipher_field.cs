using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_field {
        utils.transutils utils = new transutils ();
        [Test]
        public void TestFeInv () {
            var in_ = new secp256k1go__Field ();
            var out_ = new secp256k1go__Field ();
            var exp = new secp256k1go__Field ();

            var in_hex = "813925AF112AAB8243F8CCBADE4CC7F63DF387263028DE6E679232A73A7F3C31";
            var exp_hex = "7F586430EA30F914965770F6098E492699C62EE1DF6CAFFA77681C179FDF3117";

            var err = skycoin.skycoin.SKY_secp256k1go_Field_SetHex (in_, in_hex);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_Field_SetHex (exp, exp_hex);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_Field_Inv (in_, out_);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var val = skycoin.skycoin.new_CharPtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Field_Equals (out_, exp, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.CharPtr_value (val)));
        }
    }
}