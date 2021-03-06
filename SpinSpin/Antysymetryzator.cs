﻿using SpinSpin.ExtensionMethods;
using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin
{
    public static class Antysymetryzator
    {
        public static List<BaseFunction> Antysymmetrize(List<BaseFunction> functions, int electronsNumber)
        {
            List<int[]> ANm = GenerateAntysymmetricANOperators(electronsNumber);
            List<int[]> ANp = GenerateSymmetricANOperators(electronsNumber);

            List<BaseFunction> result = new List<BaseFunction>();

            foreach (var function in functions)
            {
                result.Add(function);
            }

            SymmetricOperatorsOperate(functions, ANp, result, electronsNumber);
            AntysymmetricOperatorsOperate(functions, ANm, result, electronsNumber);

            result = result.SumAllFunctions(electronsNumber);
            return result;

        }

        private static void AntysymmetricOperatorsOperate(List<BaseFunction> functions, List<int[]> ANm, List<BaseFunction> result, int electronsNumber)
        {
            //operatory antysymetryzacji
            foreach (var operatorr in ANm)
            {
                foreach (var function in functions)
                {
                    result.Add(new BaseFunction
                    {
                        function = Permutator.Permutate(function.Clone(electronsNumber).function, operatorr, electronsNumber),
                        spinFunction = Permutator.Permutate(function.Clone(electronsNumber).spinFunction, operatorr, electronsNumber),
                        factor = function.Clone(electronsNumber).factor * (-1)
                    });
                }
            }
        }

        private static List<BaseFunction> SymmetricOperatorsOperate(List<BaseFunction> functions, List<int[]> ANp, List<BaseFunction> result, int electronsNumber)
        {
            //operatory symetryzacji
            foreach (var operatorr in ANp)
            {
                foreach (var function in functions)
                {
                    result.Add(new BaseFunction
                    {
                        function = Permutator.Permutate(function.Clone(electronsNumber).function, operatorr, electronsNumber),
                        spinFunction = Permutator.Permutate(function.Clone(electronsNumber).spinFunction, operatorr, electronsNumber),
                        factor = function.Clone(electronsNumber).factor * 1
                    });
                }
            }
            return result;
        }

        private static List<int[]> GenerateSymmetricANOperators(int electronsNumber)
        {
            List<int[]> ANp = new List<int[]>();
            
            switch (electronsNumber)
            {
                case 2:
                    break;
                case 3:
                    ANp.Add(new int[] { 1, 2, 2, 3 });
                    ANp.Add(new int[] { 3, 2, 2, 1 });
                    break;
                case 4:
                    ANp.Add(new int[] { 2, 3, 2, 4 });
                    ANp.Add(new int[] { 2, 3, 3, 4 });
                    ANp.Add(new int[] { 1, 2, 3, 4 });
                    ANp.Add(new int[] { 1, 2, 1, 3 });
                    ANp.Add(new int[] { 1, 4, 2, 4 });
                    ANp.Add(new int[] { 1, 3, 1, 2 });
                    ANp.Add(new int[] { 1, 4, 3, 4 });
                    ANp.Add(new int[] { 1, 3, 2, 4 });
                    ANp.Add(new int[] { 1, 4, 1, 2 });
                    ANp.Add(new int[] { 1, 3, 3, 4 });
                    ANp.Add(new int[] { 2, 3, 1, 4 });
                    break;
                default:
                    throw new NotSupportedException();
            }
            return ANp;
        }

        private static List<int[]> GenerateAntysymmetricANOperators(int electronsNumber)
        {
            List<int[]> ANm = new List<int[]>();

            switch (electronsNumber)
            {
                case 2:
                    ANm.Add(new int[] { 1, 2 });
                    break;
                case 3:
                    ANm.Add(new int[] { 1, 2 });
                    ANm.Add(new int[] { 2, 3 });
                    ANm.Add(new int[] { 3, 1 });
                    break;
                case 4:
                    ANm.Add(new int[] { 3, 4 });
                    ANm.Add(new int[] { 2, 3 });
                    ANm.Add(new int[] { 2, 4 });
                    ANm.Add(new int[] { 1, 2 });
                    ANm.Add(new int[] { 1, 3 });
                    ANm.Add(new int[] { 1, 4 });
                    ANm.Add(new int[] { 1, 4, 2, 4, 3, 4 });
                    ANm.Add(new int[] { 1, 3, 3, 4, 2, 4 });
                    ANm.Add(new int[] { 1, 2, 2, 3, 2, 4 });
                    ANm.Add(new int[] { 1, 4, 2, 3, 3, 4 });
                    ANm.Add(new int[] { 1, 4, 1, 2, 2, 3 });
                    ANm.Add(new int[] { 1, 3, 2, 4, 3, 4 });
                    break;
                default:
                    throw new NotSupportedException();
            }

            return ANm;
        }
    }
}
