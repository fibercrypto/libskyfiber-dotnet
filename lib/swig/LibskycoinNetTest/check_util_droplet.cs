// using System;
// using NUnit.Framework;
// using skycoin;
// using utils;
// namespace LibskycoinNetTest {
//     [TestFixture ()]
//     public class check_util_droplet : skycoin.skycoin {

//         utils.transutils transutils = new utils.transutils ();

//         struct TestStr {
//             public string s;
//             public ulong n;
//             public int e;
//         }

//         TestStr[] cases = new TestStr[30];
//         public void FullTestStr () {

//             var cas = new TestStr ();
//             cas.s = "0";
//             cas.n = 0;
//             cases[0] = cas;

//             cas = new TestStr ();
//             cas.s = "0.";
//             cas.n = 0;
//             cases[1] = cas;

//             cas = new TestStr ();
//             cas.s = "0.0";
//             cas.n = 0;
//             cases[2] = cas;

//             cas = new TestStr ();
//             cas.s = "0.000000";
//             cas.n = 0;
//             cases[3] = cas;

//             cas = new TestStr ();
//             cas.s = "0.0000000";
//             cas.n = 0;
//             cases[4] = cas;

//             cas = new TestStr ();
//             cas.s = "0.0000001";
//             cas.n = 0;
//             cas.e = SKY_ErrTooManyDecimals;
//             cases[5] = cas;

//             cas = new TestStr ();
//             cas.s = "0.000001";
//             cas.n = 1;
//             cases[6] = cas;

//             cas = new TestStr ();
//             cas.s = "0.0000010";
//             cas.n = 1;
//             cases[7] = cas;

//             cas = new TestStr ();
//             cas.s = "1";
//             cas.n = (ulong) 1e6;
//             cases[8] = cas;

//             cas = new TestStr ();
//             cas.s = "1.000001";
//             cas.n = (ulong) 1e6 + 1;
//             cases[9] = cas;

//             cas = new TestStr ();
//             cas.s = "-1";
//             cas.e = SKY_ErrNegativeValue;
//             cases[10] = cas;

//             cas = new TestStr ();
//             cas.s = "10000";
//             cas.n = (ulong) 1e6 * (ulong) 1e4;
//             cases[11] = cas;

//             cas = new TestStr ();
//             cas.s = "123456789.123456";
//             cas.n = 123456789123456;
//             cases[12] = cas;

//             cas = new TestStr ();
//             cas.s = "123.000456";
//             cas.n = 123000456;
//             cases[13] = cas;

//             cas = new TestStr ();
//             cas.s = "100SKY";
//             cas.e = SKY_ERROR;
//             cases[14] = cas;

//             cas = new TestStr ();
//             cas.s = "";
//             cas.e = SKY_ERROR;
//             cases[15] = cas;

//             cas = new TestStr ();
//             cas.s = "999999999999999999999999999999999999999999";
//             cas.e = SKY_ErrTooLarge;
//             cases[16] = cas;

//             cas = new TestStr ();
//             cas.s = "9223372036854.775807";
//             cas.n = 9223372036854775807;
//             cases[17] = cas;

//             cas = new TestStr ();
//             cas.s = "-9223372036854.775807";
//             cas.e = SKY_ErrNegativeValue;
//             cases[18] = cas;

//             cas = new TestStr ();
//             cas.s = "9223372036854775808";
//             cas.e = SKY_ErrTooLarge;
//             cases[19] = cas;

//             cas = new TestStr ();
//             cas.s = "9223372036854775807.000001";
//             cas.e = SKY_ErrTooLarge;
//             cases[20] = cas;

//             cas = new TestStr ();
//             cas.s = "9223372036854775807";
//             cas.e = SKY_ErrTooLarge;
//             cases[21] = cas;

//             cas = new TestStr ();
//             cas.s = "9223372036854775806.000001";
//             cas.e = SKY_ErrTooLarge;
//             cases[22] = cas;

//             cas = new TestStr ();
//             cas.s = "1.1";
//             cas.n = (ulong) (1e6 + 1e5);
//             cases[23] = cas;

//             cas = new TestStr ();
//             cas.s = "1.01";
//             cas.n = (ulong) (1e6 + 1e4);
//             cases[24] = cas;

//             cas = new TestStr ();
//             cas.s = "1.001";
//             cas.n = (ulong) (1e6 + 1e3);
//             cases[25] = cas;

//             cas = new TestStr ();
//             cas.s = "1.0001";
//             cas.n = (ulong) (1e6 + 1e2);
//             cases[26] = cas;

//             cas = new TestStr ();
//             cas.s = "1.00001";
//             cas.n = (ulong) (1e6 + 1e1);
//             cases[27] = cas;

//             cas = new TestStr ();
//             cas.s = "1.000001";
//             cas.n = (ulong) (1e6 + 1e0);
//             cases[28] = cas;

//             cas = new TestStr ();
//             cas.s = "1.0000001";
//             cas.e = SKY_ErrTooManyDecimals;
//             cases[29] = cas;
//         }

//         [Test]
//         public void TestFromString () {
//             FullTestStr ();
//             for (int i = 0; i < cases.Length; i++) {
//                 var tc = cases[i];
//                 var n = new_GoUint64p ();
//                 var err = SKY_droplet_FromString (tc.s, n);
//                 var n_v = GoUint64p_value (n);
//                 if (tc.e == SKY_OK) {
//                     Assert.AreEqual (err, SKY_OK);

//                     Assert.AreEqual (tc.n, n_v, "result " + n_v.ToString ());
//                 } else {
//                     Assert.AreEqual (tc.e, err);
//                     Assert.AreEqual (0, n_v);
//                 }
//             }
//         }

//         public void FullTestStr1 () {
//             cases = new TestStr[9];
//             var cas = new TestStr ();
//             cas.n = 0;
//             cas.s = "0.000000";
//             cases[0] = cas;

//             cas = new TestStr ();
//             cas.n = 1;
//             cas.s = "0.000001";
//             cases[1] = cas;

//             cas = new TestStr ();
//             cas.n = (ulong) (1e6);
//             cas.s = "1.000000";
//             cases[2] = cas;

//             cas = new TestStr ();
//             cas.n = 100100;
//             cas.s = "0.100100";
//             cases[3] = cas;

//             cas = new TestStr ();
//             cas.n = 1001000;
//             cas.s = "1.001000";
//             cases[4] = cas;

//             cas = new TestStr ();
//             cas.n = 999;
//             cas.s = "0.000999";
//             cases[5] = cas;

//             cas = new TestStr ();
//             cas.n = 999000000;
//             cas.s = "999.000000";
//             cases[6] = cas;

//             cas = new TestStr ();
//             cas.n = 123000456;
//             cas.s = "123.000456";
//             cases[7] = cas;

//             cas = new TestStr ();
//             cas.n = 9223372036854775808;
//             cas.e = SKY_ErrTooLarge;
//             cases[8] = cas;
//         }

//         [Test]
//         public void TestToString () {
//             FullTestStr1 ();
//             for (int i = 0; i < cases.Length; i++) {
//                 var tc = cases[i];
//                 var s = new _GoString_ ();
//                 var err = SKY_droplet_ToString (tc.n, s);
//                 if (tc.e == SKY_OK) {
//                     Assert.AreEqual (err, SKY_OK);
//                     Assert.IsTrue (tc.s == s.p, i.ToString ());
//                 } else {
//                     Assert.AreEqual (tc.e, err);
//                     Assert.IsTrue (null == s.p);
//                 }
//             }
//         }
//     }
// }