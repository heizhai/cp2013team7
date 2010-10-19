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

        private short hWestLanes = 3;
        private short hEastLanes = 3;
        private short vNorthLanes = 2;
        private short vSouthLanes = 4;
        private int vLaneLength = DEFAULT_ROAD_LENGTH;
        private int hLaneLength = DEFAULT_ROAD_LENGTH;
        private int vRoadSpeed = DEFAULT_ROAD_SPEED;
        private int hRoadSpeed = DEFAULT_ROAD_SPEED;
        private int vIntersectionCenter = 250;
        private int hIntersectionCenter = 250;
        private double hCarProbability = 0.4;
        private double vCarProbability = 0.4;
        private double turnLeftProbability = 1.00;
        private double turnRightProbability = 1.00;
        private double breakdownProbability = 0.01;
        private int breakdownTime = 5000;
        private bool trafficFlow = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
        private bool simulationRunning = false;

        public static bool automate = false; 

        //Static
        public static int LANE_WIDTH = 15;
        public static int CAR_LENGTH = 30;
        public static int CAR_WIDTH = 12;
        public static int CAR_MOVE = 1;
        public static bool TRAFFIC_EAST_SOUTH = true;
        public static bool TRAFFIC_WEST_NORTH = false;
        public static bool TRAFFIC_FLOW_LEFT_HAND_TRAFFIC = false;
        public static bool TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC = true;
        public static bool ROAD_SOUTH_NORTH = true;
        public static bool ROAD_EAST_WEST = false;
        public static int FRAME_LENGTH = 41;
        public static int[] V_LANE_BOUNDS = { 1, 4 };
        public static int[] H_LANE_BOUNDS = { 1, 3 };
        public static double[] H_CAR_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static double[] V_CAR_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static double[] TURN_LEFT_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static double[] TURN_RIGHT_PROBABILITY_BOUNDS = {0.0, 1.0};
        public static double BREAKDOWN_PROBABILITY_LIMIT = 100.00;
        public static int CAR_FREQUENCY = 48;
        public static int[] LIGHT_CYCLE_BOUNDS = { 1, 10 };
        public static int LIGHT_CYCLE_TIME = 15000;

        public static int DEFAULT_ROAD_LENGTH = 500;
        public static int DEFAULT_TRAFFIC_JAM_THRESHOLD = 6;
        public static int DEFAULT_ROAD_SPEED = 1;
        public static bool DEFAULT_TRAFFIC_FLOW = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
        

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
            this.hWestLanes = hWestLanes;
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
            this.hEastLanes = hEastLanes;
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
            this.vNorthLanes = vNorthLanes;
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
            this.vSouthLanes = vSouthLanes;
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

        public int getvRoadSpeed()
        {
            return vRoadSpeed;
        }

        public void setvRoadSpeed(int vRoadSpeed)
        {
            this.vRoadSpeed = vRoadSpeed;
        }

        public int gethRoadSpeed()
        {
            return hRoadSpeed;
        }

        public void sethRoadSpeed(int hRoadSpeed)
        {
            this.hRoadSpeed = hRoadSpeed;
        }

        public double getTurnLeftProbability()
        {
            return turnLeftProbability;
        }

        public void setTurnLeftProbability(double turnLeftProbability)
        {
            if (turnLeftProbability >= TURN_LEFT_PROBABILITY_BOUNDS[0] && turnLeftProbability <= TURN_LEFT_PROBABILITY_BOUNDS[1])
            {
                this.turnLeftProbability = turnLeftProbability;
            }
        }

        public double getTurnRightProbability()
        {
            return turnRightProbability;
        }

        public void setTurnRightProbability(double turnRightProbability)
        {
            if (turnRightProbability >= TURN_RIGHT_PROBABILITY_BOUNDS[0] && turnRightProbability <= TURN_RIGHT_PROBABILITY_BOUNDS[1])
            {
                this.turnRightProbability = turnRightProbability;
            }
        }
    }
}
