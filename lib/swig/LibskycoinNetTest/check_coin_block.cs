using System;
using NUnit.Framework;
using skycoin;
using utils;

namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_block : skycoin.skycoin {

        utils.transutils transutils = new utils.transutils ();

        public SWIGTYPE_p_Transactions__Handle makeTestTransactions () {
            var txns = new_Transactions__HandlePtr();
            var err = SKY_coin_Create_Transactions (txns);
            Assert.AreEqual (err, SKY_OK);
            var txn = transutils.makeEmptyTransaction ();
            err = SKY_coin_Transactions_Add (txns, txn);
            Assert.AreEqual (err, SKY_OK);
            return txns;
        }
        public uint makeNewBlock (cipher_SHA256 uxHash, SWIGTYPE_p_Block__Handle newBlock) {
            var bodyhash = new cipher_SHA256 ();
            var block = new_Block__HandlePtr ();
            var transactions = makeTestTransactions ();
            var err = SKY_coin_NewEmptyBlock (transactions, block);
            Assert.AreEqual (err, SKY_OK);

            var pBlockP = new_coin__BlockPtr ();
            err = SKY_coin_GetBlockObject (block, pBlockP);
            Assert.AreEqual (err, SKY_OK);
            var pBlock = coin__BlockPtr_value (pBlockP);
            pBlock.Head.Version = 0x02;
            pBlock.Head.Time = 100;
            pBlock.Head.BkSeq = 0;
            pBlock.Head.Fee = 10;
            var body = new_BlockBody__HandlePtr ();
            err = SKY_coin_GetBlockBody (block, body);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_BlockBody_Hash (body, bodyhash);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_coin_NewBlock (block, 100 + 200, uxHash, transactions, transutils.feeCalc, newBlock);
            Assert.AreEqual (err, SKY_OK);
            return err;
        }

        [Test]
        public void TestNewBlock () {
            var txns = transutils.makeTransactions (1);
            var block = new_Block__HandlePtr ();
            var err = SKY_coin_NewEmptyBlock (txns, block);
            Assert.AreEqual (err, SKY_OK);
            var pBlockP = new_coin__BlockPtr ();
            err = SKY_coin_GetBlockObject (block, pBlockP);
            Assert.AreEqual (err, SKY_OK);
            var pBlock = coin__BlockPtr_value (pBlockP);
            pBlock.Head.Version = 0x02;
            pBlock.Head.Time = 100;
            pBlock.Head.BkSeq = 98;
            var uxHash = transutils.RandSHA256 ();
            // invalid txn fees panics
            err = SKY_coin_NewBlock (block, 133, uxHash, txns, transutils.badCalc, block);
            Assert.AreEqual (err, SKY_ERROR);
            // no txns panics
            var tnxs1 = new_Transactions__HandlePtr ();
            SKY_coin_Create_Transactions (tnxs1);
            err = SKY_coin_NewBlock (block, 133, uxHash, tnxs1, transutils.feeCalc, block);
            Assert.AreEqual (err, SKY_ERROR);

            // Valid block in fine
            ulong fee = 121;
            ulong currentTime = 133;
            var b = new_Block__HandlePtr ();
            err = SKY_coin_NewBlock (block, currentTime, uxHash, txns, transutils.fix121, b);
            Assert.AreEqual (err, SKY_OK);

            var pBlock2P = new_coin__BlockPtr ();
            err = SKY_coin_GetBlockObject (b, pBlock2P);
            var pBlock2 = coin__BlockPtr_value (pBlock2P);
            Assert.AreEqual (pBlock2.Head.Fee, fee);
            var hashs = GoUint8Ptr_value (pBlock2.Head.PrevHash);
            var prevHash = new cipher_SHA256 ();
            var prevHashP = new cipher_SHA256 ();
            SKY_coin_Block_PreHashHeader (block, prevHash);
            SKY_coin_Block_HashHeader (b, prevHashP);
            Assert.AreEqual (prevHash.isEqual (prevHashP), 0);
            Assert.AreEqual (pBlock2.Head.Time, currentTime);
            Assert.AreEqual (pBlock2.Head.BkSeq, pBlock.Head.BkSeq + 1);
            var pUxHash = new cipher_SHA256 ();
            convertGoUint8toSHA256 (pBlock2.Head.UxHash, pUxHash);
            Assert.AreEqual (uxHash.isEqual (pUxHash), 1);
        }

        [Test]
        public void TestBlockHashHeader () {
            var uxHash = transutils.RandSHA256 ();
            var b = new_Block__HandlePtr ();
            var err = makeNewBlock (uxHash, b);
            Assert.AreEqual (err, SKY_OK);

            var sha1 = new cipher_SHA256 ();
            var sha2 = new cipher_SHA256 ();
            err = SKY_coin_Block_HashHeader (b, sha1);
            Assert.AreEqual (err, SKY_OK);

            var chP = new_coin__BlockPtr ();
            SKY_coin_GetBlockObject (b, chP);
            var ch = coin__BlockPtr_value (chP);
            SKY_coin_BlockHeader_Hash (ch.Head, sha2);
            Assert.AreEqual (sha1.isEqual (sha2), 1);
            Assert.AreEqual (sha1.isEqual (new cipher_SHA256 ()), 0);
        }

        [Test]
        public void TestBlockHashBody () {
            var uxHash = transutils.RandSHA256 ();
            var b = new_Block__HandlePtr ();
            var err = makeNewBlock (uxHash, b);
            Assert.AreEqual (err, SKY_OK);

            var sha1 = new cipher_SHA256 ();
            var sha2 = new cipher_SHA256 ();
            SKY_coin_Block_HashBody (b, sha1);
            var body = new_BlockBody__HandlePtr ();
            SKY_coin_GetBlockBody (b, body);
            SKY_coin_BlockBody_Hash (body, sha2);
            Assert.AreEqual (sha1.isEqual (sha2), 1);
            var hb = new cipher_SHA256 ();
            SKY_coin_Block_HashBody (b, hb);
            var bh = new cipher_SHA256 ();
            SKY_coin_BlockBody_Hash (body, bh);
            Assert.AreEqual (bh.isEqual (hb), 1);
        }

        [Test]
        public void TestNewGenesisBlock () {
            var pubkey = new cipher_PubKey ();
            var seckey = new cipher_SecKey ();
            ulong genTime = 1000;
            ulong genCoins = (ulong) 1000e6;

            var err = SKY_cipher_GenerateKeyPair (pubkey, seckey);
            Assert.AreEqual (err, SKY_OK);
            var addr = new cipher__Address ();
            err = SKY_cipher_AddressFromPubKey (pubkey, addr);
            Assert.AreEqual (err, SKY_OK);
            var block = new_Block__HandlePtr ();
            err = SKY_coin_NewGenesisBlock (addr, genCoins, genTime, block);
            Assert.AreEqual (err, SKY_OK);
            var pBlockPP = new_coin__BlockPtr ();
            err = SKY_coin_GetBlockObject (block, pBlockPP);
            var pBlock = coin__BlockPtr_value (pBlockPP);
            Assert.AreEqual (err, SKY_OK);
            var nullHash = new cipher_SHA256 ();
            var Head = new GoSlice ();
            err = SKY_coin_BlockHeader_Bytes (pBlock.Head, Head);
            Assert.AreEqual (err, SKY_OK);
            var strnullHash = nullHash.getStr ();
            var strHead = new _GoString_ ();
            Head.getString (strHead);
            Assert.AreEqual (strnullHash.p, strHead.p);
            Assert.AreEqual (genTime, pBlock.Head.Time);
            Assert.AreEqual (0, pBlock.Head.BkSeq);
            Assert.AreEqual (0, pBlock.Head.Version);
            Assert.AreEqual (0, pBlock.Head.Fee);
        }

        struct testcase {
            public int index;
            public int failure;
        }

        [Test]
        public void TestCreateUnspent () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var a = new cipher__Address ();

            var err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            var h = new cipher_SHA256 ();
            var handle = new_Transaction__Handlep ();
            makeEmptyTransaction (handle);
            err = SKY_coin_Transaction_PushOutput (handle, a, 11000000, 255);
            Assert.AreEqual (err, SKY_OK);
            var bh = new coin__BlockHeader ();
            bh.Time = 0;
            bh.BkSeq = 1;
            testcase[] t = new testcase[2];
            var tc = new testcase ();
            tc.index = 0;
            tc.failure = SKY_OK;
            t[0] = tc;
            tc = new testcase ();
            tc.failure = SKY_ERROR;
            tc.index = 10;
            t[1] = tc;
            var ux = new coin__UxOut ();
            var tests_count = t.Length;
            for (int i = 0; i < tests_count; i++) {
                err = SKY_coin_CreateUnspent (bh, handle, t[i].index, ux);
                if (t[i].failure == SKY_ERROR) {
                    continue;
                }
                Assert.AreEqual (bh.Time, ux.Head.Time);
                Assert.AreEqual (bh.BkSeq, ux.Head.BkSeq);
            }
        }

        [Test]
        public void TestCreateUnspents () {
            var a = new cipher__Address ();
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();

            var err = SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, SKY_OK);
            var h = new cipher_SHA256 ();
            var txn = new_Transaction__Handlep ();
            makeEmptyTransaction (txn);
            err = SKY_coin_Transaction_PushOutput (txn, a, (ulong) 11e6, 255);
            Assert.AreEqual (err, SKY_OK);
            var bh = new coin__BlockHeader ();
            bh.Time = 0;
            bh.BkSeq = 1;
            var uxouts = new coin_UxOutArray ();
            err = SKY_coin_CreateUnspents (bh, txn, uxouts);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (uxouts.count, 1);
        }

    }

}