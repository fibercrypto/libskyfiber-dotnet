using System;
using NUnit.Framework;
using skycoin;
namespace utils {
    public class transutils {
        public uint makeAddress (cipher__Address paddress) {
            var pubkey = new cipher_PubKey ();
            var seckey = new cipher_SecKey ();
            var addr = new cipher__Address ();
            var result = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pubkey, seckey);
            Assert.AreEqual (result, skycoin.skycoin.SKY_OK);
            result = skycoin.skycoin.SKY_cipher_AddressFromPubKey (pubkey, addr);
            return result;
        }

        public uint makeEmptyTransaction (SWIGTYPE_p_Transaction__Handle txn) {
            var result = skycoin.skycoin.SKY_coin_Create_Transaction (txn);
            Assert.AreEqual (result, skycoin.skycoin.SKY_OK);
            return result;
        }
    }
}