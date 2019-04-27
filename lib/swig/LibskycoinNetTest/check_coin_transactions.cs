using System;
using NUnit.Framework;
using skycoin;
using utils;

namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_transactions : skycoin.skycoin {

        utils.transutils transutils = new utils.transutils ();
        [Test]
        public void TestTransactionVerify () {

            // Mismatch header hash
            var tx = transutils.makeEmptyTransaction ();
            var ptx = makeTransaction (tx);
            ptx.setInnerHash (new cipher_SHA256 ());
            var err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // No inputs
            tx = new_Transaction__Handlep ();
            makeTransaction (tx);
            err = SKY_coin_Transaction_ResetInputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // No outputs
            makeTransaction (tx);
            err = SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, SKY_OK, "");
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Invalid number of Sigs
            makeTransaction (tx);
            err = SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);
            err = SKY_coin_Transaction_ResetSignatures (tx, 20);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Too many sigs & inputs
            makeTransaction (tx);
            err = SKY_coin_Transaction_ResetSignatures (tx, short.MaxValue);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_ResetInputs (tx, short.MaxValue);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Duplicate inputs
            var ux = new coin__UxOut ();
            var s = new cipher_SecKey ();
            var h = new cipher_SHA256 ();

            makeUxOutWithSecret (ux, s);
            makeTransactionFromUxOut (ux, s, tx);
            err = SKY_coin_Transaction_GetInputAt (tx, 0, h);
            Assert.AreEqual (err, SKY_OK);
            var p1 = new_GoUint16p ();
            err = SKY_coin_Transaction_PushInput (tx, h, p1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            var secKeys = new cipher_SecKeys ();
            secKeys.allocate (2);
            secKeys.setAt (0, s);
            secKeys.setAt (1, s);
            err = SKY_coin_Transaction_SignInputs (tx, secKeys);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Duplicate outputs
            makeTransaction (tx);
            var pOutput = new coin__TransactionOutput ();
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Address = new cipher__Address ();
            err = SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Output coins are 0
            makeTransaction (tx);
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Coins = 0;
            err = SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Output coin overflow
            makeTransaction (tx);
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            pOutput.Coins = (ulong) (ulong.MaxValue - 3e6);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_ERROR);

            // Output coins are not multiples of 1e6 (valid, decimal restriction is not enforced here)
            makeTransaction (tx);
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            pOutput.Coins += 10;
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, pOutput.Coins, pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_ResetSignatures (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            var p = new cipher_PubKey ();
            s = new cipher_SecKey ();
            err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            secKeys = new cipher_SecKeys ();
            secKeys.allocate (1);
            secKeys.setAt (0, s);
            err = SKY_coin_Transaction_SignInputs (tx, secKeys);
            Assert.AreEqual (err, SKY_OK);
            Assert.IsTrue (pOutput.Coins % 1e6 != 0);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_OK);
            // Valid
            makeTransaction (tx);
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOutput);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_ResetOutputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, (ulong) (10e6), pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (tx, pOutput.Address, (ulong) (1e6), pOutput.Hours);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_UpdateHeader (tx);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_Verify (tx);
            Assert.AreEqual (err, SKY_OK);
        }

        [Test]
        public void TestTransactionVerifyInput () {
            // Valid
            var ux = new coin__UxOut ();
            var s = new cipher_SecKey ();
            makeUxOutWithSecret (ux, s);
            var tx = new_Transaction__Handlep ();
            makeTransactionFromUxOut (ux, s, tx);
            var seckeys = new coin_UxOutArray ();
            seckeys.allocate (1);
            seckeys.setAt (0, ux);
            var err = SKY_coin_Transaction_VerifyInput (tx, seckeys);
            Assert.AreEqual (err, SKY_OK);
        }

        [Test]
        public void TestTransactionPushInput () {
            var tx = new_Transaction__Handlep ();
            makeEmptyTransaction (tx);
            var ux = new coin__UxOut ();
            makeUxOut (ux);
            var sha = new cipher_SHA256 ();
            var err = SKY_coin_UxOut_Hash (ux, sha);
            Assert.AreEqual (err, SKY_OK);
            var r = new_GoUint16p ();
            err = SKY_coin_Transaction_PushInput (tx, sha, r);
            Assert.AreEqual (GoUint16p_value (r), 0);
            var count = new_Gointp ();
            err = SKY_coin_Transaction_GetInputsCount (tx, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (count), 1);
            var sha1 = new cipher_SHA256 ();
            err = SKY_coin_Transaction_GetInputAt (tx, 0, sha1);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (sha.isEqual (sha1), 1);
            err = SKY_coin_Transaction_ResetInputs (tx, 0);
            Assert.AreEqual (err, SKY_OK);
            for (int i = 0; i < short.MaxValue; i++) {
                r = new_GoUint16p ();
                err = SKY_coin_Transaction_PushInput (tx, new cipher_SHA256 (), r);
                Assert.AreEqual (err, SKY_OK);
            }
            makeUxOut (ux);
            err = SKY_coin_UxOut_Hash (ux, sha);
            Assert.AreEqual (err, SKY_OK);
        }

        [Test]
        public void TestTransactionPushOutput () {
            var tx = transutils.makeEmptyTransaction ();
            var a = transutils.makeAddress ();
            var err = SKY_coin_Transaction_PushOutput (tx, a, 100, 150);
            Assert.AreEqual (err, SKY_OK);
            var count = new_Gointp ();
            err = SKY_coin_Transaction_GetOutputsCount (tx, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (count), 1);
            var pOut1 = new coin__TransactionOutput ();
            var pOut = new coin__TransactionOutput ();
            pOut1.Address = a;
            pOut1.Coins = 100;
            pOut1.Hours = 150;
            err = SKY_coin_Transaction_GetOutputAt (tx, 0, pOut);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (pOut.isEqual (pOut1), 1);
            for (int i = 1; i < 20; i++) {
                a = transutils.makeAddress ();
                err = SKY_coin_Transaction_PushOutput (tx, a, (ulong) (i * 100), (ulong) (i * 50));
                Assert.AreEqual (err, SKY_OK);
                count = new_Gointp ();
                err = SKY_coin_Transaction_GetOutputsCount (tx, count);
                Assert.AreEqual (err, SKY_OK);
                Assert.AreEqual (Gointp_value (count), (i + 1));
                pOut1 = new coin__TransactionOutput ();
                pOut = new coin__TransactionOutput ();
                pOut1.Address = a;
                pOut1.Coins = (ulong) (i * 100);
                pOut1.Hours = (ulong) (i * 50);
                err = SKY_coin_Transaction_GetOutputAt (tx, i, pOut);
                Assert.AreEqual (err, SKY_OK);
                Assert.AreEqual (pOut.isEqual (pOut1), 1);
            }
        }

        [Test]
        public void TestTransactionSignInputs () {
            var handle = transutils.makeEmptyTransaction ();
            //  Panics if txns already signed
            var sig = new cipher_Sig ();
            var err = SKY_coin_Transaction_PushSignature (handle, sig);
            Assert.AreEqual (err, SKY_OK);
            var seckeys = new cipher_SecKeys ();
            seckeys.allocate (1);
            seckeys.setAt (0, new cipher_SecKey ());
            // Panics if not enough keys
            handle = transutils.makeEmptyTransaction ();
            var s = new cipher_SecKey ();
            var s2 = new cipher_SecKey ();
            var ux = new coin__UxOut ();
            var ux2 = new coin__UxOut ();
            makeUxOutWithSecret (ux, s);
            var h = new cipher_SHA256 ();
            err = SKY_coin_UxOut_Hash (ux, h);
            Assert.AreEqual (err, SKY_OK);
            var r = new_GoUint16p ();
            err = SKY_coin_Transaction_PushInput (handle, h, r);
            Assert.AreEqual (err, SKY_OK);
            makeUxOutWithSecret (ux2, s2);
            err = SKY_coin_UxOut_Hash (ux2, h);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushInput (handle, h, r);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), 40, 80);
            Assert.AreEqual (err, SKY_OK);
            var count = new_Gointp ();
            err = SKY_coin_Transaction_GetSignaturesCount (handle, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (count), 0);
            // Valid signing
            h = new cipher_SHA256 ();
            SKY_coin_Transaction_HashInner (handle, h);
            seckeys = new cipher_SecKeys ();
            seckeys.allocate (2);
            seckeys.setAt (0, s);
            seckeys.setAt (1, s2);
            err = SKY_coin_Transaction_SignInputs (handle, seckeys);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_GetSignaturesCount (handle, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (count), 2);
            var h2 = new cipher_SHA256 ();
            err = SKY_coin_Transaction_HashInner (handle, h2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (h2), 1);
            var p = new cipher_PubKey ();
            err = SKY_cipher_PubKeyFromSecKey (s, p);
            Assert.AreEqual (err, SKY_OK);
            var a = new cipher__Address ();
            var a2 = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_PubKeyFromSecKey (s2, p);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey (p, a2);
            Assert.AreEqual (err, SKY_OK);
            var sha1 = new cipher_SHA256 ();
            var sha2 = new cipher_SHA256 ();
            var txin0 = new cipher_SHA256 ();
            var txin1 = new cipher_SHA256 ();
            err = SKY_coin_Transaction_GetInputAt (handle, 0, txin0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_GetInputAt (handle, 1, txin1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddSHA256 (h, txin0, sha1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddSHA256 (h, txin1, sha2);
            Assert.AreEqual (err, SKY_OK);
            var txsig0 = new cipher_Sig ();
            var txsig1 = new cipher_Sig ();
            err = SKY_coin_Transaction_GetSignatureAt (handle, 0, txsig0);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_GetSignatureAt (handle, 1, txsig1);
            Assert.AreEqual (err, SKY_OK);
        }

        [Test]
        public void TestTransactionHash () {
            var handle = new_Transaction__Handlep ();
            makeTransaction (handle);
            var h = new cipher_SHA256 ();
            var h2 = new cipher_SHA256 ();
            var err = SKY_coin_Transaction_Hash (handle, h);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (h2), 0);
            err = SKY_coin_Transaction_HashInner (handle, h2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (h2), 0);
        }

        [Test]
        public void TestTransactionUpdateHeader () {
            var handle = new_Transaction__Handlep ();
            var tx = makeTransaction (handle);
            var h = new cipher_SHA256 ();
            var h1 = new cipher_SHA256 ();
            var h2 = new cipher_SHA256 ();
            var err = SKY_coin_Transaction_HashInner (handle, h);
            Assert.AreEqual (err, SKY_OK);
            tx.setInnerHash (new cipher_SHA256 ());
            err = SKY_coin_Transaction_UpdateHeader (handle);
            Assert.AreEqual (err, SKY_OK);
            var arg = new cipher_SHA256 ();
            arg = tx.GetInnerHash ();
            h1.assignFrom (arg);
            err = SKY_coin_Transaction_HashInner (handle, h2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h1.isEqual (new cipher_SHA256 ()), 0);
            Assert.AreEqual (h1.isEqual (h), 1);
            Assert.AreEqual (h1.isEqual (h2), 1);
        }

        [Test]
        public void TestTransactionHashInner () {
            var handle = new_Transaction__Handlep ();
            makeTransaction (handle);
            var h = new cipher_SHA256 ();
            var err = SKY_coin_Transaction_HashInner (handle, h);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (new cipher_SHA256 ()), 0);

            // If tx.In is changed, hash should change
            var handle2 = transutils.copyTransaction (handle);
            Assert.AreEqual (err, SKY_OK);
            var ux = new coin__UxOut ();
            makeUxOut (ux);
            h = new cipher_SHA256 ();
            var h1 = new cipher_SHA256 ();
            err = SKY_coin_UxOut_Hash (ux, h);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_SetInputAt (handle2, 0, h);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_UxOut_Hash (ux, h1);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (h1), 1);
            err = SKY_coin_Transaction_HashInner (handle, h);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_HashInner (handle2, h1);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (h.isEqual (h1), 0);

            // If tx.Out is changed, hash should change
            handle2 = transutils.copyTransaction (handle);
            var a = transutils.makeAddress ();
            var pOut = new coin__TransactionOutput ();
            err = SKY_coin_Transaction_GetOutputAt (handle2, 0, pOut);
            Assert.AreEqual (err, SKY_OK);
            pOut.Address = a;
            err = SKY_coin_Transaction_SetOutputAt (handle2, 0, pOut);
            Assert.AreEqual (err, SKY_OK);
            var sha1 = new cipher_SHA256 ();
            var sha2 = new cipher_SHA256 ();
            err = SKY_coin_Transaction_HashInner (handle, sha1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_HashInner (handle2, sha2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (sha1.isEqual (sha2), 0);

            // If tx.Head is changed, hash should not change
            handle2 = transutils.copyTransaction (handle);
            var sig = new cipher_Sig ();
            err = SKY_coin_Transaction_PushSignature (handle, sig);
            Assert.AreEqual (err, SKY_OK);
            sha1 = new cipher_SHA256 ();
            sha2 = new cipher_SHA256 ();
            err = SKY_coin_Transaction_HashInner (handle, sha1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_HashInner (handle2, sha2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (sha1.isEqual (sha2), 1);
        }

        [Test]
        public void TestTransactionSerialization () {
            var handle = new_Transaction__Handlep ();
            var tx = makeTransaction (handle);
            var b = new GoSlice ();
            var err = SKY_coin_Transaction_Serialize (handle, b);
            Assert.AreEqual (err, SKY_OK);
            var handle2 = new_Transaction__Handlep ();
            err = SKY_coin_TransactionDeserialize (b, handle2);
            Assert.AreEqual (err, SKY_OK);
            var tx2 = new coin__Transaction ();
            err = SKY_coin_GetTransactionObject (handle2, tx2);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (tx.isEqual (tx2), 0);
        }

        [Test]
        public void TestTransactionOutputHours () {
            var handle = transutils.makeEmptyTransaction ();
            var err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), (ulong) 1e6, 100);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), (ulong) 1e6, 200);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), (ulong) 1e6, 500);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), (ulong) 1e6, 0);
            Assert.AreEqual (err, SKY_OK);
            var hours = new_GoUint64p ();
            err = SKY_coin_Transaction_OutputHours (handle, hours);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint64p_value (hours), 800);
            err = SKY_coin_Transaction_PushOutput (handle, transutils.makeAddress (), (ulong) 1e6, ulong.MaxValue - 700);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transaction_OutputHours (handle, hours);
            Assert.AreEqual (err, SKY_ERROR);
        }

        [Test]
        public void TestTransactionsSize () {
            var handle = new_Transactions__HandlePtr ();
            var err = (uint) makeTransactions (10, handle);
            var size = (long) 0;
            for (int i = 0; i < 10; i++) {
                var tx = transutils.makeEmptyTransaction ();
                err = SKY_coin_Transactions_GetAt (handle, i, tx);
                Assert.AreEqual (err, SKY_OK);
                var b = new GoSlice ();
                err = SKY_coin_Transaction_Serialize (tx, b);
                Assert.AreEqual (err, SKY_OK);
                size += b.len;
            }
            Assert.AreNotEqual (size, 0);
            var sizetx = new_GoUint32Ptr ();
            err = SKY_coin_Transactions_Size (handle, sizetx);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (sizetx), size);
        }

        [Test]
        public void TestTransactionsHashes () {
            var handle = new_Transactions__HandlePtr ();
            makeTransactions (4, handle);
            var hashes = new cipher_SHA256s ();
            var err = SKY_coin_Transactions_Hashes (handle, hashes);
            Assert.AreEqual (err, SKY_OK);
            var len_hashes = hashes.count;
            Assert.AreEqual (len_hashes, 4);
            for (int i = 0; i < len_hashes; i++) {
                var tx = new_Transaction__Handlep ();
                err = SKY_coin_Transactions_GetAt (handle, i, tx);
                Assert.AreEqual (err, SKY_OK);
                var h = new cipher_SHA256 ();
                err = SKY_coin_Transaction_Hash (tx, h);
                Assert.AreEqual (err, SKY_OK);
                Assert.AreEqual (h.isEqual (hashes.getAt (i)), 1);
            }
        }

        [Test]
        public void TestTransactionsTruncateBytesTo () {
            var handles = new_Transactions__HandlePtr ();
            var err = (uint) makeTransactions (10, handles);
            var trunc = (uint) 0;
            var count = new_GoUint32Ptr ();
            var len_tnxs = new_Gointp ();
            err = SKY_coin_Transactions_Length (handles, len_tnxs);
            long len_tnxs_value = Gointp_value (len_tnxs);
            for (long i = 0; i < (len_tnxs_value / 2); i++) {
                count = new_GoUint32Ptr ();
                var handle = new_Transaction__Handlep ();
                err = SKY_coin_Transactions_GetAt (handles, (long) i, handle);
                Assert.AreEqual (err, SKY_OK);
                err = SKY_coin_Transaction_Size (handle, count);
                Assert.AreEqual (err, SKY_OK);
                trunc += GoUint32Ptr_value (count);
            }
            // Trucating halfway
            var tnxs2 = new_Transactions__HandlePtr ();
            err = SKY_coin_Transactions_TruncateBytesTo (handles, trunc, tnxs2);
            Assert.AreEqual (err, SKY_OK);
            var len_tnxs2 = new_Gointp ();

            err = SKY_coin_Transactions_Length (tnxs2, len_tnxs2);
            Assert.AreEqual (err, SKY_OK);

            Assert.AreEqual (Gointp_value (len_tnxs2), len_tnxs_value / 2);
            count = new_GoUint32Ptr ();
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (count), trunc);

            // Stepping into next boundary has same cutoff, must exceed
            trunc += 1;
            err = SKY_coin_Transactions_TruncateBytesTo (handles, trunc, tnxs2);
            Assert.AreEqual (err, SKY_OK);
            var len = new_Gointp ();
            err = SKY_coin_Transactions_Length (tnxs2, len);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (len), len_tnxs_value / 2);
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (count), trunc - 1);

            // Moving to 1 before next level
            var tnxs_5 = transutils.makeEmptyTransaction ();
            err = SKY_coin_Transactions_GetAt (handles, 5, tnxs_5);
            Assert.AreEqual (err, SKY_OK);
            count = new_GoUint32Ptr ();
            err = SKY_coin_Transaction_Size (tnxs_5, count);
            Assert.AreEqual (err, SKY_OK);
            trunc += (GoUint32Ptr_value (count) - 2);
            err = SKY_coin_Transactions_TruncateBytesTo (handles, trunc, tnxs2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_Length (tnxs2, len);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (len), 5);
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            var count_tnxs5 = new_GoUint32Ptr ();
            err = SKY_coin_Transaction_Size (tnxs_5, count_tnxs5);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual ((trunc - GoUint32Ptr_value (count_tnxs5) + 1), GoUint32Ptr_value (count));

            // Moving to next level
            trunc += 1;
            err = SKY_coin_Transactions_TruncateBytesTo (handles, trunc, tnxs2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_Length (tnxs2, len);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (len), 6);
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (count), trunc);

            // Truncating to full available amt
            var trunc1 = new_GoUint32Ptr ();
            err = SKY_coin_Transactions_Size (handles, trunc1);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_TruncateBytesTo (handles, GoUint32Ptr_value (trunc1), tnxs2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (count), GoUint32Ptr_value (trunc1));

            // Truncating to 0
            trunc = 0;
            err = SKY_coin_Transactions_TruncateBytesTo (handles, 0, tnxs2);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_Length (tnxs2, len);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (Gointp_value (len), 0);
            err = SKY_coin_Transactions_Size (tnxs2, count);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint32Ptr_value (count), trunc);
        }

        struct ux {
            public ulong coins;
            public ulong hours;
        }
        struct StrTest {
            public string name;
            public ux[] inUxs;
            public ux[] outUxs;
            public int err;
            public ulong headTime;
        }

        StrTest[] cases;
        public void FullCases () {
            cases = new StrTest[5];

            var c = new StrTest ();
            c.name = "Input coins overflow";
            c.err = SKY_ERROR;
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) (ulong.MaxValue - 1e6 + 1);
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 1e6;
            c.inUxs[1].hours = 0;
            c.outUxs = new ux[0];
            c.headTime = 0;
            cases[0] = c;

            c = new StrTest ();
            c.name = "Output coins overflow";
            c.err = SKY_ERROR;
            c.inUxs = new ux[1];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.outUxs = new ux[2];
            c.outUxs[0].coins = (ulong) (ulong.MaxValue - 10e6 + 1);
            c.outUxs[0].coins = 0;
            c.outUxs[1].coins = (ulong) 20e6;
            c.outUxs[1].hours = 1;
            c.headTime = 0;
            cases[1] = c;

            c = new StrTest ();
            c.name = "Insufficient coins";
            c.err = SKY_ERROR;
            // c.headTime = 0;
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[2];
            c.outUxs[0].coins = (ulong) 20e6;
            c.outUxs[0].coins = 1;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 1;
            cases[2] = c;

            c = new StrTest ();
            c.name = "Destroyed coins";
            c.err = SKY_ERROR;
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[2];
            c.outUxs[0].coins = (ulong) 5e6;
            c.outUxs[0].coins = 1;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 1;
            c.headTime = 0;
            cases[3] = c;

            c = new StrTest ();
            c.name = "valid";
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[3];
            c.outUxs[0].coins = (ulong) 10e6;
            c.outUxs[0].hours = 11;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 1;
            c.outUxs[2].coins = (ulong) 5e6;
            c.outUxs[2].hours = 0;
            c.headTime = 0;
            c.err = SKY_OK;
            cases[4] = c;

        }

        [Test]
        public void TestVerifyTransactionCoinsSpending () {
            FullCases ();
            for (int i = 0; i < cases.Length; i++) {
                var tc = cases[i];
                var uxIn = new coin_UxOutArray ();
                var uxOut = new coin_UxOutArray ();

                uxIn.allocate (tc.inUxs.Length);
                uxOut.allocate (tc.outUxs.Length);
                for (int j = 0; j < tc.inUxs.Length; j++) {
                    var ch = tc.inUxs[j];
                    var puxIn = new coin__UxOut ();
                    puxIn.Body.Coins = ch.coins;
                    puxIn.Body.Hours = ch.hours;
                    uxIn.setAt (j, puxIn);
                }
                for (int j = 0; j < tc.outUxs.Length; j++) {
                    var ch = tc.outUxs[j];
                    var puxOut = new coin__UxOut ();
                    puxOut.Body.Coins = ch.coins;
                    puxOut.Body.Hours = ch.hours;
                    uxOut.setAt (j, puxOut);
                }
                Assert.AreEqual (tc.inUxs.Length, uxIn.count);
                Assert.AreEqual (tc.outUxs.Length, uxOut.count);
                var err = SKY_coin_VerifyTransactionCoinsSpending (uxIn, uxOut);
                Assert.AreEqual (err, tc.err, "Iteration " + i.ToString () + tc.name);
            }
        }
        public void FullCases2 () {

            cases = new StrTest[7];
            var c = new StrTest ();
            c.name = "Input coins overflow";
            c.err = SKY_ERROR;
            c.inUxs = new ux[2];
            c.inUxs[0].hours = (ulong) (ulong.MaxValue - 1e6 + 1);
            c.inUxs[0].coins = (ulong) 3e6;
            c.inUxs[1].coins = (ulong) 1e6;
            c.inUxs[1].hours = (ulong) 1e6;
            c.outUxs = new ux[0];
            cases[0] = c;

            c = new StrTest ();
            c.name = "Insufficient coin hours";
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[2];
            c.outUxs[0].coins = (ulong) 15e6;
            c.outUxs[0].hours = 10;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 11;
            c.headTime = 0;
            c.err = SKY_ERROR;
            cases[1] = c;

            c = new StrTest ();
            c.name = "coin hours time calculation overflow";
            c.err = SKY_ERROR;
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[3];
            c.outUxs[0].coins = (ulong) 10e6;
            c.outUxs[0].hours = 11;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 1;
            c.outUxs[2].coins = (ulong) 5e6;
            c.outUxs[2].hours = 0;
            c.headTime = ulong.MaxValue;
            cases[2] = c;

            c = new StrTest ();
            c.name = "Invalid (coin hours overflow when adding earned hours, which is treated as 0, and now enough coin hours)";
            c.err = SKY_ERROR;
            c.inUxs = new ux[1];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = ulong.MaxValue;
            c.outUxs = new ux[1];
            c.outUxs[0].coins = (ulong) 10e6;
            c.outUxs[0].hours = 1;
            c.headTime = (long) 1e6;
            cases[3] = c;

            c = new StrTest ();
            c.name = "Valid (coin hours overflow when adding earned hours, which is treated as 0, but not sending any hours)";
            c.inUxs = new ux[1];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = ulong.MaxValue;
            c.outUxs = new ux[1];
            c.outUxs[0].coins = (ulong) 10e6;
            c.outUxs[0].hours = 0;
            c.headTime = (long) 1e6;
            cases[4] = c;

            c = new StrTest ();
            c.name = "Valid (base inputs have insufficient coin hours, but have sufficient after adjusting coinhours by headTime)";
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[2];
            c.outUxs[0].coins = (ulong) 15e6;
            c.outUxs[0].hours = 10;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 11;
            c.headTime = 1492707255;
            cases[5] = c;

            c = new StrTest ();
            c.name = "valid";
            c.inUxs = new ux[2];
            c.inUxs[0].coins = (ulong) 10e6;
            c.inUxs[0].hours = 10;
            c.inUxs[1].coins = (ulong) 15e6;
            c.inUxs[1].hours = 10;
            c.outUxs = new ux[3];
            c.outUxs[0].coins = (ulong) 10e6;
            c.outUxs[0].hours = 11;
            c.outUxs[1].coins = (ulong) 10e6;
            c.outUxs[1].hours = 1;
            c.outUxs[2].coins = (ulong) 5e6;
            c.outUxs[2].hours = 0;
            cases[6] = c;

        }

        [Test]
        public void TestVerifyTransactionHoursSpending () {
            FullCases2 ();

            for (int i = 0; i < cases.Length; i++) {
                var tc = cases[i];
                var uxIn = new coin_UxOutArray ();
                var uxOut = new coin_UxOutArray ();

                uxIn.allocate (tc.inUxs.Length);
                uxOut.allocate (tc.outUxs.Length);
                for (int j = 0; j < tc.inUxs.Length; j++) {
                    var ch = tc.inUxs[j];
                    var puxIn = new coin__UxOut ();
                    puxIn.Body.Coins = ch.coins;
                    puxIn.Body.Hours = ch.hours;
                    uxIn.setAt (j, puxIn);
                }
                for (int j = 0; j < tc.outUxs.Length; j++) {
                    var ch = tc.outUxs[j];
                    var puxOut = new coin__UxOut ();
                    puxOut.Body.Coins = ch.coins;
                    puxOut.Body.Hours = ch.hours;
                    uxOut.setAt (j, puxOut);
                }
                Assert.AreEqual (tc.inUxs.Length, uxIn.count);
                Assert.AreEqual (tc.outUxs.Length, uxOut.count);
                var err = SKY_coin_VerifyTransactionHoursSpending (tc.headTime, uxIn, uxOut);
                Assert.AreEqual (err, tc.err);
            }
        }

        [Test]
        public void TestTransactionsFees () {
            var txns = new_Transactions__HandlePtr ();
            SKY_coin_Create_Transactions (txns);

            // Nil txns
            var fee = new_GoUint64p ();
            var err = SKY_coin_Transactions_Fees (txns, transutils.calc, fee);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint64p_value (fee), 0);

            var txn = new_Transaction__Handlep ();
            SKY_coin_Create_Transaction (txn);
            err = SKY_coin_Transactions_Add (txns, txn);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_Transactions_Add (txns, txn);
            Assert.AreEqual (err, SKY_OK);

            // 2 transactions, calc() always returns 1
            fee = new_GoUint64p ();
            err = SKY_coin_Transactions_Fees (txns, transutils.calc, fee);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint64p_value (fee), 2);

            // calc error
            fee = new_GoUint64p ();
            err = SKY_coin_Transactions_Fees (txns, transutils.badCalc, fee);
            Assert.AreEqual (err, SKY_ERROR);

            // summing of calculated fees overflows
            fee = new_GoUint64p ();
            err = SKY_coin_Transactions_Fees (txns, transutils.overflow, fee);
            Assert.AreEqual (err, SKY_ERROR);
        }
    }

}