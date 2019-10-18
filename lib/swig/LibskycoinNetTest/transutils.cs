using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using skycoin;
namespace utils
{
    public class transutils : skycoin.skycoin
    {
        public cipher__Address makeAddress()
        {
            var pubkey = new cipher_PubKey();
            var seckey = new cipher_SecKey();
            var addr = new cipher__Address();
            var result = SKY_cipher_GenerateKeyPair(pubkey, seckey);
            Assert.AreEqual(result, SKY_OK);
            result = SKY_cipher_AddressFromPubKey(pubkey, addr);
            Assert.AreEqual(result, SKY_OK);
            return addr;
        }

        public SWIGTYPE_p_Transaction__Handle makeEmptyTransaction()
        {
            var txn = new_Transaction__HandlePtr();
            var result = SKY_coin_Create_Transaction(txn);
            Assert.AreEqual(result, SKY_OK);
            return txn;
        }

        public SWIGTYPE_p_Transaction__Handle copyTransaction(SWIGTYPE_p_Transaction__Handle handle)
        {
            var handle2 = new_Transaction__HandlePtr();
            var err = SKY_coin_Transaction_Verify(handle);
            Assert.AreEqual(err, SKY_OK, "SKY_coin_Transaction_Verify failed");
            err = SKY_coin_Transaction_Copy(handle, handle2);
            Assert.AreEqual(err, SKY_OK);
            return handle2;
        }

        public SWIGTYPE_p_Transaction__Handle makeTransactionFromUxOuts(coin_UxOutArray uxs, cipher_SecKeys secs)
        {
            Assert.AreEqual(uxs.count, secs.count);

            var txn = makeEmptyTransaction();

            var err = SKY_coin_Transaction_PushOutput(txn, makeAddress(), (ulong)(1e6), 50);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_PushOutput(txn, makeAddress(), (ulong)(5e6), 50);
            Assert.AreEqual(err, SKY_OK);

            for (int i = 0; i < uxs.count; i++)
            {
                var ux = uxs.getAt(i);
                var has_ux = new cipher_SHA256();
                err = SKY_coin_UxOut_Hash(ux, has_ux);
                Assert.AreEqual(err, SKY_OK);
                err = SKY_coin_Transaction_PushInput(txn, has_ux);
                Assert.AreEqual(err, SKY_OK);
            }

            return txn;
        }
        public SWIGTYPE_p_Transaction__Handle makeTransactionFromUxOut(coin__UxOut ux, cipher_SecKey s)
        {

            var uxOut = new coin_UxOutArray();
            uxOut.append(ux);
            var secs = new cipher_SecKeys();
            secs.allocate(1);
            secs.setAt(0, s);
            var txn = makeTransactionFromUxOuts(uxOut, secs);
            return txn;
        }

        public void makeUxBodyWithSecret(coin__UxBody uxBody_tmp, cipher_SecKey secKey)
        {
            var p = new cipher_PubKey();
            var err = SKY_cipher_GenerateKeyPair(p, secKey);
            Assert.AreEqual(err, SKY_OK);
            var uxBody = new coin__UxBody();
            var SrcTransaction = new cipher_SHA256();
            SKY_testutil_RandSHA256(SrcTransaction);
            uxBody.SetSrcTransaction(SrcTransaction);
            var Address = new cipher__Address();
            err = SKY_cipher_AddressFromPubKey(p, Address);
            Assert.AreEqual(err, SKY_OK);
            uxBody.Address = Address;
            uxBody.Coins = (ulong)(1e6);
            uxBody.Hours = 100;
            coin__UxBodyPtr_assign(uxBody_tmp, uxBody);

        }

        public coin__UxOut makeUxOutWithSecret(cipher_SecKey secKey)
        {
            var body = new_coin__UxBodyPtr();
            makeUxBodyWithSecret(body, secKey);
            var head = new coin__UxHead();
            head.Time = 100;
            head.BkSeq = 2;
            var uxOut_tmp = new coin__UxOut();
            uxOut_tmp.Head = head;
            uxOut_tmp.Body = coin__UxBodyPtr_value(body);
            return uxOut_tmp;
        }

        public SWIGTYPE_p_Transaction__Handle makeTransaction()
        {
            var s = new cipher_SecKey();
            var ux = makeUxOutWithSecret(s);
            return makeTransactionFromUxOut(ux, s);
        }

        public SWIGTYPE_p_Transactions__Handle makeTransactions(uint n)
        {
            var handle = new_Transactions__HandlePtr();
            var err = SKY_coin_Create_Transactions(handle);
            Assert.AreEqual(err, SKY_OK);
            for (uint i = 0; i < n; i++)
            {
                var thandle = makeTransaction();
                SKY_coin_Transactions_Add(handle, thandle);
            }
            var count = new_GoIntPtr();
            err = SKY_coin_Transactions_Length(handle, count);
            Assert.AreEqual(n, GoIntPtr_value(count));
            Assert.AreEqual(err, SKY_OK);
            return handle;
        }

        public coin__UxBody makeUxBody()
        {
            var uxb = new_coin__UxBodyPtr();
            var s = new cipher_SecKey();
            makeUxBodyWithSecret(uxb, s);
            return coin__UxBodyPtr_value(uxb);
        }

        public coin__UxOut makeUxOut()
        {
            var s = new cipher_SecKey();
            return makeUxOutWithSecret(s);
        }

        public coin_UxOutArray makeUxOutArray(int n)
        {
            var result = new coin_UxOutArray();
            result.allocate(n);
            for (int i = 0; i < n; i++)
            {
                var uxOut = makeUxOut();
                result.setAt(i, uxOut);
            }
            Assert.AreEqual(result.count, n, "Not equal len");
            return result;
        }

        public cipher_SHA256 RandSHA256()
        {
            var sha = new cipher_SHA256();
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(128, b);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_cipher_SumSHA256(b, sha);
            Assert.AreEqual(err, SKY_OK);
            return sha;
        }

        public GoSlice RandBytes(int n)
        {
            var b = new GoSlice();
            var err = SKY_cipher_RandByte(n, b);
            Assert.AreEqual(err, SKY_OK);
            return b;
        }

        public bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.ASCII.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode " + e.Message);
            }
        }

        public SWIGTYPE_p_FeeCalculator feeCalc = skycoin.skycoin.feeCalc();
        public SWIGTYPE_p_FeeCalculator badCalc = skycoin.skycoin.badCalc();
        public SWIGTYPE_p_FeeCalculator fix121 = skycoin.skycoin.fix121();
        public SWIGTYPE_p_FeeCalculator calc = skycoin.skycoin.calcCalc();
        public SWIGTYPE_p_FeeCalculator overflow = skycoin.skycoin.overflow();

        public string base64Decode(string data)
        {
            try
            {
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode " + e.Message);
            }
        }
    }
}