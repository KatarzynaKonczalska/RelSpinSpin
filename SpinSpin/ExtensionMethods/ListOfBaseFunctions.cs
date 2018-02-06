using SpinSpin.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.ExtensionMethods
{
    public static class ListOfBaseFunctions
    {
        //public static List<BaseFunction> SumFunctions_new(this List<BaseFunction> functions)
        //{
        //    List<BaseFunction> result = new List<BaseFunction>();
        //    List<BaseFunction> visited = new List<BaseFunction>();



        //}

        public static List<BaseFunction> SumAllFunctions(this List<BaseFunction> functions)
        {
            List<BaseFunction> result = new List<BaseFunction>();
            List<BaseFunction> visited = new List<BaseFunction>();

            foreach (var function in functions)
            {
                foreach (var tempFunction in functions)
                {
                    if (!visited.ContainFunction(tempFunction) && function.FunctionEquals(tempFunction)
                        && function.SpinsAreEquals(tempFunction) && !ReferenceEquals(function, tempFunction))
                    {
                        function.factor += tempFunction.factor;
                    }
                }
                if (!visited.ContainFunction(function))
                {
                    visited.Add(function);
                    result.Add(function);
                }
            }
            return result;
        }
    }
}
