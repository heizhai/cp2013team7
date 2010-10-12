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
            string Str = textBox1.Text.Trim();
            short Num;
            bool isNum = short.TryParse(Str, out Num);

            if (isNum)
            {
                short num = Convert.ToInt16(textBox1.Text);
                if (num < 4 && num > 0)
                {
                //    Settings.getSimSettings().setVLanes(num);
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
