using NUnit.Framework;
using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ChangePlacesTest
    {
        [Test]
        public void ChangePlacesInFunctionTest()
        {
            List<Function> testList = new List<Function>();
            testList.Add(new Function(0.3, new int[] { 1, 3, 2 }, new int[] { 3, 2, 1 }, new int[] { 1, 2 }));
            Function result = PlaceChanger.ChangePlaces(testList).First();

            Assert.IsTrue(Enumerable.SequenceEqual(result.braFunction, new int[] { 1, 2, 3 }));
            Assert.IsTrue(Enumerable.SequenceEqual(result.ketFunction, new int[] { 2, 3, 1 }));
            Assert.IsTrue(Enumerable.SequenceEqual(result.delta, new int[] { 1, 3 }));
        }
    }
}
