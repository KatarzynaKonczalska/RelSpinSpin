using SpinSpin.Interface.Controllers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpinSpin.Interface
{
    public partial class Form1 : Form
    {
        OperatorsFromButtonsController operatorsFromButtonsController = new OperatorsFromButtonsController();
        OperatorsController operatorsController;

        Point previousLocation;
        Control activeControl;

        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("alfa");
            listBox1.Items.Add("beta");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button panel = new Button();
            panel.Width = 50;
            panel.Height = 50;
            panel.Location = new Point(0, 0);

            panel.MouseDown += new MouseEventHandler(panel_MouseDown);
            panel.MouseMove += new MouseEventHandler(panel_MouseMove);
            panel.MouseUp += new MouseEventHandler(panel_MouseUp);

            operatorsFromButtonsController.AddFunctionButton(panel);

            panel2.Controls.Add(panel);
            panel.Text = operatorsFromButtonsController.GetFunctionNumber().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Nie została zaznaczona funkcja");
                return;
            }
            Button panel = new Button();
            
            panel.Width = 50;
            panel.Height = 50;
            panel.Location = new Point(0, 0);

            panel.MouseDown += new MouseEventHandler(panel_MouseDown);
            panel.MouseMove += new MouseEventHandler(panel_MouseMove);
            panel.MouseUp += new MouseEventHandler(panel_MouseUp);

            var vector = listBox1.SelectedItem.ToString();
            operatorsFromButtonsController.AddSpinButton(panel, vector);

            panel.Text = operatorsFromButtonsController.GetSpinNumber().ToString() + "." + listBox1.SelectedItem.ToString();
            panel1.Controls.Add(panel);
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
                return;

            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as Control;
            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var functionButtons = operatorsFromButtonsController.GetFunctionButtons();
            var spinButtons = operatorsFromButtonsController.GetSpinButtons();
            operatorsController = new OperatorsController(functionButtons,spinButtons);
            youngForFunction.Text = operatorsController.AllFunctionOperatorsToString();
            youngForSpin.Text = operatorsController.AllSpinOperatorsToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var funkcjaPrzestrzenna = operatorsFromButtonsController.GetFunction();
            var funkcjaSpinowa = operatorsFromButtonsController.GetSpinFunction();
            int electronNumber = funkcjaPrzestrzenna.Count;

            var operatorySymetryczne = operatorsController.GetSymmetricOperators();
            var operatoryAntysymetryczne = operatorsController.GetAntysymmetricOperators();
            var operatorySymetryczneSpinu = operatorsController.GetSymmetricSpinOperators();
            var operatoryAntysymetryczneSpinu = operatorsController.GetAntysymmetricSpinOperators();

            var norm = Math.Sqrt(operatorsController.GetNorm()) * factorialRecursion(funkcjaPrzestrzenna.Count);
            Action form = new Action(funkcjaPrzestrzenna.ToArray(), funkcjaSpinowa.ToArray(),
                operatorySymetryczne, operatoryAntysymetryczne, operatorySymetryczneSpinu, operatoryAntysymetryczneSpinu, norm,
                electronNumber);
            form.Show();
        }

        private double factorialRecursion(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * factorialRecursion(number - 1);
        }

    }
}




