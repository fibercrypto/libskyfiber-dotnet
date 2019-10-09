using System;
using NUnit.Framework;
using skycoin;
using utils;

namespace LibskycoinNetTest
{
    [TestFixture()]
    public class check_coin_block : skycoin.skycoin
    {

        utils.transutils transutils = new utils.transutils();

        public SWIGTYPE_p_Transactions__Handle makeTestTransactions()
        {
            var txns = new_Transactions__HandlePtr();
            var err = SKY_coin_Create_Transactions(txns);
            Assert.AreEqual(err, SKY_OK);
            var txn = transutils.makeEmptyTransaction();
            err = SKY_coin_Transactions_Add(txns, txn);
            Assert.AreEqual(err, SKY_OK);
            return txns;
        }
        public uint makeNewBlock(cipher_SHA256 uxHash, SWIGTYPE_p_Block__Handle newBlock)
        {
            var bodyhash = new cipher_SHA256();
            var block = new_Block__HandlePtr();
            var transactions = makeTestTransactions();
            var err = SKY_coin_NewEmptyBlock(transactions, block);
            Assert.AreEqual(err, SKY_OK);

            var pBlockHeader = new_BlockHeader__HandlePtr();
            err = SKY_coin_Block_GetBlockHeader(block, pBlockHeader);
            Assert.AreEqual(err, SKY_OK);

            SKY_coin_BlockHeader_SetTime(pBlockHeader, 100);
            SKY_coin_BlockHeader_SetBkSeq(pBlockHeader, 0);
            SKY_coin_BlockHeader_SetVersion(pBlockHeader, 0x02);
            SKY_coin_BlockHeader_SetFee(pBlockHeader, 10);

            var body = new_BlockBody__HandlePtr();
            err = SKY_coin_GetBlockBody(block, body);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_BlockBody_Hash(body, bodyhash);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_NewBlock(block, 100 + 200, uxHash, transactions, transutils.feeCalc, newBlock);
            Assert.AreEqual(err, SKY_OK);
            return err;
        }

        [Test]
        public void TestNewBlock()
        {
            var prevBlock = new_Block__HandlePtr();
            var newBlock = new_Block__HandlePtr();
            var pPrevBlockHeader = new_BlockHeader__HandlePtr();
            var txns = makeTestTransactions();
            var err = SKY_coin_NewEmptyBlock(txns, prevBlock);
            err = SKY_coin_Block_GetBlockHeader(prevBlock, pPrevBlockHeader);
            Assert.AreEqual(err, SKY_OK);

            SKY_coin_BlockHeader_SetTime(pPrevBlockHeader, 100);
            SKY_coin_BlockHeader_SetBkSeq(pPrevBlockHeader, 98);
            var uxHash = transutils.RandSHA256();
            // invalid txn fees panics
            txns = new_Transactions__HandlePtr();
            SKY_coin_Create_Transactions(txns);
            err = SKY_coin_NewBlock(prevBlock, 133, uxHash, txns, transutils.badCalc, newBlock);
            Assert.AreEqual(err, SKY_ERROR);
            // no txns panics
            txns = new_Transactions__HandlePtr();
            SKY_coin_Create_Transactions(txns);
            err = SKY_coin_NewBlock(prevBlock, 133, uxHash, txns, transutils.feeCalc, newBlock);
            Assert.AreEqual(err, SKY_ERROR);

            // Valid block in fine
            txns = new_Transactions__HandlePtr();
            err = SKY_coin_Create_Transactions(txns);
            Assert.AreEqual(err, SKY_OK);
            var tnx = transutils.makeEmptyTransaction();
            err = SKY_coin_Transactions_Add(txns, tnx);
            Assert.AreEqual(err, SKY_OK);

            ulong fee = 121;
            ulong currentTime = 133;
            newBlock = new_Block__HandlePtr();
            err = SKY_coin_NewBlock(prevBlock, currentTime, uxHash, txns, transutils.fix121, newBlock);
            Assert.AreEqual(err, SKY_OK);

            var pnewBlockHeader = new_BlockHeader__HandlePtr();
            err = SKY_coin_Block_GetBlockHeader(newBlock, pnewBlockHeader);
            var pHeadFee = new_GoUint64p();
            var pHeadTime = new_GoUint64p();
            var pHeadBkSeq = new_GoUint64p();
            SKY_coin_BlockHeader_Fee(pnewBlockHeader, pHeadFee);
            SKY_coin_BlockHeader_Time(pnewBlockHeader, pHeadTime);
            SKY_coin_BlockHeader_BkSeq(pnewBlockHeader, pHeadBkSeq);
            var pTransLength = new_Gointp();
            err = SKY_coin_Transactions_Length(txns, pTransLength);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(GoUint64p_value(pHeadFee), (fee * (ulong)Gointp_value(pTransLength)));
            Assert.AreEqual(GoUint64p_value(pHeadTime), currentTime);
            pPrevBlockHeader = new_BlockHeader__HandlePtr();
            err = SKY_coin_Block_GetBlockHeader(prevBlock, pPrevBlockHeader);
            Assert.AreEqual(err, SKY_OK);
            var pPrevBkSeq = new_GoUint64p();
            SKY_coin_BlockHeader_BkSeq(pPrevBlockHeader, pPrevBkSeq);
            var pnewuxHash = new cipher_SHA256();
            SKY_coin_BlockHeader_UxHash(pnewBlockHeader, pnewuxHash);
            Assert.AreEqual(pnewuxHash.isEqual(uxHash), 1);
        }

        [Test]
        public void TestBlockHashHeader()
        {
            var uxHash = transutils.RandSHA256();
            var b = new_Block__HandlePtr();
            var err = makeNewBlock(uxHash, b);
            Assert.AreEqual(err, SKY_OK);

            var sha1 = new cipher_SHA256();
            var sha2 = new cipher_SHA256();
            err = SKY_coin_Block_HashHeader(b, sha1);
            Assert.AreEqual(err, SKY_OK);
            var blockheader = new_BlockHeader__HandlePtr();
            err = SKY_coin_Block_GetBlockHeader(b, blockheader);
            err = SKY_coin_BlockHeader_Hash(blockheader, sha2);
            Assert.AreEqual(sha1.isEqual(sha2), 1);
            Assert.AreEqual(sha1.isEqual(new cipher_SHA256()), 0);
        }

        [Test]
        public void TestBlockHashBody()
        {
            var uxHash = transutils.RandSHA256();
            var b = new_Block__HandlePtr();
            var err = makeNewBlock(uxHash, b);
            Assert.AreEqual(err, SKY_OK);

            var sha1 = new cipher_SHA256();
            var sha2 = new cipher_SHA256();
            SKY_coin_Block_HashBody(b, sha1);
            var body = new_BlockBody__HandlePtr();
            SKY_coin_GetBlockBody(b, body);
            SKY_coin_BlockBody_Hash(body, sha2);
            Assert.AreEqual(sha1.isEqual(sha2), 1);
            var hb = new cipher_SHA256();
            SKY_coin_Block_HashBody(b, hb);
            var bh = new cipher_SHA256();
            SKY_coin_BlockBody_Hash(body, bh);
            Assert.AreEqual(bh.isEqual(hb), 1);
        }

        [Test]
        public void TestNewGenesisBlock()
        {
            var pubkey = new cipher_PubKey();
            var seckey = new cipher_SecKey();
            ulong genTime = 1000;
            ulong genCoins = (ulong)1000e6;

            var err = SKY_cipher_GenerateKeyPair(pubkey, seckey);
            Assert.AreEqual(err, SKY_OK);
            var addr = new cipher__Address();
            err = SKY_cipher_AddressFromPubKey(pubkey, addr);
            Assert.AreEqual(err, SKY_OK);
            var block = new_Block__HandlePtr();
            err = SKY_coin_NewGenesisBlock(addr, genCoins, genTime, block);
            Assert.AreEqual(err, SKY_OK);
            var pBlockPP = new_coin__BlockPtr();
            err = SKY_coin_GetBlockObject(block, pBlockPP);
            var pBlock = coin__BlockPtr_value(pBlockPP);
            Assert.AreEqual(err, SKY_OK);
            var nullHash = new cipher_SHA256();
            var Head = new GoSlice();
            err = SKY_coin_BlockHeader_Bytes(pBlock.Head, Head);
            Assert.AreEqual(err, SKY_OK);
            var strnullHash = nullHash.getStr();
            var strHead = new _GoString_();
            Head.getString(strHead);
            Assert.AreEqual(strnullHash.p, strHead.p);
            Assert.AreEqual(genTime, pBlock.Head.Time);
            Assert.AreEqual(0, pBlock.Head.BkSeq);
            Assert.AreEqual(0, pBlock.Head.Version);
            Assert.AreEqual(0, pBlock.Head.Fee);
        }

        struct testcase
        {
            public int index;
            public int failure;
        }

        [Test]
        public void TestCreateUnspent()
        {
            var p = new cipher_PubKey();
            var s = new cipher_SecKey();
            var a = new cipher__Address();

            var err = SKY_cipher_GenerateKeyPair(p, s);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey(p, a);
            Assert.AreEqual(err, SKY_OK);
            var h = new cipher_SHA256();
            var handle = transutils.makeEmptyTransaction();
            err = SKY_coin_Transaction_PushOutput(handle, a, 11000000, 255);
            Assert.AreEqual(err, SKY_OK);
            var bh = new coin__BlockHeader();
            bh.Time = 0;
            bh.BkSeq = 1;
            testcase[] t = new testcase[2];
            var tc = new testcase();
            tc.index = 0;
            tc.failure = SKY_OK;
            t[0] = tc;
            tc = new testcase();
            tc.failure = SKY_ERROR;
            tc.index = 10;
            t[1] = tc;
            var ux = new coin__UxOut();
            var tests_count = t.Length;
            for (int i = 0; i < tests_count; i++)
            {
                err = SKY_coin_CreateUnspent(bh, handle, t[i].index, ux);
                if (t[i].failure == SKY_ERROR)
                {
                    continue;
                }
                Assert.AreEqual(bh.Time, ux.Head.Time);
                Assert.AreEqual(bh.BkSeq, ux.Head.BkSeq);
            }
        }

        [Test]
        public void TestCreateUnspents()
        {
            var a = new cipher__Address();
            var p = new cipher_PubKey();
            var s = new cipher_SecKey();

            var err = SKY_cipher_GenerateKeyPair(p, s);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_cipher_AddressFromPubKey(p, a);
            Assert.AreEqual(err, SKY_OK);
            var h = new cipher_SHA256();
            var txn = transutils.makeEmptyTransaction();
            err = SKY_coin_Transaction_PushOutput(txn, a, (ulong)11e6, 255);
            Assert.AreEqual(err, SKY_OK);
            var bh = new coin__BlockHeader();
            bh.Time = 0;
            bh.BkSeq = 1;
            var uxouts = new coin_UxOutArray();
            err = SKY_coin_CreateUnspents(bh, txn, uxouts);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(uxouts.count, 1);
        }

    }

}