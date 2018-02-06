using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Operators
{
    public class UnitOperator : IPermutationOperator
    {
        private FunctionSymmetry functionSymmetry;

        public UnitOperator()
        {
            this.functionSymmetry = SpinSpin.FunctionSymmetry.symmetric;
        }

        public FunctionSymmetry FunctionSymmetry()
        {
            return this.functionSymmetry;
        }

        public int[] Permutations()
        {
            return new int[] { 1, 1 };
        }
    }
}
