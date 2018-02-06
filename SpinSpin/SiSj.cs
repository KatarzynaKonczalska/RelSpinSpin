using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin
{
    public static class SiSj
    {
        public static List<BaseFunction> Operate(List<BaseFunction> functions, int i, int j)
        {
            List<BaseFunction> result = new List<BaseFunction>();
            foreach (var function in functions)
            {
                var example = function.Clone();
                var x = SPSM(example, i, j);
                example = function.Clone();
                var y = SMSP(example, i, j);
                example = function.Clone();
                var z = SZSZ(example, i, j);
                if (x != null) result.Add(x.Clone());
                if (y != null) result.Add(y.Clone());
                if (z != null) result.Add(z.Clone());
            }

            //List<BaseFunction> copy = new List<BaseFunction>();
            //foreach (var el in result)
            //{
            //    copy.Add(el.Clone());
            //    //copy.Add(new BaseFunction(el.factor, el.function, el.spinFunction));
            //}

            //var test = result.Where(x => ( Enumerable.SequenceEqual(x.spinFunction, new string[] {"alfa","beta","alfa" }) )).ToList();

            return SumSameFunctions(result);
        }

        private static List<BaseFunction> SumSameFunctions(List<BaseFunction> result)
        {
            //sumowanie
            List<BaseFunction> zredukowane = new List<BaseFunction>();
            foreach (var function in result)
            {
                if (!zredukowane.ContainFunction(function))
                {
                    zredukowane.Add(function);
                    foreach (var f in result)
                    {
                        if (!ReferenceEquals(function, f) && f.FunctionEquals(function))
                        {
                            function.factor += f.factor;
                        }
                    }
                }
            }
            List<BaseFunction> final = zredukowane.Where(x => x.factor != 0).ToList();

            return final;
        }

        public static BaseFunction SPSM(BaseFunction function, int i, int j)
        {
            i -= 1;
            j -= 1;
            if (function.spinFunction[i] != "alfa" && function.spinFunction[j] != "beta")
            {
                BaseFunction f = new BaseFunction(function.factor,
                    function.function, function.spinFunction);
                f.factor *= 0.5;
                f.spinFunction[i] = "alfa";
                f.spinFunction[j] = "beta";
                //result.Add(f);
                return f;
            }
            //return result;
            return null;
        }

        public static bool ContainFunction(this List<BaseFunction> functions, BaseFunction function)
        {
            foreach (var func in functions)
            {
                if (func.FunctionEquals(function))
                {
                    return true;
                }
            }
            return false;
        }

        private static BaseFunction SMSP(BaseFunction function, int i, int j)
        {
            i -= 1;
            j -= 1;
            if (function.spinFunction[i] != "beta" && function.spinFunction[j] != "alfa")
            {
                BaseFunction f = new BaseFunction(function.factor,
                    function.function, function.spinFunction);
                f.factor *= 0.5;
                f.spinFunction[i] = "beta";
                f.spinFunction[j] = "alfa";
                return f;
            }
            return null;
        }

        private static BaseFunction SZSZ(BaseFunction function, int i, int j)
        {
            i -= 1;
            j -= 1;
            BaseFunction f = new BaseFunction(function.factor,
                    function.function, function.spinFunction);
            if (function.spinFunction[i] == "alfa")
            {
                f.spinFunction[i] = "alfa";
                f.factor *= 0.5;
            }
            else
            {
                f.spinFunction[i] = "beta";
                f.factor *= -0.5;
            }

            if (function.spinFunction[j] == "alfa")
            {
                f.spinFunction[j] = "alfa";
                f.factor *= 0.5;
            }
            else
            {
                f.spinFunction[j] = "beta";
                f.factor *= -0.5;
            }
            return f;
        }
    }
}
