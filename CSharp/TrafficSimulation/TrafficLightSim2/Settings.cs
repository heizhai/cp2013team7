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

        private static short hWestLanes = 3;
        private static short hEastLanes = 3;
        private static short vNorthLanes = 2;
        private static short vSouthLanes = 4;
        private int vLaneLength = 500;
        private int hLaneLength = 500;
        private int vIntersectionCenter = 250;
        private int hIntersectionCenter = 250;
        private double hCarProbability = 1;
        private double vCarProbability = 1;
        private double breakdownProbability = 0.05;
        private int breakdownTime = 10000;
        private bool trafficFlow = false;
        private bool simulationRunning = false;

        public static bool automate = false;
     //   public static short numberCycles;

        //Static
        public static int LANE_WIDTH = 15;
        public static int DEFAULT_ROAD_LENGTH = 500;
        public static int CAR_LENGTH = 30;
        public static int CAR_WIDTH = 12;
        public static int CAR_MOVE = 1;
        public static bool TRAFFIC_EAST_SOUTH = true;
        public static bool TRAFFIC_WEST_NORTH = false;
        public static bool TRAFFIC_FLOW_LEFT_HAND_TRAFFIC = false;
        public static bool TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC = true;
        public static bool ROAD_SOUTH_NORTH = true;
        public static bool ROAD_EAST_WEST = false;
        public static int FRAME_LENGTH =  41;
        public static int[] V_LANE_BOUNDS = {1,4};
        public static int[] H_LANE_BOUNDS = {1,3};
        public static double[] H_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static double[] V_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static double BREAKDOWN_PROBABILITY_LIMIT = 100.00;
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

         //return the hLanes

        public short getHWestLanes()
        {
            return hWestLanes;
        }

        public void setHWestLanes(short hWestLanes)
        {
            Settings.hWestLanes = hWestLanes;
        }

        public short getHEastLanes()
        {
            return hEastLanes;
        }

        /**
         * @param the number of westbound lanes
         */
        public void setHEastLanes(short hEastLanes)
        {
            Settings.hEastLanes = hEastLanes;
        }

        /**
         * @return the number of northbound lanes
         */
        public short getVNorthLanes()
        {
            return vNorthLanes;
        }

        /**
         * @param the number of northbound lanes
         */
        public void setVNorthLanes(short vNorthLanes)
        {
            Settings.vNorthLanes = vNorthLanes;
        }

        /**
         * @return the number of southbound vertical lanes
         */
        public short getVSouthLanes()
        {
            return vSouthLanes;
        }

        /**
         * @param the number of southbound vertical lanes
         */
        public void setVSouthLanes(short vSouthLanes)
        {
            Settings.vSouthLanes = vSouthLanes;
        }

        public double getHCarProbability()
        {
            return hCarProbability;
        }

        public void setHCarProbability(double hCarProbability)
        {
            this.hCarProbability = hCarProbability;
        }

         //return the vCarProbability

        public double getVCarProbability()
        {
            return vCarProbability;
        }

        public void setVCarProbability(double vCarProbability)
        {
            this.vCarProbability = vCarProbability;
        }

         //return the vLaneLength
        public int getvLaneLength()
        {
            return vLaneLength;
        }

        public void setvLaneLength(int vLaneLength)
        {
            this.vLaneLength = vLaneLength;
        }

         //return the hLaneLength

        public int gethLaneLength()
        {
            return hLaneLength;
        }

        public void sethLaneLength(int hLaneLength)
        {
            this.hLaneLength = hLaneLength;
        }

        public bool getSimulationRunning()
        {
            return simulationRunning;
        }

        public void setSimulationRunning(bool simulationRunning)
        {
            this.simulationRunning = simulationRunning;
        }

        public bool getTrafficFlow()
        {
            return trafficFlow;
        }

        public void setTrafficFlow(bool trafficFlow)
        {
            this.trafficFlow = trafficFlow;
        }

        public double getBreakdownProbability()
        {
            return breakdownProbability;
        }

        public void setBreakdownProbability(double breakdownProbability)
        {
            if (breakdownProbability < 100.00)
            {
                this.breakdownProbability = breakdownProbability;
            }
        }

        public int getBreakdownTime()
        {
            return breakdownTime;
        }

        public void setBreakdownTime(int breakdownTime)
        {
            this.breakdownTime = breakdownTime;
        }

        public int getvIntersectionCenter()
        {
            return vIntersectionCenter;
        }

        public void setvIntersectionCenter(int vIntersectionCenter)
        {
            this.vIntersectionCenter = vIntersectionCenter;
        }

        public int gethIntersectionCenter()
        {
            return hIntersectionCenter;
        }

        public void sethIntersectionCenter(int hIntersectionCenter)
        {
            this.hIntersectionCenter = hIntersectionCenter;
        }
    }
}
