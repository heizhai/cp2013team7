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
    public partial class setVLanes : Form
    {
        mainWindow mainForm;

        public setVLanes(mainWindow mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string Str1 = textBox1.Text.Trim();
            string Str2 = textBox2.Text.Trim();

            short Num1;
            short Num2;
            bool isNum1 = short.TryParse(Str1, out Num1);
            bool isNum2 = short.TryParse(Str2, out Num2);


            if (isNum1 && isNum2)
            {
                short num1 = Convert.ToInt16(textBox1.Text);
                short num2 = Convert.ToInt16(textBox2.Text);

                if (num1 < 4 && num1 > 0 && num2 < 4 && num2 > 0)
                {
                    Settings.getSimSettings().setVNorthLanes(num1);
                    Settings.getSimSettings().setVSouthLanes(num2);

                    mainForm.Refresh();
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
