using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TrafficLightSim2
{
    class Program
    {
        static void Main()
        {
            Intersection ourIntersection = new Intersection();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrafficLightSimulator());
        }
    }
}