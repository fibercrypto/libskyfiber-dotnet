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

public class coin__Transaction : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal coin__Transaction(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(coin__Transaction obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~coin__Transaction() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          skycoinPINVOKE.delete_coin__Transaction(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int isEqual(coin__Transaction t) {
    int ret = skycoinPINVOKE.coin__Transaction_isEqual(swigCPtr, coin__Transaction.getCPtr(t));
    return ret;
  }

  public SWIGTYPE_p_GoInt32_ Length {
    set {
      skycoinPINVOKE.coin__Transaction_Length_set(swigCPtr, SWIGTYPE_p_GoInt32_.getCPtr(value));
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_GoInt32_ ret = new SWIGTYPE_p_GoInt32_(skycoinPINVOKE.coin__Transaction_Length_get(swigCPtr), true);
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_GoInt8_ Type {
    set {
      skycoinPINVOKE.coin__Transaction_Type_set(swigCPtr, SWIGTYPE_p_GoInt8_.getCPtr(value));
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_GoInt8_ ret = new SWIGTYPE_p_GoInt8_(skycoinPINVOKE.coin__Transaction_Type_get(swigCPtr), true);
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_GoUint8_ InnerHash {
    set {
      skycoinPINVOKE.coin__Transaction_InnerHash_set(swigCPtr, SWIGTYPE_p_GoUint8_.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = skycoinPINVOKE.coin__Transaction_InnerHash_get(swigCPtr);
      SWIGTYPE_p_GoUint8_ ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_GoUint8_(cPtr, false);
      return ret;
    } 
  }

  public SWIGTYPE_p_GoSlice_ Sigs {
    set {
      skycoinPINVOKE.coin__Transaction_Sigs_set(swigCPtr, SWIGTYPE_p_GoSlice_.getCPtr(value));
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_GoSlice_ ret = new SWIGTYPE_p_GoSlice_(skycoinPINVOKE.coin__Transaction_Sigs_get(swigCPtr), true);
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_GoSlice_ In {
    set {
      skycoinPINVOKE.coin__Transaction_In_set(swigCPtr, SWIGTYPE_p_GoSlice_.getCPtr(value));
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_GoSlice_ ret = new SWIGTYPE_p_GoSlice_(skycoinPINVOKE.coin__Transaction_In_get(swigCPtr), true);
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_GoSlice_ Out {
    set {
      skycoinPINVOKE.coin__Transaction_Out_set(swigCPtr, SWIGTYPE_p_GoSlice_.getCPtr(value));
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      SWIGTYPE_p_GoSlice_ ret = new SWIGTYPE_p_GoSlice_(skycoinPINVOKE.coin__Transaction_Out_get(swigCPtr), true);
      if (skycoinPINVOKE.SWIGPendingException.Pending) throw skycoinPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public coin__Transaction() : this(skycoinPINVOKE.new_coin__Transaction(), true) {
  }

}

}