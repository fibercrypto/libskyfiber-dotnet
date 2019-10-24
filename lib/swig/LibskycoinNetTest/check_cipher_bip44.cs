using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest
{
    [TestFixture()]
    public class check_cipher_bip44 : skycoin.skycoin
    {
        public const uint CoinTypeBitcoin = 0;
        public const uint CoinTypeBitcoinTestnet = 1;
        public const uint CoinTypeSkycoin = 8000;
        public const uint ExternalChainIndex = 0;
        public const uint ChangeChainIndex = 1;
        public const uint FirstHardenedChild = 0x80000000;

        private GoSlice mustDefaultSeed()
        {
            var seed = new GoSlice();
            var mnemonic = "dizzy cigar grant ramp inmate uniform gold success able payment faith practice";
            var passphrase = "";
            var err = SKY_bip39_NewSeed(mnemonic, passphrase, seed);
            Assert.AreEqual(err, SKY_OK);
            var str = new _GoString_();
            err = SKY_base58_Hex2String(seed, str);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(str.p, "24e563fb095d766df3862c70432cc1b2210b24d232da69af7af09d2ec86d28782ce58035bae29994c84081836aebe36a9b46af1578262fefc53e37efbe94be57");
            return seed;
        }
        [Test()]
        public void TestNewCoin()
        {
            // bad seed
            var tmp = new GoSlice();
            SKY_cipher_RandByte(3, tmp);
            var coin = new_Coin__HandlePtr();
            var err = SKY_bip44_NewCoin(tmp, CoinTypeBitcoin, coin);
            Assert.AreEqual(err, SKY_ErrInvalidSeedLength);

            // bad coin_type
            var Bad = mustDefaultSeed();
            err = SKY_bip44_NewCoin(Bad, FirstHardenedChild, coin);
            Assert.AreEqual(err, SKY_ErrInvalidCoinType);
            err = SKY_bip44_NewCoin(Bad, FirstHardenedChild + 1, coin);
            Assert.AreEqual(err, SKY_ErrInvalidCoinType);

            var c = new_Coin__HandlePtr();
            err = SKY_bip44_NewCoin(Bad, CoinTypeBitcoin, c);
            Assert.AreEqual(err, SKY_OK);

            var account = new_Account__HandlePtr();
            err = SKY_bip44_Coin_Account(c, 0, account);
            Assert.AreEqual(err, SKY_OK);
            var acc_string = new _GoString_();
            err = SKY_bip44_Account_String(account, acc_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(acc_string.p, "xprv9yKAFQtFghZSe4mfdpdqFm1WWmGeQbYMB4MSGUB85zbKGQgSxty4duZb8k6hNoHVd2UR7Y3QhWU3rS9wox9ewgVG7gDLyYTL4yzEuqUCjvF");
            var privk = new_PrivateKey__HandlePtr();
            var pubk = new_PublicKey__HandlePtr();
        }
    }
}