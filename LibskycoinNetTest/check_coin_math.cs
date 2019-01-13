using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibSkycoinNetTest {
    [TestFixture]
    public class check_coin_math : skycoin.skycoin {

        utils.transutils transutils = new utils.transutils ();
        [Test]
        public void TestAddUint64 () {
            var r = new_GoUint64p ();
            var err = SKY_coin_AddUint64 (10, 11, r);
            Assert.AreEqual (err, SKY_OK);
            Assert.AreEqual (GoUint64p_value (r), 21);
            err = SKY_coin_AddUint64 (ulong.MaxValue, 1, r);
            Assert.AreEqual (err, SKY_ErrUint64AddOverflow);
        }
    //     struct math_test {
    //         public ulong a;
    //         public ulong b;
    //         public int failure;
    //     }

    //     math_test[] cases = new math_test[4];

    //     public void FullCases () {
    //         var c = new math_test ();
    //         c.a = 0;
    //         c.b = 0;
    //         c.failure = skycoin.skycoin.SKY_OK;
    //         cases[0] = c;

    //         c = new math_test ();
    //         c.a = 1;
    //         c.b = 1;
    //         c.failure = skycoin.skycoin.SKY_OK;
    //         cases[1] = c;

    //         c = new math_test ();
    //         c.a = long.MaxValue;
    //         c.b = long.MaxValue;
    //         c.failure = skycoin.skycoin.SKY_OK;
    //         cases[2] = c;

    //         c = new math_test ();
    //         c.a = ulong.MaxValue;
    //         c.b = 0;
    //         c.failure = skycoin.skycoin.SKY_ERROR;
    //         cases[3] = c;
    //     }

    //     [Test]
    //     public void TestUint64ToInt64 () {
    //         for (int i = 0; i < cases.Length; i++) {
    //             var r = skycoin.skycoin.new_Gointp ();
    //             var err = skycoin.skycoin.SKY_coin_Uint64ToInt64 (cases[i].a, r);
    //             Assert.AreEqual (err, cases[i].failure);
    //             Assert.AreEqual (cases[i].b, skycoin.skycoin.Gointp_value (r));
    //         }
    //     }
    }
}