using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim
{
    class Car
    {
        private int lanePostion;

        public Car()
        {
        }

        public int carDriveCycle(int cycleDistance)
        {
            return cycleDistance;
        }

        public int getPostion()
        {
            return lanePostion;
        }
    }
}
