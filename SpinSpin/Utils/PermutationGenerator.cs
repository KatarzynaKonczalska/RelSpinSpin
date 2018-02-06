using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Utils
{
    public static class PermutationGenerator
    {
        public static List<int[]> GeneratePairs()
        {
            List<int[]> result = new List<int[]>(2);
            switch (GlobalParameters.N)
            {
                case 2:
                    result.Add(new int[] { 1, 2 });
                    break;
                case 3:
                    result.Add(new int[] { 1, 2 });
                    result.Add(new int[] { 1, 3 });
                    result.Add(new int[] { 2, 3 });
                    break;
                case 4:
                    result.Add(new int[] { 1, 2 });
                    result.Add(new int[] { 1, 3 });
                    result.Add(new int[] { 2, 3 });
                    result.Add(new int[] { 1, 4 });
                    result.Add(new int[] { 2, 4 });
                    result.Add(new int[] { 3, 4 });
                    break;
            }
            return result;
        }
    }
}
