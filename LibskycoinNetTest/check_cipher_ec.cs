using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_ec {
        utils.transutils utils = new transutils ();

        [Test]
        public void TestECmult () {
            var u1 = skycoin.skycoin.new_Number_HandlePtr ();
            var u2 = skycoin.skycoin.new_Number_HandlePtr ();
            skycoin.skycoin.SKY_secp256k1go_Number_Create (u1);
            skycoin.skycoin.SKY_secp256k1go_Number_Create (u2);
            var public_kej = new secp256k1go__XYZ ();
            var expres = new secp256k1go__XYZ ();
            var pr = new secp256k1go__XYZ ();
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (public_kej.X, "0EAEBCD1DF2DF853D66CE0E1B0FDA07F67D1CABEFDE98514AAD795B86A6EA66D");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (public_kej.Y, "BEB26B67D7A00E2447BAECCC8A4CEF7CD3CAD67376AC1C5785AEEBB4F6441C16");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (public_kej.Z, "0000000000000000000000000000000000000000000000000000000000000001");
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (u1, "B618EBA71EC03638693405C75FC1C9ABB1A74471BAAF1A3A8B9005821491C4B4");
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (u2, "8554470195DE4678B06EDE9F9286545B51FF2D9AA756CE35A39011783563EA60");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (expres.X, "EB6752420B6BDB40A760AC26ADD7E7BBD080BF1DF6C0B009A0D310E4511BDF49");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (expres.Y, "8E8CEB84E1502FC536FFE67967BC44314270A0B38C79865FFED5A85D138DCA6B");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (expres.Z, "813925AF112AAB8243F8CCBADE4CC7F63DF387263028DE6E679232A73A7F3C31");

            Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_XYZ_ECmult (public_kej, pr, u2, u1), skycoin.skycoin.SKY_OK);
            var val = skycoin.skycoin.new_CharPtr ();
            Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_XYZ_Equals (pr, expres, val), skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value (val), 1);
        }

        [Test]
        public void TestMultGen () {
            var noce = skycoin.skycoin.new_Number_HandlePtr ();
            var err = skycoin.skycoin.SKY_secp256k1go_Number_Create (noce);
            var x = new secp256k1go__Field ();
            var y = new secp256k1go__Field ();
            var z = new secp256k1go__Field ();
            var pr = new secp256k1go__XYZ ();
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (noce, "9E3CD9AB0F32911BFDE39AD155F527192CE5ED1F51447D63C4F154C118DA598E");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (x, "02D1BF36D37ACD68E4DD00DB3A707FD176A37E42F81AEF9386924032D3428FF0");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (y, "FD52E285D33EC835230EA69F89D9C38673BD5B995716A4063C893AF02F938454");
            skycoin.skycoin.SKY_secp256k1go_Field_SetHex (z, "4C6ACE7C8C062A1E046F66FD8E3981DC4E8E844ED856B5415C62047129268C1B");
            skycoin.skycoin.SKY_secp256k1go_ECmultGen (pr, noce);
            skycoin.skycoin.SKY_secp256k1go_Field_Normalize (pr.X);
            skycoin.skycoin.SKY_secp256k1go_Field_Normalize (pr.Y);
            skycoin.skycoin.SKY_secp256k1go_Field_Normalize (pr.Z);
            var val = skycoin.skycoin.new_CharPtr ();
            Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_Field_Equals (pr.X, x, val), skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value (val), 1);
            Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_Field_Equals (pr.Y, y, val), skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value (val), 1);
            Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_Field_Equals (pr.Z, z, val), skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.CharPtr_value (val), 1);

        }
    }
}