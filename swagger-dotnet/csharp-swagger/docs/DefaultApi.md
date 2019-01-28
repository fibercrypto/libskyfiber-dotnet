# IO.Swagger.Api.DefaultApi

All URIs are relative to *http://staging.node.skycoin.net*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CoinSupply**](DefaultApi.md#coinsupply) | **GET** /api/v1/coinSupply | 
[**CsrfToken**](DefaultApi.md#csrftoken) | **GET** /api/v1/csrf | Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
[**ResendUnconfirmedTxns**](DefaultApi.md#resendunconfirmedtxns) | **POST** /api/v1/resendUnconfirmedTxns | 
[**Version**](DefaultApi.md#version) | **GET** /api/v1/version | 
[**WalletFolder**](DefaultApi.md#walletfolder) | **GET** /api/v1/wallets/folderName | 


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
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="csrftoken"></a>
# **CsrfToken**
> InlineResponse200 CsrfToken ()

Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.

Response -> CSRF token to use in POST requests

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CsrfTokenExample
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
                // Creates a new CSRF token. Previous CSRF tokens are invalidated by this call.
                InlineResponse200 result = apiInstance.CsrfToken();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DefaultApi.CsrfToken: " + e.Message );
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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json
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
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

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

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="version"></a>
# **Version**
> BuildInfo Version ()



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
            // Configure API key authorization: csrfAuth
            Configuration.Default.AddApiKey("csrf_Token", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("csrf_Token", "Bearer");

            var apiInstance = new DefaultApi();

            try
            {
                BuildInfo result = apiInstance.Version();
                Debug.WriteLine(result);
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

[**BuildInfo**](BuildInfo.md)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="walletfolder"></a>
# **WalletFolder**
> void WalletFolder ()



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

            try
            {
                apiInstance.WalletFolder();
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
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

[csrfAuth](../README.md#csrfAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

