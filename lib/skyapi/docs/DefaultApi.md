# Skyapi.Api.DefaultApi

All URIs are relative to *http://127.0.0.1:6420*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddressCount**](DefaultApi.md#addresscount) | **GET** /api/v1/addresscount | Returns the total number of unique address that have coins.
[**AddressUxouts**](DefaultApi.md#addressuxouts) | **GET** /api/v1/address_uxouts | 
[**ApiV1RawtxGet**](DefaultApi.md#apiv1rawtxget) | **GET** /api/v1/rawtx | 
[**ApiV2MetricsGet**](DefaultApi.md#apiv2metricsget) | **GET** /api/v2/metrics | 
[**BalanceGet**](DefaultApi.md#balanceget) | **GET** /api/v1/balance | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**BalancePost**](DefaultApi.md#balancepost) | **POST** /api/v1/balance | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**Block**](DefaultApi.md#block) | **GET** /api/v1/block | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**BlockchainMetadata**](DefaultApi.md#blockchainmetadata) | **GET** /api/v1/blockchain/metadata | Returns the blockchain metadata.
[**BlockchainProgress**](DefaultApi.md#blockchainprogress) | **GET** /api/v1/blockchain/progress | Returns the blockchain sync progress.
[**Blocks**](DefaultApi.md#blocks) | **GET** /api/v1/blocks | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**CoinSupply**](DefaultApi.md#coinsupply) | **GET** /api/v1/coinSupply | 
[**Csrf**](DefaultApi.md#csrf) | **GET** /api/v1/csrf | Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
[**DataDELETE**](DefaultApi.md#datadelete) | **DELETE** /api/v2/data | 
[**DataGET**](DefaultApi.md#dataget) | **GET** /api/v2/data | 
[**DataPOST**](DefaultApi.md#datapost) | **POST** /api/v2/data | 
[**DefaultConnections**](DefaultApi.md#defaultconnections) | **GET** /api/v1/network/defaultConnections | defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.
[**Health**](DefaultApi.md#health) | **GET** /api/v1/health | Returns node health data.
[**LastBlocks**](DefaultApi.md#lastblocks) | **GET** /api/v1/last_blocks | 
[**NetworkConnection**](DefaultApi.md#networkconnection) | **GET** /api/v1/network/connection | This endpoint returns a specific connection.
[**NetworkConnections**](DefaultApi.md#networkconnections) | **GET** /api/v1/network/connections | This endpoint returns all outgoings connections.
[**NetworkConnectionsDisconnect**](DefaultApi.md#networkconnectionsdisconnect) | **POST** /api/v1/network/connection/disconnect | 
[**NetworkConnectionsExchange**](DefaultApi.md#networkconnectionsexchange) | **GET** /api/v1/network/connections/exchange | 
[**NetworkConnectionsTrust**](DefaultApi.md#networkconnectionstrust) | **GET** /api/v1/network/connections/trust | trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.
[**OutputsGet**](DefaultApi.md#outputsget) | **GET** /api/v1/outputs | If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
[**OutputsPost**](DefaultApi.md#outputspost) | **POST** /api/v1/outputs | If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
[**PendingTxs**](DefaultApi.md#pendingtxs) | **GET** /api/v1/pendingTxs | 
[**ResendUnconfirmedTxns**](DefaultApi.md#resendunconfirmedtxns) | **POST** /api/v1/resendUnconfirmedTxns | 
[**Richlist**](DefaultApi.md#richlist) | **GET** /api/v1/richlist | Returns the top skycoin holders.
[**Transaction**](DefaultApi.md#transaction) | **GET** /api/v1/transaction | 
[**TransactionInject**](DefaultApi.md#transactioninject) | **POST** /api/v1/injectTransaction | Broadcast a hex-encoded, serialized transaction to the network.
[**TransactionPost**](DefaultApi.md#transactionpost) | **POST** /api/v2/transaction | 
[**TransactionPostUnspent**](DefaultApi.md#transactionpostunspent) | **POST** /api/v2/transaction/unspent | 
[**TransactionRaw**](DefaultApi.md#transactionraw) | **GET** /api/v2/transaction/raw | Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.
[**TransactionVerify**](DefaultApi.md#transactionverify) | **POST** /api/v2/transaction/verify | 
[**TransactionsGet**](DefaultApi.md#transactionsget) | **GET** /api/v1/transactions | Returns transactions that match the filters.
[**TransactionsPost**](DefaultApi.md#transactionspost) | **POST** /api/v1/transactions | Returns transactions that match the filters.
[**Uxout**](DefaultApi.md#uxout) | **GET** /api/v1/uxout | Returns an unspent output by ID.
[**VerifyAddress**](DefaultApi.md#verifyaddress) | **POST** /api/v2/address/verify | Verifies a Skycoin address.
[**Version**](DefaultApi.md#version) | **GET** /api/v1/version | 
[**Wallet**](DefaultApi.md#wallet) | **GET** /api/v1/wallet | Returns a wallet by id.
[**WalletBalance**](DefaultApi.md#walletbalance) | **GET** /api/v1/wallet/balance | Returns the wallet&#39;s balance, both confirmed and predicted.  The predicted balance is the confirmed balance minus the pending spends.
[**WalletCreate**](DefaultApi.md#walletcreate) | **POST** /api/v1/wallet/create | 
[**WalletDecrypt**](DefaultApi.md#walletdecrypt) | **POST** /api/v1/wallet/decrypt | Decrypts wallet.
[**WalletEncrypt**](DefaultApi.md#walletencrypt) | **POST** /api/v1/wallet/encrypt | Encrypt wallet.
[**WalletFolder**](DefaultApi.md#walletfolder) | **GET** /api/v1/wallets/folderName | 
[**WalletNewAddress**](DefaultApi.md#walletnewaddress) | **POST** /api/v1/wallet/newAddress | 
[**WalletNewSeed**](DefaultApi.md#walletnewseed) | **GET** /api/v1/wallet/newSeed | 
[**WalletRecover**](DefaultApi.md#walletrecover) | **POST** /api/v2/wallet/recover | Recovers an encrypted wallet by providing the seed. The first address will be generated from seed and compared to the first address of the specified wallet. If they match, the wallet will be regenerated with an optional password. If the wallet is not encrypted, an error is returned.
[**WalletSeed**](DefaultApi.md#walletseed) | **POST** /api/v1/wallet/seed | This endpoint only works for encrypted wallets. If the wallet is unencrypted, The seed will be not returned.
[**WalletSeedVerify**](DefaultApi.md#walletseedverify) | **POST** /api/v2/wallet/seed/verify | Verifies a wallet seed.
[**WalletTransaction**](DefaultApi.md#wallettransaction) | **POST** /api/v1/wallet/transaction | Creates a signed transaction
[**WalletTransactionSign**](DefaultApi.md#wallettransactionsign) | **POST** /api/v2/wallet/transaction/sign | Creates a signed transaction
[**WalletTransactions**](DefaultApi.md#wallettransactions) | **GET** /api/v1/wallet/transactions | 
[**WalletUnload**](DefaultApi.md#walletunload) | **POST** /api/v1/wallet/unload | Unloads wallet from the wallet service.
[**WalletUpdate**](DefaultApi.md#walletupdate) | **POST** /api/v1/wallet/update | Update the wallet.
[**Wallets**](DefaultApi.md#wallets) | **GET** /api/v1/wallets | 



## AddressCount

> InlineResponse200 AddressCount ()

Returns the total number of unique address that have coins.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class AddressCountExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // Returns the total number of unique address that have coins.
                InlineResponse200 result = apiInstance.AddressCount();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.AddressCount: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**InlineResponse200**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | addressCount response object |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## AddressUxouts

> List&lt;Object&gt; AddressUxouts (string address)



Returns the historical, spent outputs associated with an address

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class AddressUxoutsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var address = address_example;  // string | address to filter by

            try
            {
                List<Object> result = apiInstance.AddressUxouts(address);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.AddressUxouts: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**| address to filter by | 

### Return type

**List<Object>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return address uxouts |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ApiV1RawtxGet

> string ApiV1RawtxGet ()



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class ApiV1RawtxGetExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                string result = apiInstance.ApiV1RawtxGet();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.ApiV1RawtxGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response is araw transaction by id |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ApiV2MetricsGet

> string ApiV2MetricsGet ()



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class ApiV2MetricsGetExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                string result = apiInstance.ApiV2MetricsGet();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.ApiV2MetricsGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Metrics |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## BalanceGet

> Object BalanceGet (string addrs)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BalanceGetExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var addrs = addrs_example;  // string | command separated list of addresses

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                Object result = apiInstance.BalanceGet(addrs);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.BalanceGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addrs** | **string**| command separated list of addresses | 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the balance of one or more addresses |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## BalancePost

> Object BalancePost (string addrs)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BalancePostExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var addrs = addrs_example;  // string | command separated list of addresses

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                Object result = apiInstance.BalancePost(addrs);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.BalancePost: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addrs** | **string**| command separated list of addresses | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the balance of one or more addresses |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Block

> List&lt;BlockSchema&gt; Block (string hash = null, int? seq = null)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BlockExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var hash = hash_example;  // string | get block by hash (optional) 
            var seq = 56;  // int? | get block by sequence number (optional) 

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                List<BlockSchema> result = apiInstance.Block(hash, seq);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Block: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **hash** | **string**| get block by hash | [optional] 
 **seq** | **int?**| get block by sequence number | [optional] 

### Return type

[**List&lt;BlockSchema&gt;**](BlockSchema.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return block Array |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## BlockchainMetadata

> Object BlockchainMetadata ()

Returns the blockchain metadata.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BlockchainMetadataExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // Returns the blockchain metadata.
                Object result = apiInstance.BlockchainMetadata();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.BlockchainMetadata: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns the blockchain metadata. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## BlockchainProgress

> Object BlockchainProgress ()

Returns the blockchain sync progress.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BlockchainProgressExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // Returns the blockchain sync progress.
                Object result = apiInstance.BlockchainProgress();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.BlockchainProgress: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns the blockchain sync progress |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Blocks

> InlineResponse2001 Blocks (int? start = null, int? end = null, List<int?> seq = null)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class BlocksExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var start = 56;  // int? | start seq (optional) 
            var end = 56;  // int? | end seq (optional) 
            var seq = new List<int?>(); // List<int?> | comma-separated list of block seqs (optional) 

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                InlineResponse2001 result = apiInstance.Blocks(start, end, seq);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Blocks: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **start** | **int?**| start seq | [optional] 
 **end** | **int?**| end seq | [optional] 
 **seq** | [**List&lt;int?&gt;**](int?.md)| comma-separated list of block seqs | [optional] 

### Return type

[**InlineResponse2001**](InlineResponse2001.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Get blocks in specific range |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## CoinSupply

> InlineResponse2002 CoinSupply ()



coinSupplyHandler returns coin distribution supply stats

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class CoinSupplyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                InlineResponse2002 result = apiInstance.CoinSupply();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.CoinSupply: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**InlineResponse2002**](InlineResponse2002.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | CoinSupply records the coin supply info. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Csrf

> InlineResponse2003 Csrf ()

Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class CsrfExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
                InlineResponse2003 result = apiInstance.Csrf();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Csrf: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**InlineResponse2003**](InlineResponse2003.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return a csrf Token. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DataDELETE

> void DataDELETE (string type = null, string key = null)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class DataDELETEExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var type = type_example;  // string | storage type. (optional) 
            var key = key_example;  // string | key of the specific value to get. (optional) 

            try
            {
                apiInstance.DataDELETE(type, key);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.DataDELETE: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **type** | **string**| storage type. | [optional] 
 **key** | **string**| key of the specific value to get. | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns empty json |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DataGET

> Object DataGET (string type = null, string key = null)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class DataGETExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var type = type_example;  // string | storage type. (optional) 
            var key = key_example;  // string | key of the specific value to get. (optional) 

            try
            {
                Object result = apiInstance.DataGET(type, key);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.DataGET: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **type** | **string**| storage type. | [optional] 
 **key** | **string**| key of the specific value to get. | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Return multiKey |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DataPOST

> void DataPOST (string type = null, string key = null, string val = null)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class DataPOSTExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var type = type_example;  // string | storage type. (optional) 
            var key = key_example;  // string | key of the specific value to get. (optional) 
            var val = val_example;  // string | additional value. (optional) 

            try
            {
                apiInstance.DataPOST(type, key, val);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.DataPOST: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **type** | **string**| storage type. | [optional] 
 **key** | **string**| key of the specific value to get. | [optional] 
 **val** | **string**| additional value. | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns empty json |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## DefaultConnections

> List&lt;string&gt; DefaultConnections ()

defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class DefaultConnectionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.
                List<string> result = apiInstance.DefaultConnections();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.DefaultConnections: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**List<string>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return an list of default connections. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Health

> Object Health ()

Returns node health data.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class HealthExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // Returns node health data.
                Object result = apiInstance.Health();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Health: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns node health data. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## LastBlocks

> Object LastBlocks (int? num)



Returns the most recent N blocks on the blockchain

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class LastBlocksExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var num = 56;  // int? | Num of blockss

            try
            {
                Object result = apiInstance.LastBlocks(num);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.LastBlocks: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **num** | **int?**| Num of blockss | 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the most recent N blocks on the blockchain |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## NetworkConnection

> NetworkConnectionSchema NetworkConnection (string addr)

This endpoint returns a specific connection.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class NetworkConnectionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var addr = addr_example;  // string | Address port

            try
            {
                // This endpoint returns a specific connection.
                NetworkConnectionSchema result = apiInstance.NetworkConnection(addr);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnection: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addr** | **string**| Address port | 

### Return type

[**NetworkConnectionSchema**](NetworkConnectionSchema.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return a connection struct |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## NetworkConnections

> InlineResponse2004 NetworkConnections (string states = null, string direction = null)

This endpoint returns all outgoings connections.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class NetworkConnectionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var states = states_example;  // string | Connection status. (optional) 
            var direction = direction_example;  // string | Direction of the connection. (optional) 

            try
            {
                // This endpoint returns all outgoings connections.
                InlineResponse2004 result = apiInstance.NetworkConnections(states, direction);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnections: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **states** | **string**| Connection status. | [optional] 
 **direction** | **string**| Direction of the connection. | [optional] 

### Return type

[**InlineResponse2004**](InlineResponse2004.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return networks connections |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## NetworkConnectionsDisconnect

> void NetworkConnectionsDisconnect (string id)



This endpoint disconnects a connection by ID or address

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class NetworkConnectionsDisconnectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Address id.

            try
            {
                apiInstance.NetworkConnectionsDisconnect(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsDisconnect: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Address id. | 

### Return type

void (empty response body)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## NetworkConnectionsExchange

> List&lt;string&gt; NetworkConnectionsExchange ()



This endpoint returns all connections found through peer exchange

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class NetworkConnectionsExchangeExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                List<string> result = apiInstance.NetworkConnectionsExchange();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsExchange: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**List<string>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return a list of all connections found through peer exchange. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## NetworkConnectionsTrust

> List&lt;string&gt; NetworkConnectionsTrust ()

trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class NetworkConnectionsTrustExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                // trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.
                List<string> result = apiInstance.NetworkConnectionsTrust();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsTrust: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**List<string>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return a list of trusted connections. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## OutputsGet

> Object OutputsGet (List<string> address = null, List<string> hash = null)

If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class OutputsGetExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var address = new List<string>(); // List<string> |  (optional) 
            var hash = new List<string>(); // List<string> |  (optional) 

            try
            {
                // If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
                Object result = apiInstance.OutputsGet(address, hash);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.OutputsGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | [**List&lt;string&gt;**](string.md)|  | [optional] 
 **hash** | [**List&lt;string&gt;**](string.md)|  | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | UnspentOutputsSummary records unspent outputs in different status. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## OutputsPost

> Object OutputsPost (string address = null, string hash = null)

If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class OutputsPostExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var address = address_example;  // string |  (optional) 
            var hash = hash_example;  // string |  (optional) 

            try
            {
                // If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
                Object result = apiInstance.OutputsPost(address, hash);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.OutputsPost: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**|  | [optional] 
 **hash** | **string**|  | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | UnspentOutputsSummary records unspent outputs in different status. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## PendingTxs

> List&lt;InlineResponse20010&gt; PendingTxs ()



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class PendingTxsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                List<InlineResponse20010> result = apiInstance.PendingTxs();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.PendingTxs: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**List&lt;InlineResponse20010&gt;**](InlineResponse20010.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Transaction inputs include the owner address, coins, hours and calculated hours. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## ResendUnconfirmedTxns

> Object ResendUnconfirmedTxns ()



Broadcasts all unconfirmed transactions from the unconfirmed transaction pool

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class ResendUnconfirmedTxnsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                Object result = apiInstance.ResendUnconfirmedTxns();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.ResendUnconfirmedTxns: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application-json, application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK, Broadcasts all unconfirmed transactions from the unconfirmed transaction pool |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Richlist

> Object Richlist (bool? includeDistribution = null, string n = null)

Returns the top skycoin holders.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class RichlistExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var includeDistribution = true;  // bool? | include distribution addresses or not, default value false (optional) 
            var n = n_example;  // string | include distribution addresses or not, default value false (optional) 

            try
            {
                // Returns the top skycoin holders.
                Object result = apiInstance.Richlist(includeDistribution, n);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Richlist: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **includeDistribution** | **bool?**| include distribution addresses or not, default value false | [optional] 
 **n** | **string**| include distribution addresses or not, default value false | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Represent richlist response |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Transaction

> Transaction Transaction (string txid)



Returns a transaction identified by its txid hash with just id

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var txid = txid_example;  // string | transaction Id

            try
            {
                Transaction result = apiInstance.Transaction(txid);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Transaction: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **txid** | **string**| transaction Id | 

### Return type

[**Transaction**](Transaction.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns a transaction identified by its txid hash. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionInject

> string TransactionInject (string rawtx, bool? noBroadcast = null)

Broadcast a hex-encoded, serialized transaction to the network.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionInjectExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var rawtx = rawtx_example;  // string | hex-encoded serialized transaction string.
            var noBroadcast = true;  // bool? | Disable the network broadcast (optional) 

            try
            {
                // Broadcast a hex-encoded, serialized transaction to the network.
                string result = apiInstance.TransactionInject(rawtx, noBroadcast);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionInject: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **rawtx** | **string**| hex-encoded serialized transaction string. | 
 **noBroadcast** | **bool?**| Disable the network broadcast | [optional] 

### Return type

**string**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Broadcasts a hex-encoded, serialized transaction to the network |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionPost

> InlineResponse2008 TransactionPost (TransactionV2ParamsAddress transactionV2ParamsAddress = null)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionPostExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var transactionV2ParamsAddress = new TransactionV2ParamsAddress(); // TransactionV2ParamsAddress |  (optional) 

            try
            {
                InlineResponse2008 result = apiInstance.TransactionPost(transactionV2ParamsAddress);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionPost: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **transactionV2ParamsAddress** | [**TransactionV2ParamsAddress**](TransactionV2ParamsAddress.md)|  | [optional] 

### Return type

[**InlineResponse2008**](InlineResponse2008.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response is a transaction |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionPostUnspent

> InlineResponse2008 TransactionPostUnspent (TransactionV2ParamsUnspent transactionV2ParamsUnspent)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionPostUnspentExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var transactionV2ParamsUnspent = new TransactionV2ParamsUnspent(); // TransactionV2ParamsUnspent | Unspent parameters

            try
            {
                InlineResponse2008 result = apiInstance.TransactionPostUnspent(transactionV2ParamsUnspent);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionPostUnspent: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **transactionV2ParamsUnspent** | [**TransactionV2ParamsUnspent**](TransactionV2ParamsUnspent.md)| Unspent parameters | 

### Return type

[**InlineResponse2008**](InlineResponse2008.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response is a transaction |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionRaw

> Object TransactionRaw (string txid = null)

Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionRawExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var txid = txid_example;  // string | Transaction id hash (optional) 

            try
            {
                // Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.
                Object result = apiInstance.TransactionRaw(txid);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionRaw: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **txid** | **string**| Transaction id hash | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the hex-encoded byte serialization of a transaction |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionVerify

> Object TransactionVerify (TransactionVerifyRequest transactionVerifyRequest)



Decode and verify an encoded transaction

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionVerifyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var transactionVerifyRequest = new TransactionVerifyRequest(); // TransactionVerifyRequest | 

            try
            {
                Object result = apiInstance.TransactionVerify(transactionVerifyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionVerify: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **transactionVerifyRequest** | [**TransactionVerifyRequest**](TransactionVerifyRequest.md)|  | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Responses ok |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionsGet

> Object TransactionsGet (string addrs = null, string confirmed = null)

Returns transactions that match the filters.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionsGetExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var addrs = addrs_example;  // string | command separated list of addresses (optional) 
            var confirmed = confirmed_example;  // string | Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] (optional) 

            try
            {
                // Returns transactions that match the filters.
                Object result = apiInstance.TransactionsGet(addrs, confirmed);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionsGet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addrs** | **string**| command separated list of addresses | [optional] 
 **confirmed** | **string**| Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns transactions that match the filters. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## TransactionsPost

> Object TransactionsPost (string addrs = null, string confirmed = null)

Returns transactions that match the filters.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class TransactionsPostExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var addrs = addrs_example;  // string | command separated list of addresses (optional) 
            var confirmed = confirmed_example;  // string | Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] (optional) 

            try
            {
                // Returns transactions that match the filters.
                Object result = apiInstance.TransactionsPost(addrs, confirmed);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionsPost: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addrs** | **string**| command separated list of addresses | [optional] 
 **confirmed** | **string**| Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns transactions that match the filters. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Uxout

> Object Uxout (string uxid = null)

Returns an unspent output by ID.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class UxoutExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var uxid = uxid_example;  // string | uxid to filter by (optional) 

            try
            {
                // Returns an unspent output by ID.
                Object result = apiInstance.Uxout(uxid);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Uxout: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **uxid** | **string**| uxid to filter by | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response for endpoint /api/v1/uxout |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## VerifyAddress

> Object VerifyAddress (Address address)

Verifies a Skycoin address.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class VerifyAddressExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var address = new Address(); // Address | Address id.

            try
            {
                // Verifies a Skycoin address.
                Object result = apiInstance.VerifyAddress(address);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.VerifyAddress: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | [**Address**](Address.md)| Address id. | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Version

> InlineResponse2005 Version ()



versionHandler returns the application version info

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class VersionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                InlineResponse2005 result = apiInstance.Version();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Version: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**InlineResponse2005**](InlineResponse2005.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | BuildInfo represents the build info |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Wallet

> Object Wallet (string id)

Returns a wallet by id.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | tags to filter by

            try
            {
                // Returns a wallet by id.
                Object result = apiInstance.Wallet(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Wallet: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| tags to filter by | 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response for endpoint /api/v1/wallet |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletBalance

> Object WalletBalance (string id)

Returns the wallet's balance, both confirmed and predicted.  The predicted balance is the confirmed balance minus the pending spends.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletBalanceExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | tags to filter by

            try
            {
                // Returns the wallet's balance, both confirmed and predicted.  The predicted balance is the confirmed balance minus the pending spends.
                Object result = apiInstance.WalletBalance(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletBalance: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| tags to filter by | 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the wallets balance |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletCreate

> Object WalletCreate (string type, string seed, string label, string seedPassphrase = null, string bip44Coin = null, string xpub = null, int? scan = null, bool? encrypt = null, string password = null)



Create a wallet

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletCreateExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var type = type_example;  // string | wallet seed passphrase [optional, bip44 type wallet only]
            var seed = seed_example;  // string | Wallet seed.
            var label = label_example;  // string | Wallet label.
            var seedPassphrase = seedPassphrase_example;  // string | wallet seed passphrase [optional, bip44 type wallet only] (optional) 
            var bip44Coin = bip44Coin_example;  // string | BIP44 coin type [optional, defaults to 8000 (skycoin's coin type), only valid if type is \"bip44\"] (optional) 
            var xpub = xpub_example;  // string | xpub key [required for xpub wallets] (optional) 
            var scan = 56;  // int? | The number of addresses to scan ahead for balances. (optional) 
            var encrypt = true;  // bool? | Encrypt wallet. (optional) 
            var password = password_example;  // string | Wallet Password (optional) 

            try
            {
                Object result = apiInstance.WalletCreate(type, seed, label, seedPassphrase, bip44Coin, xpub, scan, encrypt, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletCreate: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **type** | **string**| wallet seed passphrase [optional, bip44 type wallet only] | 
 **seed** | **string**| Wallet seed. | 
 **label** | **string**| Wallet label. | 
 **seedPassphrase** | **string**| wallet seed passphrase [optional, bip44 type wallet only] | [optional] 
 **bip44Coin** | **string**| BIP44 coin type [optional, defaults to 8000 (skycoin&#39;s coin type), only valid if type is \&quot;bip44\&quot;] | [optional] 
 **xpub** | **string**| xpub key [required for xpub wallets] | [optional] 
 **scan** | **int?**| The number of addresses to scan ahead for balances. | [optional] 
 **encrypt** | **bool?**| Encrypt wallet. | [optional] 
 **password** | **string**| Wallet Password | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Response for endpoint /api/v1/wallet |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletDecrypt

> Object WalletDecrypt (string id, string password)

Decrypts wallet.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletDecryptExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // Decrypts wallet.
                Object result = apiInstance.WalletDecrypt(id, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletDecrypt: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet id. | 
 **password** | **string**| Wallet password. | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint decrypts wallets. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletEncrypt

> Object WalletEncrypt (string id, string password)

Encrypt wallet.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletEncryptExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // Encrypt wallet.
                Object result = apiInstance.WalletEncrypt(id, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletEncrypt: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet id. | 
 **password** | **string**| Wallet password. | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint encrypt wallets. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletFolder

> InlineResponse2007 WalletFolder (string addr)



Returns the wallet directory path

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletFolderExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var addr = addr_example;  // string | Address port

            try
            {
                InlineResponse2007 result = apiInstance.WalletFolder(addr);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletFolder: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **addr** | **string**| Address port | 

### Return type

[**InlineResponse2007**](InlineResponse2007.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return the wallet directory path |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletNewAddress

> Object WalletNewAddress (string id, string num = null, string password = null)



Generates new addresses

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletNewAddressExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet Id
            var num = num_example;  // string | The number you want to generate (optional) 
            var password = password_example;  // string | Wallet Password (optional) 

            try
            {
                Object result = apiInstance.WalletNewAddress(id, num, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletNewAddress: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet Id | 
 **num** | **string**| The number you want to generate | [optional] 
 **password** | **string**| Wallet Password | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint generate new addresses |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletNewSeed

> Object WalletNewSeed (string entropy = null)



Returns the wallet directory path

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletNewSeedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var entropy = entropy_example;  // string | Entropy bitSize. (optional) 

            try
            {
                Object result = apiInstance.WalletNewSeed(entropy);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletNewSeed: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **entropy** | **string**| Entropy bitSize. | [optional] 

### Return type

**Object**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Generates wallet seed |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletRecover

> Object WalletRecover (string id, string seed, string seedPassphrase = null, string password = null)

Recovers an encrypted wallet by providing the seed. The first address will be generated from seed and compared to the first address of the specified wallet. If they match, the wallet will be regenerated with an optional password. If the wallet is not encrypted, an error is returned.

Recovers an encrypted wallet by providing the wallet seed and optional seed passphrase

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletRecoverExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet id.
            var seed = seed_example;  // string | Wallet seed.
            var seedPassphrase = seedPassphrase_example;  // string | Wallet seed-passphrase. (optional) 
            var password = password_example;  // string | Wallet password. (optional) 

            try
            {
                // Recovers an encrypted wallet by providing the seed. The first address will be generated from seed and compared to the first address of the specified wallet. If they match, the wallet will be regenerated with an optional password. If the wallet is not encrypted, an error is returned.
                Object result = apiInstance.WalletRecover(id, seed, seedPassphrase, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletRecover: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet id. | 
 **seed** | **string**| Wallet seed. | 
 **seedPassphrase** | **string**| Wallet seed-passphrase. | [optional] 
 **password** | **string**| Wallet password. | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint recover wallets. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletSeed

> Object WalletSeed (string id, string password)

This endpoint only works for encrypted wallets. If the wallet is unencrypted, The seed will be not returned.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletSeedExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet Id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // This endpoint only works for encrypted wallets. If the wallet is unencrypted, The seed will be not returned.
                Object result = apiInstance.WalletSeed(id, password);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletSeed: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet Id. | 
 **password** | **string**| Wallet password. | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint Returns seed of wallet of given id |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletSeedVerify

> Object WalletSeedVerify (string seed = null)

Verifies a wallet seed.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletSeedVerifyExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var seed = seed_example;  // string | Seed to be verified. (optional) 

            try
            {
                // Verifies a wallet seed.
                Object result = apiInstance.WalletSeedVerify(seed);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletSeedVerify: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **seed** | **string**| Seed to be verified. | [optional] 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Verifies a wallet seed. |  -  |
| **422** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletTransaction

> Object WalletTransaction (WalletTransactionRequest walletTransactionRequest)

Creates a signed transaction

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletTransactionExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var walletTransactionRequest = new WalletTransactionRequest(); // WalletTransactionRequest | 

            try
            {
                // Creates a signed transaction
                Object result = apiInstance.WalletTransaction(walletTransactionRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletTransaction: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **walletTransactionRequest** | [**WalletTransactionRequest**](WalletTransactionRequest.md)|  | 

### Return type

**Object**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns blocks between a start and end point. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletTransactionSign

> InlineResponse2009 WalletTransactionSign (WalletTransactionSignRequest walletTransactionSignRequest)

Creates a signed transaction

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletTransactionSignExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var walletTransactionSignRequest = new WalletTransactionSignRequest(); // WalletTransactionSignRequest | 

            try
            {
                // Creates a signed transaction
                InlineResponse2009 result = apiInstance.WalletTransactionSign(walletTransactionSignRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletTransactionSign: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **walletTransactionSignRequest** | [**WalletTransactionSignRequest**](WalletTransactionSignRequest.md)|  | 

### Return type

[**InlineResponse2009**](InlineResponse2009.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Signs an unsigned transaction, returning the transaction with updated signatures and the encoded, serialized transaction. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletTransactions

> InlineResponse2006 WalletTransactions (string id)



### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletTransactionsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet Id.

            try
            {
                InlineResponse2006 result = apiInstance.WalletTransactions(id);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletTransactions: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet Id. | 

### Return type

[**InlineResponse2006**](InlineResponse2006.md)

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns all unconfirmed transactions for all addresses in a given wallet. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletUnload

> void WalletUnload (string id)

Unloads wallet from the wallet service.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletUnloadExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet Id.

            try
            {
                // Unloads wallet from the wallet service.
                apiInstance.WalletUnload(id);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletUnload: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet Id. | 

### Return type

void (empty response body)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint returns nothing. |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## WalletUpdate

> string WalletUpdate (string id, string label)

Update the wallet.

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletUpdateExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("X-CSRF-TOKEN", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("X-CSRF-TOKEN", "Bearer");

            var apiInstance = new DefaultApi(Configuration.Default);
            var id = id_example;  // string | Wallet Id.
            var label = label_example;  // string | The label the wallet will be updated to.

            try
            {
                // Update the wallet.
                string result = apiInstance.WalletUpdate(id, label);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletUpdate: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet Id. | 
 **label** | **string**| The label the wallet will be updated to. | 

### Return type

**string**

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: text/plain, application/json, application/xml

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint Returns the label the wallet will be updated to . |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## Wallets

> List&lt;Object&gt; Wallets ()



Returns all loaded wallets

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Skyapi.Api;
using Skyapi.Client;
using Skyapi.Model;

namespace Example
{
    public class WalletsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://127.0.0.1:6420";
            var apiInstance = new DefaultApi(Configuration.Default);

            try
            {
                List<Object> result = apiInstance.Wallets();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling DefaultApi.Wallets: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**List<Object>**

### Authorization

No authorization required

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json, application/xml, 

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | This endpoint return all loaded wallets |  -  |
| **0** | A GenericError is the default error message that is generated. For certain status codes there are more appropriate error structures. |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

