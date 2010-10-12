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
    public partial class setMultiCycle : Form
    {
        mainWindow mainForm;
        SimulationEnvironment simulation;

        public setMultiCycle(mainWindow mainForm, SimulationEnvironment simulation)
        {
            this.mainForm = mainForm;
            this.simulation = simulation;
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string Str = textBox1.Text.Trim();
            short Num;
            bool isNum = short.TryParse(Str, out Num);

            if (isNum)
            {
                short num = Convert.ToInt16(textBox1.Text);
                if (num < 10 && num > 0)
                {
                    simulation.multiLightCycle(num);
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
