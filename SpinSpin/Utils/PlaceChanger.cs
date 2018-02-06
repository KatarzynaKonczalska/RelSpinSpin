using SpinSpin.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Utils
{
    public static class PlaceChanger
    {
        public static List<Function> ChangePlaces(List<Function> mixedFunctions)
        {
            List<Function> result = new List<Function>();

            foreach (var mixedFunction in mixedFunctions)
            {
                if (ElectronsInOrder(mixedFunction))
                {
                    result.Add(mixedFunction);
                    continue;
                }
                int N = mixedFunction.braFunction.Length;
                result.Add(ChangePlacesSingleFunction(mixedFunction));
            }
            return result;
        }

        public static List<Function> ChangePlacesInDelta(List<Function> mixedFunctions)
        {
            List<Function> result = new List<Function>();

            foreach (Function function in mixedFunctions)
            {
                if (function.delta[0] < function.delta[1]) result.Add(new Function(function.factor, function.braFunction, function.ketFunction, function.delta));
                else result.Add(new Function(function.factor, function.braFunction, function.ketFunction, new int[] { function.delta[1], function.delta[0] }));
            }
            return result;
        }

        private static Function ChangePlacesSingleFunction(Function mixedFunction)
        {
            int[] newBra = (int[])mixedFunction.braFunction.Clone();
            int[] newKet = (int[])mixedFunction.ketFunction.Clone();

            for (int i = 1; i <= newBra.Length; i++)
            {
                int placeShouldBe = i - 1;
                int valueInPlace = newBra[placeShouldBe];
                newBra = Permutator.Permutate(newBra, new int[] { i, valueInPlace });
                newKet = Permutator.Permutate(newKet, new int[] { i, valueInPlace });
            }
            int[] newDelta = new int[2];
            int deltaFirstValuePlace = Permutator.Find(mixedFunction.braFunction, mixedFunction.delta[0]);
            int deltaNewFirstValue = newBra[deltaFirstValuePlace];
            newDelta[0] = deltaNewFirstValue;

            int deltaSecondValuePlace = Permutator.Find(mixedFunction.braFunction, mixedFunction.delta[1]);
            int deltaNewSecondValue = newBra[deltaSecondValuePlace];
            newDelta[1] = deltaNewSecondValue;

            return new Function(mixedFunction.factor, newBra, newKet, newDelta);
        }

        private static bool ElectronsInOrder(Function mixedFunction)
        {
            int lastElectronNumber = mixedFunction.braFunction.Length;
            for (int i = 0; i < lastElectronNumber; i++)
            {
                if (mixedFunction.braFunction[i] != (i + 1)) return false;
            }
            return true;
        }
    }
}
