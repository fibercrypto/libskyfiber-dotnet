# Libskycoin .Net

[![Build Status](https://travis-ci.org/libskycoin-dotnet.svg?branch=develop)](https://travis-ci.org/simelo/libskycoin-dotnet)

.Net extension for Skycoin API.
A .Net extension generated with SWIG to access Skycoin API from .Net.

## Table of Contents

<!-- MarkdownTOC levels="1,2,3,4,5" autolink="true" bracket="round" -->
- [Installation](#installation)
- [Using the API](#usage)
  - [Naming](#naming)
  - [Parameters](#parameters)
    - [Handles](#handles)
    - [Byte Slices](#byte-slices)
    - [Structures](#structures)
    - [Fixed Size Arrays](#fixed-size-array)
    - [Other Slices](#other-slices)
    - [Memory Managemanet](#memory-management)
- [Make rules](#make-rules)
- [Development setup](#development-setup)
<!-- /MarkdownTOC -->

## Installation

Download the repository from http://github.com/simelo/libskycoin-dotnet.git. 
Execute (`nuget restore LibskycoinNet.sln`) to install the library. Although executing (`nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner`) is a better choice for making changes to the library. However, when using tox these commands are not required at all because calling tox will make any necessary installation and execute the tests.

## Usage
### Naming

The exported function in Libskycoin .NET have the following naming format: `SKY_package_func_name` where package is replace by the package where the original Skycoin function is and func_name is the name of the function. For example, `LoadConfig` function from `cli` package is called in .Net `SKY_cli_LoadConfig`
### Parameters

All skycoin exported functions return an error object as the last of the return parameters. In .NET error is return as an `uint` and it is the first return parameter. The rest of the parameters are returned in the same order.

Receivers in Skycoin are the first of the input parameters. Simple types, like integer, float, string will be used as the corresponding types in .NET, except what act as pointers.

#### Handles

Some of Skycoin types are too complex to be exported to a scripting language. So, handles are used instead. Therefore all functions taking a complex type will receive a handle instead of the original Skycoin type. For example, having these functions exported from Skycoin:

```go
	func LoadConfig() (Config, error)
	func (c Config) FullWalletPath() string
```


Config is a struct type that is treated as a handle in Libskycoin .Net . The usage in .Net will be:

```csharp

using skycoin;
	
public function main(){
    var configHandle = skycoin.skycoin.new_Config_HandlePtr();
	var err = skycoin.skycoin.SKY_cli_LoadConfig(configHandle);
    if(err == skycoin.skycoin.SKY_OK) //0 then no error
    {
        var fullWalletPath = new _GoString()_;
	    err = skycoin.SKY_cli_FullWalletPath(configHandle,fullWallerPath);
        Assert.AreEqual(err,skycoin.skycoin.SKY_OK);
	    Console.WriteLine(fullWallerPath.p);
	//Close the handle after using the it
	//so the garbage collector can delete the object associated with  it. 
	skycoin.skycoin.SKY_handle_close( configHandle );
    }
	else{
		#Error
		Console.WriteLine(err);
    }
}
```

#### Byte slices

Parameters of type byte[] will treated as string . Example, this function in Skycoin:

```go
func (s ScryptChacha20poly1305) Encrypt(data, password []byte) ([]byte, error)
```

Will be called like this:

```csharp
var encrypt_settings = new encrypt__ScryptChacha20poly1305();
var data = new GoSlice(); //It will be passed as a parameter of type []byte
var pwd = new GoSlice(); //As []byte too
var dataStr = new _GoString();
var pwdStr = new _GoString();
dataStr.setString("Data to encrypt" );
data.convertString(dataStr);
pwdStr.SetString("password");
pwd.convertString(pwdStr);
var encrypted = new GoSlice();
var err = skycoin.skycoin.SKY_encrypt_ScryptChacha20poly1305_Encrypt(encrypt_settings, data, pwd,encrypted);
if(err == skycoin.skycoin.SKY_OK){

Console.WriteLine(encrypted.getString().p); //Encrypted is GoSlice
}
```

#### Structures

Structures that are not exported as handles are treated like classes in .NET. In the previous example type ScryptChacha20poly1305 is created in .NET like:

```csharp
var encrypt_settings = new encrypt__ScryptChacha20poly1305()
```

And passed as first parameter in call to SKY_encrypt_ScryptChacha20poly1305_Encrypt.

#### Fixed Sized Arrays

Parameters of fixed size array are wrapped in structures when called from python.

Given these types in Skycoin:

```go
	type PubKey [33]byte
	type SecKey [32]byte
```

And this exported function:

```go
	func GenerateDeterministicKeyPair(seed []byte) (PubKey, SecKey)
```
	
This is how it is used in .NET:

```csharp
//Generates random seed
var data = new GoSlice();
var err = skycoin.skycoin.SKY_cipher_RandByte(32,data);
Assert.AreEqual(err,skycoin.skycoin.SKY_OK);
var pubkey = new cipher_PubKey();
var seckey = new cipher_SecKey();
err = skycoin.skycoin.SKY_cipher_GenerateDeterministicKeyPair(data, pubkey, seckey);
```

pubkey and seckey are objects of type structure containing a field name data for the corresponding type of PubKey and SecKey. Something like:

```cpp
	cipher_PubKey struct{
		data [33]byte;
	} cipher_PubKey;

	cipher_SecKey struct{
		data [32]byte;
	} ;
```

#### Other Slices

Other slices of type different than byte were wrapped inside classes. Calling the following function:

```go
func GenerateDeterministicKeyPairs(seed []byte, n int) []SecKey
```
	
Would be like:

```csharp
//Generates random seed
var seed = new GoSlice();
var err = skycoin.SKY_cipher_RandByte(32,seed);
var seckeys = new cipher__SecKeys();
err = skycoin.SKY_cipher_GenerateDeterministicKeyPairs(seed, 2,seckeys);
for(int i=0;i<seckeys.count,i++){
	var pubkey = new cipher_PubKey();
    var seckey = new cipher_SecKey();
    seckeys.getAt(seckey,i);
	skycoin.skycoin.SKY_cipher_PubKeyFromSecKey(seckey, pubkey);
	err = skycoin.skycoin.SKY_cipher_PubKey_Verify(pubkey);
	Assert.AreEqual(err,skycoin.skycoin.SKY_OK);
}
```

### Memory Management

Memory management is transparent to the user. Any object allocated inside the library is left to be managed by .NET garbage collector.

## Make Rules

All these make rules require skycoin to be a git submodule of libskycoin .NET

- build-libc
  * Compiles skycoin C language library.
- build-swig
  * Creates the wrapper C code to generate the Python library.
