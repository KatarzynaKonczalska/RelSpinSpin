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
        public static List<Function> Redukcja(List<BaseFunction> bra, List<BaseFunction> ket, int i, int j)
        {
            List<Function> result = new List<Function>();
            foreach (var braF in bra)
            {
                foreach (var ketF in ket)
                {
                    //if (ketF.SpinsAreEquals(braF))
                    if(Enumerable.SequenceEqual(ketF.spinFunction,braF.spinFunction))
                    {
                        result.Add(new Function(ketF.factor * braF.factor, braF.CloneFunction(),
                            ketF.CloneFunction(), new int[] { i, j }));
                    }
                }
            }

            result = result.Where(x => x.factor != 0).ToList();
            return result;
        }

        public static List<Function> ReductionWithDelta(List<Function> functions)
        {
            var result = new List<Function>();

            foreach (var function in functions)
            {
                if (!result.ContainFunctionWithDelta(function))
                {
                    result.Add(function);
                    foreach (var f in functions)
                    {
                        if (!ReferenceEquals(f, function) && f.FunctionsEquals(function))
                        {
                            function.factor += f.factor;
                        }
                    }
                }
            }
            return result;
        }


        public static bool ContainFunctionWithDelta(this List<Function> functions, Function function)
        {
            foreach (var func in functions)
            {
                if (func.FunctionsEquals(function) && Enumerable.SequenceEqual(func.delta, function.delta))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
