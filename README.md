 # Libskycoin for .Net

[![Build Status](https://travis-ci.org/simelo/libskycoin-dotnet.svg?branch=develop)](https://travis-ci.org/simelo/libskycoin-dotnet)

.Net client library for Skycoin API. This library is a .Net composed assembly generated with SWIG and OPENAPI to access Skycoin API from .Net.

## Table of Contents

<!-- MarkdownTOC levels="1,2,3,4,5" autolink="true" bracket="round" -->

- [LibSkycoinNet wrappers for the Skycoin cipher](libskycoinnet-wrappers-for the skycoin cipher)
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
- [SkyApi wrapper for Skycoin REST API](#skyapi-wrapper-for-skycoin-rest-api)
- [Make rules](#make-rules)
- [Development setup](#development-setup)
  - [Running tests](#running-tests)
  - [Releases](#releases)
    - [Update the version](#update-the-version)
    - [Pre-release testing](#pre-release-testing)
    - [Creating release builds](#creating-release-builds)
  <!-- /MarkdownTOC -->


## LibSkycoinNet wrappers for the Skycoin cipher

**LibSkycoinNet** is an assembly that provides wrappers to the Skycoin core cipher API's, implemented in go lang, hence linking directly to the original node code. No transpilation involved.

### Installation

Before installing, make sure you understand the choices available to [install a Nuget package](https://docs.microsoft.com/en-us/nuget/consume-packages/ways-to-install-a-package) and the [level of support in your platform](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools) for each possible configuration. For instance, in case of [installing Nuget client tools](https://docs.microsoft.com/en-us/nuget/install-nuget-client-tools) the process would look like this, using [`dotnet add package` command](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package).

```sh

dotnet add package LibSkycoinNet
```

... or using [Nuget install command](https://docs.microsoft.com/en-us/nuget/tools/cli-ref-install) ...

```sh

nuget install LibSkycoinNet
```

It is possible to install the package using [Visual Studio Package Manager Console](https://docs.microsoft.com/en-us/nuget/tools/package-manager-console) by executing the following command and then follow the instructions to [install and use a package in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio).

```sh

Install-Package LibSkycoinNet
```

For getting similar results using a graphical IDE interface consider package name to be `LibSkycoinNet` and consult the articles listed below:

- [Install and use a package in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio)
- [Package Manager UI reference for Windows users](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui)
- [Including a NuGet package in your project using Mac OS](https://docs.microsoft.com/en-us/visualstudio/mac/nuget-walkthrough)

### Install from sources

Download the repository from http://github.com/simelo/libskycoin-dotnet.git.
Execute (`nuget restore LibskycoinNet.sln`) to install the library. Although executing (`nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner`) is a better choice for making changes to the library. However, when using tox these commands are not required at all because calling tox will make any necessary installation and execute the tests.

### Usage

#### Naming

The exported function in LibSkycoin .NET have the following naming format: `SKY_package_func_name` where package is replace by the package where the original Skycoin function is and func_name is the name of the function. For example, `LoadConfig` function from `cli` package is called in .Net `SKY_cli_LoadConfig`

#### Parameters

All skycoin exported functions return an error object as the last of the return parameters. In .NET error is return as an `uint` and it is the first return parameter. The rest of the parameters are returned in the same order.

Receivers in Skycoin are the first of the input parameters. Simple types, like integer, float, string will be used as the corresponding types in .NET, except what act as pointers.

##### Handles

Some of Skycoin types are too complex to be exported to a scripting language. So, handles are used instead. Therefore all functions taking a complex type will receive a handle instead of the original Skycoin type. For example, having these functions exported from Skycoin:

```go
  func LoadConfig() (Config, error)
  func (c Config) FullWalletPath() string
```

Config is a struct type that is treated as a handle in LibSkycoin .Net . The usage in .Net will be:

```csharp

using skycoin;
namespace LibskycoinNet
{
    public class Skycoin : skycoin.skycoin
    {
    public function main(){
      var configHandle = new_Config_HandlePtr();
      var err = SKY_cli_LoadConfig(configHandle);
      if(err == SKY_OK) {
        // SkY_OK means no error
        var fullWalletPath = new _GoString()_;
        err = SKY_cli_FullWalletPath(configHandle,fullWallerPath);
        Assert.AreEqual(err,SKY_OK);
        Console.WriteLine(fullWallerPath.p);

        //Close the handle after using the it
        //so the garbage collector can delete the object associated with  it.
        SKY_handle_close( configHandle );
      } else {
        #Error
        Console.WriteLine(err);
      }
    }
  }
}
```

##### Byte slices

Parameters of type byte[] will treated as string . Example, this function in Skycoin:

```go
func (s ScryptChacha20poly1305) Encrypt(data, password []byte) ([]byte, error)
```

... should be invoked like this:

```csharp
var encrypt_settings = new encrypt__ScryptChacha20poly1305();
var data = new GoSlice(); //It will be passed as a parameter of type []byte
var pwd = new GoSlice(); //As []byte too
var dataStr = new _GoString();
var pwdStr = new _GoString();
var encrypted = new GoSlice();

dataStr.setString("Data to encrypt" );
data.convertString(dataStr);
pwdStr.SetString("password");
pwd.convertString(pwdStr);

var err = SKY_encrypt_ScryptChacha20poly1305_Encrypt(encrypt_settings, data, pwd,encrypted);
if(err == SKY_OK){
  Console.WriteLine(encrypted.getString().p); //Encrypted is GoSlice
}
```

##### Structures

Structures that are not exported as handles are treated like .NET classes. In the previous example type ScryptChacha20poly1305 is created in .NET like:

```csharp
var encrypt_settings = new encrypt__ScryptChacha20poly1305()
```

And passed as first parameter in call to `SKY_encrypt_ScryptChacha20poly1305_Encrypt`.

##### Fixed Sized Arrays

Parameters of fixed size array are wrapped in structures when called from .NET.

Given these types in Skycoin and this exported function:

```go
  type PubKey [33]byte
  type SecKey [32]byte

  func GenerateDeterministicKeyPair(seed []byte) (PubKey, SecKey)
```

This is a way to use them to write assertions in .NET:

```csharp
//Generates random seed
var data = new GoSlice();
var err = SKY_cipher_RandByte(32,data);

Assert.AreEqual(err,SKY_OK);

var pubkey = new cipher_PubKey();
var seckey = new cipher_SecKey();

err = SKY_cipher_GenerateDeterministicKeyPair(data, pubkey,seckey);
```

In the example above `pubkey` and `seckey` are objects of an structure type containing a field named `data` holding the corresponding instance of `PubKey` and `SecKey`. Something like:

```cpp
  cipher_PubKey struct{
    data [33]byte;
  } cipher_PubKey;

  cipher_SecKey struct{
    data [32]byte;
  } ;
```

##### Other Slices

Other slices of base type different from `byte` are indeed wrapped inside classes. Let's see how to call the following function:

```go
func GenerateDeterministicKeyPairs(seed []byte, n int) []SecKey
```

In C# this how it should be used to generate a deterministic sequence of pairs of public / private keys given a random seed:

```csharp
//Generates random seed
var seed = new GoSlice();
var err = SKY_cipher_RandByte(32,seed);
var seckeys = new cipher__SecKeys();

err = SKY_cipher_GenerateDeterministicKeyPairs(seed, 2,seckeys);

for(int i=0;i<seckeys.count,i++){
  var pubkey = new cipher_PubKey();
  var seckey = new cipher_SecKey();
  seckeys.getAt(seckey,i);

  SKY_cipher_PubKeyFromSecKey(seckey, pubkey);
  err = SKY_cipher_PubKey_Verify(pubkey);
  Assert.AreEqual(err,SKY_OK);
}
```

### Memory Management

Memory management is transparent to the user. Any object allocated inside the library is left to be managed by the .NET garbage collector.

## SkyApi wrapper for Skycoin REST API

This wrapper is Auto generated by openapi-generator directly from `Skycoin Api` code for version v0.25.1.

For further details of usage of `RestCsharp wrapper for Skycoin Api` see [Autogenerated documentation](./lib/restsharp/README.md)

### Specific Node Address

For use a specific node of Skycoin, you just need to create a instance of `ApiClient` passing the node address:

```csharp
var restSharpClient = new ApiClient("specific_node_address")
```

Then in `restSharpClient` you can use all methods related to Api Client.

## Make Rules

The following `make` rules are available after `git checkout` of this repository. They all require [Skycoin](https://github.com/skycoin/skycoin) to be checked out as a `git submodule` of libskycoin .NET .

```
$ make help

configure                      Setup build environment

## Development setup

It is highly recommended for developers to setup their environment using
the available Docker images.
Read the [LibSkycoinNet Docker docs](docker/images/dev-cli/README.md) for further
details.

The project has two branches: `master` and `develop`.

- `develop` is the default branch and will always have the latest code.
  The submodule at `gopath/src/github.com/skycoin/skycoin` has to be
  in sync with `skycoin/skycoin` `develop` branch.
- `master` will always be equal to the current stable release on the website, and should correspond with the latest release tag.
  The submodule at `gopath/src/github.com/skycoin/skycoin` has to be
  in sync with `skycoin/skycoin` `master` branch.

Separate stable development branches will be created to work on releases for supporting the
most recent stable version of Skycoin. The name of these branches should be the Skycoin
major and minor version numbers followed by `dev` suffix e.g. `0.25dev`.
These branches may be forked out of either `master` or `develop` branches, and 
the submodule at `gopath/src/github.com/skycoin/skycoin` has to be
in sync with the corresponding tag of `skycoin/skycoin` official repository.

Stable development branches are created most of the time for the following reasons:

- A Skycoin release increasing [patch version number](https://semver.org/).
- Enhanced support and bug fixes for a version of LibSkycoinNet compiled against an
  stable version of Skycoin
- Backporting useful features added in `develop`.

### Running tests

```sh
$ make test
```

### Releases

#### Update the version

0. If the `master` branch has commits that are not in `develop` (e.g. due to a hotfix applied to `master`), merge `master` into `develop` (and fix any build or test failures)
0. Switch to a new release branch named `release-X.Y.Z` for preparing the release.
0. Ensure that the submodule at `gopath/src/github.com/skycoin/skycoin` is in sync with respect to the corresponding tag in https://github.com/skycoin/skycoin repository.
0. Update package version (`LibskycoinNet/LibskycoinNet.csproj`)
0. Run `make build` to make sure that the code base is up to date
0. Update `CHANGELOG.md`: move the "unreleased" changes to the version and add the date.
0. Follow the steps in [pre-release testing](#pre-release-testing)
0. Make a PR merging the release branch into `master`
0. Review the PR and merge it
0. Update files in https://github.com/skycoin/repo-info/tree/master/repos/skycoin/remote for `skycoin/skycoindev-dotnet` Docker image, adding a new file for the new version and adjusting any configuration text that may have changed
0. Tag the `master` branch with the version number. Version tags start with `v`, e.g. `v0.20.0`. Sign the tag. If you have your GPG key in github, creating a release on the Github website will automatically tag the release. It can be tagged from the command line with `git tag -as v0.20.0 $COMMIT_ID`, but Github will not recognize it as a "release".
0. Release builds are created and uploaded by travis. To do it manually, checkout the master branch and follow the [create release builds instructions](#creating-release-builds).
0. Checkout `develop` branch and bump package up to next [`dev` version number](https://docs.microsoft.com/en-us/nuget/create-packages/prerelease-packages).

#### Pre-release testing

Perform these actions before releasing:

    make test

#### Creating release builds

Release builds should be created from `master` branch . After [updating release version](#update-the-version) it is necessary to follow these steps

##### Requirements
Have installed the `mono` for the creation of the package.

https://www.mono-project.com/download/stable/

##### Building

    msbuild /p:Configuration=Release LibskycoinNet.sln

Final results are placed in the LibskycoinNet/bin/Release/ folder.