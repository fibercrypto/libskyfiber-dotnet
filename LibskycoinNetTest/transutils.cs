using System;
using System.Text.RegularExpressions;
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
            var err = skycoin.skycoin.SKY_coin_Transaction_Verify (handle);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "SKY_coin_Transaction_Verify failed");
            err = skycoin.skycoin.SKY_coin_Transaction_Copy (handle, handle2);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            return handle2;
        }

        public void makeTransactionFromUxOut (coin__UxOut ux, cipher_SecKey s, SWIGTYPE_p_Transaction__Handle handle, coin__Transaction ptx) {
            handle = makeEmptyTransaction ();
            var h = new cipher_SHA256 ();
            Assert.AreEqual (skycoin.skycoin.SKY_cipher_SecKey_Verify (s), skycoin.skycoin.SKY_OK);
            var err = skycoin.skycoin.SKY_coin_UxOut_Hash (ux, h);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var r = skycoin.skycoin.new_GoUint16p ();
            skycoin.skycoin.SKY_coin_Transaction_PushInput (handle, h, r);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (handle, makeAddress (), (ulong) 1e6, 50);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (handle, makeAddress (), (ulong) 5e6, 50);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var seckeys = new cipher_SecKeys ();
            seckeys.allocate (1);
            seckeys.setAt (0, s);
            err = skycoin.skycoin.SKY_coin_Transaction_SignInputs (handle, seckeys);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_UpdateHeader (handle);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_GetTransactionObject (handle, ptx);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_Verify (handle);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

        }

        public void makeUxBodyWithSecret (coin__UxBody uxBody, cipher_SecKey secKey) {
            var p = new cipher_PubKey ();
            skycoin.skycoin.SKY_cipher_GenerateKeyPair (p, secKey);
            var b = new GoSlice ();
            skycoin.skycoin.SKY_cipher_RandByte (128, b);
            var h = new cipher_SHA256 ();
            skycoin.skycoin.SKY_cipher_SumSHA256 (b, h);
            uxBody.SetSrcTransaction (h);
            var a = new cipher__Address ();
            skycoin.skycoin.SKY_cipher_AddressFromPubKey (p, a);
            uxBody.Address = a;
            uxBody.Coins = (ulong) (1e6);
            uxBody.Hours = 100;
        }

        public void makeUxOutWithSecret (coin__UxOut uxOut, cipher_SecKey secKey) {
            var uxBody = new coin__UxBody ();
            makeUxBodyWithSecret (uxBody, secKey);
            uxOut = new coin__UxOut ();
            var uxHead = new coin__UxHead ();
            uxHead.Time = 100;
            uxHead.BkSeq = 2;
            uxOut.Body = uxBody;
            uxOut.Head = uxHead;
        }

        public void makeTransaction (SWIGTYPE_p_Transaction__Handle handle, coin__Transaction ptx) {
            var s = new cipher_SecKey ();
            var ux = new coin__UxOut ();
            makeUxOutWithSecret (ux, s);
            makeTransactionFromUxOut (ux, s, handle, ptx);
        }

        public SWIGTYPE_p_Transactions__Handle makeTransactions (int n) {
            var handle = skycoin.skycoin.new_Transactions__Handlep ();
            skycoin.skycoin.SKY_coin_Create_Transactions (handle);
            for (int i = 0; i < n; i++) {
                var thandle = makeEmptyTransaction ();
                var ptx = new coin__Transaction ();
                makeTransaction (thandle, ptx);
                skycoin.skycoin.SKY_coin_Transactions_Add (handle, thandle);
            }
            var count = skycoin.skycoin.new_Gointp ();
            skycoin.skycoin.SKY_coin_Transactions_Length (handle, count);
            Assert.AreEqual (n, skycoin.skycoin.Gointp_value (count));
            return handle;
        }

        public coin__UxBody makeUxBody () {
            var uxb = new coin__UxBody ();
            var s = new cipher_SecKey ();
            makeUxBodyWithSecret (uxb, s);
            return uxb;
        }

        public coin__UxOut makeUxOut () {
            var uxb = new coin__UxOut ();
            var s = new cipher_SecKey ();
            makeUxOutWithSecret (uxb, s);
            return uxb;
        }

        public coin_UxOutArray makeUxOutArray (int n) {
            var result = new coin_UxOutArray ();

            for (int i = 0; i < n; i++) {
                result.append (makeUxOut ());
            }
            Assert.AreEqual (result.count, n, "Not equal len");
            return result;
        }

        public cipher_SHA256 RandSHA256 () {
            var sha = new cipher_SHA256 ();
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (128, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_cipher_SumSHA256 (b, sha);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            return sha;
        }

        public GoSlice RandBytes (int n) {
            var b = new GoSlice ();
            var err = skycoin.skycoin.SKY_cipher_RandByte (n, b);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            return b;
        }

        public bool IsBase64String (string s) {
            s = s.Trim ();
            return (s.Length % 4 == 0) && Regex.IsMatch (s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        public string base64Encode (string data) {
            try {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.ASCII.GetBytes (data);
                string encodedData = Convert.ToBase64String (encData_byte);
                return encodedData;
            } catch (Exception e) {
                throw new Exception ("Error in base64Encode " + e.Message);
            }
        }

        public SWIGTYPE_p_FeeCalculator feeCalc = skycoin.skycoin.feeCalc ();
        public SWIGTYPE_p_FeeCalculator badCalc = skycoin.skycoin.badCalc ();
        public SWIGTYPE_p_FeeCalculator fix121 = skycoin.skycoin.fix121 ();
        public SWIGTYPE_p_FeeCalculator calc = skycoin.skycoin.calcCalc ();
        public SWIGTYPE_p_FeeCalculator overflow = skycoin.skycoin.overflow();

        public string base64Decode (string data) {
            try {
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding ();
                System.Text.Decoder utf8Decode = encoder.GetDecoder ();

                byte[] todecode_byte = Convert.FromBase64String (data);
                int charCount = utf8Decode.GetCharCount (todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars (todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String (decoded_char);
                return result;
            } catch (Exception e) {
                throw new Exception ("Error in base64Decode " + e.Message);
            }
        }
    }
}