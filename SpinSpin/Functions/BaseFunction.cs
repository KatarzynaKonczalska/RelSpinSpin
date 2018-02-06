using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Functions
{
    public class BaseFunction
    {
        public double factor { get; set; }
        public int[] function { get; set; }
        public string[] spinFunction { get; set; }

        public BaseFunction()
        {

        }

        public BaseFunction(double factor, int[] function, string[] spinFunction)
        {
            this.factor = factor;
            this.function = function;
            this.spinFunction = spinFunction;
        }

        public BaseFunction Clone()
        {
            double copyFactor = this.factor;
            int[] copyFunction = new int[GlobalParameters.N];
            string[] copySpinFunction = new string[GlobalParameters.N];
            for (int i = 0; i < GlobalParameters.N; i++)
            {
                copyFunction[i] = function[i];
                copySpinFunction[i] = spinFunction[i];
            }
            BaseFunction baseFunction = new BaseFunction(copyFactor, copyFunction, copySpinFunction);
            return baseFunction;
        }

        public int[] CloneFunction()
        {
            int[] result = new int[GlobalParameters.N];
            for (int i = 0; i < GlobalParameters.N; i++)
            {
                result[i] = function[i];
            }
            return result;
        }

        public bool SpinsAreEquals(BaseFunction otherFunction)
        {
            bool result = true;
            for (int i = 0; i < GlobalParameters.N; i++)
            {
                if (!otherFunction.spinFunction[i].Equals(spinFunction[i]))
                {
                    result = false;
                }
            }
            return result;
        }

        public bool FunctionEquals(BaseFunction otherFunction)
        {
            bool result = true;
            for (int i=0; i <GlobalParameters.N; i++)
            {
                if (otherFunction.function[i] != function[i]
                    || otherFunction.spinFunction[i] != spinFunction[i])
                {
                    result = false;
                }
            }
            return result;
        }

        public string ToStringBaseFunction()
        {
            string result = "";

            if (this.function == null && this.spinFunction == null)
            {
                throw new ArgumentNullException();
            }

            else if (this.function == null && this.spinFunction != null)
            {
                result += this.factor + " * ";
                foreach (var spin in this.spinFunction)
                {
                    result += spin.ToString() + " ";
                }
            }

            else if (this.function != null && this.spinFunction == null)
            {
                result += this.factor + " * ";
                foreach (var value in this.function)
                {
                    result += value.ToString() + " ";
                }
            }

            else
            {
                result += this.factor + " * ";
                foreach (var value in this.function)
                {
                    result += value.ToString() + " ";
                }
                foreach (var spin in this.spinFunction)
                {
                    result += spin.ToString() + " ";
                }
            }

            return result;
        }
    }
}
