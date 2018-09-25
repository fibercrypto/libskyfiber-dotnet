using System;
using NUnit.Framework;
using skycoin;
namespace LibskycoinNetTest {
    [TestFixture ()]
    public class check_util_fee {

        struct verifyTxFeeTestCase {
            public ulong inputHours;
            public ulong ouputHours;
            public int err;
        }
        verifyTxFeeTestCase[] burnFactor2verifyTxFeeTestCase = new verifyTxFeeTestCase[15];

        public void FullburnFactor2verifyTxFeeTestCase () {

            verifyTxFeeTestCase cases = new verifyTxFeeTestCase ();
            cases.inputHours = 0;
            cases.ouputHours = 0;
            cases.err = skycoin.skycoin.SKY_ErrTxnNoFee;
            burnFactor2verifyTxFeeTestCase[0] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 1;
            cases.ouputHours = 0;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[1] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 1;
            cases.ouputHours = 1;
            cases.err = skycoin.skycoin.SKY_ErrTxnNoFee;
            burnFactor2verifyTxFeeTestCase[2] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 2;
            cases.ouputHours = 0;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[3] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 2;
            cases.ouputHours = 1;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[4] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 2;
            cases.ouputHours = 2;
            cases.err = skycoin.skycoin.SKY_ErrTxnNoFee;
            burnFactor2verifyTxFeeTestCase[5] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 3;
            cases.ouputHours = 0;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[6] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 3;
            cases.ouputHours = 1;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[7] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 3;
            cases.ouputHours = 2;
            cases.err = skycoin.skycoin.SKY_ErrTxnInsufficientFee;
            burnFactor2verifyTxFeeTestCase[8] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 3;
            cases.ouputHours = 3;
            cases.err = skycoin.skycoin.SKY_ErrTxnNoFee;
            burnFactor2verifyTxFeeTestCase[9] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 4;
            cases.ouputHours = 0;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[10] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 4;
            cases.ouputHours = 1;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[11] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 4;
            cases.ouputHours = 2;
            cases.err = skycoin.skycoin.SKY_OK;
            burnFactor2verifyTxFeeTestCase[12] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 4;
            cases.ouputHours = 3;
            cases.err = skycoin.skycoin.SKY_ErrTxnInsufficientFee;
            burnFactor2verifyTxFeeTestCase[13] = cases;

            cases = new verifyTxFeeTestCase ();
            cases.inputHours = 4;
            cases.ouputHours = 4;
            cases.err = skycoin.skycoin.SKY_ErrTxnNoFee;
            burnFactor2verifyTxFeeTestCase[14] = cases;
        }

        [Test]
        public void TestVerifyTransactionFee () {
            FullburnFactor2verifyTxFeeTestCase ();
            var empty = skycoin.skycoin.new_Transaction__Handlep ();
            skycoin.skycoin.makeEmptyTransaction (empty);
            var hours = skycoin.skycoin.new_GoUint64p ();
            var err = skycoin.skycoin.SKY_coin_Transaction_OutputHours (empty, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), 0);

            // A txn with no outputs hours and no coinhours burn fee is valid
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (empty, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrTxnNoFee);

            // A txn with no outputs hours but with a coinhours burn fee is valid
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (empty, 100);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            var txn = skycoin.skycoin.new_Transaction__Handlep ();
            skycoin.skycoin.makeEmptyTransaction (txn);
            var addr = skycoin.skycoin.new_cipher__Addressp ();
            err = (uint) skycoin.skycoin.makeAddress (addr);
            Assert.AreEqual (err, 0);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (txn, addr, 0, 1000000);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (txn, addr, 0, 3000000);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_OutputHours (txn, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            Assert.AreEqual (skycoin.skycoin.GoUint64p_value (hours), 4000000);

            // A txn with insufficient net coinhours burn fee is invalid
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, 0);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrTxnNoFee);
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, 1);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ErrTxnInsufficientFee);

            // A txn with sufficient net coinhours burn fee is valid
            err = skycoin.skycoin.SKY_coin_Transaction_OutputHours (txn, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, skycoin.skycoin.GoUint64p_value (hours));
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_coin_Transaction_OutputHours (txn, hours);
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, ((ulong) (skycoin.skycoin.GoUint64p_value (hours) * 10)));
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK);

            // fee + hours overflows
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, ((ulong.MaxValue - 3000000)));
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR);

            // txn has overflowing output hours
            err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (txn, addr, 0,
                (ulong.MaxValue - 1000000 - 3000000 + 1));
            Assert.AreEqual (err, skycoin.skycoin.SKY_OK, "txn has overflowing output hours");
            err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, 10);
            Assert.AreEqual (err, skycoin.skycoin.SKY_ERROR, "SKY_fee_VerifyTransactionFee failed");

            int len = burnFactor2verifyTxFeeTestCase.Length;
            for (int i = 0; i < len; i++) {
                txn = skycoin.skycoin.new_Transaction__Handlep ();
                skycoin.skycoin.makeEmptyTransaction (txn);
                verifyTxFeeTestCase tc = burnFactor2verifyTxFeeTestCase[i];
                err = skycoin.skycoin.SKY_coin_Transaction_PushOutput (txn, addr, 0, tc.ouputHours);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.IsTrue (tc.inputHours >= tc.ouputHours);
                err = skycoin.skycoin.SKY_fee_VerifyTransactionFee (txn, (ulong) (tc.inputHours - tc.ouputHours));
                Assert.AreEqual (tc.err, err, "Iter " + i.ToString () + " is " + tc.err.ToString () + " != " + err.ToString ());;
            }
        }
        struct requiredFeeTestCase {
            public ulong hours;
            public ulong fee;
        }
        requiredFeeTestCase[] burnFactor2verifyTxFeeTestCase2 = new requiredFeeTestCase[12];
        public void FullburnFactor2RequiredFeeTestCases2 () {
            var cases = new requiredFeeTestCase ();
            cases.hours = 0;
            cases.fee = 0;
            burnFactor2verifyTxFeeTestCase2[0] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 1;
            cases.fee = 1;
            burnFactor2verifyTxFeeTestCase2[1] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 2;
            cases.fee = 1;
            burnFactor2verifyTxFeeTestCase2[2] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 3;
            cases.fee = 2;
            burnFactor2verifyTxFeeTestCase2[3] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 4;
            cases.fee = 2;
            burnFactor2verifyTxFeeTestCase2[4] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 5;
            cases.fee = 3;
            burnFactor2verifyTxFeeTestCase2[5] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 6;
            cases.fee = 3;
            burnFactor2verifyTxFeeTestCase2[6] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 7;
            cases.fee = 4;
            burnFactor2verifyTxFeeTestCase2[7] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 998;
            cases.fee = 499;
            burnFactor2verifyTxFeeTestCase2[8] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 999;
            cases.fee = 500;
            burnFactor2verifyTxFeeTestCase2[9] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 1000;
            cases.fee = 500;
            burnFactor2verifyTxFeeTestCase2[10] = cases;
            cases = new requiredFeeTestCase ();
            cases.hours = 1001;
            cases.fee = 501;
            burnFactor2verifyTxFeeTestCase2[11] = cases;
        }

        [Test]
        public void TestRequiredFee () {
            FullburnFactor2RequiredFeeTestCases2 ();
            var cases = burnFactor2verifyTxFeeTestCase2;

            for (int i = 0; i < cases.Length; i++) {
                var tc = cases[i];
                var fee = skycoin.skycoin.new_GoUint64p ();
                var err = skycoin.skycoin.SKY_fee_RequiredFee (tc.hours, fee);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (tc.fee, skycoin.skycoin.GoUint64p_value (fee));
                var remainingHours = skycoin.skycoin.new_GoUint64p ();
                err = skycoin.skycoin.SKY_fee_RemainingHours (tc.hours, remainingHours);
                Assert.AreEqual (err, skycoin.skycoin.SKY_OK);
                Assert.AreEqual (tc.hours - skycoin.skycoin.GoUint64p_value (fee), skycoin.skycoin.GoUint64p_value (remainingHours));
            }
        }

        struct uxInput {
            public ulong time;
            public ulong coins;
            public ulong hours;
        }
        struct StrTest {
            public ulong[] outs;
            public uxInput[] ins;
            public ulong headTime;
            public ulong fee;
            public int err;
        }
        StrTest[] ListCases = new StrTest[6];
        public void FullCases () {
            ulong headTime = 1000;
            ulong nextTime = (headTime + 3600); //1 hour later
            var cases = new StrTest ();
            cases.fee = 5;
            cases.outs = new ulong[1];
            cases.outs[0] = 5;
            cases.ins = new uxInput[1];
            cases.ins[0].time = headTime;
            cases.ins[0].coins = (ulong) 10e6;
            cases.ins[0].hours = (ulong) 10;
            cases.headTime = headTime;
            ListCases[0] = cases;
        }

        [Test]
        public void TestTransactionFee () {

            for (int i = 0; i < ListCases.Length; i++) {
                var tc = ListCases[i];
                var tx = skycoin.skycoin.new_Transaction__Handlep ();

            }

        }

    }
}