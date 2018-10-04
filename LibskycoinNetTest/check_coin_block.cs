using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_coin_block {

        utils.transutils transutils = new utils.transutils ();
        public SWIGTYPE_p_Transactions__Handle makeTestTransactions () {
            var txns = skycoin.skycoin.new_Transactions__Handlep ();
            var err = skycoin.skycoin.SKY_coin_Create_Transactions (txns);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var txn = transutils.makeEmptyTransaction ();
            err = skycoin.skycoin.SKY_coin_Transactions_Add (txns, txn);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            return txns;
        }

        [Test]
        public void TestNewBlock () {
            var txns = makeTestTransactions ();
            var block = skycoin.skycoin.new_Block__HandlePtr ();
            var err = skycoin.skycoin.SKY_coin_NewEmptyBlock (txns, block);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var pBlock = new coin__Block ();
            err = skycoin.skycoin.SKY_coin_GetBlockObject (block, pBlock);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            pBlock.Head.Version = 0x02;
            pBlock.Head.Time = 100;
            pBlock.Head.BkSeq = 98;
            var uxHash = transutils.RandSHA256 ();
            // TODO: Not complete by callback
        }

        [Test]
        public void TestBlockHashBody () {
            // TODO: not complete by callback
        }

        [Test]
        public void TestNewGenesisBlock () {
            var pubkey = new cipher_PubKey ();
            var seckey = new cipher_SecKey ();
            ulong genTime = 1000;
            ulong genCoins = (ulong) 1000e6;

            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pubkey, seckey);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var addr = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (pubkey, addr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var block = skycoin.skycoin.new_Block__HandlePtr ();
            err = skycoin.skycoin.SKY_coin_NewGenesisBlock (addr, genCoins, genTime, block);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var pBlock = new coin__Block ();
            err = skycoin.skycoin.SKY_coin_GetBlockObject (block, pBlock);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var nullHash = new cipher_SHA256 ();
            var Head = new GoSlice ();
            err = skycoin.skycoin.SKY_coin_BlockHeader_Bytes (pBlock.Head, Head);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.IsTrue (nullHash.getStr ().p == Head.getString ().p);
            // Assert.AreEqual (genTime, pBlock.Head.Time);
            Assert.AreEqual (0, pBlock.Head.BkSeq);
            Assert.AreEqual (0, pBlock.Head.Version);
            Assert.AreEqual (0, pBlock.Head.Fee);
        }

    }

}