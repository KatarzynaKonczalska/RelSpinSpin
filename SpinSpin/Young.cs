using SpinSpin.ExtensionMethods;
using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Young
{
    public static class Young
    {
        public static List<BaseFunction> FunctionsCombine(List<BaseFunction> functions, List<BaseFunction> spinFunctions, int electronsNumber)
        {
            List<BaseFunction> result = new List<BaseFunction>();
            foreach (var function in functions)
            {
                foreach (var spin in spinFunctions)
                {
                    result.Add(new BaseFunction
                    {
                        function = function.function,
                        spinFunction = spin.spinFunction,
                        factor = function.factor * spin.factor
                    });
                }
            }
            result = result.SumAllFunctions(electronsNumber);
            return result;
        }

        //public static List<BaseFunction> Operate(BaseFunction function, List<int[]> symmetricalOperators,
        //     List<int[]> antysymmetricalOperators, List<int[]> symmetricalSpinOperators, List<int[]> antysymmetricalSpinOperators)
        //{
        //    var functions = FunctionOperate(function, symmetricalOperators, antysymmetricalOperators);
        //    var spins = SpinOperate(function, symmetricalSpinOperators, antysymmetricalSpinOperators);

        //    List<BaseFunction> result = new List<BaseFunction>();

        //    foreach (var fun in functions)
        //    {
        //        foreach (var spin in spins)
        //        {
        //            result.Add(new BaseFunction
        //            {
        //                function = fun.function,
        //                spinFunction = spin.spinFunction,
        //                factor = fun.factor * spin.factor
        //            });
        //        }
        //    }

        //    // skracanie
        //    result = result.SumAllFunctions();

        //    return result;
        //}



        public static List<BaseFunction> FunctionOperate(BaseFunction function, List<int[]> symmetricalOperators, List<int[]> antysymmetricalOperators, int electronsNumber)
        {
            List<BaseFunction> result = new List<BaseFunction>();

            result = addBaseFunction(function, result);
            result = symmetricalOperatorsOperate(function, symmetricalOperators, result, electronsNumber);
            result = antysymmetricalOperatorsOperate(function, antysymmetricalOperators, result, electronsNumber);

            return result;
        }

        private static List<BaseFunction> addBaseFunction(BaseFunction function, List<BaseFunction> result)
        {
            result.Add(new BaseFunction
            {
                function = function.function,
                factor = function.factor
            });
            return result;
        }

        private static List<BaseFunction> antysymmetricalOperatorsOperate(BaseFunction function, List<int[]> antysymmetricalOperators, List<BaseFunction> result, int electronsNumber)
        {
            if (antysymmetricalOperators.Any())
            {
                foreach (var operatorr in antysymmetricalOperators)
                {
                    result.Add(new BaseFunction
                    {
                        function = Permutator.Permutate(function.function, operatorr, electronsNumber),
                        factor = function.factor * (-1)
                    });
                }
            }
            return result;
        }

        private static List<BaseFunction> symmetricalOperatorsOperate(BaseFunction function, List<int[]> symmetricalOperators, List<BaseFunction> result, int electronsNumber)
        {
            if (symmetricalOperators.Any())
            {
                foreach (var operatorr in symmetricalOperators)
                {
                    result.Add(new BaseFunction
                    {
                        function = Permutator.Permutate(function.function, operatorr, electronsNumber),
                        factor = function.factor * 1
                    });
                }
            }
            return result;
        }

        public static List<BaseFunction> SpinOperate(BaseFunction function, List<int[]> symmetricalSpinOperators, List<int[]> antysymmetricalSpinOperators, int electronsNumber)
        {
            List<BaseFunction> result = new List<BaseFunction>();
            result.Add(new BaseFunction
            {
                spinFunction = function.spinFunction,
                factor = function.factor
            });

            //symetryzacja
            foreach (var operatorr in symmetricalSpinOperators)
            {
                result.Add(new BaseFunction
                {
                    spinFunction = Permutator.Permutate(function.spinFunction, operatorr, electronsNumber),
                    factor = function.factor * 1
                });
            }

            //antysymetryzacja
            foreach (var operatorr in antysymmetricalSpinOperators)
            {
                result.Add(new BaseFunction
                {
                    spinFunction = Permutator.Permutate(function.spinFunction, operatorr, electronsNumber),
                    factor = function.factor * (-1)
                });
            }

            return result;
        }

    }
}
