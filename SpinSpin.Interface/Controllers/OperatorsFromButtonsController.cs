using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinSpin.Interface.Controllers
{
    public class OperatorsFromButtonsController
    {
        private int functionN;
        private int spinN;

        private List<Button> functionButtons = new List<Button>();
        private List<Button> spinButtons = new List<Button>();

        private List<int> funkcjaPrzestrzenna = new List<int>();
        private List<string> funkcjaSpinowa = new List<string>();

        public OperatorsFromButtonsController()
        {
            functionN = 0;
            spinN = 0;
        }

        public void AddFunctionButton(Button panel)
        {
            functionN++;
            funkcjaPrzestrzenna.Add(functionN);
            functionButtons.Add(panel);
        }

        public void AddSpinButton(Button panel, string vector)
        {
            spinN++;
            funkcjaSpinowa.Add(vector);
            spinButtons.Add(panel);
        }

        public int GetSpinNumber()
        {
            return spinN;
        }

        public int GetFunctionNumber()
        {
            return functionN;
        }

        public List<Button> GetFunctionButtons()
        {
            return functionButtons;
        }

        public List<Button> GetSpinButtons()
        {
            return spinButtons;
        }

        public List<int> GetFunction()
        {
            return funkcjaPrzestrzenna;
        }

        public List<string> GetSpinFunction()
        {
            return funkcjaSpinowa;
        }

    }
}
