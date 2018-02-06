using SpinSpin.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin
{
    public class PermutationOperator : IPermutationOperator
    {
        private int[] permutations { get; set; }
        private FunctionSymmetry functionSymmetry { get; set; }

        public PermutationOperator(int[] permutations, FunctionSymmetry functionSymmetry)
        {
            this.permutations = permutations;
            this.functionSymmetry = functionSymmetry;
        }

        int[] IPermutationOperator.Permutations()
        {
            return this.permutations;
        }

        public FunctionSymmetry FunctionSymmetry()
        {
            return functionSymmetry;
        }
    }

}
