using NUnit.Framework;
using SpinSpin;
using SpinSpin.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class AntysymmetrizatorTest
    {
        [Test]
        public void AntysymmetrizatorTestt()
        {
            var baseFunction = new BaseFunction(1, new int[] { 1, 2, 3 }, new string[] { "alfa", "beta", "alfa" });
            List<BaseFunction> list = new List<BaseFunction>();
            list.Add(baseFunction);
            var result = Antysymetryzator.Antysymmetrize(list);

            
        }
    }
}
