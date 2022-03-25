using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrarySolver;
using System.Threading;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ThreeTasksTests
    {
        [TestMethod]
        public void NumbersWithNDivisors_ReturnsListOfDiv()
        {
            // arrange
            long start = 10;
            long finish = 1000;
            long n = 6;
            CancellationToken obj = new CancellationToken();
            long expected0 = 24;
            long expected1 = 30;
            long expected179 = 999; 

            // act
            List<long> range = Solver.NumbersWithNDivisors(start, finish, n, obj);

            // assert
            Assert.AreEqual(expected0, range[0]);
            Assert.AreEqual(expected1, range[1]);
            Assert.AreEqual(expected179, range[179]);
        }

        [TestMethod]
        public void NumbersWithSumOfMinMaxDivisorsEqualsN_ReturnsArrayOfNums()
        {
            // arrange
            long start = 10;
            long count = 20;
            long n = 5;
            CancellationToken obj = new CancellationToken();
            long expected0 = 26;
            long expected1 = 46;
            long expected19 = 406;


            // act
            long[] nums = Solver.NumbersWithSumOfMinMaxDivisorsEqualsN(start, count, n, obj);

            // assert
            Assert.AreEqual(expected0, nums[0]);
            Assert.AreEqual(expected1, nums[1]);
            Assert.AreEqual(expected19, nums[19]);
        }

        [TestMethod]
        public void NumbersWithDigitNInBaseP_ReturnsListOfNums()
        {
            // arrange
            long[] numbers = {3,4,6,7};
            long P = 8;
            long n = 6;
            long expected0 = 6;

            // act
            List<long> nums = Solver.NumbersWithDigitNInBaseP(numbers, P, n);

            // assert
            Assert.AreEqual(expected0, nums[0]);
        }
    }
}