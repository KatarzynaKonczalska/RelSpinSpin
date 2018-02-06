using SpinSpin;
using SpinSpin.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin
{
    public static class Reduktor
    {
        public static List<Function> Redukcja(List<BaseFunction> bra, List<BaseFunction> ket, int i, int j, int electronsNumber)
        {
            List<Function> result = new List<Function>();
            foreach (var braF in bra)
            {
                foreach (var ketF in ket)
                {
                    //if (ketF.SpinsAreEquals(braF))
                    if(Enumerable.SequenceEqual(ketF.spinFunction,braF.spinFunction))
                    {
                        result.Add(new Function(ketF.factor * braF.factor, braF.CloneFunction(electronsNumber),
                            ketF.CloneFunction(electronsNumber), new int[] { i, j }));
                    }
                }
            }

            result = result.Where(x => x.factor != 0).ToList();
            return result;
        }

        public static List<Function> ReductionWithDelta(List<Function> functions, int electronsNumber)
        {
            var result = new List<Function>();

            foreach (var function in functions)
            {
                if (!result.ContainFunctionWithDelta(function, electronsNumber))
                {
                    result.Add(function);
                    foreach (var f in functions)
                    {
                        if (!ReferenceEquals(f, function) && f.FunctionsEquals(function, electronsNumber))
                        {
                            function.factor += f.factor;
                        }
                    }
                }
            }
            return result;
        }


        public static bool ContainFunctionWithDelta(this List<Function> functions, Function function, int electronsNumber)
        {
            foreach (var func in functions)
            {
                if (func.FunctionsEquals(function, electronsNumber) && Enumerable.SequenceEqual(func.delta, function.delta))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
