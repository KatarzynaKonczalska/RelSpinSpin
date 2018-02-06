using SpinSpin.Functions;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinSpin.Interface
{
    public partial class Action : Form
    {
        int step = 0;
        double factor = (double)1;
        int[] funkcjaPrzestrzenna;
        string[] funkcjaSpinowa;
        double norm;
        List<int[]> operatorySymetryczne = new List<int[]>();
        List<int[]> operatoryAntysymetryczne = new List<int[]>();
        List<int[]> operatorySymetryczneSpinu = new List<int[]>();
        List<int[]> operatoryAntysymetryczneSpinu = new List<int[]>();

        BaseFunction baseFunction;
        List<BaseFunction> youngFunctions;
        List<BaseFunction> youngSpinFunctions;
        List<BaseFunction> completeYoung;
        List<BaseFunction> antysymmetrizedFunctions;
        List<BaseFunction> SiSjFunctions;
        List<Function> reductionFunctionList;
        List<Function> placesChanged;
        List<Function> lastReduction;

        public Action(int[] funkcjaPrzestrzenna, string[] funkcjaSpinowa,
            List<int[]> operatorySymetryczne, List<int[]> operatoryAntysymetryczne,
            List<int[]> operatorySymetryczneSpinu, List<int[]> operatoryAntysymetryczneSpinu,
            double norm)
        {
            InitializeComponent();
            step = 0;
            this.funkcjaPrzestrzenna = funkcjaPrzestrzenna;
            this.funkcjaSpinowa = funkcjaSpinowa;
            this.operatorySymetryczne = operatorySymetryczne;
            this.operatoryAntysymetryczne = operatoryAntysymetryczne;
            this.operatorySymetryczneSpinu = operatorySymetryczneSpinu;
            this.operatoryAntysymetryczneSpinu = operatoryAntysymetryczneSpinu;
            this.norm = norm;
        }

        private void changeStep(stepAction stepAction)
        {
            if (stepAction == stepAction.Add) step++;
            else step--;

            switch (step)
            {
                case 1:
                    baseFunction = new BaseFunction(factor, funkcjaPrzestrzenna, funkcjaSpinowa);
                    youngFunctions = Young.Young.FunctionOperate(baseFunction, operatorySymetryczne, operatoryAntysymetryczne);
                    stepNameLabel.Text = "Operator Younga dla funkcji przestrzennej";
                    showStepFunctions(youngFunctions);
                    previousStepButton.Enabled = false;
                    break;
                case 2:
                    previousStepButton.Enabled = true;
                    youngSpinFunctions = Young.Young.SpinOperate(baseFunction, operatorySymetryczneSpinu, operatoryAntysymetryczneSpinu);
                    stepNameLabel.Text = "Operator Younga dla funkcji spinowej";
                    showStepFunctions(youngSpinFunctions);
                    break;
                case 3:
                    completeYoung = Young.Young.FunctionsCombine(youngFunctions, youngSpinFunctions);
                    antysymmetrizedFunctions = Antysymetryzator.Antysymmetrize(completeYoung);
                    stepNameLabel.Text = "AN ([Y(fi)] [Y+psi]) =";
                    showStepFunctions(antysymmetrizedFunctions);
                    break;
                case 4:
                    var deltas = PermutationGenerator.GeneratePairs();
                    reductionFunctionList = new List<Function>();
                    foreach (var delta in deltas)
                    {
                        var y = new List<Function>();
                        SiSjFunctions = SiSj.Operate(antysymmetrizedFunctions, delta[0], delta[1]);
                        var Fi = new List<BaseFunction>(); //
                        Fi.Add(baseFunction); //
                        y = Reduktor.Redukcja(antysymmetrizedFunctions, SiSjFunctions, delta[0], delta[1]);
                        //y = Reduktor.Redukcja(Fi, SiSjFunctions, delta[0], delta[1]);
                        //y = Reduktor.Redukcja(completeYoung, SiSjFunctions, delta[0], delta[1]); -> ważne
                        foreach (var element in y)
                        {
                            reductionFunctionList.Add(element);
                        }
                    }
                    stepNameLabel.Text = "SiSj (AN ([Y(fi)] [Y+psi])) =";
                    showLastStepFunctions(reductionFunctionList);


                        
                    break;
                case 5:
                    placesChanged = PlaceChanger.ChangePlaces(reductionFunctionList);
                    stepNameLabel.Text = "Change Places";
                    showLastStepFunctions(placesChanged);
                    nextStepButton.Enabled = true;
                    break;
                case 6:
                    placesChanged = PlaceChanger.ChangePlacesInDelta(placesChanged);
                    stepNameLabel.Text = "Change Places in delta";
                    showLastStepFunctions(placesChanged);
                    break;
                case 7:
                    lastReduction = Reduktor.ReductionWithDelta(placesChanged);
                    stepNameLabel.Text = "Final result: ";
                    foreach(var item in lastReduction)
                    {
                        item.factor = item.factor / (norm*norm);
                    }
                    showLastStepFunctions(lastReduction);
                    nextStepButton.Enabled = false;
                    break;


            }
        }

        private enum stepAction
        {
            Add,
            Substract
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            changeStep(stepAction.Add);
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            changeStep(stepAction.Substract);
        }

        private void showStepFunctions(List<BaseFunction> functions)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var function in functions)
            {
                builder.Append(function.ToStringBaseFunction()).Append(Environment.NewLine);
            }
            resultTextBox.Text = builder.ToString();
        }

        private void showLastStepFunctions(List<Function> functions)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var function in functions)
            {
                builder.Append(function.ToString()).Append(Environment.NewLine);
            }
            resultTextBox.Text = builder.ToString();
        }
    }
}
