//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class coin__UxOut : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal coin__UxOut(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(coin__UxOut obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~coin__UxOut() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          skycoinPINVOKE.delete_coin__UxOut(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int isEqual(coin__UxOut u) {
    int ret = skycoinPINVOKE.coin__UxOut_isEqual(swigCPtr, coin__UxOut.getCPtr(u));
    return ret;
  }

  public coin__UxHead Head {
    set {
      skycoinPINVOKE.coin__UxOut_Head_set(swigCPtr, coin__UxHead.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = skycoinPINVOKE.coin__UxOut_Head_get(swigCPtr);
      coin__UxHead ret = (cPtr == global::System.IntPtr.Zero) ? null : new coin__UxHead(cPtr, false);
      return ret;
    } 
  }

  public coin__UxBody Body {
    set {
      skycoinPINVOKE.coin__UxOut_Body_set(swigCPtr, coin__UxBody.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = skycoinPINVOKE.coin__UxOut_Body_get(swigCPtr);
      coin__UxBody ret = (cPtr == global::System.IntPtr.Zero) ? null : new coin__UxBody(cPtr, false);
      return ret;
    } 
  }

  public coin__UxOut() : this(skycoinPINVOKE.new_coin__UxOut(), true) {
  }

}