using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_sig {
        bool forceLowS = true;
        utils.transutils utils = new transutils ();
        [Test]
        public void TestSigRecover () {
            string[][] vs = new string[2][];

            vs[0] = new string[] {
                "6028b9e3a31c9e725fcbd7d5d16736aaaafcc9bf157dfb4be62bcbcf0969d488",
                "036d4a36fa235b8f9f815aa6f5457a607f956a71a035bf0970d8578bf218bb5a",
                "9cff3da1a4f86caf3683f865232c64992b5ed002af42b321b8d8a48420680487",
                "0",
                "56dc5df245955302893d8dda0677cc9865d8011bc678c7803a18b5f6faafec08",
                "54b5fbdcd8fac6468dac2de88fadce6414f5f3afbb103753e25161bef77705a6",
            };

            vs[1] = new string[] {
                "b470e02f834a3aaafa27bd2b49e07269e962a51410f364e9e195c31351a05e50",
                "560978aed76de9d5d781f87ed2068832ed545f2b21bf040654a2daff694c8b09",
                "9ce428d58e8e4caf619dc6fc7b2c2c28f0561654d1f80f322c038ad5e67ff8a6",
                "1",
                "15b7e7d00f024bffcd2e47524bb7b7d3a6b251e23a3a43191ed7f0a418d9a578",
                "bf29a25e2d1f32c5afb18b41ae60112723278a8af31275965a6ec1d95334e840",
            };

            var xp = new secp256k1go__XY ();
            var public_key = new secp256k1go__XY ();
            var sig = skycoin.skycoin.new_Signature_HandlePtr ();
            var err = skycoin.skycoin.SKY_secp256k1go_Signature_Create (sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var msg = skycoin.skycoin.new_Number_HandlePtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Number_Create (msg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            for (int i = 0; i < vs.Length; i++) {
                var v = vs[i];
                var r = skycoin.skycoin.new_Number_HandlePtr ();
                var s = skycoin.skycoin.new_Number_HandlePtr ();
                err = skycoin.skycoin.SKY_secp256k1go_Signature_GetR (sig, r);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                skycoin.skycoin.SKY_secp256k1go_Number_SetHex (r, v[0]);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_secp256k1go_Signature_GetR (sig, s);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                skycoin.skycoin.SKY_secp256k1go_Number_SetHex (s, v[1]);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                skycoin.skycoin.SKY_secp256k1go_Number_SetHex (msg, v[2]);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                var rid = Convert.ToInt64 (v[3]);
                skycoin.skycoin.SKY_secp256k1go_Field_SetHex (xp.X, v[4]);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                skycoin.skycoin.SKY_secp256k1go_Field_SetHex (xp.Y, v[5]);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                var val = skycoin.skycoin.new_CharPtr ();
                skycoin.skycoin.SKY_secp256k1go_Signature_Recover (sig, public_key, msg, rid, val);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.IsTrue (skycoin.skycoin.CharPtr_value (val) == 1);
                Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_Field_Equals (xp.X, public_key.X, val), skycoin.skycoin.SKY_OK, i.ToString ());
                Assert.AreEqual (skycoin.skycoin.SKY_secp256k1go_Field_Equals (xp.Y, public_key.Y, val), skycoin.skycoin.SKY_OK, i.ToString ());
            }
        }

        [Test]
        public void TestSigVerify () {
            var key = new secp256k1go__XY ();
            var sig = skycoin.skycoin.new_Signature_HandlePtr ();
            var err = skycoin.skycoin.SKY_secp256k1go_Signature_Create (sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var msg = skycoin.skycoin.new_Number_HandlePtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Number_Create (msg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var arg = "D474CBF2203C1A55A411EEC4404AF2AFB2FE942C434B23EFE46E9F04DA8433CA";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (msg, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var r = skycoin.skycoin.new_Number_HandlePtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Signature_GetR (sig, r);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "98F9D784BA6C5C77BB7323D044C0FC9F2B27BAA0A5B0718FE88596CC56681980";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (r, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var s = skycoin.skycoin.new_Number_HandlePtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Signature_GetS (sig, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "E3599D551029336A745B9FB01566624D870780F363356CEE1425ED67D1294480";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (s, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "7d709f85a331813f9ae6046c56b3a42737abf4eb918b2e7afee285070e968b93";
            err = skycoin.skycoin.SKY_secp256k1go_Field_SetHex (key.X, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "26150d1a63b342986c373977b00131950cb5fc194643cad6ea36b5157eba4602";
            err = skycoin.skycoin.SKY_secp256k1go_Field_SetHex (key.Y, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var val = skycoin.skycoin.new_CharPtr ();
            err = skycoin.skycoin.SKY_secp256k1go_Signature_Verify (sig, key, msg, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.CharPtr_value (val)));

            arg = "2c43a883f4edc2b66c67a7a355b9312a565bb3d33bb854af36a06669e2028377";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (msg, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_Signature_GetR (sig, r);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "6b2fa9344462c958d4a674c2a42fbedf7d6159a5276eb658887e2e1b3915329b";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (r, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_Signature_GetS (sig, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "eddc6ea7f190c14a0aa74e41519d88d2681314f011d253665f301425caf86b86";
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (s, arg);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            arg = "02a60d70cfba37177d8239d018185d864b2bdd0caf5e175fd4454cc006fd2d75ac";
            var xy = new GoSlice ();
            err = skycoin.skycoin.SKY_base58_String2Hex (arg, xy);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_XY_ParsePubkey (key, xy, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_secp256k1go_Signature_Verify (sig, key, msg, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.CharPtr_value (val)));
        }

        [Test]
        public void TestSigSign () {
            var sig = skycoin.skycoin.new_Signature_HandlePtr ();
            var msg = skycoin.skycoin.new_Number_HandlePtr ();
            var sec = skycoin.skycoin.new_Number_HandlePtr ();
            var non = skycoin.skycoin.new_Number_HandlePtr ();
            var r = skycoin.skycoin.new_Number_HandlePtr ();
            var s = skycoin.skycoin.new_Number_HandlePtr ();
            var val = skycoin.skycoin.new_CharPtr ();
            skycoin.skycoin.SKY_secp256k1go_Signature_Create (sig);
            skycoin.skycoin.SKY_secp256k1go_Number_Create (sec);
            skycoin.skycoin.SKY_secp256k1go_Number_Create (msg);
            skycoin.skycoin.SKY_secp256k1go_Number_Create (non);
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (sec, "73641C99F7719F57D8F4BEB11A303AFCD190243A51CED8782CA6D3DBE014D146");
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (sec, "D474CBF2203C1A55A411EEC4404AF2AFB2FE942C434B23EFE46E9F04DA8433CA");
            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (sec, "9E3CD9AB0F32911BFDE39AD155F527192CE5ED1F51447D63C4F154C118DA598E");
            var recid = skycoin.skycoin.new_Gointp ();
            var res = skycoin.skycoin.new_Gointp ();
            var err = skycoin.skycoin.SKY_secp256k1go_Signature_Sign (sig, sec, msg, non, res, recid);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (res), 1);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (recid), 0);

            skycoin.skycoin.SKY_secp256k1go_Number_SetHex (non, "98f9d784ba6c5c77bb7323d044c0fc9f2b27baa0a5b0718fe88596cc56681980");
            skycoin.skycoin.SKY_secp256k1go_Signature_GetR (sig, r);
            err = skycoin.skycoin.SKY_secp256k1go_Number_IsEqual (r, non, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.CharPtr_value (val)));
            err = skycoin.skycoin.SKY_secp256k1go_Number_SetHex (non, "1ca662aaefd6cc958ba4604fea999db133a75bf34c13334dabac7124ff0cfcc1");
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            skycoin.skycoin.SKY_secp256k1go_Signature_GetS (sig, s);
            err = skycoin.skycoin.SKY_secp256k1go_Number_IsEqual (s, non, val);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.CharPtr_value (val)));

        }
    }
}