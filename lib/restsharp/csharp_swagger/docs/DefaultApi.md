# IO.Swagger.Api.DefaultApi

All URIs are relative to *http://staging.node.skycoin.net*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddressCount**](DefaultApi.md#addresscount) | **GET** /api/v1/addresscount | Returns the total number of unique address that have coins.
[**AddressUxouts**](DefaultApi.md#addressuxouts) | **GET** /api/v1/address_uxouts | 
[**BalanceGet**](DefaultApi.md#balanceget) | **GET** /api/v1/balance | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**BalancePost**](DefaultApi.md#balancepost) | **POST** /api/v1/balance | Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
[**Block**](DefaultApi.md#block) | **GET** /api/v1/block | 
[**BlockchainMetadata**](DefaultApi.md#blockchainmetadata) | **GET** /api/v1/blockchain/metadata | Returns the blockchain metadata.
[**BlockchainProgress**](DefaultApi.md#blockchainprogress) | **GET** /api/v1/blockchain/progress | Returns the blockchain sync progress.
[**BlocksGet**](DefaultApi.md#blocksget) | **GET** /api/v1/blocks | blocksHandler returns blocks between a start and end point,
[**BlocksPost**](DefaultApi.md#blockspost) | **POST** /api/v1/blocks | blocksHandler returns blocks between a start and end point,
[**CoinSupply**](DefaultApi.md#coinsupply) | **GET** /api/v1/coinSupply | 
[**Csrf**](DefaultApi.md#csrf) | **GET** /api/v1/csrf | Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
[**DefaultConnections**](DefaultApi.md#defaultconnections) | **GET** /api/v1/network/defaultConnections | defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.
[**ExplorerAddress**](DefaultApi.md#exploreraddress) | **GET** /api/v1/explorer/address | 
[**Health**](DefaultApi.md#health) | **GET** /api/v1/health | Returns node health data.
[**InjectTransaction**](DefaultApi.md#injecttransaction) | **POST** /api/v1/injectTransaction | Broadcast a hex-encoded, serialized transaction to the network.
[**LastBlocks**](DefaultApi.md#lastblocks) | **GET** /api/v1/last_blocks | 
[**NetworkConnection**](DefaultApi.md#networkconnection) | **GET** /api/v1/network/connection | This endpoint returns a specific connection.
[**NetworkConnections**](DefaultApi.md#networkconnections) | **GET** /api/v1/network/connections | This endpoint returns all outgoings connections.
[**NetworkConnectionsDisconnect**](DefaultApi.md#networkconnectionsdisconnect) | **GET** /api/v1/network/connection/disconnect | 
[**NetworkConnectionsExchange**](DefaultApi.md#networkconnectionsexchange) | **GET** /api/v1/network/connections/exchange | 
[**NetworkConnectionsTrust**](DefaultApi.md#networkconnectionstrust) | **GET** /api/v1/network/connections/trust | trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.
[**OutputsGet**](DefaultApi.md#outputsget) | **GET** /api/v1/outputs | If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
[**OutputsPost**](DefaultApi.md#outputspost) | **POST** /api/v1/outputs | If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
[**PendingTxs**](DefaultApi.md#pendingtxs) | **GET** /api/v1/pendingTxs | 
[**Rawtx**](DefaultApi.md#rawtx) | **GET** /api/v1/rawtx | Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.
[**ResendUnconfirmedTxns**](DefaultApi.md#resendunconfirmedtxns) | **POST** /api/v1/resendUnconfirmedTxns | 
[**Richlist**](DefaultApi.md#richlist) | **GET** /api/v1/richlist | Returns the top skycoin holders.
[**Transaction**](DefaultApi.md#transaction) | **GET** /api/v1/transaction | 
[**TransactionsGet**](DefaultApi.md#transactionsget) | **GET** /api/v1/transactions | Returns transactions that match the filters.
[**TransactionsPost**](DefaultApi.md#transactionspost) | **POST** /api/v1/transactions | Returns transactions that match the filters.
[**Uxout**](DefaultApi.md#uxout) | **GET** /api/v1/uxout | Returns an unspent output by ID.
[**VerifyAddress**](DefaultApi.md#verifyaddress) | **POST** /api/v2/address/verify | healthHandler returns node health data.
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
[**WalletSpent**](DefaultApi.md#walletspent) | **POST** /api/v1/wallet/spend | 
[**WalletTransactions**](DefaultApi.md#wallettransactions) | **GET** /api/v1/wallet/transactions | Returns returns all unconfirmed transactions for all addresses in a given wallet.
[**WalletUnload**](DefaultApi.md#walletunload) | **POST** /api/v1/wallet/unload | Unloads wallet from the wallet service.
[**WalletUpdate**](DefaultApi.md#walletupdate) | **POST** /api/v1/wallet/update | Update the wallet.
[**Wallets**](DefaultApi.md#wallets) | **GET** /api/v1/wallets | 


<a name="addresscount"></a>
# **AddressCount**
> InlineResponse2001 AddressCount ()

Returns the total number of unique address that have coins.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AddressCountExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                // Returns the total number of unique address that have coins.
                InlineResponse2001 result = apiInstance.AddressCount();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.AddressCount: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**InlineResponse2001**](InlineResponse2001.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="addressuxouts"></a>
# **AddressUxouts**
> List<InlineResponse200> AddressUxouts (string address = null)



Returns the historical, spent outputs associated with an address

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AddressUxoutsExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var address = address_example;  // string | address to filter by (optional) 

            try
            {
                List&lt;InlineResponse200&gt; result = apiInstance.AddressUxouts(address);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.AddressUxouts: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**| address to filter by | [optional] 

### Return type

[**List<InlineResponse200>**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="balanceget"></a>
# **BalanceGet**
> InlineResponse2002 BalanceGet (string addrs)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BalanceGetExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var addrs = addrs_example;  // string | command separated list of addresses

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                InlineResponse2002 result = apiInstance.BalanceGet(addrs);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BalanceGet: " + e.Message );
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

[**InlineResponse2002**](InlineResponse2002.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="balancepost"></a>
# **BalancePost**
> InlineResponse2002 BalancePost (string addrs)

Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BalancePostExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var addrs = addrs_example;  // string | command separated list of addresses

            try
            {
                // Returns the balance of one or more addresses, both confirmed and predicted. The predicted balance is the confirmed balance minus the pending spends.
                InlineResponse2002 result = apiInstance.BalancePost(addrs);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BalancePost: " + e.Message );
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

[**InlineResponse2002**](InlineResponse2002.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="block"></a>
# **Block**
> InlineResponse2003 Block (bool? verbose = null, string hash = null, int? seq = null)



Returns a block by hash or seq. Note: only one of hash or seq is allowed

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BlockExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var verbose = true;  // bool? | include verbose (optional)  (default to true)
            var hash = hash_example;  // string |  (optional) 
            var seq = 56;  // int? |  (optional) 

            try
            {
                InlineResponse2003 result = apiInstance.Block(verbose, hash, seq);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Block: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **verbose** | **bool?**| include verbose | [optional] [default to true]
 **hash** | **string**|  | [optional] 
 **seq** | **int?**|  | [optional] 

### Return type

[**InlineResponse2003**](InlineResponse2003.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="blockchainmetadata"></a>
# **BlockchainMetadata**
> InlineResponse2004 BlockchainMetadata ()

Returns the blockchain metadata.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BlockchainMetadataExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                // Returns the blockchain metadata.
                InlineResponse2004 result = apiInstance.BlockchainMetadata();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BlockchainMetadata: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**InlineResponse2004**](InlineResponse2004.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="blockchainprogress"></a>
# **BlockchainProgress**
> InlineResponse2005 BlockchainProgress ()

Returns the blockchain sync progress.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BlockchainProgressExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                // Returns the blockchain sync progress.
                InlineResponse2005 result = apiInstance.BlockchainProgress();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BlockchainProgress: " + e.Message );
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

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="blocksget"></a>
# **BlocksGet**
> InlineResponse2006 BlocksGet (bool? verbose = null, int? start = null, int? end = null, string seqs = null)

blocksHandler returns blocks between a start and end point,

or an explicit list of sequences. If using start and end, the block sequences include both the start and end point. Explicit sequences cannot be combined with start and end.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BlocksGetExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var verbose = true;  // bool? | include verbose (optional)  (default to true)
            var start = 56;  // int? |  (optional) 
            var end = 56;  // int? |  (optional) 
            var seqs = seqs_example;  // string |  (optional) 

            try
            {
                // blocksHandler returns blocks between a start and end point,
                InlineResponse2006 result = apiInstance.BlocksGet(verbose, start, end, seqs);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BlocksGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **verbose** | **bool?**| include verbose | [optional] [default to true]
 **start** | **int?**|  | [optional] 
 **end** | **int?**|  | [optional] 
 **seqs** | **string**|  | [optional] 

### Return type

[**InlineResponse2006**](InlineResponse2006.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="blockspost"></a>
# **BlocksPost**
> InlineResponse2006 BlocksPost (bool? verbose = null, int? start = null, int? end = null, string seqs = null)

blocksHandler returns blocks between a start and end point,

or an explicit list of sequences. If using start and end, the block sequences include both the start and end point. Explicit sequences cannot be combined with start and end.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class BlocksPostExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var verbose = true;  // bool? | include verbose (optional)  (default to true)
            var start = 56;  // int? |  (optional) 
            var end = 56;  // int? |  (optional) 
            var seqs = seqs_example;  // string |  (optional) 

            try
            {
                // blocksHandler returns blocks between a start and end point,
                InlineResponse2006 result = apiInstance.BlocksPost(verbose, start, end, seqs);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.BlocksPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **verbose** | **bool?**| include verbose | [optional] [default to true]
 **start** | **int?**|  | [optional] 
 **end** | **int?**|  | [optional] 
 **seqs** | **string**|  | [optional] 

### Return type

[**InlineResponse2006**](InlineResponse2006.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="coinsupply"></a>
# **CoinSupply**
> void CoinSupply ()



coinSupplyHandler returns coin distribution supply stats

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CoinSupplyExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                apiInstance.CoinSupply();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.CoinSupply: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="csrf"></a>
# **Csrf**
> InlineResponse2007 Csrf ()

Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CsrfExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                // Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
                InlineResponse2007 result = apiInstance.Csrf();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Csrf: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**InlineResponse2007**](InlineResponse2007.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="defaultconnections"></a>
# **DefaultConnections**
> List<string> DefaultConnections ()

defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DefaultConnectionsExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                // defaultConnectionsHandler returns the list of default hardcoded bootstrap addresses.\\n They are not necessarily connected to.
                List&lt;string&gt; result = apiInstance.DefaultConnections();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.DefaultConnections: " + e.Message );
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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="exploreraddress"></a>
# **ExplorerAddress**
> List<InlineResponse2008> ExplorerAddress (string address = null)



Returns all transactions (confirmed and unconfirmed) for an address

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ExplorerAddressExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var address = address_example;  // string | tags to filter by (optional) 

            try
            {
                List&lt;InlineResponse2008&gt; result = apiInstance.ExplorerAddress(address);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.ExplorerAddress: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**| tags to filter by | [optional] 

### Return type

[**List<InlineResponse2008>**](InlineResponse2008.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="health"></a>
# **Health**
> InlineResponse2009 Health ()

Returns node health data.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class HealthExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                // Returns node health data.
                InlineResponse2009 result = apiInstance.Health();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Health: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**InlineResponse2009**](InlineResponse2009.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="injecttransaction"></a>
# **InjectTransaction**
> void InjectTransaction (string rawtx)

Broadcast a hex-encoded, serialized transaction to the network.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class InjectTransactionExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var rawtx = rawtx_example;  // string | hex-encoded serialized transaction string.

            try
            {
                // Broadcast a hex-encoded, serialized transaction to the network.
                apiInstance.InjectTransaction(rawtx);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.InjectTransaction: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **rawtx** | **string**| hex-encoded serialized transaction string. | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="lastblocks"></a>
# **LastBlocks**
> InlineResponse2006 LastBlocks (bool? verbose = null, int? num = null)



Returns the most recent N blocks on the blockchain

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class LastBlocksExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var verbose = true;  // bool? | include verbose (optional)  (default to true)
            var num = 56;  // int? |  (optional) 

            try
            {
                InlineResponse2006 result = apiInstance.LastBlocks(verbose, num);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.LastBlocks: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **verbose** | **bool?**| include verbose | [optional] [default to true]
 **num** | **int?**|  | [optional] 

### Return type

[**InlineResponse2006**](InlineResponse2006.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="networkconnection"></a>
# **NetworkConnection**
> InlineResponse20010 NetworkConnection (string addr)

This endpoint returns a specific connection.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class NetworkConnectionExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var addr = addr_example;  // string | Address port

            try
            {
                // This endpoint returns a specific connection.
                InlineResponse20010 result = apiInstance.NetworkConnection(addr);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnection: " + e.Message );
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

[**InlineResponse20010**](InlineResponse20010.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="networkconnections"></a>
# **NetworkConnections**
> List<InlineResponse20010> NetworkConnections (string states = null, string direction = null)

This endpoint returns all outgoings connections.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class NetworkConnectionsExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var states = states_example;  // string | Connection status. (optional) 
            var direction = direction_example;  // string | Direction of the connection. (optional) 

            try
            {
                // This endpoint returns all outgoings connections.
                List&lt;InlineResponse20010&gt; result = apiInstance.NetworkConnections(states, direction);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnections: " + e.Message );
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

[**List<InlineResponse20010>**](InlineResponse20010.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="networkconnectionsdisconnect"></a>
# **NetworkConnectionsDisconnect**
> void NetworkConnectionsDisconnect (string id)



This endpoint disconnects a connection by ID or address

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class NetworkConnectionsDisconnectExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Address id.

            try
            {
                apiInstance.NetworkConnectionsDisconnect(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsDisconnect: " + e.Message );
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

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="networkconnectionsexchange"></a>
# **NetworkConnectionsExchange**
> List<string> NetworkConnectionsExchange ()



This endpoint returns all connections found through peer exchange

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class NetworkConnectionsExchangeExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                List&lt;string&gt; result = apiInstance.NetworkConnectionsExchange();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsExchange: " + e.Message );
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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="networkconnectionstrust"></a>
# **NetworkConnectionsTrust**
> List<string> NetworkConnectionsTrust ()

trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class NetworkConnectionsTrustExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                // trustConnectionsHandler returns all trusted connections.\\n They are not necessarily connected to. In the default configuration, these will be a subset of the default hardcoded bootstrap addresses.
                List&lt;string&gt; result = apiInstance.NetworkConnectionsTrust();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.NetworkConnectionsTrust: " + e.Message );
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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="outputsget"></a>
# **OutputsGet**
> InlineResponse20011 OutputsGet (string address, string hash)

If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OutputsGetExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var address = address_example;  // string | 
            var hash = hash_example;  // string | 

            try
            {
                // If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
                InlineResponse20011 result = apiInstance.OutputsGet(address, hash);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.OutputsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**|  | 
 **hash** | **string**|  | 

### Return type

[**InlineResponse20011**](InlineResponse20011.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="outputspost"></a>
# **OutputsPost**
> InlineResponse20011 OutputsPost (string address, string hash)

If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OutputsPostExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var address = address_example;  // string | 
            var hash = hash_example;  // string | 

            try
            {
                // If neither addrs nor hashes are specificed, return all unspent outputs. If only one filter is specified, then return outputs match the filter. Both filters cannot be specified.
                InlineResponse20011 result = apiInstance.OutputsPost(address, hash);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.OutputsPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**|  | 
 **hash** | **string**|  | 

### Return type

[**InlineResponse20011**](InlineResponse20011.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="pendingtxs"></a>
# **PendingTxs**
> List<InlineResponse20012> PendingTxs (bool? verbose = null)



Returns pending (unconfirmed) transactions

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PendingTxsExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var verbose = true;  // bool? | include verbose transaction input data (optional)  (default to true)

            try
            {
                List&lt;InlineResponse20012&gt; result = apiInstance.PendingTxs(verbose);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.PendingTxs: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **verbose** | **bool?**| include verbose transaction input data | [optional] [default to true]

### Return type

[**List<InlineResponse20012>**](InlineResponse20012.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="rawtx"></a>
# **Rawtx**
> void Rawtx (string txid = null)

Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class RawtxExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var txid = txid_example;  // string | Transaction id hash (optional) 

            try
            {
                // Returns the hex-encoded byte serialization of a transaction. The transaction may be confirmed or unconfirmed.
                apiInstance.Rawtx(txid);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Rawtx: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="resendunconfirmedtxns"></a>
# **ResendUnconfirmedTxns**
> void ResendUnconfirmedTxns ()



Broadcasts all unconfirmed transactions from the unconfirmed transaction pool

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ResendUnconfirmedTxnsExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                apiInstance.ResendUnconfirmedTxns();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.ResendUnconfirmedTxns: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="richlist"></a>
# **Richlist**
> InlineResponse20013 Richlist (string includeDistribution = null, string n = null)

Returns the top skycoin holders.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class RichlistExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var includeDistribution = includeDistribution_example;  // string | include distribution addresses or not, default value false (optional) 
            var n = n_example;  // string | include distribution addresses or not, default value false (optional) 

            try
            {
                // Returns the top skycoin holders.
                InlineResponse20013 result = apiInstance.Richlist(includeDistribution, n);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Richlist: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **includeDistribution** | **string**| include distribution addresses or not, default value false | [optional] 
 **n** | **string**| include distribution addresses or not, default value false | [optional] 

### Return type

[**InlineResponse20013**](InlineResponse20013.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="transaction"></a>
# **Transaction**
> InlineResponse20014 Transaction (string txid, bool? encoded = null, bool? verbose = null)



Returns a transaction identified by its txid hash

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class TransactionExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var txid = txid_example;  // string | transaction hash
            var encoded = true;  // bool? | return as a raw encoded transaction. (optional) 
            var verbose = true;  // bool? | include verbose transaction input data (optional)  (default to true)

            try
            {
                InlineResponse20014 result = apiInstance.Transaction(txid, encoded, verbose);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Transaction: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **txid** | **string**| transaction hash | 
 **encoded** | **bool?**| return as a raw encoded transaction. | [optional] 
 **verbose** | **bool?**| include verbose transaction input data | [optional] [default to true]

### Return type

[**InlineResponse20014**](InlineResponse20014.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="transactionsget"></a>
# **TransactionsGet**
> List<InlineResponse20014> TransactionsGet (string addrs = null, string confirmed = null, bool? verbose = null)

Returns transactions that match the filters.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class TransactionsGetExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var addrs = addrs_example;  // string | command separated list of addresses (optional) 
            var confirmed = confirmed_example;  // string | Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] (optional) 
            var verbose = true;  // bool? | include verbose transaction input data (optional)  (default to true)

            try
            {
                // Returns transactions that match the filters.
                List&lt;InlineResponse20014&gt; result = apiInstance.TransactionsGet(addrs, confirmed, verbose);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionsGet: " + e.Message );
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
 **verbose** | **bool?**| include verbose transaction input data | [optional] [default to true]

### Return type

[**List<InlineResponse20014>**](InlineResponse20014.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="transactionspost"></a>
# **TransactionsPost**
> List<InlineResponse20014> TransactionsPost (string addrs = null, string confirmed = null, bool? verbose = null)

Returns transactions that match the filters.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class TransactionsPostExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var addrs = addrs_example;  // string | command separated list of addresses (optional) 
            var confirmed = confirmed_example;  // string | Whether the transactions should be confirmed [optional, must be 0 or 1; if not provided, returns all] (optional) 
            var verbose = true;  // bool? | include verbose transaction input data (optional)  (default to true)

            try
            {
                // Returns transactions that match the filters.
                List&lt;InlineResponse20014&gt; result = apiInstance.TransactionsPost(addrs, confirmed, verbose);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.TransactionsPost: " + e.Message );
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
 **verbose** | **bool?**| include verbose transaction input data | [optional] [default to true]

### Return type

[**List<InlineResponse20014>**](InlineResponse20014.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="uxout"></a>
# **Uxout**
> InlineResponse200 Uxout (string uxid = null)

Returns an unspent output by ID.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UxoutExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var uxid = uxid_example;  // string | uxid to filter by (optional) 

            try
            {
                // Returns an unspent output by ID.
                InlineResponse200 result = apiInstance.Uxout(uxid);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Uxout: " + e.Message );
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

[**InlineResponse200**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="verifyaddress"></a>
# **VerifyAddress**
> InlineResponse20022 VerifyAddress (string address)

healthHandler returns node health data.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class VerifyAddressExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var address = address_example;  // string | Address id.

            try
            {
                // healthHandler returns node health data.
                InlineResponse20022 result = apiInstance.VerifyAddress(address);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.VerifyAddress: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **address** | **string**| Address id. | 

### Return type

[**InlineResponse20022**](InlineResponse20022.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="version"></a>
# **Version**
> void Version ()



versionHandler returns the application version info

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class VersionExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();

            try
            {
                apiInstance.Version();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Version: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="wallet"></a>
# **Wallet**
> InlineResponse20015 Wallet (string id = null)

Returns a wallet by id.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | tags to filter by (optional) 

            try
            {
                // Returns a wallet by id.
                InlineResponse20015 result = apiInstance.Wallet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Wallet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| tags to filter by | [optional] 

### Return type

[**InlineResponse20015**](InlineResponse20015.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletbalance"></a>
# **WalletBalance**
> InlineResponse2002 WalletBalance (string id)

Returns the wallet's balance, both confirmed and predicted.  The predicted balance is the confirmed balance minus the pending spends.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletBalanceExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | tags to filter by

            try
            {
                // Returns the wallet's balance, both confirmed and predicted.  The predicted balance is the confirmed balance minus the pending spends.
                InlineResponse2002 result = apiInstance.WalletBalance(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletBalance: " + e.Message );
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

[**InlineResponse2002**](InlineResponse2002.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletcreate"></a>
# **WalletCreate**
> InlineResponse20015 WalletCreate (string seed, string label, int? scan = null, bool? encrypt = null, string password = null)



Loads wallet from seed, will scan ahead N address and load addresses till the last one that have coins.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletCreateExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var seed = seed_example;  // string | Wallet seed.
            var label = label_example;  // string | Wallet label.
            var scan = 56;  // int? | The number of addresses to scan ahead for balances. (optional) 
            var encrypt = true;  // bool? | Encrypt wallet. (optional) 
            var password = password_example;  // string | Wallet Password (optional) 

            try
            {
                InlineResponse20015 result = apiInstance.WalletCreate(seed, label, scan, encrypt, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletCreate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **seed** | **string**| Wallet seed. | 
 **label** | **string**| Wallet label. | 
 **scan** | **int?**| The number of addresses to scan ahead for balances. | [optional] 
 **encrypt** | **bool?**| Encrypt wallet. | [optional] 
 **password** | **string**| Wallet Password | [optional] 

### Return type

[**InlineResponse20015**](InlineResponse20015.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletdecrypt"></a>
# **WalletDecrypt**
> InlineResponse20015 WalletDecrypt (string id, string password)

Decrypts wallet.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletDecryptExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // Decrypts wallet.
                InlineResponse20015 result = apiInstance.WalletDecrypt(id, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletDecrypt: " + e.Message );
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

[**InlineResponse20015**](InlineResponse20015.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletencrypt"></a>
# **WalletEncrypt**
> InlineResponse20015 WalletEncrypt (string id, string password)

Encrypt wallet.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletEncryptExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // Encrypt wallet.
                InlineResponse20015 result = apiInstance.WalletEncrypt(id, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletEncrypt: " + e.Message );
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

[**InlineResponse20015**](InlineResponse20015.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletfolder"></a>
# **WalletFolder**
> InlineResponse20021 WalletFolder (string addr)



Returns the wallet directory path

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletFolderExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var addr = addr_example;  // string | Address port

            try
            {
                InlineResponse20021 result = apiInstance.WalletFolder(addr);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletFolder: " + e.Message );
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

[**InlineResponse20021**](InlineResponse20021.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletnewaddress"></a>
# **WalletNewAddress**
> InlineResponse20016 WalletNewAddress (string id, string num = null, string password = null)



Generates new addresses

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletNewAddressExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet Id
            var num = num_example;  // string | The number you want to generate (optional) 
            var password = password_example;  // string | Wallet Password (optional) 

            try
            {
                InlineResponse20016 result = apiInstance.WalletNewAddress(id, num, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletNewAddress: " + e.Message );
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

[**InlineResponse20016**](InlineResponse20016.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletnewseed"></a>
# **WalletNewSeed**
> InlineResponse20017 WalletNewSeed (string entropy = null)



Returns the wallet directory path

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletNewSeedExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var entropy = entropy_example;  // string | Entropy bitSize. (optional) 

            try
            {
                InlineResponse20017 result = apiInstance.WalletNewSeed(entropy);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletNewSeed: " + e.Message );
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

[**InlineResponse20017**](InlineResponse20017.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletrecover"></a>
# **WalletRecover**
> InlineResponse20023 WalletRecover (string id, string seed, string password = null)

Recovers an encrypted wallet by providing the seed. The first address will be generated from seed and compared to the first address of the specified wallet. If they match, the wallet will be regenerated with an optional password. If the wallet is not encrypted, an error is returned.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletRecoverExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet id.
            var seed = seed_example;  // string | Wallet seed.
            var password = password_example;  // string | Wallet password. (optional) 

            try
            {
                // Recovers an encrypted wallet by providing the seed. The first address will be generated from seed and compared to the first address of the specified wallet. If they match, the wallet will be regenerated with an optional password. If the wallet is not encrypted, an error is returned.
                InlineResponse20023 result = apiInstance.WalletRecover(id, seed, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletRecover: " + e.Message );
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
 **password** | **string**| Wallet password. | [optional] 

### Return type

[**InlineResponse20023**](InlineResponse20023.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletseed"></a>
# **WalletSeed**
> InlineResponse20017 WalletSeed (string id, string password)

This endpoint only works for encrypted wallets. If the wallet is unencrypted, The seed will be not returned.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletSeedExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet Id.
            var password = password_example;  // string | Wallet password.

            try
            {
                // This endpoint only works for encrypted wallets. If the wallet is unencrypted, The seed will be not returned.
                InlineResponse20017 result = apiInstance.WalletSeed(id, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletSeed: " + e.Message );
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

[**InlineResponse20017**](InlineResponse20017.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletspent"></a>
# **WalletSpent**
> InlineResponse20018 WalletSpent (string id, string dst, string coins, string password)



Creates and broadcasts a transaction sending money from one of our wallets to destination address.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletSpentExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet id
            var dst = dst_example;  // string | Recipient address
            var coins = coins_example;  // string | Number of coins to spend, in droplets. 1 coin equals 1e6 droplets.
            var password = password_example;  // string | Wallet password.

            try
            {
                InlineResponse20018 result = apiInstance.WalletSpent(id, dst, coins, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletSpent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet id | 
 **dst** | **string**| Recipient address | 
 **coins** | **string**| Number of coins to spend, in droplets. 1 coin equals 1e6 droplets. | 
 **password** | **string**| Wallet password. | 

### Return type

[**InlineResponse20018**](InlineResponse20018.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="wallettransactions"></a>
# **WalletTransactions**
> InlineResponse20019 WalletTransactions (string id, bool? verbose = null)

Returns returns all unconfirmed transactions for all addresses in a given wallet.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletTransactionsExample
    {
        public void main()
        {
            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet id.
            var verbose = true;  // bool? | include verbose transaction input data (optional)  (default to true)

            try
            {
                // Returns returns all unconfirmed transactions for all addresses in a given wallet.
                InlineResponse20019 result = apiInstance.WalletTransactions(id, verbose);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletTransactions: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **string**| Wallet id. | 
 **verbose** | **bool?**| include verbose transaction input data | [optional] [default to true]

### Return type

[**InlineResponse20019**](InlineResponse20019.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletunload"></a>
# **WalletUnload**
> void WalletUnload (string id)

Unloads wallet from the wallet service.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletUnloadExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet Id.

            try
            {
                // Unloads wallet from the wallet service.
                apiInstance.WalletUnload(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletUnload: " + e.Message );
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

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletupdate"></a>
# **WalletUpdate**
> void WalletUpdate (string id, string label)

Update the wallet.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletUpdateExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();
            var id = id_example;  // string | Wallet Id.
            var label = label_example;  // string | The label the wallet will be updated to.

            try
            {
                // Update the wallet.
                apiInstance.WalletUpdate(id, label);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.WalletUpdate: " + e.Message );
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

void (empty response body)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="wallets"></a>
# **Wallets**
> List<InlineResponse20020> Wallets ()



Returns all loaded wallets

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WalletsExample
    {
        public void main()
        {
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                List&lt;InlineResponse20020&gt; result = apiInstance.Wallets();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.Wallets: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<InlineResponse20020>**](InlineResponse20020.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

