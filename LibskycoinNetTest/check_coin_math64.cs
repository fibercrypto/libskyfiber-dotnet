using System; 
using NUnit.Framework; 
using skycoin; 
using utils; 
namespace LibskycoinNetTest {
[TestFixture ()]
    public class check_coin_math64:skycoin.skycoin {

utils.transutils transutils = new utils.transutils (); 

struct math_test {
public long a; 
public ulong b; 
public int failure; 
}

math_test[] cases = new math_test[1]; 

public void FullCases () {
var c = new math_test (); 
c = new math_test (); 
c.a = long.MaxValue; 
c.b = long.MaxValue; 
c.failure = SKY_OK; 
cases[0] = c; 
}

[Test]
public void Test64BitIntToUint32() {
for (int i = 0; i < cases.Length; i++) {
				math_test math_Test = cases[i]; 
				var result = new_GoUint32Ptr(); 
				var err = SKY_coin_IntToUint32(math_Test.a, result); 
				if(math_Test.failure == SKY_OK){
					Assert.AreEqual(math_Test.failure , err);
					Assert.AreEqual(GoUint32Ptr_value(result), math_Test.a);
				}
}
}
}
}