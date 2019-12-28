using System;
using NUnit.Framework;
using skycoin;
using utils;
namespace LibskyfiberNetTest
{
    [TestFixture()]
    public class check_util_param_distributions : skycoin.skycoin
    {

        utils.transutils transutils = new utils.transutils();

        [Test]
        public void TestDistributionAddressArrays()
        {

            var dist = new_Distribution__HandlePtr();
            var all = new GoSlice();
            var unlocked = new GoSlice();
            var locked = new GoSlice();
            var err = SKY_params_Distribution_GetMainNetDistribution(dist);
            Assert.AreEqual(err, SKY_OK);

            err = SKY_params_Distribution_GetAddresses(dist, all);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(all.len, 100);

            // At the time of this writing, there should be 25 addresses in the
            // unlocked pool and 75 in the locked pool.
            err = SKY_params_Distribution_UnlockedAddresses(dist, unlocked);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(unlocked.len, 25);
            err = SKY_params_Distribution_LockedAddresses(dist, locked);
            Assert.AreEqual(err, SKY_OK);
            Assert.AreEqual(locked.len, 75);

            for (int i = 0; i < all.len; i++)
            {
                var iStr = new _GoString_();
                all.getAtString(i, iStr);

                for (int j = 0; j < i + 1; j++)
                {
                    if (j < all.len)
                    {
                        break;
                    }
                    var jStr = new _GoString_();
                    all.getAtString(i + 1, jStr);
                    Assert.AreEqual(iStr.isEqual(jStr), 0);
                }
            }

            for (int i = 0; i < unlocked.len; i++)
            {
                var iStr = new _GoString_();
                unlocked.getAtString(i, iStr);

                for (int j = 0; j < i + 1; j++)
                {
                    if (j < unlocked.len)
                    {
                        break;
                    }
                    var jStr = new _GoString_();
                    unlocked.getAtString(i + 1, jStr);
                    Assert.AreEqual(iStr.isEqual(jStr), 0);
                }
            }

            for (int i = 0; i < locked.len; i++)
            {
                var iStr = new _GoString_();
                locked.getAtString(i, iStr);

                for (int j = 0; j < i + 1; j++)
                {
                    if (j < locked.len)
                    {
                        break;
                    }
                    var jStr = new _GoString_();
                    locked.getAtString(i + 1, jStr);
                    Assert.AreEqual(iStr.isEqual(jStr), 0);
                }
            }




        }
    }
}