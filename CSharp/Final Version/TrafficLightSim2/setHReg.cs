using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrafficLightSim2
{
    public partial class setHReg : Form
    {
       mainWindow mainForm;

        public setHReg(mainWindow mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string Str = textBox1.Text.Trim();
            double Num;
            bool isNum = double.TryParse(Str, out Num);

            if (isNum)
            {
                double num = Convert.ToDouble(textBox1.Text);
                if (num < 1.1 && num > -0.1)
                {
                    Settings.getSimSettings().setHCarProbability(num);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Number");
                }
            }

            else
            {
                MessageBox.Show("Enter number!");
            }
        }
    }
}
