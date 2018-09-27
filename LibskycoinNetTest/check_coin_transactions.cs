using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_transactions {

        utils.transutils transutils = new utils.transutils ();
        [Test]
        public void TestTransactionVerify () {

            // Mismatch header hash
            var tx = skycoin.skycoin.new_Transaction__Handlep ();
            var ptx = skycoin.skycoin.makeTransaction (tx);
            ptx.setInnerHash (new cipher_SHA256 ());
            var err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // No inputs
            tx = skycoin.skycoin.new_Transaction__Handlep ();
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetInputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // No outputs
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "");
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Invalid number of Sigs
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetSignatures (tx, 20);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Too many sigs & inputs
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetSignatures (tx, short.MaxValue);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetInputs (tx, short.MaxValue);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Duplicate inputs
            var ux = new coin__UxOut ();
            var s = new cipher_SecKey ();
            var h = new cipher_SHA256 ();

            skycoin.skycoin.makeUxOutWithSecret (ux, s);
            skycoin.skycoin.makeTransactionFromUxOut (ux, s, tx);
            err = skycoin.skycoin.SKY_coin_Transaction_GetInputAt (tx, 0, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var p1 = skycoin.skycoin.new_GoUint16p ();
            err = skycoin.skycoin.SKY_coin_Transaction_PushInput (tx, h, p1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var secKeys = new cipher_SecKeys ();
            secKeys.allocate (2);
            secKeys.setAt (0, s);
            secKeys.setAt (1, s);
            err = skycoin.skycoin.SKY_coin_Transaction_SignInputs (tx, secKeys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            

        }
    }
}