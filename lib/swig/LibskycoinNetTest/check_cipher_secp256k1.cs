// using System;
// using NUnit.Framework;
// using skycoin;
// namespace LibskycoinNetTest
// {

//     [TestFixture()]
//     public class check_cipher_secp256k1 : skycoin.skycoin
//     {
//         private string[] _testSeckey = {
//     "08efb79385c9a8b0d1c6f5f6511be0c6f6c2902963d874a3a4bacc18802528d3",
//     "78298d9ecdc0640c9ae6883201a53f4518055442642024d23c45858f45d0c3e6",
//     "04e04fe65bfa6ded50a12769a3bd83d7351b2dbff08c9bac14662b23a3294b9e",
//     "2f5141f1b75747996c5de77c911dae062d16ae48799052c04ead20ccd5afa113"};


//         [Test]
//         public void Test_Abnormal_Keys2()
//         {
//             for (int i = 0; i < _testSeckey.Length; i++)
//             {
//                 var seckkey1 = new GoSlice();
//                 var pubkey1 = new GoSlice();
//                 long err = SKY_base58_String2Hex(_testSeckey[i], seckkey1);
//                 Assert.AreEqual(err, SKY_OK);
//                 err = SKY_secp256k1_PubkeyFromSeckey(seckkey1, pubkey1);
//                 Assert.AreEqual(err, SKY_OK);
//                 err = SKY_secp256k1_VerifyPubkey(pubkey1);
//                 Assert.AreEqual(err, 1);
//             }
//         }

//         [Test]
//         public void Test_Abnormal_Keys3()
//         {
//             for (int i = 0; i < _testSeckey.Length; i++)
//             {
//                 var seckkey1 = new GoSlice();
//                 var pubkey1 = new GoSlice();
//                 long err = SKY_base58_String2Hex(_testSeckey[i], seckkey1);
//                 Assert.AreEqual(err, SKY_OK);
//                 err = SKY_secp256k1_PubkeyFromSeckey(seckkey1, pubkey1);
//                 Assert.AreEqual(err, SKY_OK);

//                 var seckkey2 = new GoSlice();
//                 var pubkey2 = new GoSlice();
//                 Random rnd = new Random();
//                 var n = rnd.Next(0, 4);
//                 err = SKY_base58_String2Hex(_testSeckey[n], seckkey2);
//                 Assert.AreEqual(err, SKY_OK);
//                 err = SKY_secp256k1_PubkeyFromSeckey(seckkey2, pubkey2);
//                 Assert.AreEqual(err, SKY_OK);
//                 var puba = new GoSlice();
//                 var pubb = new GoSlice();
//                 err = SKY_secp256k1_ECDH(pubkey1, seckkey2, puba);
//                 Assert.AreEqual(err, SKY_OK);
//                 err = SKY_secp256k1_ECDH(pubkey2, seckkey1, pubb);
//                 Assert.AreEqual(err, SKY_OK);
//             }
//         }
//     }
// }