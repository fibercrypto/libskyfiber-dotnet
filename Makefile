PWD = $(shell pwd)
GOPATH_DIR = $(PWD)/gopath
SKYCOIN_DIR = gopath/src/github.com/skycoin/skycoin
SKYBUILD_DIR = $(SKYCOIN_DIR)/build
BUILDLIBC_DIR = $(SKYBUILD_DIR)/libskycoin
LIBC_DIR = $(SKYCOIN_DIR)/lib/cgo
LIBSWIG_DIR = swig
BUILD_DIR = build
BIN_DIR = $(SKYCOIN_DIR)/bin
INCLUDE_DIR = $(SKYCOIN_DIR)/include
FULL_PATH_LIB = $(PWD)/$(BUILDLIBC_DIR)

LIB_FILES = $(shell find $(SKYCOIN_DIR)/lib/cgo -type f -name "*.go")
SRC_FILES = $(shell find $(SKYCOIN_DIR)/src -type f -name "*.go")
SWIG_FILES = $(shell find $(LIBSWIG_DIR) -type f -name "*.i")
HEADER_FILES = $(shell find $(SKYCOIN_DIR)/include -type f -name "*.h")

# Compilation flags for libskycoin
CC_VERSION = $(shell $(CC) -dumpversion)
STDC_FLAG = $(python -c "if tuple(map(int, '$(CC_VERSION)'.split('.'))) < (6,): print('-std=C99'")
LIBC_LIBS = -lcriterion
LIBC_FLAGS = -I$(LIBSRC_DIR) -I$(INCLUDE_DIR) -I$(BUILD_DIR)/usr/include -L $(BUILDLIB_DIR) -L$(BUILD_DIR)/usr/lib

# Platform specific checks
OSNAME = $(TRAVIS_OS_NAME)

ifeq ($(shell uname -s),Linux)
  LDLIBS=$(LIBC_LIBS) -lpthread
  LDPATH=$(shell printenv LD_LIBRARY_PATH)
  LDPATHVAR=LD_LIBRARY_PATH
  LDFLAGS=$(LIBC_FLAGS) $(STDC_FLAG)
  LDCOPY=.
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
  LDFLAGS=$(LIBC_FLAGS) -framework CoreFoundation -framework Security
  LDCOPY=~/Library/Frameworks/
else
  LDLIBS = $(LIBC_LIBS)
  LDPATH=$(shell printenv LD_LIBRARY_PATH)
  LDPATHVAR=LD_LIBRARY_PATH
  LDFLAGS=$(LIBC_FLAGS)
endif

configure: ## Setup build environment
	mkdir -p $(BUILD_DIR)/usr/tmp $(BUILD_DIR)/usr/lib $(BUILD_DIR)/usr/include
	mkdir -p $(BUILDLIBC_DIR) $(BIN_DIR) $(INCLUDE_DIR)

$(BUILDLIBC_DIR)/libskycoin.a: $(LIB_FILES) $(SRC_FILES) $(HEADER_FILES)
	rm -f $(BUILDLIBC_DIR)/libskycoin.a
	GOPATH="$(GOPATH_DIR)" make -C $(SKYCOIN_DIR) build-libc-static
	ls $(BUILDLIBC_DIR)
	rm -f swig/include/libskycoin.h
	mkdir -p swig/include
	grep -v "_Complex" $(INCLUDE_DIR)/libskycoin.h > swig/include/libskycoin.h

build-libc: configure $(BUILDLIBC_DIR)/libskycoin.a ## Build libskycoin static C client library

build-swig: ## Generate csharp source code from SWIG interface definitions
	#Generate structs.i from skytypes.gen.h
	rm -f $(LIBSWIG_DIR)/structs.i
	cp $(INCLUDE_DIR)/skytypes.gen.h $(LIBSWIG_DIR)/structs.i
	#sed -i 's/#/%/g' $(LIBSWIG_DIR)/structs.i
	{ \
		if [[ "$$(uname -s)" == "Darwin" ]]; then \
			sed -i '.kbk' 's/#/%/g' $(LIBSWIG_DIR)/structs.i ;\
		else \
			sed -i 's/#/%/g' $(LIBSWIG_DIR)/structs.i ;\
		fi \
	}
	mkdir -p ./LibskycoinNet/skycoin
	mkdir -p ./LibSkycoinDotNet/skycoin
	rm -f swig/include/swig.h
	rm -f skycoinnet_wrap.o
	rm -f LibskycoinNet/skycoin/skycoinnet_wrap.c
	rm -f LibSkycoinDotNet/skycoin/skycoinnet_wrap.c
	swig -csharp -oldvarnames -v -namespace  skycoin -Iswig/include -I$(INCLUDE_DIR) -outdir LibskycoinNet/skycoin -o skycoinnet_wrap.c $(LIBSWIG_DIR)/libdotnet.i
	swig -csharp -oldvarnames -v -namespace  skycoin -Iswig/include -I$(INCLUDE_DIR) -outdir LibSkycoinDotNet/skycoin -o skycoinnet_wrap.c $(LIBSWIG_DIR)/libdotnet.i
	
build-libskycoin-net: build-libc build-swig ## Build shared library including SWIG wrappers
	$(CC) -c -fpic -Iswig/include -I$(INCLUDE_DIR) -libskycoin skycoinnet_wrap.c
	rm -rfv build/usr/lib/libskycoin.so
	$(CC) -shared skycoinnet_wrap.o $(BUILDLIBC_DIR)/libskycoin.a -o build/usr/lib/libskycoin.so $(LDFLAGS)
	mkdir -p LibskycoinNetTest/bin
	mkdir -p LibSkycoinDotNetTest/bin
	mkdir -p LibskycoinNetTest/bin/Release
	mkdir -p LibSkycoinDotNetTest/bin/Release
	mkdir -p LibSkycoinDotNetTest/bin/Release/netcoreapp2.2
	rm -rfv  LibSkycoinNetTest/bin/Release/libskycoin.so
	rm -rfv  LibSkycoinDotNetTest/bin/Release/libskycoin.so
	# cp build/usr/lib/libskycoin.so LibskycoinNetTest/bin/Release/
	# mkdir -p $(LDCOPY)
	# /usr/bin/sudo cp build/usr/lib/libskycoin.so $(LDCOPY)
	

install-deps: ## Install development dependencies
	nuget restore LibskycoinNet.sln
	dotnet restore LibSkycoinDotNet.sln
	nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner

build-sln: install-deps build-libc build-swig
	$(LDPATHVAR)="$(PWD)/build/usr/lib/" dotnet msbuild /p:VisualStudioVersion=15.0 /p:Configuration=Release LibSkycoinDotNet.sln
	$(LDPATHVAR)="$(PWD)/build/usr/lib/" msbuild /p:VisualStudioVersion=15.0 /p:Configuration=Release LibskycoinNet.sln


build: build-libskycoin-net build-sln ## Build LibSkycoinNet Assembly

test: build ## Run LibSkycoinNet test suite
	$(LDPATHVAR)="$(PWD)/build/usr/lib/:$(LDPATHVAR)" dotnet test LibSkycoinDotNet.sln
	$(LDPATHVAR)="$(PWD)/build/usr/lib/:$(LDPATHVAR)" mono ./testrunner/NUnit.Runners.2.6.4/tools/nunit-console.exe ./LibskycoinNetTest/bin/Release/LibskycoinNetTest.dll -labels
	

help:
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
