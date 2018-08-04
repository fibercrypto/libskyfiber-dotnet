using System;
using LibskycoinNet;
using NUnit.Framework;

namespace LibskycoinNetTest
{
	[TestFixture]
    public class LibskycoinNetTest
    {
        public LibskycoinNetTest()
        {
        }

        [Test]
		public void SumTest(){
			var sky = new Skycoin();
			var r = sky.Sum(2, 3);
			Assert.AreEqual(r, 8);
		}
    }


}
