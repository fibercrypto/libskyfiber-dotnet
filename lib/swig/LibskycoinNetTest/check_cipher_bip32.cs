using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest
{
    [TestFixture()]
    public class check_cipher_bip32 : skycoin.skycoin
    {
        private const uint FirstHardenedChild = 0x80000000;
        private struct testChildKey
        {
            string path;
            string privKey;
            string pubKey;
            string fingerprint;
            string identifier;
            string chainCode;
            string hexPubKey;
            string wifPrivKey;
            uint childNUmber;
            char depth;
        }

        [Test()]
        public void TestBip32TestVectors()
        {

        }
    }
}