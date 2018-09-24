//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace skycoin {

public class cipher_SHA256 : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal cipher_SHA256(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(cipher_SHA256 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~cipher_SHA256() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          skycoinPINVOKE.delete_cipher_SHA256(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int isEqual(cipher_SHA256 a) {
    int ret = skycoinPINVOKE.cipher_SHA256_isEqual(swigCPtr, cipher_SHA256.getCPtr(a));
    return ret;
  }

  public void assignFrom(SWIGTYPE_p_void data) {
    skycoinPINVOKE.cipher_SHA256_assignFrom(swigCPtr, SWIGTYPE_p_void.getCPtr(data));
  }

  public void assignTo(SWIGTYPE_p_void data) {
    skycoinPINVOKE.cipher_SHA256_assignTo(swigCPtr, SWIGTYPE_p_void.getCPtr(data));
  }

  public _GoString_ getStr() {
    _GoString_ ret = new _GoString_(skycoinPINVOKE.cipher_SHA256_getStr(swigCPtr), true);
    return ret;
  }

  public SWIGTYPE_p_unsigned_char data {
    set {
      skycoinPINVOKE.set_cipher_SHA256_data(swigCPtr, SWIGTYPE_p_unsigned_char.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = skycoinPINVOKE.get_cipher_SHA256_data(swigCPtr);
      SWIGTYPE_p_unsigned_char ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_unsigned_char(cPtr, false);
      return ret;
    } 
  }

  public cipher_SHA256() : this(skycoinPINVOKE.new_cipher_SHA256(), true) {
  }

}

}
