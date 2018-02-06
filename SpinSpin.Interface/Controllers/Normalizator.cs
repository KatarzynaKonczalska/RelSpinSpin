using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Interface
{
    public class Normalizator
    {
        public static int Normalize(List<int[]> operatorySymetryczne, List<int[]> operatoryAntysymetryczne,
            List<int[]> operatorySymetryczneSpinu, List<int[]> operatoryAntysymetryczneSpinu)
        {
            int norm = 1;
            norm = Norm(operatorySymetryczne, norm);
            norm = Norm(operatoryAntysymetryczne, norm);
            norm = Norm(operatorySymetryczneSpinu, norm);
            norm = Norm(operatoryAntysymetryczneSpinu, norm);

            return norm;
        }

        private static int Norm(List<int[]> operators, int norm)
        {
            foreach (var item in operators)
            {
                norm = norm * item.Length;
            }

            return norm;
        }
    }
}
