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
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Duplicate outputs
            skycoin.skycoin.makeTransaction (tx);
            var pOutput = new coin__TransactionOutput ();
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Address = new cipher__Address ();
            err = skycoin.skycoin.SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Output coins are 0
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Coins = 0;
            err = skycoin.skycoin.SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Output coin overflow
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Coins = (ulong) (ulong.MaxValue - 3e6);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // Output coins are not multiples of 1e6 (valid, decimal restriction is not enforced here)
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            pOutput.Coins += 10;
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var p = new cipher_PubKey ();
            s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            secKeys = new cipher_SecKeys ();
            secKeys.allocate (1);
            secKeys.setAt (0, s);
            err = skycoin.skycoin.SKY_coin_Transaction_SignInputs (tx, secKeys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.IsTrue (pOutput.Coins % 1e6 != 0);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // Valid
            skycoin.skycoin.makeTransaction (tx);
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, (ulong) (10e6), pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, pOutput.Address, (ulong) (1e6), pOutput.Hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestTransactionVerifyInput () {
            // Valid
            var ux = new coin__UxOut ();
            var s = new cipher_SecKey ();
            skycoin.skycoin.makeUxOutWithSecret (ux, s);
            var tx = skycoin.skycoin.new_Transaction__Handlep ();
            skycoin.skycoin.makeTransactionFromUxOut (ux, s, tx);
            var seckeys = new coin_UxOutArray ();
            seckeys.allocate (1);
            seckeys.setAt (0, ux);
            var err = skycoin.skycoin.SKY_coin_Transaction_VerifyInput (tx, seckeys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestTransactionPushInput () {
            var tx = skycoin.skycoin.new_Transaction__Handlep ();
            skycoin.skycoin.makeEmptyTransaction (tx);
            var ux = new coin__UxOut ();
            skycoin.skycoin.makeUxOut (ux);
            var sha = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_coin_UxOut_Hash (ux, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var r = skycoin.skycoin.new_GoUint16p ();
            err = skycoin.skycoin.SKY_coin_Transaction_PushInput (tx, sha, r);
            Assert.AreEqual (skycoin.skycoin.GoUint16p_value (r), 0);
            var count = skycoin.skycoin.new_Gointp ();
            err = skycoin.skycoin.SKY_coin_Transaction_GetInputsCount (tx, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (count), 1);
            var sha1 = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_coin_Transaction_GetInputAt (tx, 0, sha1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (sha.isEqual (sha1), 1);
            err = skycoin.skycoin.SKY_coin_Transaction_ResetInputs (tx, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            for (int i = 0; i < short.MaxValue; i++) {
                r = skycoin.skycoin.new_GoUint16p ();
                err = skycoin.skycoin.SKY_coin_Transaction_PushInput (tx, new cipher_SHA256 (), r);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            }
            skycoin.skycoin.makeUxOut (ux);
            err = skycoin.skycoin.SKY_coin_UxOut_Hash (ux, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestTransactionPushOutput () {
            var tx = skycoin.skycoin.new_Transaction__Handlep ();
            transutils.makeEmptyTransaction (tx);
            var a = new cipher__Address ();
            transutils.makeAddress (a);
            var err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (tx, a, 100, 150);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var count = skycoin.skycoin.new_Gointp ();
            err = skycoin.skycoin.SKY_coin_Transaction_GetOutputsCount (tx, count);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.Gointp_value (count), 1);
            var pOut1 = new coin__TransactionOutput ();
            var pOut = new coin__TransactionOutput ();
            pOut1.Address = a;
            pOut1.Coins = 100;
            pOut1.Hours = 150;
            err =skycoin.skycoin.SKY_coin_Transaction_GetOutputAt (tx, 0, pOut);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            



        }
    }
}