using SpinSpin.Operators;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Interface
{
    public static class TransformerToCompleteOperators
    {
        public static string AllOperatorsToString(List<int[]> symmetricOperators, List<int[]> antysymmetricOperators)
        {
            var allOperators = TransformToCompleteOperators(symmetricOperators, antysymmetricOperators);
            string result = ("1");
            foreach (var operatorr in allOperators)
            {
                if (operatorr.GetType() == typeof(PermutationOperator))
                {
                    if (operatorr.FunctionSymmetry().Equals(FunctionSymmetry.antisymmetric))
                    {
                        result += "-P";
                    }
                    else
                    {
                        result += "+P";
                    }
                    result += IntArrayToString(operatorr.Permutations());
                }
            }
            return result;
        }

        public static List<int[]> GetSymmetricOperators(List<int[]> symmetricOperators, List<int[]> antysymmetricOperators)
        {
            var allOperators = TransformToCompleteOperators(symmetricOperators, antysymmetricOperators);
            var result = allOperators.Where(x => x.FunctionSymmetry() == FunctionSymmetry.symmetric && x.GetType() != typeof(UnitOperator)).Select(x => x.Permutations()).ToList();
            return result;
        }

        public static List<int[]> GetAntysymmetricOperators(List<int[]> symmetricOperators, List<int[]> antysymmetricOperators)
        {
            var allOperators = TransformToCompleteOperators(symmetricOperators, antysymmetricOperators);
            var result = allOperators.Where(x => x.FunctionSymmetry() == FunctionSymmetry.antisymmetric).Select(x => x.Permutations()).ToList();
            return result;
        }

        public static List<IPermutationOperator> TransformToCompleteOperators(List<int[]> symmetricOperators, List<int[]> antysymmetricOperators)
        {
            List<List<IPermutationOperator>> transformedOperators = AllToOperatorsTransform(symmetricOperators, antysymmetricOperators);
            return AllOperatorsMultiply(transformedOperators);
        }

        private static List<IPermutationOperator> AllOperatorsMultiply(List<List<IPermutationOperator>> transformedOperators)
        {
            while (transformedOperators.Count > 1)
            {
                var first = transformedOperators.First();
                transformedOperators.Remove(first);
                var second = transformedOperators.First();
                transformedOperators.Remove(second);
                transformedOperators.Add(MultiplyOperatorsInBrackets(first, second));
            }
            return transformedOperators.First();
        }

        private static List<List<IPermutationOperator>> AllToOperatorsTransform(List<int[]> symmetricOperators, List<int[]> antysymmetricOperators)
        {
            List<List<IPermutationOperator>> transformedOperators = new List<List<IPermutationOperator>>();
            foreach (var item in symmetricOperators)
            {
                if (item.Length > 2)
                {
                    //List<int[]> complexItem = new List<int[]>();
                    //int first = item[0];
                    //int second = item[1];
                    //int third = item[2];
                    //switch (item.Length)
                    //{
                    //    case 3:
                    //        complexItem.Add(new int[] { first, second });
                    //        complexItem.Add(new int[] { first, third });
                    //        complexItem.Add(new int[] { second, third });
                    //        complexItem.Add(new int[] { first, second, third });
                    //        complexItem.Add(new int[] { second, first, third });
                    //        transformedOperators.Add(TransformToOperator(complexItem, FunctionSymmetry.symmetric));
                    //        break;
                    //    case 4:

                    //        int fourth = item[3];
                    //        break;
                    //}
                    transformedOperators.Add(TransformDoublePermutationToOperator(item, FunctionSymmetry.symmetric));
                }
                else
                {
                    transformedOperators.Add(TransformToOperator(item, FunctionSymmetry.symmetric));
                }


            }
            foreach (var item in antysymmetricOperators)
            {
                if (item.Length > 2)
                {
                    //List<int[]> complexItem = new List<int[]>();
                    //int first = item[0];
                    //int second = item[1];
                    //int third = item[2];
                    //switch (item.Length)
                    //{
                    //    case 3:
                    //        complexItem.Add(new int[] { first, second });
                    //        complexItem.Add(new int[] { first, third });
                    //        complexItem.Add(new int[] { second, third });
                    //        complexItem.Add(new int[] { first, second, third });
                    //        complexItem.Add(new int[] { second, first, third });
                    //        transformedOperators.Add(TransformToOperator(complexItem, FunctionSymmetry.antisymmetric));
                    //        break;
                    //    case 4:

                    //        int fourth = item[3];
                    //        break;
                    //}
                    transformedOperators.Add(TransformDoublePermutationToOperator(item, FunctionSymmetry.antisymmetric));
                }
                else
                {
                    transformedOperators.Add(TransformToOperator(item, FunctionSymmetry.antisymmetric));
                }
            }

            return transformedOperators;
        }


        private static List<IPermutationOperator> MultiplyOperatorsInBrackets(List<IPermutationOperator> firstOperator, List<IPermutationOperator> secondOperator)
        {
            List<IPermutationOperator> result = new List<IPermutationOperator>();
            foreach (var first in firstOperator)
            {
                foreach (var second in secondOperator)
                {
                    result.Add(FixOperator(first, second));
                }
            }
            return result;
        }

        private static IPermutationOperator FixOperator(IPermutationOperator first, IPermutationOperator second)
        {
            Type firstType = first.GetType();
            Type secondType = second.GetType();

            if (firstType == typeof(PermutationOperator) && secondType == typeof(PermutationOperator))
            {
                return new PermutationOperator(CombineArraysIntoOne(first.Permutations(), second.Permutations()),
                    FixSymmetries(first.FunctionSymmetry(), second.FunctionSymmetry()));
            }
            else if (firstType == typeof(UnitOperator) && secondType == typeof(PermutationOperator))
            {
                return second;
            }
            else return first;

        }

        private static List<IPermutationOperator> TransformToOperator(int[] operatorr, FunctionSymmetry functionSymmetry)
        {
            List<IPermutationOperator> result = new List<IPermutationOperator>();
            result.Add(new UnitOperator());

            if (Permutator.IsOdd(operatorr))
            {
                var transformed = Permutator.TransformToEvenOperator(operatorr);
                result.Add(new PermutationOperator(transformed, functionSymmetry));
            }
            else
            {
                result.Add(new PermutationOperator(operatorr, functionSymmetry));
            }
            return result;
        }

        private static List<IPermutationOperator> TransformDoublePermutationToOperator(int[] item, FunctionSymmetry permutationSymmetry)
        {
            List<IPermutationOperator> result = new List<IPermutationOperator>();
            result.Add(new UnitOperator());

            int length = item.Length;
            int first = item[0];
            int second = item[1];
            int third = item[2];

            if (permutationSymmetry == FunctionSymmetry.antisymmetric)
            {
                switch (item.Length)
                {
                    case 3:
                        result.Add(new PermutationOperator(new int[] { first, second }, FunctionSymmetry.antisymmetric));
                        result.Add(new PermutationOperator(new int[] { first, third }, FunctionSymmetry.antisymmetric));
                        result.Add(new PermutationOperator(new int[] { second, third }, FunctionSymmetry.antisymmetric));
                        result.Add(new PermutationOperator(new int[] { first, second, second, third }, FunctionSymmetry.symmetric));
                        result.Add(new PermutationOperator(new int[] { second, first, first, third }, FunctionSymmetry.symmetric));
                        break;
                    case 4:
                        throw new NotImplementedException();
                        break;
                }
            }
            else
            {
                switch (item.Length)
                {
                    case 3:
                        result.Add(new PermutationOperator(new int[] { first, second }, FunctionSymmetry.symmetric));
                        result.Add(new PermutationOperator(new int[] { first, third }, FunctionSymmetry.symmetric));
                        result.Add(new PermutationOperator(new int[] { second, third }, FunctionSymmetry.symmetric));
                        result.Add(new PermutationOperator(new int[] { first, second, second, third }, FunctionSymmetry.symmetric));
                        result.Add(new PermutationOperator(new int[] { second, first, first, third }, FunctionSymmetry.symmetric));
                        break;
                    case 4:
                        throw new NotImplementedException();
                        break;
                }
            }
            return result;

        }

        private static List<IPermutationOperator> TransformToOperator(List<int[]> complexItem, FunctionSymmetry functionSymmetry)
        {
            List<IPermutationOperator> result = new List<IPermutationOperator>();
            result.Add(new UnitOperator());

            foreach (var operatorr in complexItem)
            {
                //List<int[]> result = new List<int[]>();
                //foreach (var operatorr in partResult)
                //{
                //    if (Permutator.IsOdd(operatorr)) result.Add(Permutator.TransformToEvenOperator(operatorr));
                //    else result.Add(operatorr);
                //}

                //return result;
                if (Permutator.IsOdd(operatorr))
                {
                    var transformed = Permutator.TransformToEvenOperator(operatorr);
                    result.Add(new PermutationOperator(transformed, functionSymmetry));
                }
                else
                {
                    result.Add(new PermutationOperator(operatorr, functionSymmetry));
                }
            }

            return result;
        }

        private static FunctionSymmetry FixSymmetries(FunctionSymmetry firstSymmetry, FunctionSymmetry secondSymmetry)
        {
            if (firstSymmetry == FunctionSymmetry.antisymmetric && secondSymmetry == FunctionSymmetry.antisymmetric)
            {
                return FunctionSymmetry.symmetric;
            }
            else if (firstSymmetry == FunctionSymmetry.symmetric && secondSymmetry == FunctionSymmetry.symmetric)
            {
                return FunctionSymmetry.symmetric;
            }
            else
            {
                return FunctionSymmetry.antisymmetric;
            }
        }

        private static int[] CombineArraysIntoOne(int[] firstOperator, int[] secondOperator)
        {
            return new List<int>(firstOperator.Concat<int>(secondOperator)).ToArray();
        }

        private static string IntArrayToString(int[] intArray)
        {
            string result = "";
            foreach (var item in intArray)
            {
                result += item.ToString();
            }
            return result;
        }
    }
}
