using System;
using System.IO;
using NUnit.Framework;
using skycoin;

namespace LibskycoinNetTest {
    [TestFixture]
    public class LibskycoinNetTest {
        public LibskycoinNetTest () { }

        [Test]
        public void SumTest () {
            var xyz = new skycoin.secp256k1go__XYZ();
            var xy = new skycoin.secp256k1go__XY ();
            uint error = skycoin.skycoin.SKY_secp256k1go_XYZ_SetXY (xyz, xy);
            Assert.AreEqual (error, 0);

        }
    }
}