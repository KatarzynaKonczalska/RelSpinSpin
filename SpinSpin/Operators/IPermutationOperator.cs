using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Operators
{
    public interface IPermutationOperator
    {
        int[] Permutations();
        FunctionSymmetry FunctionSymmetry();
    }
}
