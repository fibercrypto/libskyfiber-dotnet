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
            err = SKY_bip44_Account_GetPrivateKey(account, privk);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_Publickey(privk, pubk);
            Assert.AreEqual(err, SKY_OK);
            var pubk_string = new _GoString_();
            err = SKY_bip32_PublicKey_String(pubk, pubk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(pubk_string.p, "xpub6CJWevR9X57jrYr8jrAqctxF4o78p4GCYHH34rajeL8J9D1bWSHKBht4yzwiTQ4FP4HyQpx99iLxvU54rbEbcxBUgxzTGGudBVXb1N2gcHF");

            err = SKY_bip44_Coin_Account(c, 1, account);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip44_Account_String(account, acc_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(acc_string.p, "xprv9yKAFQtFghZSgShGXkxHsYQfFaqMyutf3izng8tV4Tmp7gidQUPB8kCuv66yukidivM2oSaUvGus8ffnYvYKChB7DME2H2AvUq8LM2rXUzF");
            privk = new_PrivateKey__HandlePtr();
            pubk = new_PublicKey__HandlePtr();
            err = SKY_bip44_Account_GetPrivateKey(account, privk);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_Publickey(privk, pubk);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_String(pubk, pubk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(pubk_string.p, "xpub6CJWevR9X57jtvmjdnVJEgMPocfrPNcWQwvPUXJ6coJnzV3mx1hRgYXPmQJh5vLQvrVCY8LtJB5xLLiPJVmpSwBe2yhonQLoQuSsCF8YPLN");

            var account_temp = new_Account__HandlePtr();
            err = SKY_bip44_Coin_Account(c, 0x80000000, account_temp);
            Assert.AreEqual(err, SKY_ErrInvalidAccount);
            err = SKY_bip44_Coin_Account(c, 0x80000001, account_temp);
            Assert.AreEqual(err, SKY_ErrInvalidAccount);

            var external = new_PrivateKey__HandlePtr();
            err = SKY_bip44_Account_External(account, external);
            Assert.AreEqual(err, SKY_OK);
            var privk_string = new _GoString_();
            err = SKY_bip32_PrivateKey_String(external, privk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(privk_string.p, "xprv9zjsvjLiqSerDzbeRXPeXwz8tuQ7eRUABkgFAgLPHw1KzGKkgBhJhGaMYHM8j2KDXBZTCv4m19qjxrrD7gusrtdpZ7xzJywdXHaMZEjf3Uv");
            pubk = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_Publickey(external, pubk);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_String(pubk, pubk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(pubk_string.p, "xpub6DjELEscfpD9SUg7XYveu5vsSwEc3tC1Yybqy4jzrGYJs4euDj1ZF4tqPZYvViMn9cvBobHyubuuh69PZ1szaBBx5oxPiQzD492B6C4QDHe");

            var external0 = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_NewPublicChildKey(external, 0, external0);
            Assert.AreEqual(err, SKY_OK);
            var Key = new GoSlice();
            var KeyStr = new _GoString_();
            err = SKY_bip32_PublicKey_GetKey(external0, Key);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_base58_Hex2String(Key, KeyStr);
            Assert.AreEqual(KeyStr.p, "034d36f3bcd74e19204e75b81b9c0726e41b799858b92bab73f4cd7498308c5c8b");

            var external1 = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_NewPublicChildKey(external, 1, external1);
            Assert.AreEqual(err, SKY_OK);
            var Key1 = new GoSlice();
            var Key1Str = new _GoString_();
            err = SKY_bip32_PublicKey_GetKey(external1, Key1);
            Assert.AreEqual(err, SKY_OK);
            SKY_base58_Hex2String(Key1, Key1Str);
            Assert.AreEqual(Key1Str.p, "02f7309e9f559d847ee9cc9ee144cfa490791e33e908fdbde2dade50a389408b01");

            var change = new_PrivateKey__HandlePtr();
            err = SKY_bip44_Account_Change(account, change);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_String(change, privk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(privk_string.p, "xprv9zjsvjLiqSerGzJyBrpZgCaGpQCeFDnZEuAV714WigmFyHT4nFLhZLeuHzLNE19PgkZeQ5Uf2pjFZjQTHbkugDbmw5TAPAvgo2jsaTnZo2A");
            pubk = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_Publickey(change, pubk);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_String(pubk, pubk_string);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(pubk_string.p, "xpub6DjELEscfpD9VUPSHtMa3LX1NS38egWQc865uPU8H2JEr5nDKnex78yP9GxhFr5cnCRgiQF1dkv7aR7moraPrv73KHwSkDaXdWookR1Sh9p");

            var change0 = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_NewPublicChildKey(change, 0, change0);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetKey(change0, Key);
            Assert.AreEqual(err, SKY_OK);
            SKY_base58_Hex2String(Key, KeyStr);
            Assert.AreEqual(KeyStr.p, "026d3eb891e81ecabedfa8560166af383457aedaf172af9d57d00508faa5f57c4c");

            var change1 = new_PublicKey__HandlePtr();
            err = SKY_bip32_PrivateKey_NewPublicChildKey(change, 1, change1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetKey(change1, Key1);
            Assert.AreEqual(err, SKY_OK);
            SKY_base58_Hex2String(Key1, Key1Str);
            Assert.AreEqual(Key1Str.p, "02681b301293fdf0292cd679b37d60b92a71b389fd994b2b57c8daf99532bfb4a5");
        }
    }
}