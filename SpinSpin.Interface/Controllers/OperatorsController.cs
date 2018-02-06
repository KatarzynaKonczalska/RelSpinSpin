using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinSpin.Interface.Controllers
{
    public class OperatorsController
    {
        private List<int[]> operatorySymetryczne = new List<int[]>();
        private List<int[]> operatoryAntysymetryczne = new List<int[]>();
        private List<int[]> operatorySymetryczneSpinu = new List<int[]>();
        private List<int[]> operatoryAyntysymetryczneSpinu = new List<int[]>();
        private int norm;

        public OperatorsController(List<Button> functionButtons, List<Button> spinButtons)
        {
            var pierwszy = functionButtons.Where(x => x.Text == "1").First();

            var neighbourhood = NeiborhoodCreator.CreateNeighborhood(functionButtons);
            operatoryAntysymetryczne = OperatorsFromButtonsGenerator.GenerateAntisymmetricOperators(neighbourhood);
            operatorySymetryczne = OperatorsFromButtonsGenerator.GenerateSymmetricOperators(neighbourhood);

           

            var operatory = TransformerToCompleteOperators.TransformToCompleteOperators(operatorySymetryczne, operatoryAntysymetryczne);

            foreach (var k in spinButtons)
            {
                k.Text = k.Text.ToArray()[0].ToString();
            }
            var neighbourhood2 = NeiborhoodCreator.CreateNeighborhood(spinButtons);
            operatoryAyntysymetryczneSpinu = OperatorsFromButtonsGenerator.GenerateAntisymmetricOperators(neighbourhood2);
            operatorySymetryczneSpinu = OperatorsFromButtonsGenerator.GenerateSymmetricOperators(neighbourhood2);

            norm = Normalizator.Normalize(operatorySymetryczne, operatoryAntysymetryczne, operatorySymetryczneSpinu, operatoryAyntysymetryczneSpinu);
        }

        public string AllSpinOperatorsToString()
        {
            return TransformerToCompleteOperators.AllOperatorsToString(operatorySymetryczneSpinu, operatoryAyntysymetryczneSpinu);
        }

        public string AllFunctionOperatorsToString()
        {
            return TransformerToCompleteOperators.AllOperatorsToString(operatorySymetryczne, operatoryAntysymetryczne);
        }

        public List<int[]> GetAntysymmetricOperators()
        {
            return TransformerToCompleteOperators.GetAntysymmetricOperators(operatorySymetryczne, operatoryAntysymetryczne);
        }

        public List<int[]> GetSymmetricOperators()
        {
            return TransformerToCompleteOperators.GetSymmetricOperators(operatorySymetryczne, operatoryAntysymetryczne);
        }

        public List<int[]> GetAntysymmetricSpinOperators()
        {
            return TransformerToCompleteOperators.GetAntysymmetricOperators(operatorySymetryczneSpinu, operatoryAyntysymetryczneSpinu);
        }

        public List<int[]> GetSymmetricSpinOperators()
        {
            return TransformerToCompleteOperators.GetSymmetricOperators(operatorySymetryczneSpinu, operatoryAyntysymetryczneSpinu);
        }

        public int GetNorm()
        {
            return norm;
        }

    }
}
