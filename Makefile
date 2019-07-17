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

# Added by Swagger
LIB_SKYCOIN_DIR = gopath/src/github.com/skycoin/libskycoin
SWAGGER_SPEC_DIR = $(LIB_SKYCOIN_DIR)/lib/swagger/skycoin.v0.25.1.openapi.v2.yml
CSHARP_CLIENT_DIR = lib/skyapi

CSHARP_SWIG_DIR = lib/swig

configure: ## Configure build environment
	set -ex
	mkdir -p $(BUILD_DIR)/usr/tmp $(BUILD_DIR)/usr/lib $(BUILD_DIR)/usr/include
	mkdir -p $(BUILDLIBC_DIR) $(BIN_DIR) $(INCLUDE_DIR)


build-libc: configure ## Build libskycoin C client library
	GOPATH="$(REPO_ROOT)/$(GOPATH_DIR)" make -C $(SKYLIBC_DIR) clean-libc
	GOPATH="$(REPO_ROOT)/$(GOPATH_DIR)" make -C $(SKYLIBC_DIR) build-libc
	rm -f swig/include/libskycoin.h
	rm -f swig/include/swig.h
	mkdir -p swig/include
	cp $(SKYLIBC_DIR)/include/swig.h swig/include/
	grep -v _Complex $(SKYLIBC_DIR)/include/libskycoin.h > swig/include/libskycoin.h

build-swig: ## Generate Python C module from SWIG interfaces
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
	swig -csharp -namespace skycoin -Iswig/include -I$(INCLUDE_DIR) -outdir $(CSHARP_SWIG_DIR)/LibskycoinNet/skycoin -o $(CSHARP_SWIG_DIR)/LibskycoinNet/skycoin/skycoinnet_wrap.c $(CSHARP_SWIG_DIR)/swig/libdotnet.i

build-libsky-shared: build-swig ## Build shared library including SWIG wrappers
	gcc -c -fpic -I$(CSHARP_SWIG_DIR)/swig/include -I$(INCLUDE_DIR) $(CSHARP_SWIG_DIR)/LibskycoinNet/skycoin/skycoinnet_wrap.c
	gcc -shared skycoinnet_wrap.o $(BUILDLIBC_DIR)/libskycoin.a -o libskycoin.so
	mv libskycoin.so $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release

install-deps-libsky: ## Install development dependencies for LibSkycoinNet
	nuget restore $(CSHARP_SWIG_DIR)/LibskycoinNet.sln
	nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner

build-libsky-sln: install-deps-libsky build-libc build-swig
	msbuild /p:Configuration=Release $(CSHARP_SWIG_DIR)/LibskycoinNet.sln
	msbuild /p:Configuration=Debug $(CSHARP_SWIG_DIR)/LibskycoinNet.sln

build-libsky: build-libsky-sln build-libsky-shared ## Build LibSkycoinNet Assembly

build-skyapi: ## Build SkyApi Assembly
	(cd $(CSHARP_CLIENT_DIR) && /bin/sh build.sh)

test-libsky: build-libsky ## Run LibSkycoinNet test suite
	mono ./testrunner/NUnit.Runners.2.6.4/tools/nunit-console.exe $(CSHARP_SWIG_DIR)/LibskycoinNetTest/bin/Release/LibskycoinNetTest.dll -labels

test-skyapi: ## Run SkyApi test suite
	(cd $(CSHARP_CLIENT_DIR) && /bin/sh mono_nunit_test.sh)

test: build test-libsky test-skyapi ## Run all tests

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
