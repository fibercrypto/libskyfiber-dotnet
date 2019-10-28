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

        public void makeUxBodyWithSecret(coin__UxBody uxBody, cipher_SecKey secKey)
        {
            var p = new cipher_PubKey();
            var err = SKY_cipher_GenerateKeyPair(p, secKey);
            Assert.AreEqual(err, SKY_OK);
            var SrcTransaction = new cipher_SHA256();
            SKY_testutil_RandSHA256(SrcTransaction);
            uxBody.SetSrcTransaction(SrcTransaction);
            var Address = new cipher__Address();
            err = SKY_cipher_AddressFromPubKey(p, Address);
            Assert.AreEqual(err, SKY_OK);
            uxBody.Address = Address;
            uxBody.Coins = (ulong)(1e6);
            uxBody.Hours = 100;
        }

        public coin__UxOut makeUxOutWithSecret(cipher_SecKey secKey)
        {
            var body = new coin__UxBody();
            makeUxBodyWithSecret(body, secKey);
            var head = new coin__UxHead();
            head.Time = 100;
            head.BkSeq = 2;
            var uxOut = new coin__UxOut();
            uxOut.Head = head;
            uxOut.Body = body;
            return uxOut;
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

        public int isTransactionHandleEq(SWIGTYPE_p_Transaction__Handle handle1, SWIGTYPE_p_Transaction__Handle handle2)
        {

            var len1 = new_GoUint32Ptr();
            var len2 = new_GoUint32Ptr();
            var type1 = new_GoUint8Ptr();
            var type2 = new_GoUint8Ptr();
            var sha1 = new cipher_SHA256();
            var sha2 = new cipher_SHA256();
            var p1 = new GoSlice();
            var p2 = new GoSlice();

            var err = SKY_coin_Transaction_GetLength(handle1, len1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetLength(handle2, len2);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetType(handle1, type1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetType(handle2, type2);
            Assert.AreEqual(err, SKY_OK);

            if (GoUint32Ptr_value(len1) != GoUint32Ptr_value(len2) || GoUint8Ptr_value(type1) != GoUint8Ptr_value(type2))
            {
                return 0;
            }
            err = SKY_coin_Transaction_GetInnerHash(handle1, sha1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetInnerHash(handle2, sha2);
            Assert.AreEqual(err, SKY_OK);
            if (sha1.isEqual(sha2) != 1)
                return 0;

            err = SKY_coin_Transaction_GetSigs(handle1, p1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetSigs(handle2, p2);
            Assert.AreEqual(err, SKY_OK);
            if (p1.isEqual(p2) != 1)
                return 0;
            err = SKY_coin_Transaction_GetIn(handle1, p1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetIn(handle2, p2);
            Assert.AreEqual(err, SKY_OK);
            if (p1.isEqual(p2) != 1)
                return 0;
            err = SKY_coin_Transaction_GetOut(handle1, p1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_coin_Transaction_GetOut(handle2, p2);
            Assert.AreEqual(err, SKY_OK);
            if (p1.isEqual(p2) != 1)
                return 0;
            return 1;
        }

        public uint isPrivateKeyEq(SWIGTYPE_p_PrivateKey__Handle handle1, SWIGTYPE_p_PrivateKey__Handle handle2)
        {
            var Version1 = new GoSlice();
            var Version2 = new GoSlice();
            var ParentFingerprint1 = new GoSlice();
            var ParentFingerprint2 = new GoSlice();
            var childNumber1 = new_GoUint32Ptr();
            var childNumber2 = new_GoUint32Ptr();
            var ChainCode1 = new GoSlice();
            var ChainCode2 = new GoSlice();
            var Key1 = new GoSlice();
            var Key2 = new GoSlice();
            var Depth1 = new_GoUint8Ptr();
            var Depth2 = new_GoUint8Ptr();

            var err = SKY_bip32_PrivateKey_GetVersion(handle1, Version1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_GetVersion(handle2, Version2);
            Assert.AreEqual(err, SKY_OK);
            if (Version1.isEqual(Version2) != 1)
            {
                Console.WriteLine("Version not equal\n");
                return 0;
            }

            err = SKY_bip32_PrivateKey_GetDepth(handle1, Depth1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_GetDepth(handle2, Depth2);
            Assert.AreEqual(err, SKY_OK);
            if (GoUint8Ptr_value(Depth1) != GoUint8Ptr_value(Depth2))
            {
                Console.WriteLine("Depth not equal\n");
                return 0;
            }

            // err = SKY_bip32_PrivateKey_GetParentFingerprint(handle1, ParentFingerprint1);
            // Assert.AreEqual(err, SKY_OK);
            // err = SKY_bip32_PrivateKey_GetParentFingerprint(handle2, ParentFingerprint2);
            // Assert.AreEqual(err, SKY_OK);
            // if (ParentFingerprint1.isEqual(ParentFingerprint2) == 0)
            // {
            //     Console.WriteLine("ParentFingerprint not equal\n");
            //     return 0;
            // }

            err = SKY_bip32_PrivateKey_ChildNumber(handle1, childNumber1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_ChildNumber(handle2, childNumber2);
            Assert.AreEqual(err, SKY_OK);
            if (GoUint32Ptr_value(childNumber1) != GoUint32Ptr_value(childNumber2))
            {
                Console.WriteLine("childNumber not equal\n");
                return 0;
            }

            err = SKY_bip32_PrivateKey_GetChainCode(handle1, ChainCode1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_GetChainCode(handle2, ChainCode2);
            Assert.AreEqual(err, SKY_OK);
            if (ChainCode1.isEqual(ChainCode2) != 1)
            {
                Console.WriteLine("ChainCode not equal\n");
                return 0;
            }

            err = SKY_bip32_PrivateKey_GetKey(handle1, Key1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PrivateKey_GetKey(handle2, Key2);
            Assert.AreEqual(err, SKY_OK);
            if (Key1.isEqual(Key2) != 1)
            {
                Console.WriteLine("Key not equal\n");
                return 0;
            }

            return 1;
        }

        public int isPublicKeyEq(SWIGTYPE_p_PublicKey__Handle handle1, SWIGTYPE_p_PublicKey__Handle handle2)
        {
            var Version1 = new GoSlice();
            var Version2 = new GoSlice();
            var ParentFingerprint1 = new GoSlice();
            var ParentFingerprint2 = new GoSlice();
            var childNumber1 = new_GoUint32Ptr();
            var childNumber2 = new_GoUint32Ptr();
            var ChainCode1 = new GoSlice();
            var ChainCode2 = new GoSlice();
            var Key1 = new GoSlice();
            var Key2 = new GoSlice();
            var Depth1 = new_GoUint8Ptr();
            var Depth2 = new_GoUint8Ptr();

            var err = SKY_bip32_PublicKey_GetVersion(handle1, Version1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetVersion(handle2, Version2);
            Assert.AreEqual(err, SKY_OK);
            if (Version1.isEqual(Version2) != 1)
            {
                Console.WriteLine("Version not equal\n");
                return 0;
            }

            err = SKY_bip32_PublicKey_GetDepth(handle1, Depth1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetDepth(handle2, Depth2);
            Assert.AreEqual(err, SKY_OK);
            if (GoUint8Ptr_value(Depth1) != GoUint8Ptr_value(Depth2))
            {
                Console.WriteLine("Depth not equal\n");
                return 0;
            }

            err = SKY_bip32_PublicKey_GetParentFingerprint(handle1, ParentFingerprint1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetParentFingerprint(handle2, ParentFingerprint2);
            Assert.AreEqual(err, SKY_OK);
            if (ParentFingerprint1.isEqual(ParentFingerprint2) != 1)
            {
                Console.WriteLine("ParentFingerprint not equal\n");
                return 0;
            }

            err = SKY_bip32_PublicKey_ChildNumber(handle1, childNumber1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_ChildNumber(handle2, childNumber2);
            Assert.AreEqual(err, SKY_OK);
            if (GoUint32Ptr_value(childNumber1) != GoUint32Ptr_value(childNumber2))
            {
                return 0;
            }

            err = SKY_bip32_PublicKey_GetChainCode(handle1, ChainCode1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetChainCode(handle2, ChainCode2);
            Assert.AreEqual(err, SKY_OK);
            if (ChainCode1.isEqual(ChainCode2) != 1)
            {
                Console.WriteLine("ChainCode not equal\n");
                return 0;
            }

            err = SKY_bip32_PublicKey_GetKey(handle1, Key1);
            Assert.AreEqual(err, SKY_OK);
            err = SKY_bip32_PublicKey_GetKey(handle2, Key2);
            Assert.AreEqual(err, SKY_OK);
            if (Key1.isEqual(Key2) != 1)
            {
                Console.WriteLine("Key not equal\n");
                return 0;
            }

            return 1;
        }
    }
}