using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficLightSim2
{
    [Serializable()]
    public class Settings : ISerializable
    {
        //Singleton Instance
        private static Settings simulationSettings = null;

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
        public static int CAR_FREQUENCY = 48;
        public static int LIGHT_CYCLE_TIME = 15000;
        public static double TRAFFIC_JAM_THRESHOLD = .8;
        public static double TRAFFIC_JAM_LANES_JAMMED = .5;

        public static int[] V_LANE_BOUNDS = { 1, 5 };
        public static int[] H_LANE_BOUNDS = { 1, 5 };
        public static double[] H_CAR_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static double[] V_CAR_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static double[] TURN_LEFT_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static double[] TURN_RIGHT_PROBABILITY_BOUNDS = { 0.0, 1.0 };
        public static int[] LIGHT_CYCLE_BOUNDS = { 1, 10 };
        public static double BREAKDOWN_PROBABILITY_LIMIT = 100.00;

        public static int DEFAULT_ROAD_LENGTH = 500;
        public static int DEFAULT_ROAD_CENTER = 250;
        public static int DEFAULT_ROAD_SPEED = 1;
        public static double DEFAULT_CAR_PROBABILITY = .9;
        public static double DEFAULT_TURN_PROBABILITY = .4;
        public static double DEFAULT_BREAKDOWN_PROBABILITY = .01;
        public static int DEFAULT_BREAKDOWN_TIME = 5000;
        public static bool DEFAULT_TRAFFIC_FLOW = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
        public static int DEFAULT_LANE_NUMBER = 3;

        private short hWestLanes = (short)DEFAULT_LANE_NUMBER;
        private short hEastLanes = (short)DEFAULT_LANE_NUMBER;
        private short vNorthLanes = (short)DEFAULT_LANE_NUMBER;
        private short vSouthLanes = (short)DEFAULT_LANE_NUMBER;
        private int vLaneLength = DEFAULT_ROAD_LENGTH;
        private int hLaneLength = DEFAULT_ROAD_LENGTH;
        private int vRoadSpeed = 4;
        private int hRoadSpeed = 2;
        private int vIntersectionCenter = DEFAULT_ROAD_CENTER;
        private int hIntersectionCenter = DEFAULT_ROAD_CENTER;
        private double hCarProbability = DEFAULT_CAR_PROBABILITY;
        private double vCarProbability = DEFAULT_CAR_PROBABILITY;
        private double turnLeftProbability = DEFAULT_TURN_PROBABILITY;
        private double turnRightProbability = DEFAULT_TURN_PROBABILITY;
        private double breakdownProbability = DEFAULT_BREAKDOWN_PROBABILITY;
        private int breakdownTime = DEFAULT_BREAKDOWN_TIME;
        private bool trafficFlow = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
        private bool simulationRunning = false;

        public static bool automate;
        

        protected Settings()
        {
            //Prevents Instantiation
        }

        protected Settings(SerializationInfo info, StreamingContext ctxt)
        {
            this.hWestLanes = (short)info.GetValue("hWestLanes", typeof(short));
            this.hEastLanes = (short)info.GetValue("hEastLanes", typeof(short));
            this.vNorthLanes = (short)info.GetValue("vNorthLanes", typeof(short));
            this.vSouthLanes = (short)info.GetValue("vSouthLanes", typeof(short));
            this.vLaneLength = (int)info.GetValue("vLaneLength", typeof(int));
            this.hLaneLength = (int)info.GetValue("hLaneLength", typeof(int));
            this.hRoadSpeed = (int)info.GetValue("hRoadSpeed", typeof(int));
            this.vRoadSpeed = (int)info.GetValue("vRoadSpeed", typeof(int));
            this.vIntersectionCenter = (int)info.GetValue("vIntersectionCenter", typeof(int));
            this.hIntersectionCenter = (int)info.GetValue("hIntersectionCenter", typeof(int));
            this.hCarProbability = (double)info.GetValue("hCarProbability", typeof(double));
            this.vCarProbability = (double)info.GetValue("vCarProbability", typeof(double));
            this.turnLeftProbability = (double)info.GetValue("turnLeftProbability", typeof(double));
            this.turnRightProbability = (double)info.GetValue("turnRightProbability", typeof(double));
            this.breakdownProbability = (double)info.GetValue("breakdownProbability", typeof(double));
            this.breakdownTime = (int)info.GetValue("breakdownTime", typeof(int));
            this.trafficFlow = (bool)info.GetValue("trafficFlow", typeof(bool));

        }

        public Hashtable outputSettings()
        {
            Hashtable outputSettings = new Hashtable();
            if (getHWestLanes() != DEFAULT_LANE_NUMBER)
            {
                outputSettings.Add("hWestLanes", getHWestLanes());
            }
            if (getHEastLanes() != DEFAULT_LANE_NUMBER)
            {
                outputSettings.Add("hEastLanes", getHEastLanes());
            }
            if (getVNorthLanes() != DEFAULT_LANE_NUMBER)
            {
                outputSettings.Add("vNorthLanes", getVNorthLanes());
            }
            if (getVSouthLanes() != DEFAULT_LANE_NUMBER)
            {
                outputSettings.Add("vSouthLanes", getVSouthLanes());
            }
            if (getvLaneLength() != DEFAULT_ROAD_LENGTH)
            {
                outputSettings.Add("vLaneLength", getvLaneLength());
            }
            if (gethLaneLength() != DEFAULT_ROAD_LENGTH)
            {
                outputSettings.Add("hLaneLength", getVSouthLanes());
            }
            if (getvRoadSpeed() != DEFAULT_ROAD_SPEED)
            {
                outputSettings.Add("vRoadSpeed", getvRoadSpeed());
            }
            if (gethRoadSpeed() != DEFAULT_ROAD_SPEED)
            {
                outputSettings.Add("hRoadSpeed", gethRoadSpeed());
            }
            if (gethIntersectionCenter() != DEFAULT_ROAD_CENTER)
            {
                outputSettings.Add("hIntersectionCenter", gethIntersectionCenter());
            }
            if (getvIntersectionCenter() != DEFAULT_ROAD_CENTER)
            {
                outputSettings.Add("vIntersectionCenter", getvIntersectionCenter());
            }
            if (getHCarProbability() != DEFAULT_CAR_PROBABILITY)
            {
                outputSettings.Add("hCarProbability", getHCarProbability());
            }
            if (getVCarProbability() != DEFAULT_CAR_PROBABILITY)
            {
                outputSettings.Add("vCarProbability", getVCarProbability());
            }
            if (getTurnLeftProbability() != DEFAULT_CAR_PROBABILITY)
            {
                outputSettings.Add("turnLeftProbability", getTurnLeftProbability());
            }
            if (getTurnRightProbability() != DEFAULT_CAR_PROBABILITY)
            {
                outputSettings.Add("turnRightProbability", getTurnRightProbability());
            }
            if (getBreakdownProbability() != DEFAULT_CAR_PROBABILITY)
            {
                outputSettings.Add("breakdownProbability", getBreakdownProbability());
            }
            if (getBreakdownTime() != DEFAULT_BREAKDOWN_TIME)
            {
                outputSettings.Add("breakdownTime", getBreakdownTime());
            }
            if (getTrafficFlow() != TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
            {
                outputSettings.Add("trafficFlow", getTrafficFlow());
            }
            return outputSettings;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            if (getHWestLanes() != DEFAULT_LANE_NUMBER)
            {
                info.AddValue("hWestLanes", this.hWestLanes);
            }
            if (getHEastLanes() != DEFAULT_LANE_NUMBER)
            {
                info.AddValue("hEastLanes", this.hEastLanes);
            }
            if (getVNorthLanes() != DEFAULT_LANE_NUMBER)
            {
                info.AddValue("vNorthLanes", this.vNorthLanes);
            }
            if (getVSouthLanes() != DEFAULT_LANE_NUMBER)
            {
                info.AddValue("vSouthLanes", this.vSouthLanes);
            }
            if (getvLaneLength() != DEFAULT_ROAD_LENGTH)
            {
                info.AddValue("vLaneLength", this.vLaneLength);
            }
            if (gethLaneLength() != DEFAULT_ROAD_LENGTH)
            {
                info.AddValue("hLaneLength", this.hLaneLength);
            }
            if (getvRoadSpeed() != DEFAULT_ROAD_SPEED)
            {
                info.AddValue("vRoadSpeed", this.vRoadSpeed);
            }
            if (gethRoadSpeed() != DEFAULT_ROAD_SPEED)
            {
                info.AddValue("hRoadSpeed", this.hRoadSpeed);
            }
            if (gethIntersectionCenter() != DEFAULT_ROAD_CENTER)
            {
                info.AddValue("hIntersectionCenter", this.hIntersectionCenter);
            }
            if (getvIntersectionCenter() != DEFAULT_ROAD_CENTER)
            {
                info.AddValue("vIntersectionCenter", this.vIntersectionCenter);
            }
            if (getHCarProbability() != DEFAULT_CAR_PROBABILITY)
            {
                info.AddValue("hCarProbability", this.hCarProbability);
            }
            if (getVCarProbability() != DEFAULT_CAR_PROBABILITY)
            {
                info.AddValue("vCarProbability", this.vCarProbability);
            }
            if (getTurnLeftProbability() != DEFAULT_CAR_PROBABILITY)
            {
                info.AddValue("turnLeftProbability", this.turnLeftProbability);
            }
            if (getTurnRightProbability() != DEFAULT_CAR_PROBABILITY)
            {
                info.AddValue("turnRightProbability", this.turnRightProbability);
            }
            if (getBreakdownProbability() != DEFAULT_CAR_PROBABILITY)
            {
                info.AddValue("breakdownProbability", this.breakdownProbability);
            }
            if (getBreakdownTime() != DEFAULT_BREAKDOWN_TIME)
            {
                info.AddValue("breakdownTime", this.breakdownTime);
            }
            if (getTrafficFlow() != TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
            {
                info.AddValue("trafficFlow", this.trafficFlow);
            }
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

        public void setLightCycleTime(int time)
        {
            LIGHT_CYCLE_TIME = time;
        }
    }
}
