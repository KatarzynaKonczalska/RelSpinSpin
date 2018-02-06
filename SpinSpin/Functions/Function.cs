using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Functions
{
    public class Function
    {
        public double factor { get; set; }
        public int[] braFunction { get; set; }
        public int[] ketFunction { get; set; }
        public int[] delta { get; set; }

        public Function(double factor, int[] braFunction, int[] ketFunction, int[] delta)
        {
            this.factor = factor;
            this.braFunction = braFunction;
            this.ketFunction = ketFunction;
            this.delta = delta;
        }

        public bool FunctionsEquals(Function otherFunction)
        {
            bool result = true;
            //bra 
            for (int i = 0; i < GlobalParameters.N; i++)
            {
                if (otherFunction.braFunction[i] != braFunction[i]
                    || otherFunction.ketFunction[i] != ketFunction[i])
                {
                    result = false;
                }
            }
            if (otherFunction.delta[0] != delta[0]
                || otherFunction.delta[1] != delta[1])
            {
                result = false;
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";
            if (this.braFunction == null && this.ketFunction == null)
            {
                throw new ArgumentException();
            }
            else if (this.braFunction == null && this.ketFunction != null)
            {
                result += this.factor + " * ";
                foreach (var ket in ketFunction)
                {
                    result += ket.ToString() + " ";
                }
            }
            else if (this.braFunction != null && this.ketFunction == null)
            {
                result += this.factor + " * ";
                foreach (var bra in braFunction)
                {
                    result += bra.ToString() + " ";
                }
            }
            else
            {
                if (this.factor >= 0) result += " ";
                result += this.factor.ToString() + "\t * \t < ";
                foreach (var bra in this.braFunction)
                {
                    result += bra.ToString() + " ";
                }
                result += " | ";
                result += this.delta[0]+ " ";
                result += this.delta[1];
                result += " | ";
                foreach (var ket in this.ketFunction)
                {
                    result += ket.ToString() + " ";
                }
                result += ">";
            }

            return result;
        }


    }
}
