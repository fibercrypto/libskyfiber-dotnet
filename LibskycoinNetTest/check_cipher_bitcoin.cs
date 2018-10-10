using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_bitcoin {
        utils.transutils utils = new transutils ();

        [Test]
        public void TestDecodeBase58BitcoinAddress () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var a = new cipher__BitcoinAddress ();
            var addrTmp = new cipher__BitcoinAddress ();

            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            skycoin.skycoin.SKY_cipher_BitcoinAddressFromPubKey (p, a);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress ("", addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58String);

            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress ("cascs", addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength);

            var b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            b.len = b.len / 2;
            var h = new _GoString_ ();
            skycoin.skycoin.SKY_base58_Hex2Base58Str (b, h);
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (h.p, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength);

            b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            h = new _GoString_ ();
            skycoin.skycoin.SKY_base58_Hex2Base58Str (b, h);
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (h.p, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (a.isEqual (addrTmp), 1);

            var As = new _GoString_ ();
            skycoin.skycoin.SKY_cipher_BitcoinAddress_String (a, As);
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (As.p, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (a.isEqual (addrTmp), 1);

            // preceding whitespace is invalid
            var as2 = " " + As.p;
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (as2, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char);

            // preceding zeroes are invalid
            as2 = "000" + As.p;
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (as2, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char);

            // trailing whitespace is invalid
            as2 = As.p + " ";
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (as2, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char);

            // trailing zeroes are invalid
            as2 = As.p + "000";
            err = skycoin.skycoin.SKY_cipher_DecodeBase58BitcoinAddress (as2, addrTmp);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char);

        }

        [Test]
        public void TestBitcoinAddressFromBytes () {
            var s = new skycoin.cipher_SecKey ();
            var p = new skycoin.cipher_PubKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_GenerateKeyPair");
            var a = new skycoin.cipher__BitcoinAddress ();
            skycoin.skycoin.SKY_cipher_BitcoinAddressFromPubKey (p, a);
            var pk = new GoSlice ();
            var b = new GoSlice ();
            var a2 = new skycoin.cipher__BitcoinAddress ();
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (b, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_BitcoinAddressFromBytes");
            Assert.AreEqual (a2.isEqual (a), 1);

            // Invalid number of bytes
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            b.len = b.len - 2;
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (b, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength);

            // Invalid checksum
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            b.setAtChar ('2', (ulong) b.len - 1);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (b, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidChecksum);

            a.Version = 2;
            skycoin.skycoin.SKY_cipher_BitcoinAddress_Bytes (a, b);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (b, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidVersion);
        }

        [Test]
        public void TestBitcoinAddressFromSecKey () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var a = new cipher__BitcoinAddress ();
            var a2 = new cipher__BitcoinAddress ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            var err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromSecKey (s, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // Valid pubkey+address
            err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromSecKey (new cipher_SecKey (), a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrPubKeyFromNullSecKey);
        }

        [Test]
        public void TestBitcoinAddressNull () {
            var a = new cipher__BitcoinAddress ();
            Assert.IsTrue (Convert.ToBoolean (skycoin.skycoin.SKY_cipher_BitcoinAddress_Null (a)));

            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            skycoin.skycoin.SKY_cipher_BitcoinAddressFromPubKey (p, a);
            Assert.IsFalse (Convert.ToBoolean (skycoin.skycoin.SKY_cipher_BitcoinAddress_Null (a)));
        }

        [Test]
        public void TestBitcoinAddressVerify () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var a = new cipher__BitcoinAddress ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            skycoin.skycoin.SKY_cipher_BitcoinAddressFromPubKey (p, a);

            // Valid pubkey+address
            var err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            // Invalid pubkey
            err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, new cipher_PubKey ());
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidPubKey);
            var p2 = new cipher_PubKey ();
            var s2 = new cipher_SecKey ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p2, s2);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, p2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidPubKey);

            // Bad Version
            a.Version = 0x01;
            err = skycoin.skycoin.SKY_cipher_BitcoinAddress_Verify (a, p2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidVersion);
        }

        [Test]
        public void TestBitcoinWIPRoundTrio () {
            var p = new cipher_PubKey ();
            var seckey1 = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, seckey1);
            var wip1 = new _GoString_ ();
            skycoin.skycoin.SKY_cipher_BitcoinWalletImportFormatFromSeckey (seckey1, wip1);
            var seckey2 = new skycoin.cipher_SecKey ();
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKeyFromBitcoinWalletImportFormat (wip1.p, seckey2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var wip2 = new _GoString_ ();
            skycoin.skycoin.SKY_cipher_BitcoinWalletImportFormatFromSeckey (seckey2, wip2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (seckey1.isEqual (seckey2), 1);
            var seckey1_hex = new _GoString_ ();
            var seckey2_hex = new _GoString_ ();
            err = skycoin.skycoin.SKY_cipher_SecKey_Hex (seckey1, seckey1_hex);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Hex (seckey2, seckey2_hex);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (seckey1_hex.p == seckey2_hex.p, true);
            Assert.AreEqual (wip1.p == wip2.p, true);
        }
    }
}