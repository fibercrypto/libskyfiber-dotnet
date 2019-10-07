// using System; 
// using NUnit.Framework; 
// using skycoin; 
// using utils; 
// namespace LibskycoinNetTest {
// [TestFixture ()]
//         public class check_util_param_distributions:skycoin.skycoin {

// utils.transutils transutils = new utils.transutils (); 

// [Test]
//     public void TestDistributionAddressArrays () {

// var addrs = new GoSlice (); 
// var unlocked = new GoSlice (); 
// var locked = new GoSlice (); 
// SKY_params_GetDistributionAddresses (addrs); 
// Assert.AreEqual (addrs.len, 100); 
// SKY_params_GetUnlockedDistributionAddresses (unlocked); 
// Assert.AreEqual (unlocked.len, 25); 
// SKY_params_GetLockedDistributionAddresses (locked); 
// Assert.AreEqual (locked.len, 75); 

// for (int i = 0; i < addrs.len; i++) {
// var iStr = new _GoString_(); 
// addrs.getAtString(i, iStr); 

// for (int j = 0; j < i + 1; j++) {
// if (j < addrs.len) {
// break; 
// }
// var jStr = new _GoString_(); 
// addrs.getAtString(i + 1, jStr); 
// Assert.AreEqual(iStr.isEqual(jStr), 0); 
// }
// }

// for (int i = 0; i < unlocked.len; i++) {
// var iStr = new _GoString_(); 
// unlocked.getAtString(i, iStr); 

// for (int j = 0; j < i + 1; j++) {
// if (j < unlocked.len) {
// break; 
// }
// var jStr = new _GoString_(); 
// unlocked.getAtString(i + 1, jStr); 
// Assert.AreEqual(iStr.isEqual(jStr), 0); 
// }
// }

// for (int i = 0; i < locked.len; i++) {
// var iStr = new _GoString_(); 
// locked.getAtString(i, iStr); 

// for (int j = 0; j < i + 1; j++) {
// if (j < locked.len) {
// break; 
// }
// var jStr = new _GoString_(); 
// locked.getAtString(i + 1, jStr); 
// Assert.AreEqual(iStr.isEqual(jStr), 0); 
// }
// }

// }
// }
// }