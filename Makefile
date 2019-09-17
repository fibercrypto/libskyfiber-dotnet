.DEFAULT_GOAL := help
.PHONY: configure build-libc build-swig develop build-libc-swig build
.PHONY: test test-ci help

# Compilation output
.ONESHELL:
SHELL := /bin/bash

MKFILE_PATH   = $(abspath $(lastword $(MAKEFILE_LIST)))
REPO_ROOT     = $(dir $(MKFILE_PATH))
GOPATH_DIR    = gopath
SKYLIBC_DIR  ?= $(GOPATH_DIR)/src/github.com/skycoin/libskycoin
SKYCOIN_DIR  ?= $(SKYLIBC_DIR)/vendor/github.com/skycoin/skycoin
SKYBUILD_DIR  = $(SKYLIBC_DIR)/build
BUILDLIBC_DIR = $(SKYBUILD_DIR)/libskycoin
LIBC_DIR      = $(SKYLIBC_DIR)/lib/cgo
BUILD_DIR     = build
DIST_DIR      = dist
BIN_DIR       = $(SKYLIBC_DIR)/bin
INCLUDE_DIR   = $(SKYLIBC_DIR)/include
FULL_PATH_LIB = $(REPO_ROOT)/$(BUILDLIBC_DIR)

LIB_FILES = $(shell find $(SKYCOIN_DIR)/lib/cgo -type f -name "*.go")
SWIG_FILES = $(shell find $(CSHARP_SWIG_DIR) -type f -name "*.i")
HEADER_FILES = $(shell find $(SKYCOIN_DIR)/include -type f -name "*.h")

# Compilation flags for libskycoin
CC_VERSION = $(shell $(CC) -dumpversion)
STDC_FLAG = $(python -c "if tuple(map(int, '$(CC_VERSION)'.split('.'))) < (6,): print('-std=C99'")
LIBC_LIBS = -lcriterion
LIBC_FLAGS = -I$(LIBSRC_DIR) -I$(INCLUDE_DIR) -I$(BUILD_DIR)/usr/include -L $(BUILDLIB_DIR) -L$(BUILD_DIR)/usr/lib

# Platform specific checks
OSNAME = $(TRAVIS_OS_NAME)
LDNAME = 
ifeq ($(shell uname -s),Linux)
  LDLIBS=$(LIBC_LIBS) -lpthread
  LDPATH=$(shell printenv LD_LIBRARY_PATH)
  LDPATHVAR=LD_LIBRARY_PATH
  LDFLAGS=$(LIBC_FLAGS) $(STDC_FLAG)
  LDCOPY=$(PWD)/build/usr/lib/
  LDNAME= libskycoin.so
  OS = linux
ifndef OSNAME
  OSNAME = linux
endif
else ifeq ($(shell uname -s),Darwin)
ifndef OSNAME
  OSNAME = osx
endif
  LDLIBS = $(LIBC_LIBS)
  LDPATH=$(shell printenv DYLD_LIBRARY_PATH)
  LDPATHVAR=DYLD_LIBRARY_PATH
  LDFLAGS=$(LIBC_FLAGS) -dynamiclib -flat_namespace -static -framework CoreFoundation -framework Security
  LDCOPY= ~/lib
  LDNAME= libskycoin.dylib
  OS = darwin
else
  LDLIBS = $(LIBC_LIBS)
  LDPATH=$(shell printenv LD_LIBRARY_PATH)
  LDPATHVAR=LD_LIBRARY_PATH
  LDFLAGS=$(LIBC_FLAGS)
endif

# Added by Swagger
LIB_SKYCOIN_DIR = gopath/src/github.com/skycoin/libskycoin
SWAGGER_SPEC_DIR = $(LIB_SKYCOIN_DIR)/lib/swagger/skycoin.v0.26.0.openapi.v2.yml
CSHARP_CLIENT_DIR = lib/skyapi
CSHARP_SWIG_DIR = lib/swig
LIBSWIG_DIR = lib/swig/swig

configure-linux:

configure-darwin:
	mkdir -p ~/Library/ ~/Library/Frameworks ~/lib

configure: configure-$(OS)
	mkdir -p $(BUILD_DIR)/usr/tmp $(BUILD_DIR)/usr/lib $(BUILD_DIR)/usr/include
	mkdir -p $(BUILDLIBC_DIR) $(BIN_DIR) $(INCLUDE_DIR)
	rm -f $(BUILDLIBC_DIR)/libskycoin.a

build-libc: configure ## Build libskycoin C client library
	GOPATH="$(REPO_ROOT)/$(GOPATH_DIR)" make -C $(SKYLIBC_DIR) clean-libc
	GOPATH="$(REPO_ROOT)/$(GOPATH_DIR)" make -C $(SKYLIBC_DIR) build-libc
	rm -f $(CSHARP_SWIG_DIR)/swig/include/libskycoin.h
	rm -f $(CSHARP_SWIG_DIR)/swig/include/swig.h
	mkdir -p $(CSHARP_SWIG_DIR)/swig/include
	cp $(SKYLIBC_DIR)/include/swig.h $(CSHARP_SWIG_DIR)/swig/include/
	grep -v _Complex $(SKYLIBC_DIR)/include/libskycoin.h > $(CSHARP_SWIG_DIR)/swig/include/libskycoin.h

build-swig: ## Generate C# C module from SWIG interfaces
	#Generate structs.i from skytypes.gen.h
	rm -f $(CSHARP_SWIG_DIR)/structs.i
	cp $(INCLUDE_DIR)/skytypes.gen.h $(CSHARP_SWIG_DIR)/structs.i
	{ \
		if [[ "$$(uname -s)" == "Darwin" ]]; then \
			sed -i '.kbk' 's/#/%/g' $(CSHARP_SWIG_DIR)/structs.i ;\
		else \
			sed -i 's/#/%/g' $(CSHARP_SWIG_DIR)/structs.i ;\
		fi \
	}
	mkdir -p $(CSHARP_SWIG_DIR)/LibskycoinNet/skycoin
	rm -f $(CSHARP_SWIG_DIR)/swig/include/swig.h
	rm -f skycoinnet_wrap.o
	rm -f skycoinnet_wrap.c
	swig -csharp -oldvarnames -namespace  skycoin -I$(LIBSWIG_DIR)/include -I$(INCLUDE_DIR) -outdir $(CSHARP_SWIG_DIR)/LibskycoinNet/skycoin -o skycoinnet_wrap.c $(LIBSWIG_DIR)/libdotnet.i
	
build-libskycoin-net: build-libc build-swig ## Build shared library including SWIG wrappers
	$(CC) -c -fpic -I$(CSHARP_SWIG_DIR)/swig/include -I$(INCLUDE_DIR) -libskycoin skycoinnet_wrap.c
	rm -rf build/usr/lib/$(LDNAME)
	$(CC) -shared skycoinnet_wrap.o $(BUILDLIBC_DIR)/libskycoin.a -o $(LDCOPY)/$(LDNAME) $(LDFLAGS)
	mkdir -p $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin
	mkdir -p $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release
	mkdir -p $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release/netcoreapp2.2
	rm -rf  $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release/$(LDNAME)

install-deps-mono: ## Install development dependencies by mono
	nuget restore $(CSHARP_SWIG_DIR)/LibskycoinNet.sln
	nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner

install-deps-dotnet: ## Install development dependencies by dotnet
	dotnet restore $(CSHARP_SWIG_DIR)/LibSkycoinDotNet.sln

build-sln-dotnet: install-deps-dotnet build-libc build-swig
	$(LDPATHVAR)="$(LDCOPY)" dotnet msbuild /p:VisualStudioVersion=15.0 /p:Configuration=Release $(CSHARP_SWIG_DIR)/LibSkycoinDotNet.sln
	
build-sln-mono: install-deps-mono build-libc build-swig
	$(LDPATHVAR)="$(LDCOPY)" msbuild /p:VisualStudioVersion=15.0 /p:Configuration=Release $(CSHARP_SWIG_DIR)/LibskycoinNet.sln

build-dotnet: build-libskycoin-net build-sln-dotnet ## Build LibSkycoinNet Assembly by DotNet

build-mono: build-libskycoin-net build-sln-mono ## Build LibSkycoinNet Assembly by Mono

test-libsky-mono: build-mono ## Run LibSkycoinNet test suite mono
	$(LDPATHVAR)="$(LDCOPY):$(LDPATHVAR)" mono ./testrunner/NUnit.Runners.2.6.4/tools/nunit-console.exe $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release/LibskycoinNetTest.dll -labels

test-libsky-dotnet: build-dotnet
	$(LDPATHVAR)="$(LDCOPY):$(LDPATHVAR)" dotnet test $(CSHARP_SWIG_DIR)/LibSkycoinDotNet.sln

build-skyapi: ## Build SkyApi Assembly
	(cd $(CSHARP_CLIENT_DIR) && /bin/sh build.sh)

test-skyapi: ## Run SkyApi test suite
	(cd $(CSHARP_CLIENT_DIR) && /bin/sh mono_nunit_test.sh)


help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
