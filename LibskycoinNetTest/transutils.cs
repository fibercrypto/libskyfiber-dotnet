using System;
using NUnit.Framework;
using skycoin;
namespace utils {
    public class transutils {
        public cipher__Address makeAddress () {
            var pubkey = new cipher_PubKey ();
            var seckey = new cipher_SecKey ();
            var addr = new cipher__Address ();
            var result = skycoin.skycoin.SKY_cipher_GenerateKeyPair (pubkey, seckey);
            Assert.AreEqual (result, skycoin.skycoin.SKY_OK);
            result = skycoin.skycoin.SKY_cipher_AddressFromPubKey (pubkey, addr);
            return addr;
        }

        public SWIGTYPE_p_Transaction__Handle makeEmptyTransaction () {
            var txn = skycoin.skycoin.new_Transaction__Handlep ();
            var result = skycoin.skycoin.SKY_coin_Create_Transaction (txn);
            Assert.AreEqual (result, skycoin.skycoin.SKY_OK);
            return txn;
        }

        public SWIGTYPE_p_Transaction__Handle copyTransaction (SWIGTYPE_p_Transaction__Handle handle) {
            var handle2 = skycoin.skycoin.new_Transaction__Handlep ();
            var err = skycoin.skycoin.SKY_coin_Transaction_Copy (handle, handle2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            return handle2;
        }
    }
}