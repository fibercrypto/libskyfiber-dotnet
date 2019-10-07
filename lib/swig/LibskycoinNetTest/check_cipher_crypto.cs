// using System;
// using NUnit.Framework;
// using skycoin;
// namespace LibskycoinNetTest {
//     [TestFixture ()]
//     public class check_cipher_cryto : skycoin.skycoin {
//         [Test]
//         public void TestNewPubKey () {
//             var b = new GoSlice ();
//             var p = new cipher_PubKey ();
//             var err = SKY_cipher_RandByte (31, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (32, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (34, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (0, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (100, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);
//             b = new GoSlice ();
//             p = new cipher_PubKey ();
//             err = SKY_cipher_NewPubKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthPubKey);

//             var s = new cipher_SecKey ();
//             var pTemp = new GoSlice ();
//             err = SKY_cipher_GenerateKeyPair (p, s);
//             pTemp = p.toSlice ();
//             Assert.AreEqual (err, SKY_OK);
//             var p2 = new cipher_PubKey ();
//             err = SKY_cipher_NewPubKey (pTemp, p2);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (p.isEqual (p2), 1);
//         }

//         [Test]
//         public void TestPubKeyVerify () {
//             // Random bytes should not be valid, most of the time
//             var failed = false;
//             for (int i = 0; i < 10; i++) {
//                 var b = new GoSlice ();
//                 var err = SKY_cipher_RandByte (33, b);
//                 Assert.AreEqual (err, SKY_OK);
//                 var p = new cipher_PubKey ();
//                 p.assignSlice (b);
//                 err = SKY_cipher_PubKey_Verify (p);
//                 if (err != SKY_OK) {
//                     failed = true;
//                     break;
//                 }
//             }
//             Assert.IsTrue (failed);
//         }

//         [Test]
//         public void TestPubKeyVerifyNil () {
//             // Empty public key should not be valid
//             var p = new cipher_PubKey ();
//             var err = SKY_cipher_PubKey_Verify (p);
//             Assert.AreNotEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestPubKeyVerifyDefault1 () {
//             // Generated pub key should be valid
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var err = SKY_cipher_GenerateKeyPair (p, s);
//             err = SKY_cipher_PubKey_Verify (p);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestPubKeyVerifyDefault2 () {
//             for (int i = 0; i < 1024; i++) {
//                 var p = new cipher_PubKey ();
//                 var s = new cipher_SecKey ();
//                 var err = SKY_cipher_GenerateKeyPair (p, s);
//                 err = SKY_cipher_PubKey_Verify (p);
//                 Assert.AreEqual (err, SKY_OK);
//             }
//         }

//         [Test]
//         public void TestPubKeyToAddress () {
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var addr = new cipher__Address ();
//             var addr1 = new cipher__Address ();
//             var err = SKY_cipher_GenerateKeyPair (p, s);
//             err = SKY_cipher_AddressFromPubKey (p, addr);
//             err = SKY_cipher_Address_Verify (addr, p);
//             Assert.AreEqual (err, SKY_OK);
//             var addrStr = new _GoString_ ();
//             err = SKY_cipher_Address_String (addr, addrStr);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_DecodeBase58Address (addrStr.p, addr1);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestPubKeyToAddress2 () {
//             for (int i = 0; i < 1024; i++) {
//                 var p = new cipher_PubKey ();
//                 var s = new cipher_SecKey ();
//                 var addr = new cipher__Address ();
//                 var addr1 = new cipher__Address ();
//                 var err = SKY_cipher_GenerateKeyPair (p, s);
//                 err = SKY_cipher_AddressFromPubKey (p, addr);
//                 err = SKY_cipher_Address_Verify (addr, p);
//                 Assert.AreEqual (err, SKY_OK);
//                 var addrStr = new _GoString_ ();
//                 err = SKY_cipher_Address_String (addr, addrStr);
//                 Assert.AreEqual (err, SKY_OK);
//                 err = SKY_cipher_DecodeBase58Address (addrStr.p, addr1);
//                 Assert.AreEqual (err, SKY_OK);
//             }
//         }

//         [Test]
//         public void TestMustNewSecKey () {
//             var b = new GoSlice ();
//             var p = new cipher_SecKey ();
//             var err = SKY_cipher_RandByte (31, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSecKey);
//             err = SKY_cipher_RandByte (32, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSecKey);
//             err = SKY_cipher_RandByte (34, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSecKey);
//             err = SKY_cipher_RandByte (0, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSecKey);
//             err = SKY_cipher_RandByte (100, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSecKey);
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (32, b);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b, p);
//             Assert.AreEqual (err, SKY_OK);
//             var b1 = new GoSlice ();
//             var p1 = new cipher_SecKey ();
//             err = SKY_cipher_RandByte (32, b1);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_NewSecKey (b1, p1);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestSecKeyVerify () {
//             // Empty secret key should not be valid
//             var s = new cipher_SecKey ();
//             var err = SKY_cipher_SecKey_Verify (s);
//             Assert.AreEqual (err, SKY_ErrInvalidSecKey);
//             // Generated sec key should be valid
//             var p = new cipher_PubKey ();
//             s = new cipher_SecKey ();
//             err = SKY_cipher_GenerateKeyPair (p, s);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (SKY_cipher_SecKey_Verify (s), SKY_OK);
//         }

//         [Test]
//         public void TestECDHonce () {
//             var pub1 = new cipher_PubKey ();
//             var pub2 = new cipher_PubKey ();
//             var sec1 = new cipher_SecKey ();
//             var sec2 = new cipher_SecKey ();
//             var buf1 = new GoSlice ();
//             var buf2 = new GoSlice ();
//             var err = SKY_cipher_GenerateKeyPair (pub1, sec1);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_GenerateKeyPair (pub2, sec2);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_ECDH (pub2, sec1, buf1);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_ECDH (pub1, sec2, buf2);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (buf1.isEqual (buf2), 1);
//         }

//         [Test]
//         public void TestECDHloop () {
//             for (int i = 0; i < 128; i++) {
//                 var pub1 = new cipher_PubKey ();
//                 var pub2 = new cipher_PubKey ();
//                 var sec1 = new cipher_SecKey ();
//                 var sec2 = new cipher_SecKey ();
//                 var buf1 = new GoSlice ();
//                 var buf2 = new GoSlice ();
//                 var err = SKY_cipher_GenerateKeyPair (pub1, sec1);
//                 Assert.AreEqual (err, SKY_OK);
//                 err = SKY_cipher_GenerateKeyPair (pub2, sec2);
//                 Assert.AreEqual (err, SKY_OK);
//                 err = SKY_cipher_ECDH (pub2, sec1, buf1);
//                 Assert.AreEqual (err, SKY_OK);
//                 err = SKY_cipher_ECDH (pub1, sec2, buf2);
//                 Assert.AreEqual (err, SKY_OK);
//                 Assert.AreEqual (buf1.isEqual (buf2), 1);
//             }
//         }

//         [Test]
//         public void TestNewSig () {
//             var b = new GoSlice ();
//             var err = SKY_cipher_RandByte (64, b);
//             var s = new cipher_Sig ();
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSig);
//             err = SKY_cipher_RandByte (66, b);
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSig);
//             err = SKY_cipher_RandByte (67, b);
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSig);
//             err = SKY_cipher_RandByte (0, b);
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSig);
//             err = SKY_cipher_RandByte (100, b);
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_ErrInvalidLengthSig);
//             s = new cipher_Sig ();
//             b = new GoSlice ();
//             err = SKY_cipher_RandByte (65, b);
//             err = SKY_cipher_NewSig (b, s);
//             Assert.AreEqual (err, SKY_OK);
//             var b1 = new GoSlice ();
//             err = SKY_cipher_RandByte (65, b1);
//             var s1 = new cipher_Sig ();
//             err = SKY_cipher_NewSig (b1, s1);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]

//         public void TestSignHash () {
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var a = new cipher__Address ();
//             SKY_cipher_GenerateKeyPair (p, s);
//             var err = SKY_cipher_AddressFromPubKey (p, a);
//             Assert.AreEqual (err, SKY_OK);
//             var b_temp = new GoSlice ();
//             SKY_cipher_RandByte (256, b_temp);
//             var h = new cipher_SHA256 ();
//             SKY_cipher_SumSHA256 (b_temp, h);
//             var sig = new cipher_Sig ();
//             SKY_cipher_SignHash (h, s, sig);
//             Assert.AreEqual (sig.isEqual (new cipher_Sig ()), 0);
//         }

//         [Test]
//         public void TestPubKeyFromSecKey () {
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var err = SKY_cipher_GenerateKeyPair (p, s);
//             var p1 = new cipher_PubKey ();
//             err = SKY_cipher_PubKeyFromSecKey (s, p1);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (p1.isEqual (p), 1);

//             s = new cipher_SecKey ();
//             err = SKY_cipher_PubKeyFromSecKey (s, p);
//             Assert.AreEqual (err, SKY_ErrPubKeyFromNullSecKey);
//         }

//         [Test]
//         public void TestPubKeyFromSig () {
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var b = new GoSlice ();
//             var h = new cipher_SHA256 ();
//             var err = SKY_cipher_GenerateKeyPair (p, s);
//             err = SKY_cipher_RandByte (256, b);
//             err = SKY_cipher_SumSHA256 (b, h);
//             var sig = new cipher_Sig ();
//             err = SKY_cipher_SignHash (h, s, sig);
//             var p2 = new cipher_PubKey ();
//             err = SKY_cipher_PubKeyFromSig (sig, h, p2);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (p2.isEqual (p), 1);
//             p2 = new cipher_PubKey ();
//             err = SKY_cipher_PubKeyFromSig (new cipher_Sig (), h, p2);
//             Assert.AreEqual (err, SKY_ErrInvalidSigPubKeyRecovery);
//         }

//         [Test]
//         public void TestGenerateKeyPair () {
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             var err = SKY_cipher_GenerateKeyPair (p, s);
//             err = SKY_cipher_PubKey_Verify (p);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_SecKey_Verify (s);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestGenerateDeterministicKeyPair () {
//             // TODO -- deterministic key pairs are useless as is because we can't
//             // generate pair n+1, only pair 0
//             var seed = new GoSlice ();
//             var err = SKY_cipher_RandByte (32, seed);
//             var p = new cipher_PubKey ();
//             var s = new cipher_SecKey ();
//             err = SKY_cipher_GenerateDeterministicKeyPair (seed, p, s);
//             err = SKY_cipher_PubKey_Verify (p);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_SecKey_Verify (s);
//             Assert.AreEqual (err, SKY_OK);
//             p = new cipher_PubKey ();
//             s = new cipher_SecKey ();
//             err = SKY_cipher_GenerateDeterministicKeyPair (seed, p, s);
//             err = SKY_cipher_PubKey_Verify (p);
//             Assert.AreEqual (err, SKY_OK);
//             err = SKY_cipher_SecKey_Verify (s);
//             Assert.AreEqual (err, SKY_OK);
//         }

//         [Test]
//         public void TestGenerateDeterministicKeyPairsUsesAllBytes () {
//             // Tests that if a seed >128 bits is used, the generator does not ignore bits >128
//             var seed = new GoSlice ();
//             var seedText = "property diet little foster provide disagree witness mountain alley weekend kitten general";
//             var seedStr = new _GoString_ ();
//             seedStr.p = seedText;
//             seed.convertString (seedStr);
//             var seckeys = new GoSlice ();
//             var err = SKY_cipher_GenerateDeterministicKeyPairs (seed, 3, seckeys);
//             Assert.AreEqual (err, SKY_OK);
//             var seckeys2 = new GoSlice ();
//             seed.len = 16;
//             err = SKY_cipher_GenerateDeterministicKeyPairs (seed, 3, seckeys2);
//             Assert.AreEqual (err, SKY_OK);
//             Assert.AreEqual (seckeys.isEqual (seckeys2), 0);
//         }

//     }
// }