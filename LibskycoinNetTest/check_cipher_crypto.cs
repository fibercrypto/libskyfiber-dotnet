﻿using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_cryto {
        [Test]
        public void TestNewPubKey () {
            var b = new GoSlice ();
            var p = new cipher_PubKey ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (31, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (32, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (34, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (0, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (100, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);
            b = new GoSlice ();
            p = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthPubKey);

            var s = new cipher_SecKey ();
            var pTemp = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            pTemp = p.toSlice ();
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var p2 = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_NewPubKey (pTemp, p2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (p.isEqual (p2), 1);
        }

        [Test]
        public void TestPubKeyVerify () {
            // Random bytes should not be valid, most of the time
            var failed = false;
            for (int i = 0; i < 10; i++) {
                var b = new GoSlice ();
                var err = skycoin.skycoin.SKY_cipher_RandByte (33, b);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                var p = new cipher_PubKey ();
                p.assignSlice (b);
                err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
                if (err != skycoin.skycoin.SKY_OK) {
                    failed = true;
                    break;
                }
            }
            Assert.IsTrue (failed);
        }

        [Test]
        public void TestPubKeyVerifyNil () {
            // Empty public key should not be valid
            var p = new cipher_PubKey ();
            var err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestPubKeyVerifyDefault1 () {
            // Generated pub key should be valid
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestPubKeyVerifyDefault2 () {
            for (int i = 0; i < 1024; i++) {
                var p = new cipher_PubKey ();
                var s = new cipher_SecKey ();
                var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
                err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            }
        }

        [Test]
        public void TestPubKeyToAddress () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var addr = new cipher__Address ();
            var addr1 = new cipher__Address ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, addr);
            err = skycoin.skycoin.SKY_cipher_Address_Verify (addr, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var addrStr = new _GoString_ ();
            err = skycoin.skycoin.SKY_cipher_Address_String (addr, addrStr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (addrStr.p, addr1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestPubKeyToAddress2 () {
            for (int i = 0; i < 1024; i++) {
                var p = new cipher_PubKey ();
                var s = new cipher_SecKey ();
                var addr = new cipher__Address ();
                var addr1 = new cipher__Address ();
                var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
                err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, addr);
                err = skycoin.skycoin.SKY_cipher_Address_Verify (addr, p);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                var addrStr = new _GoString_ ();
                err = skycoin.skycoin.SKY_cipher_Address_String (addr, addrStr);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_cipher_DecodeBase58Address (addrStr.p, addr1);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            }
        }

        [Test]
        public void TestMustNewSecKey () {
            var b = new GoSlice ();
            var p = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (31, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSecKey);
            err = skycoin.skycoin.SKY_cipher_RandByte (32, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSecKey);
            err = skycoin.skycoin.SKY_cipher_RandByte (34, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSecKey);
            err = skycoin.skycoin.SKY_cipher_RandByte (0, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSecKey);
            err = skycoin.skycoin.SKY_cipher_RandByte (100, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSecKey);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (32, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var b1 = new GoSlice ();
            var p1 = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_RandByte (32, b1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b1, p1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestSecKeyVerify () {
            // Empty secret key should not be valid
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidSecKey);
            // Generated sec key should be valid
            var p = new cipher_PubKey ();
            s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.SKY_cipher_SecKey_Verify (s), skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestECDHonce () {
            var pub1 = new cipher_PubKey ();
            var pub2 = new cipher_PubKey ();
            var sec1 = new cipher_SecKey ();
            var sec2 = new cipher_SecKey ();
            var buf1 = new GoSlice ();
            var buf2 = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pub1, sec1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pub2, sec2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ECDH (pub2, sec1, buf1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ECDH (pub1, sec2, buf2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (buf1.isEqual (buf2), 1);
        }

        [Test]
        public void TestECDHloop () {
            for (int i = 0; i < 128; i++) {
                var pub1 = new cipher_PubKey ();
                var pub2 = new cipher_PubKey ();
                var sec1 = new cipher_SecKey ();
                var sec2 = new cipher_SecKey ();
                var buf1 = new GoSlice ();
                var buf2 = new GoSlice ();
                var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pub1, sec1);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pub2, sec2);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_cipher_ECDH (pub2, sec1, buf1);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                err = skycoin.skycoin.SKY_cipher_ECDH (pub1, sec2, buf2);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (buf1.isEqual (buf2), 1);
            }
        }

        [Test]
        public void TestNewSig () {
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (64, b);
            var s = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSig);
            err = skycoin.skycoin.SKY_cipher_RandByte (66, b);
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSig);
            err = skycoin.skycoin.SKY_cipher_RandByte (67, b);
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSig);
            err = skycoin.skycoin.SKY_cipher_RandByte (0, b);
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSig);
            err = skycoin.skycoin.SKY_cipher_RandByte (100, b);
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidLengthSig);
            s = new cipher_Sig ();
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (65, b);
            err = skycoin.skycoin.SKY_cipher_NewSig (b, s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var b1 = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (65, b1);
            var s1 = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_NewSig (b1, s1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestChkSig () {
            var s = new cipher_SecKey ();
            var p = new cipher_PubKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var a = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_Address_Verify (a, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (256, b);
            var h = new cipher_SHA256 ();
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var sig = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            // Empty sig should be invalid
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, new cipher_Sig ());
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidSigForPubKey);
            // Random sigs should not pass
            for (int i = 0; i < 100; i++) {
                var b1 = new GoSlice ();
                skycoin.skycoin.SKY_cipher_RandByte (65, b1);
                var sig1 = new cipher_Sig ();
                skycoin.skycoin.SKY_cipher_NewSig (b1, sig1);
                err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig1);
                Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);
            }
            // Sig for one hash does not work for another hash
            var h2 = new cipher_SHA256 ();
            var b2 = new skycoin.GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (256, b2);
            skycoin.skycoin.SKY_cipher_SumSHA256 (b2, h2);
            var sig2 = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_SignHash (h2, s, sig2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h2, sig2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig2);
            Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h2, sig);
            Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);

            // Different secret keys should not create same sig
            var p2 = new cipher_PubKey ();
            var s2 = new cipher_SecKey ();
            var a2 = new cipher__Address ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p2, s2);
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p2, a2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            h = new cipher_SHA256 ();
            sig = new cipher_Sig ();
            sig2 = new cipher_Sig ();
            skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            skycoin.skycoin.SKY_cipher_SignHash (h, s2, sig2);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a2, h, sig2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (sig.isEqual (sig2), 0);
            var b_temp = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (256, b_temp);
            h = new cipher_SHA256 ();
            skycoin.skycoin.SKY_cipher_SumSHA256 (b_temp, h);
            sig = new cipher_Sig ();
            sig2 = new cipher_Sig ();
            skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            skycoin.skycoin.SKY_cipher_SignHash (h, s2, sig2);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a2, h, sig2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (sig.isEqual (sig2), 0);
            // Bad address should be invalid
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig2);
            Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a2, h, sig);
            Assert.AreNotEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]

        public void TestSignHash () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var a = new cipher__Address ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            var err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var b_temp = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (256, b_temp);
            var h = new cipher_SHA256 ();
            skycoin.skycoin.SKY_cipher_SumSHA256 (b_temp, h);
            var sig = new cipher_Sig ();
            skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            Assert.AreEqual (sig.isEqual (new cipher_Sig ()), 0);
            err = skycoin.skycoin.SKY_cipher_ChkSig (a, h, sig);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestPubKeyFromSecKey () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            var p1 = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSecKey (s, p1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (p1.isEqual (p), 1);

            s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSecKey (s, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrPubKeyFromNullSecKey);
        }

        [Test]
        public void TestPubKeyFromSig () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var b = new GoSlice ();
            var h = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_RandByte (256, b);
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            var sig = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            var p2 = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSig (sig, h, p2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (p2.isEqual (p), 1);
            p2 = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSig (new cipher_Sig (), h, p2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidSigForPubKey);
        }

        [Test]
        public void TestVerifySignature () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var b = new GoSlice ();
            var h = new cipher_SHA256 ();
            var h2 = new cipher_SHA256 ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_RandByte (256, b);
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            b = new GoSlice ();
            err = skycoin.skycoin.SKY_cipher_RandByte (256, b);
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, h2);
            var sig = new cipher_Sig ();
            err = skycoin.skycoin.SKY_cipher_SignHash (h, s, sig);
            err = skycoin.skycoin.SKY_cipher_VerifySignature (p, sig, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_VerifySignature (p, new cipher_Sig (), h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrInvalidSigForPubKey);
            err = skycoin.skycoin.SKY_cipher_VerifySignature (p, sig, h2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrPubKeyRecoverMismatch);
            var p2 = new cipher_PubKey ();
            var s2 = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p2, s2);
            err = skycoin.skycoin.SKY_cipher_VerifySignature (p2, sig, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrPubKeyRecoverMismatch);
            err = skycoin.skycoin.SKY_cipher_VerifySignature (new cipher_PubKey (), sig, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrPubKeyRecoverMismatch);
        }

        [Test]
        public void TestGenerateKeyPair () {
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            var err = skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, s);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestGenerateDeterministicKeyPair () {
            // TODO -- deterministic key pairs are useless as is because we can't
            // generate pair n+1, only pair 0
            var seed = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (32, seed);
            var p = new cipher_PubKey ();
            var s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateDeterministicKeyPair (seed, p, s);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            p = new cipher_PubKey ();
            s = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_GenerateDeterministicKeyPair (seed, p, s);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (s);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }

        [Test]
        public void TestGenerateDeterministicKeyPairsUsesAllBytes () {
            // Tests that if a seed >128 bits is used, the generator does not ignore bits >128
            var seed = new GoSlice ();
            var seedText = "property diet little foster provide disagree witness mountain alley weekend kitten general";
            var seedStr = new _GoString_ ();
            seedStr.p = seedText;
            seed.convertString (seedStr);
            var seckeys = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_GenerateDeterministicKeyPairs (seed, 3, seckeys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var seckeys2 = new GoSlice ();
            seed.len = 16;
            err = skycoin.skycoin.SKY_cipher_GenerateDeterministicKeyPairs (seed, 3, seckeys2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (seckeys.isEqual (seckeys2), 0);
        }

        [Test]
        public void TestPubkey1 () {
            // This was migrated from coin/coin_test.go
            var a = "02fa939957e9fc52140e180264e621c2576a1bfe781f88792fb315ca3d1786afb8";
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_base58_String2Hex (a, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var p = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_NewPubKey (b, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var addr = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, addr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_Address_Verify (addr, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

        }

        [Test]
        public void TestSecKey1 () {
            // This was migrated from coin/coin_test.go
            var a = "5a42c0643bdb465d90bf673b99c14f5fa02db71513249d904573d2b8b63d353d";
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_base58_String2Hex (a, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (b.len, 32);

            var seckey = new cipher_SecKey ();
            err = skycoin.skycoin.SKY_cipher_NewSecKey (b, seckey);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SecKey_Verify (seckey);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var p = new cipher_PubKey ();
            err = skycoin.skycoin.SKY_cipher_PubKeyFromSecKey (seckey, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_PubKey_Verify (p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var addr = new cipher__Address ();
            err = skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, addr);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            skycoin.skycoin.SKY_cipher_Address_Verify (addr, p);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            var test = new GoSlice ();
            var strTest = new _GoString_ ();
            strTest.SetString ("test message");
            test.convertString (strTest);
            var hash = new cipher_SHA256 ();
            skycoin.skycoin.SKY_cipher_SumSHA256 (test, hash);
            err = skycoin.skycoin.SKY_cipher_CheckSecKeyHash (seckey, hash);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
        }
    }
}