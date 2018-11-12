using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_util_param_distributions : skycoin.skycoin {

        utils.transutils transutils = new utils.transutils ();

        [Test]
        public void TestDistributionAddressArrays () {

            var all = new GoSlice ();
            var unlocked = new GoSlice ();
            var locked = new GoSlice ();
            SKY_params_GetDistributionAddresses (all);
            Assert.AreEqual (all.len, 100);
            SKY_params_GetUnlockedDistributionAddresses (unlocked);
            Assert.AreEqual (unlocked.len, 25);
            SKY_params_GetLockedDistributionAddresses (locked);
            Assert.AreEqual (locked.len, 75);
            var str1 = new _GoString_ ();
            var err = all.getAtString (0, str1);
            System.Console.WriteLine ("El test :" + str1.p);
        }
    }
}