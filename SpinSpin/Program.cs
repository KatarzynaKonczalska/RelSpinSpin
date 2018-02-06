using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using SpinSpin.ExtensionMethods;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int[]> symmetricalOperators = new List<int[]>();
            List<int[]> antysymmetricalOperators = new List<int[]>();

            List<int[]> symmetricalSpinOperators = new List<int[]>();
            List<int[]> antysymmetricalSpinOperators = new List<int[]>();

            //------------------------------------------------------------------------
            //operatory dla 2 elektronów
            //symmetricalOperators.Add(new int[2] { 1, 2 });
            //antysymmetricalSpinOperators.Add(new int[2] { 1, 2 });
            //------------------------------------------------------------------------


            //------------------------------------------------------------------------
            //operatory dla 3 elektronów
            symmetricalOperators.Add(new int[2] { 1, 2 });
            //antysymmetricalOperators.Add(new int[3] { 2, 1, 3 });
            antysymmetricalOperators.Add(new int[2] { 2, 3 });

            //symmetricalSpinOperators.Add(new int[2] { 2, 3 });
            antysymmetricalSpinOperators.Add(new int[2] { 1, 2 });
            //antysymmetricalSpinOperators.Add(new int[3] { 2, 1, 3 });


            //int[] function = new int[GlobalParameters.N] { 1, 2, 3 };
            //string[] spinFunction = new string[GlobalParameters.N] { "beta", "alfa", "alfa" };

            int[] function = new int[GlobalParameters.N] { 1, 2, 3};
            string[] spinFunction = new string[GlobalParameters.N] { "alfa", "alfa", "beta"};


            //int[] function = new int[GlobalParameters.N] { 1, 2 };
            //string[] spinFunction = new string[GlobalParameters.N] { "beta", "alfa" };


            double factor = (double)1;
            BaseFunction baseFunction = new BaseFunction(factor, function, spinFunction);

            Console.WriteLine("Fi = ");
            Console.WriteLine(baseFunction.ToStringBaseFunction());
            Console.WriteLine("\n");


            List<BaseFunction> functions = new List<BaseFunction>();
            functions.Add(baseFunction);

            Console.WriteLine("[Y(fi)] [Y+psi] = ");
            //var young = Young.Young.Operate(baseFunction, symmetricalOperators, antysymmetricalOperators, symmetricalSpinOperators, antysymmetricalSpinOperators);
            var functionYoung = Young.Young.FunctionOperate(baseFunction, symmetricalOperators, antysymmetricalOperators);
            var spinFunctionYoung = Young.Young.SpinOperate(baseFunction, symmetricalOperators, antysymmetricalSpinOperators);


            var young = Young.Young.FunctionsCombine(functionYoung, spinFunctionYoung);

            foreach (var item in young)
            {
                Console.WriteLine(item.ToStringBaseFunction());
            }
            Console.WriteLine("\n");

            var AN = Antysymetryzator.Antysymmetrize(young);
            Console.WriteLine("AN ([Y(fi)] [Y+psi]) =");
            foreach (var item in AN)
            {
                Console.WriteLine(item.ToStringBaseFunction());
            }
            Console.WriteLine("\n");

            //tworzenie bra
            List<BaseFunction> bra = new List<BaseFunction>();
            foreach (var f in young)
            {
                bra.Add(f.Clone());
            }

            var sisj = SiSj.Operate(young,1,2);
            Console.WriteLine("SiSj (AN ([Y(fi)] [Y+psi])) =");
            foreach (var item in sisj)
            {
                Console.WriteLine(item.ToStringBaseFunction());
            }
            Console.WriteLine("\n");



            var deltas = PermutationGenerator.GeneratePairs();
            var functionsList = new List<Function>();
            var y = new List<Function>();

            if (GlobalParameters.ifDeltaSum)
            {
                foreach (var delta in deltas)
                {
                    y = Reduktor.Redukcja(young, sisj, delta[0], delta[1]);
                    foreach (var element in y)
                    {
                        functionsList.Add(element);
                    }
                }
            }
            else
            {
                y = Reduktor.Redukcja(bra, sisj, 1, 2);
                foreach (var element in y)
                {
                    functionsList.Add(element);
                }
            }

            //foreach(var item in functionsList)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            foreach (var el in y)
            {
                el.factor = (double)el.factor * (double)(-(double)8 / (double)3);
            }
            var wynik = new List<Function>();






        }

    }
}