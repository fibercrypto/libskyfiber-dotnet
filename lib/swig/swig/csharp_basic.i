
%inline %{

	GoInt isGoSlice_Eq(GoSlice_* slice1, GoSlice_* slice2)
{
    return (slice1->len == slice2->len) &&
           (memcmp(slice1->data, slice2->data, slice1->len) == 0);
}

#include "json.h"
	//Define function SKY_handle_close to avoid including libskycoin.h
	void SKY_handle_close(Handle p0);

	GoUint32_ zeroFeeCalculator(Transaction__Handle handle, GoUint64_ *pFee, void* context){
  *pFee = 0;
  return 0;
}

	GoUint32_ calcFeeCalculator(Transaction__Handle handle, GoUint64_ *pFee, void* context){
  *pFee = 1;
  return 0;
}

 GoUint32_ fix121FeeCalculator(Transaction__Handle handle, GoUint64_ *pFee, void* context){
  *pFee = 121;
  return 0;
}

 GoUint32_ badFeeCalculator(Transaction__Handle handle, GoUint64_ *pFee, void* context){
  return 0x7FFFFFFF;
}

GoUint32_ overflowFeeCalculator(Transaction__Handle handle, GoUint64_ *pFee, void *context)
{
  *pFee = 0xFFFFFFFFFFFFFFFF;
  return 0;
}

FeeCalculator feeCalc(){
 FeeCalculator feeCalc = {zeroFeeCalculator, NULL};
 return feeCalc;
}

FeeCalculator fix121(){
 FeeCalculator feeCalc = {fix121FeeCalculator, NULL};
 return feeCalc;
}

FeeCalculator badCalc(){
 FeeCalculator feeCalc = {badFeeCalculator, NULL};
 return feeCalc;
}

FeeCalculator calcCalc(){
 FeeCalculator feeCalc = {calcFeeCalculator, NULL};
 return feeCalc;
}

FeeCalculator overflow(){
 FeeCalculator feeCalc = {overflowFeeCalculator, NULL};
 return feeCalc;
}
	int MEMPOOLIDX = 0;
	void *MEMPOOL[1024 * 256];

	int JSONPOOLIDX = 0;
	json_value *JSON_POOL[128];

	int HANDLEPOOLIDX = 0;
	Handle HANDLE_POOL[128];

	typedef struct
	{
		Client__Handle client;
		WalletResponse__Handle wallet;
	} wallet_register;

	int WALLETPOOLIDX = 0;
	wallet_register WALLET_POOL[64];

	int stdout_backup;
	int pipefd[2];

	void *registerMemCleanup(void *p)
	{
		int i;
		for (i = 0; i < MEMPOOLIDX; i++)
		{
			if (MEMPOOL[i] == NULL)
			{
				MEMPOOL[i] = p;
				return p;
			}
		}
		MEMPOOL[MEMPOOLIDX++] = p;
		return p;
	}

	void freeRegisteredMemCleanup(void *p)
	{
		int i;
		for (i = 0; i < MEMPOOLIDX; i++)
		{
			if (MEMPOOL[i] == p)
			{
				free(p);
				MEMPOOL[i] = NULL;
				break;
			}
		}
	}

	int registerJsonFree(void *p)
	{
		int i;
		for (i = 0; i < JSONPOOLIDX; i++)
		{
			if (JSON_POOL[i] == NULL)
			{
				JSON_POOL[i] = p;
				return i;
			}
		}
		JSON_POOL[JSONPOOLIDX++] = p;
		return JSONPOOLIDX - 1;
	}



	int registerWalletClean(Client__Handle clientHandle,
													WalletResponse__Handle walletHandle)
	{
		int i;
		for (i = 0; i < WALLETPOOLIDX; i++)
		{
			if (WALLET_POOL[i].wallet == 0 && WALLET_POOL[i].client == 0)
			{
				WALLET_POOL[i].wallet = walletHandle;
				WALLET_POOL[i].client = clientHandle;
				return i;
			}
		}
		WALLET_POOL[WALLETPOOLIDX].wallet = walletHandle;
		WALLET_POOL[WALLETPOOLIDX].client = clientHandle;
		return WALLETPOOLIDX++;
	}

	int registerHandleClose(Handle handle)
	{
		int i;
		for (i = 0; i < HANDLEPOOLIDX; i++)
		{
			if (HANDLE_POOL[i] == 0)
			{
				HANDLE_POOL[i] = handle;
				return i;
			}
		}
		HANDLE_POOL[HANDLEPOOLIDX++] = handle;
		return HANDLEPOOLIDX - 1;
	}

	void closeRegisteredHandle(Handle handle)
	{
		int i;
		for (i = 0; i < HANDLEPOOLIDX; i++)
		{
			if (HANDLE_POOL[i] == handle)
			{
				HANDLE_POOL[i] = 0;
				SKY_handle_close(handle);
				break;
			}
		}
	}
#include <stdlib.h>
#include <time.h>
#include <stdio.h>
#include <sys/stat.h>
#include <unistd.h>
	void cleanupWallet(Client__Handle client, WalletResponse__Handle wallet)
	{
		int result;
		GoString_ strWalletDir;
		GoString_ strFileName;
		memset(&strWalletDir, 0, sizeof(GoString_));
		memset(&strFileName, 0, sizeof(GoString_));

		result = SKY_api_Handle_Client_GetWalletDir(client, &strWalletDir);
		if (result != 0)
		{
			return;
		}
		result = SKY_api_Handle_Client_GetWalletFileName(wallet, &strFileName);
		if (result != 0)
		{
			free((void *)strWalletDir.p);
			return;
		}
		char fullPath[128];
		if (strWalletDir.n + strFileName.n < 126)
		{
			strcpy(fullPath, strWalletDir.p);
			if (fullPath[0] == 0 || fullPath[strlen(fullPath) - 1] != '/')
				strcat(fullPath, "/");
			strcat(fullPath, strFileName.p);
			result = unlink(fullPath);
			if (strlen(fullPath) < 123)
			{
				strcat(fullPath, ".bak");
				result = unlink(fullPath);
			}
		}
		GoString str = {strFileName.p, strFileName.n};
		result = SKY_api_Client_UnloadWallet(client, str);
		GoString strFullPath = {fullPath, strlen(fullPath)};
		free((void *)strWalletDir.p);
		free((void *)strFileName.p);
	}

	void cleanRegisteredWallet(
			Client__Handle client,
			WalletResponse__Handle wallet)
	{

		int i;
		for (i = 0; i < WALLETPOOLIDX; i++)
		{
			if (WALLET_POOL[i].wallet == wallet && WALLET_POOL[i].client == client)
			{
				WALLET_POOL[i].wallet = 0;
				WALLET_POOL[i].client = 0;
				cleanupWallet(client, wallet);
				return;
			}
		}
	}

	void setup(void)
	{
		srand((unsigned int)time(NULL));
	}


	// TODO: Move to libsky_io.c
	void fprintbuff(FILE * f, void *buff, size_t n)
	{
		unsigned char *ptr = (unsigned char *)buff;
		fprintf(f, "[ ");
		for (; n; --n, ptr++)
		{
			fprintf(f, "%02d ", *ptr);
		}
		fprintf(f, "]");
	}

	int parseBoolean(const char *str, int length)
	{
		int result = 0;
		if (length == 1)
		{
			result = str[0] == '1' || str[0] == 't' || str[0] == 'T';
		}
		else
		{
			result = strncmp(str, "true", length) == 0 ||
							 strncmp(str, "True", length) == 0 ||
							 strncmp(str, "TRUE", length) == 0;
		}
		return result;
	}

	void toGoString(GoString_ * s, GoString * r)
	{
		GoString *tmp = r;

		*tmp = (*(GoString *)s);
	}

	int copySlice(GoSlice_ * pdest, GoSlice_ * psource, int elem_size)
	{
		pdest->len = psource->len;
		pdest->cap = psource->len;
		int size = pdest->len * elem_size;
		pdest->data = malloc(size);
		if (pdest->data == NULL)
			return 1;
		registerMemCleanup(pdest->data);
		memcpy(pdest->data, psource->data, size);
		return 0;
	}

	int concatSlices(GoSlice_ * slice1, GoSlice_ * slice2, int elem_size, GoSlice_ *result)
	{
		int size1 = slice1->len;
		int size2 = slice2->len;
		int size = size1 + size2;
		if (size <= 0)
			return 1;
		void *data = malloc(size * elem_size);
		if (data == NULL)
			return 1;
		registerMemCleanup(data);
		result->data = data;
		result->len = size;
		result->cap = size;
		char *p = data;
		if (size1 > 0)
		{
			memcpy(p, slice1->data, size1 * elem_size);
			p += (elem_size * size1);
		}
		if (size2 > 0)
		{
			memcpy(p, slice2->data, size2 * elem_size);
		}
		return 0;
	}
	void parseJsonMetaData(char *metadata, long long *n, long long *r, long long *p, long long *keyLen)
	{
		*n = *r = *p = *keyLen = 0;
		int length = strlen(metadata);
		int openingQuote = -1;
		const char *keys[] = {"n", "r", "p", "keyLen"};
		int keysCount = 4;
		int keyIndex = -1;
		int startNumber = -1;
		int i;
		int k;
		for (i = 0; i < length; i++)
		{
			if (metadata[i] == '\"')
			{
				startNumber = -1;
				if (openingQuote >= 0)
				{
					keyIndex = -1;
					metadata[i] = 0;
					for (k = 0; k < keysCount; k++)
					{
						if (strcmp(metadata + openingQuote + 1, keys[k]) == 0)
						{
							keyIndex = k;
						}
					}
					openingQuote = -1;
				}
				else
				{
					openingQuote = i;
				}
			}
			else if (metadata[i] >= '0' && metadata[i] <= '9')
			{
				if (startNumber < 0)
					startNumber = i;
			}
			else if (metadata[i] == ',')
			{
				if (startNumber >= 0)
				{
					metadata[i] = 0;
					int number = atoi(metadata + startNumber);
					startNumber = -1;
					if (keyIndex == 0)
						*n = number;
					else if (keyIndex == 1)
						*r = number;
					else if (keyIndex == 2)
						*p = number;
					else if (keyIndex == 3)
						*keyLen = number;
				}
			}
			else
			{
				startNumber = -1;
			}
		}
	}

	int cutSlice(GoSlice_ * slice, int start, int end, int elem_size, GoSlice_ *result)
	{
		int size = end - start;
		if (size <= 0)
			return 1;
		void *data = malloc(size * elem_size);
		if (data == NULL)
			return 1;
		registerMemCleanup(data);
		result->data = data;
		result->len = size;
		result->cap = size;
		char *p = slice->data;
		p += (elem_size * start);
		memcpy(data, p, elem_size * size);
		return 0;
	}

	coin__Transaction *makeEmptyTransaction(Transaction__Handle * handle)
	{
		int result;
		coin__Transaction *ptransaction = NULL;
		result = SKY_coin_Create_Transaction(handle);
		registerHandleClose(*handle);
		result = SKY_coin_GetTransactionObject(*handle, &ptransaction);
		return ptransaction;
	}
	int makeUxBodyWithSecret(coin__UxBody * puxBody, cipher__SecKey * pseckey)
	{
		cipher__PubKey pubkey;
		cipher__Address address;
		int result;

		memset(puxBody, 0, sizeof(coin__UxBody));
		puxBody->Coins = 1000000;
		puxBody->Hours = 100;

		result = SKY_cipher_GenerateKeyPair(&pubkey, pseckey);
		if (result != 0)
		{
			return 1;
		}

		GoSlice slice;
		memset(&slice, 0, sizeof(GoSlice));
		cipher__SHA256 hash;

		result = SKY_cipher_RandByte(128, (coin__UxArray *)&slice);
		registerMemCleanup(slice.data);
		if (result != 0)
		{
			return 1;
		}
		result = SKY_cipher_SumSHA256(slice, &puxBody->SrcTransaction);
		if (result != 0)
		{
			return 1;
		}

		result = SKY_cipher_AddressFromPubKey(&pubkey, &puxBody->Address);
		if (result != 0)
		{
			return 1;
		}
		return result;
	}
	int makeUxOutWithSecret(coin__UxOut * puxOut, cipher__SecKey * pseckey)
	{
		int result;
		memset(puxOut, 0, sizeof(coin__UxOut));
		result = makeUxBodyWithSecret(&puxOut->Body, pseckey);
		puxOut->Head.Time = 100;
		puxOut->Head.BkSeq = 2;
		return result;
	}
	int makeUxOut(coin__UxOut * puxOut)
	{
		cipher__SecKey seckey;
		return makeUxOutWithSecret(puxOut, &seckey);
	}
	int makeUxArray(coin_UxOutArray * parray, int n)
	{
		parray->data = malloc(sizeof(coin__UxOut) * n);
		if (!parray->data)
			return 1;
		registerMemCleanup(parray->data);
		parray->count = parray->count = n;
		coin__UxOut *p = (coin__UxOut *)parray->data;
		int result = 0;
		int i;
		for (i = 0; i < n; i++)
		{
			result = makeUxOut(p);
			if (result != 0)
				break;
			p++;
		}
		return result;
	}
	int makeAddress(cipher__Address * paddress)
	{
		cipher__PubKey pubkey;
		cipher__SecKey seckey;
		cipher__Address address;
		int result;

		result = SKY_cipher_GenerateKeyPair(&pubkey, &seckey);
		if (result != 0)
			return 1;

		result = SKY_cipher_AddressFromPubKey(&pubkey, paddress);
		if (result != 0)
			return 1;
		return result;
	}
	coin__Transaction *makeTransactionFromUxOut(coin__UxOut * puxOut, cipher__SecKey * pseckey, Transaction__Handle * handle)
	{
		int result;
		coin__Transaction *ptransaction = NULL;
		result = SKY_coin_Create_Transaction(handle);
		//   cr_assert(result == SKY_OK, "SKY_coin_Create_Transaction failed");
		registerHandleClose(*handle);
		result = SKY_coin_GetTransactionObject(*handle, &ptransaction);
		//   cr_assert(result == SKY_OK, "SKY_coin_GetTransactionObject failed");
		cipher__SHA256 sha256;
		result = SKY_coin_UxOut_Hash(puxOut, &sha256);
		//   cr_assert(result == SKY_OK, "SKY_coin_UxOut_Hash failed");
		GoUint16 r;
		r = SKY_coin_Transaction_PushInput(*handle, &sha256);

		cipher__Address address1, address2;
		result = makeAddress(&address1);
		//   cr_assert(result == SKY_OK, "makeAddress failed");
		result = makeAddress(&address2);
		//   cr_assert(result == SKY_OK, "makeAddress failed");

		result = SKY_coin_Transaction_PushOutput(*handle, &address1, 1000000, 50);
		//   cr_assert(result == SKY_OK, "SKY_coin_Transaction_PushOutput failed");
		result = SKY_coin_Transaction_PushOutput(*handle, &address2, 5000000, 50);
		//   cr_assert(result == SKY_OK, "SKY_coin_Transaction_PushOutput failed");

		GoSlice secKeys = {pseckey, 1, 1};
		result = SKY_coin_Transaction_SignInputs(*handle, secKeys);
		//   cr_assert(result == SKY_OK, "SKY_coin_Transaction_SignInputs failed");
		result = SKY_coin_Transaction_UpdateHeader(*handle);
		//   cr_assert(result == SKY_OK, "SKY_coin_Transaction_UpdateHeader failed");
		return ptransaction;
	}

	coin__Transaction *makeTransaction(Transaction__Handle * handle)
	{
		int result;
		coin__UxOut uxOut;
		cipher__SecKey seckey;
		coin__Transaction *ptransaction = NULL;
		result = makeUxOutWithSecret(&uxOut, &seckey);
		ptransaction = makeTransactionFromUxOut(&uxOut, &seckey, handle);
		return ptransaction;
	}

	int makeTransactions(int n, Transactions__Handle *handle)
	{
		int result = SKY_coin_Create_Transactions(handle);
		if (result != 0)
			return 1;
		registerHandleClose(*handle);
		int i;
		for (i = 0; i < n; i++)
		{
			Transaction__Handle thandle;
			makeTransaction(&thandle);
			registerHandleClose(thandle);
			result = SKY_coin_Transactions_Add(*handle, thandle);
			if (result != 0)
				return 1;
		}
		return result;
	}

	// Base 64

	int b64_int(unsigned int ch)
	{
		if (ch == 43)
			return 62;
		if (ch == 47)
			return 63;
		if (ch == 61)
			return 64;
		if ((ch > 47) && (ch < 58))
			return ch + 4;
		if ((ch > 64) && (ch < 91))
			return ch - 'A';
		if ((ch > 96) && (ch < 123))
			return (ch - 'a') + 26;
		return -1;
	}

	int b64_decode(const unsigned char *in, unsigned int in_len, unsigned char *out)
	{

		unsigned int i = 0, j = 0, k = 0, s[4];
		for (i = 0; i < in_len; i++)
		{
			int n = b64_int(*(in + i));
			if (n < 0)
				return -1;
			s[j++] = n;
			if (j == 4)
			{
				out[k + 0] = ((s[0] & 255) << 2) + ((s[1] & 0x30) >> 4);
				if (s[2] != 64)
				{
					out[k + 1] = ((s[1] & 0x0F) << 4) + ((s[2] & 0x3C) >> 2);
					if ((s[3] != 64))
					{
						out[k + 2] = ((s[2] & 0x03) << 6) + (s[3]);
						k += 3;
					}
					else
					{
						k += 2;
					}
				}
				else
				{
					k += 1;
				}
				j = 0;
			}
		}

		return k;
	}

	int DecodeBase64(GoSlice encrypted, GoString_ * outs)
	{
		char encryptedText[1024];
		int decode_length = b64_decode((unsigned char *)encrypted.data,
																	 encrypted.len, encryptedText);

		outs->p=encryptedText;
		outs->n = decode_length;
		return decode_length;
	}

int putUvarint(GoSlice* buf , GoUint64 x){
	int i = 0;
	while( x >= 0x80 && i < buf->cap) {
		((unsigned char*)buf->data)[i] = ((GoUint8)x) | 0x80;
		x >>= 7;
		i++;
	}
	if( i < buf->cap ){
		((unsigned char*)buf->data)[i] = (GoUint8)(x);
		buf->len = i + 1;
	} else {
		buf->len = i;
	}
	return buf->len;
}

int putVarint(GoSlice* buf , GoInt64 x){
	GoUint64 ux = (GoUint64)x << 1;
	if ( x < 0 ) {
		ux = ~ux;
	}
	return putUvarint(buf, ux);
}

void hashKeyIndexNonce(GoSlice_ key, GoInt64 index,
	cipher__SHA256 *nonceHash, cipher__SHA256 *resultHash){
	GoUint32 errcode;
	int length = 32 + sizeof(cipher__SHA256);
	unsigned char buff[length];
	GoSlice slice = {buff, 0, length};
	memset(buff, 0, length * sizeof(char));
	putVarint( &slice, index );
	memcpy(buff + 32, *nonceHash, sizeof(cipher__SHA256));
	slice.len = length;
	cipher__SHA256 indexNonceHash;
	errcode = SKY_cipher_SumSHA256(slice, &indexNonceHash);
	SKY_cipher_AddSHA256(key.data, &indexNonceHash, resultHash);
}

void convertGoUint8toSHA256(GoUint8_* __in, cipher_SHA256* __out){
memcpy(__out->data, __in, 32);
}

GoInt isPrivateKeyEq(PrivateKey__Handle handle1, PrivateKey__Handle handle2)
{
    GoUint8 bufferVersion1[1024];
    GoUint8 bufferVersion2[1024];
    GoSlice_ Version1 = {bufferVersion1, 0, 1024};
    GoSlice_ Version2 = {bufferVersion2, 0, 1024};
    GoUint8 bufferParentFingerprint1[1024];
    GoUint8 bufferParentFingerprint2[1024];
    GoSlice_ ParentFingerprint1 = {bufferParentFingerprint1, 0, 1024};
    GoSlice_ ParentFingerprint2 = {bufferParentFingerprint2, 0, 1024};
    GoUint32 childNumber1;
    GoUint32 childNumber2;
    GoUint8 bufferChainCode1[1024];
    GoUint8 bufferChainCode2[1024];
    GoSlice_ ChainCode1 = {bufferChainCode1, 0, 1024};
    GoSlice_ ChainCode2 = {bufferChainCode2, 0, 1024};
    GoUint8 bufferKey1[1024];
    GoUint8 bufferKey2[1024];
    GoSlice_ Key1 = {bufferKey1, 0, 1024};
    GoSlice_ Key2 = {bufferKey2, 0, 1024};
    GoUint8 Depth1;
    GoUint8 Depth2;

    GoUint32 err = SKY_bip32_PrivateKey_GetVersion(handle1, &Version1);
    err = SKY_bip32_PrivateKey_GetVersion(handle2, &Version2);
    if (!isGoSlice_Eq(&Version1, &Version2)) {
        printf("Version not equal\n");
        return 0;
    }

    err = SKY_bip32_PrivateKey_GetDepth(handle1, &Depth1);
    err = SKY_bip32_PrivateKey_GetDepth(handle2, &Depth2);
    if (Depth1 != Depth2) {
        printf("Depth not equal\n");
        return 0;
    }

    err = SKY_bip32_PrivateKey_GetParentFingerprint(handle1, &ParentFingerprint1);
    err = SKY_bip32_PrivateKey_GetParentFingerprint(handle2, &ParentFingerprint2);
    if (!isGoSlice_Eq(&ParentFingerprint1, &ParentFingerprint2)) {
        printf("ParentFingerprint not equal\n");
        return 0;
    }

    err = SKY_bip32_PrivateKey_ChildNumber(handle1, &childNumber1);
    err = SKY_bip32_PrivateKey_ChildNumber(handle2, &childNumber2);
    if (childNumber1 != childNumber2) {
        printf("childNumber not equal\n");
        return 0;
    }

    err = SKY_bip32_PrivateKey_GetChainCode(handle1, &ChainCode1);
    err = SKY_bip32_PrivateKey_GetChainCode(handle2, &ChainCode2);
    if (!isGoSlice_Eq(&ChainCode1, &ChainCode2)) {
        printf("ChainCode not equal\n");
        return 0;
    }

    err = SKY_bip32_PrivateKey_GetKey(handle1, &Key1);
    err = SKY_bip32_PrivateKey_GetKey(handle2, &Key2);
    if (!isGoSlice_Eq(&Key1, &Key2)) {
        printf("Key not equal\n");
        return 0;
    }

    return 1;
}

GoInt isGoSliceEq(GoSlice* slice1, GoSlice* slice2)
{
    return (slice1->len == slice2->len) &&
           (memcmp(slice1->data, slice2->data, slice1->len) == 0);
}



GoInt isPublicKeyEq(PublicKey__Handle handle1, PublicKey__Handle handle2)
{
    GoUint8 bufferVersion1[1024];
    GoUint8 bufferVersion2[1024];
    GoSlice_ Version1 = {bufferVersion1, 0, 1024};
    GoSlice_ Version2 = {bufferVersion2, 0, 1024};
    GoUint8 bufferParentFingerprint1[1024];
    GoUint8 bufferParentFingerprint2[1024];
    GoSlice_ ParentFingerprint1 = {bufferParentFingerprint1, 0, 1024};
    GoSlice_ ParentFingerprint2 = {bufferParentFingerprint2, 0, 1024};
    GoUint32 childNumber1;
    GoUint32 childNumber2;
    GoUint8 bufferChainCode1[1024];
    GoUint8 bufferChainCode2[1024];
    GoSlice_ ChainCode1 = {bufferChainCode1, 0, 1024};
    GoSlice_ ChainCode2 = {bufferChainCode2, 0, 1024};
    GoUint8 bufferKey1[1024];
    GoUint8 bufferKey2[1024];
    GoSlice_ Key1 = {bufferKey1, 0, 1024};
    GoSlice_ Key2 = {bufferKey2, 0, 1024};
    GoUint8 Depth1;
    GoUint8 Depth2;

    GoUint32 err = SKY_bip32_PublicKey_GetVersion(handle1, &Version1);
    err = SKY_bip32_PublicKey_GetVersion(handle2, &Version2);
    if (!isGoSlice_Eq(&Version1, &Version2)) {
        return 0;
    }

    err = SKY_bip32_PublicKey_GetDepth(handle1, &Depth1);
    
    err = SKY_bip32_PublicKey_GetDepth(handle2, &Depth2);
    
    if (Depth1 != Depth2) {
        return 0;
    }

    err = SKY_bip32_PublicKey_GetParentFingerprint(handle1, &ParentFingerprint1);
    
    err = SKY_bip32_PublicKey_GetParentFingerprint(handle2, &ParentFingerprint2);
    
    if (!isGoSlice_Eq(&ParentFingerprint1, &ParentFingerprint2)) {
        return 0;
    }

    err = SKY_bip32_PublicKey_ChildNumber(handle1, &childNumber1);
    
    err = SKY_bip32_PublicKey_ChildNumber(handle2, &childNumber2);
    
    if (childNumber1 != childNumber2) {
        return 0;
    }

    err = SKY_bip32_PublicKey_GetChainCode(handle1, &ChainCode1);
    
    err = SKY_bip32_PublicKey_GetChainCode(handle2, &ChainCode2);
    
    if (!isGoSlice_Eq(&ChainCode1, &ChainCode2)) {
        return 0;
    }

    err = SKY_bip32_PublicKey_GetKey(handle1, &Key1);
    
    err = SKY_bip32_PublicKey_GetKey(handle2, &Key2);
    
    if (!isGoSlice_Eq(&Key1, &Key2)) {
        return 0;
    }

    return 1;
}
	%}