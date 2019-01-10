using NUnit.Framework;
using skycoin;

namespace Tests
{
    public class Tests: skycoin.skycoin
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
            
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(SKY_OK,0);
        }
    }
}