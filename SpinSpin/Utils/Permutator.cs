using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Utils
{
    public static class Permutator
    {

        public static int[] Permutate(int[] baseFunction, int[] operatorr, int electronsNumber)
        {
            if (IsOdd(operatorr)) operatorr = TransformToEvenOperator(operatorr);

            var function = new int[electronsNumber];
            for (int i = 0; i < electronsNumber; i++)
            {
                function[i] = baseFunction[i];
            }

            int N = operatorr.Length;
            for (int i = (N - 1); i > 0; i = i - 2)
            {
                //znaleźć obie liczby
                int polePierwszej = Find(function, operatorr[i]);
                int poleDrugiej = Find(function, operatorr[i - 1]);

                function[polePierwszej] = operatorr[i - 1];
                function[poleDrugiej] = operatorr[i];
            }
            return function;
        }

        public static int[] TransformToEvenOperator(int[] operatorr)
        {
            List<int> firstPart = operatorr.Take(2).ToList();
            firstPart.Add(firstPart.Last());
            List<int> secondPart = operatorr.Skip(2).ToList();
            firstPart.AddRange(secondPart);
            return firstPart.ToArray();
        }

        public static int Find(int[] function, int number)
        {
            for(int i=0;i<function.Length;i++)
            {
                if(function[i]==number)
                {
                    return i;
                }
            }
            throw new Exception();
        }

        //public static string[] Permutate(string[] baseFunction, string[] functionToPermutate , int[] operatorr)
        //{
        //    var function = new string[GlobalParameters.N]
        //}


        public static string[] Permutate(string[] baseFunction, int[] operatorr, int electronsNumber)
        {
            var function = new string[electronsNumber];
            for (int i = 0; i < electronsNumber; i++)
            {
                function[i] = baseFunction[i];
            }

            for (int i = (operatorr.Length - 1); i > 0; i = i - 2)
            {
                int numerPola1 = operatorr[i] - 1;
                int numerPola2 = operatorr[i - 1] - 1;
                string a = function[numerPola1];
                function[numerPola1] = function[numerPola2];
                function[numerPola2] = a;
            }

            return function;
        }

        public static bool IsOdd(int[] operatorr)
        {
            return operatorr.Length % 2 != 0;
        }
    }
}
