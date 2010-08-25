using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Settings
    {
        //Singleton Instance
        private static Settings simulationSettings = null;

        private static short hLanes = 3;
        private static short vLanes = 3;
        private int vLaneLength = 500;
        private int hLaneLength = 500;
        private int vLaneStopLine = 250;
        private int hLaneStopLine = 250;
        private double hCarProbability = 1;
        private double vCarProbability = 1;
        private bool simulationRunning = false;

        public static bool automate = false;
        public static short numberCycles;

        //Static
        public static int LANE_WIDTH = 15;
        public static int DEFAULT_ROAD_LENGTH = 500;
        public static int CAR_LENGTH = 30;
        public static int CAR_WIDTH = 12;
        public static int CAR_MOVE = 1;
        public static bool TRAFFIC_EAST_SOUTH = true;
        public static bool TRAFFIC_WEST_NORTH = false;
        public static bool ROAD_SOUTH_NORTH = true;
        public static bool ROAD_EAST_WEST = false;
        public static int FRAME_LENGTH =  100;
        public static int[] V_LANE_BOUNDS = {1,4};
        public static int[] H_LANE_BOUNDS = {1,3};
        public static double[] H_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static double[] V_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static int CAR_FREQUENCY = 48;
        public static int[] LIGHT_CYCLE_BOUNDS = {1,10};
        public static int LIGHT_CYCLE_TIME = 15000;

        protected Settings()
        {
            //Prevents Instantiation
        }

        public static Settings getSimSettings()
        {
            if (simulationSettings == null)
            {
                simulationSettings = new Settings();
            }
            return simulationSettings;
        }

        /**
         * @return the hLanes
         */
        public short getHLanes()
        {
            return hLanes;
        }

        /**
         * @param hLanes the hLanes to set
         */
        public void setHLanes(short hLanes)
        {
            Settings.hLanes = hLanes;
        }

        /**
         * @return the vLanes
         */
        public short getVLanes()
        {
            return vLanes;
        }

        /**
         * @param vLanes the vLanes to set
         */
        public void setVLanes(short vLanes)
        {
            Settings.vLanes = vLanes;
        }

        /**
         * @return the hCarProbability
         */
        public double getHCarProbability()
        {
            return hCarProbability;
        }

        /**
         * @param hCarProbability the hCarProbability to set
         */
        public void setHCarProbability(double hCarProbability)
        {
            this.hCarProbability = hCarProbability;
        }

        /**
         * @return the vCarProbability
         */
        public double getVCarProbability()
        {
            return vCarProbability;
        }

        /**
         * @param vCarProbability the vCarProbability to set
         */
        public void setVCarProbability(double vCarProbability)
        {
            this.vCarProbability = vCarProbability;
        }

        /**
         * @return the vLaneLength
         */
        public int getvLaneLength()
        {
            return vLaneLength;
        }

        /**
         * @param vLaneLength the vLaneLength to set
         */
        public void setvLaneLength(int vLaneLength)
        {
            this.vLaneLength = vLaneLength;
        }

        /**
         * @return the hLaneLength
         */
        public int gethLaneLength()
        {
            return hLaneLength;
        }

        /**
         * @param hLaneLength the hLaneLength to set
         */
        public void sethLaneLength(int hLaneLength)
        {
            this.hLaneLength = hLaneLength;
        }

        /**
         * @return the vLaneStopLine
         */
        public int getvLaneStopLine()
        {
            return vLaneStopLine;
        }

        /**
         * @param vLaneStopLine the vLaneStopLine to set
         */
        public void setvLaneStopLine(int vLaneStopLine)
        {
            this.vLaneStopLine = vLaneStopLine;
        }

        /**
         * @return the hLaneStopLine
         */
        public int gethLaneStopLine()
        {
            return hLaneStopLine;
        }

        /**
         * @param hLaneStopLine the hLaneStopLine to set
         */
        public void sethLaneStopLine(int hLaneStopLine)
        {
            this.hLaneStopLine = hLaneStopLine;
        }

        public bool getSimulationRunning()
        {
            return simulationRunning;
        }

        public void setSimulationRunning(bool simulationRunning)
        {
            this.simulationRunning = simulationRunning;
        }
    }
}
