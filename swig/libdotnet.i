%module skycoin
%include "typemaps.i"

%{
	#define SWIG_FILE_WITH_INIT
	#include "libskycoin.h"
	#include "swig.h"
	#include "skyerrors.h"
%}

%include "csharp_java_typemap.i"
%include "csharp_structs_typemaps.i"
%include "csharp_java_basic.i"
%include "csharp_skycoin.mem.i"
%include "csharp_java_callback.i"

%include "/gopath/src/github.com/skycoin/skycoin/lib/swig/includes.i"
%include "swig.h"
/* Find the modified copy of libskycoin */
%include "libskycoin.h"
%include "structs.i"
%include "skyerrors.h"
// %include "base64.h"

