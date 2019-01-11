using NUnit.Framework;
using skycoin;
namespace utils
{
    [TestFixture]
    public class TestUnits : skycoin.skycoin
    {
        [Test]
        public void Test1()
        {
            
            Assert.AreEqual(0,SKY_OK);
        }
    }
}