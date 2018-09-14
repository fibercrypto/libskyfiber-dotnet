﻿using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_address {
        [Test ()]
        public void TestDecodeBase58Address () {
            uint err = 0;
            var s = new skycoin.cipher_SecKey ();
            var p = new skycoin.cipher_PubKey ();
            Assert.AreEqual (skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s), skycoin.skycoin.SKY_OK, "Failed SKY_cipher_GenerateKeyPair");
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_SecKey_Verify ");
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_PubKey_Verify");
            var a = new skycoin.cipher__Address ();
            var a1 = new skycoin.cipher__Address ();
            var a2 = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            err = skycoin.skycoin.SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, " Failed SKY_cipher_Address_Verify");
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address ("", a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58String, "Failed SKY_cipher_DecodeBase58Address 1");
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address ("asa", a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength, "Failed SKY_cipher_DecodeBase58Address 2");

            var addrStr = new skycoin._GoString_ ();
            err = skycoin.skycoin.SKY_cipher_Address_String (a, addrStr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (addrStr.p, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_DecodeBase58Address");
            Assert.IsTrue (a.isEqual (a2) == 1);
            var as2 = " " + addrStr.p;
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char, "Failed SKY_cipher_DecodeBase58Address 3");

            as2 = "000" + addrStr.p;
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char, "Failed SKY_cipher_DecodeBase58Address 3");

            as2 = addrStr.p + "000";
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (as2, a1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidBase58Char, "Failed SKY_cipher_DecodeBase58Address 3");
        }

        [Test]
        public void TestAddressFromBytes () {
            var s = new skycoin.cipher_SecKey ();
            var p = new skycoin.cipher_PubKey ();
            uint err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a = new skycoin.cipher__Address ();
            var a2 = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var b = new skycoin.GoSlice ();
            var b2 = new skycoin.GoSlice ();

            err = skycoin.skycoin.SKY_cipher_Address_Bytes (a, b);
            err = skycoin.skycoin.SKY_cipher_Address_Bytes (a, b2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_AddressFromBytes (b2, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (a2.isEqual (a), 1);
            // Invalid number of bytes
            long len_b = b.len;
            b.len = b.len - 2;
            err = skycoin.skycoin.SKY_cipher_AddressFromBytes (b, a2);
            Assert.AreEqual (skycoin.skycoin.SKY_ErrAddressInvalidLength, err, "Failed SKY_cipher_AddressFromBytes 2");
            b.len = len_b;
            a.setVersion ('2');
            err = skycoin.skycoin.SKY_cipher_Address_Bytes (a, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "SKY_cipher_Address_Bytes SKY_ErrAddressInvalidVersion");
            err = skycoin.skycoin.SKY_cipher_AddressFromBytes (b, a);
            Assert.AreEqual (skycoin.skycoin.SKY_ErrAddressInvalidVersion, err);
        }

        [Test]
        public void TestBitcoinAddressFromBytes () {
            var s = new skycoin.cipher_SecKey ();
            var p = new skycoin.cipher_PubKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_GenerateKeyPair");
            var a = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_AddressFromPubKey");
            var pk = new skycoin.GoSlice ();
            err = skycoin.skycoin.SKY_cipher_Address_BitcoinBytes (a, pk);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_Address_BitcoinBytes");
            var a2 = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (pk, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_BitcoinAddressFromBytes");
            Assert.AreEqual (a2.isEqual (a), 1);

            // Invalid number of bytes
            var b = new skycoin.GoSlice ();
            err = skycoin.skycoin.SKY_cipher_Address_BitcoinBytes (a, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "Failed SKY_cipher_Address_BitcoinBytes");
            var b_len = b.len;
            b.len = b.len - 2;
            var a3 = new skycoin.cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes (b, a3);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrAddressInvalidLength);

            a.setVersion ('2');
            err = skycoin.skycoin.SKY_cipher_Address_BitcoinBytes (a, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_BitcoinAddressFromBytes(b,a3);
            Assert.AreEqual(err,skycoin.skycoin.SKY_ErrAddressInvalidVersion);
        }
    }
}