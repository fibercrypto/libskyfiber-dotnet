﻿using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_cipher_address {
        [Test ()]
        public void TestDecodeBase58Address () {
            var p = new skycoin.cipher_PubKey ();
            var s = new skycoin.cipher_SecKey ();
            uint err = 0;
            Assert.AreEqual (err, 0);
            Assert.AreEqual (err, 0);

        }
    }
}