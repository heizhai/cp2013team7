using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficLightSim2
{
    [Serializable()]
    public class RoadIntersection : ISerializable
   {
        public static short RED_LIGHT = 0;
	    public static short YELLOW_LIGHT = 1;
	    public static short GREEN_LIGHT = 2;
        public static short TURNING_YELLOW_LIGHT = 3;
        public static short TURNING_GREEN_LIGHT = 4;
        public static short TURNING_RED_LIGHT = 5;
	
	    private Road road;
	    private short lightState;
        private bool roadOrientation;
        private int intersectionCenter;

        public RoadIntersection(int intersectionCenter, short noWestNorthLanes, short noEastSouthLanes, int roadLength, int roadSpeed, bool roadOrientation)
        {
            this.road = new Road(noEastSouthLanes, noWestNorthLanes, roadLength, roadSpeed);
            this.intersectionCenter = intersectionCenter;
            this.roadOrientation = roadOrientation;

        }

        public RoadIntersection(SerializationInfo info, StreamingContext ctxt)
       {
          this.road = (Road)info.GetValue("road", typeof(Road));
       }

       public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
       {
          info.AddValue("road", this.road);
       }
	
	     //Returns the road element which this section of the intersection belongs to

	    public Road getRoad() 
        {
	        return road;
	    }
	
	    
        // Sets the road element which this section of the intersection belongs to

	    public void setRoad(Road road) 
        {
            this.road = road;
        }
	

        // Returns the state that the lights are in (Red, Yellow or Green). This can be compared to the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT from the Simulation class
  
        public short getLightState() 
        {
	        return lightState;
	    }
	
          
        //Sets the state that the lights are in (Red, Yellow or Green). This can be set using the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT. Note, values that do not match the constants will be assumed to be 0 (red).

        public void setLightState(short lightState) 
        {
	        if(lightState < 0 || lightState > 5) 
            {
                lightState = 0;
	        }
            
            this.lightState = lightState;

            if (this.lightState == GREEN_LIGHT)
            {
                carStoppedReset();
            }

            else if (this.lightState == TURNING_GREEN_LIGHT)
            {
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    if (this.getRoadOrientation() == Settings.ROAD_EAST_WEST)
                    {
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0), true);
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) - 1)), true);
                    }

                    else
                    {
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) - 1)), true);
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0), true);
                    }
                }
                else
                {
                    if (this.getRoadOrientation() == Settings.ROAD_EAST_WEST)
                    {
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0), true);
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) - 1)), true);
                    }

                    else
                    {
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) - 1)), true);
                        this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0), true);
                    }
                }
            }
        }

        public void carStoppedResetForward() 
        {
            foreach (Lane l in this.road.getLanes(Settings.TRAFFIC_EAST_SOUTH)) 
            {
                carLaneStoppedReset(l, false);
            }
            foreach (Lane l in this.road.getLanes(Settings.TRAFFIC_WEST_NORTH)) 
            {
                carLaneStoppedReset(l, false);
            }
        }

        /**
         * resets the stop setting of
         *
         * @param intersectionCenter the intersectionCenter to set
         */
        public void carLaneStoppedReset(Lane lane, Boolean turningCars) 
        {
            foreach (Car c in lane.getCars()) 
            {
                if(turningCars) 
                {
                    if ((Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC && c.getTurningRight() == true) || (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC && c.getTurningLeft() == true)) 
                    {
                        c.setStopped(false);
                    }
                } 
                else 
                {
                    c.setStopped(turningCars);
                }
            }
        }

        // Returns the location of the Intersection's stop line on the road.
        
        public int getIntersectionCenter() 
        {
	        return intersectionCenter;
	    }
	
	     //Sets the location of the Intersection's stop line on the road.

	    public void setIntersectionCenter(int intersectionCenter) 
        {
	        this.intersectionCenter = intersectionCenter;
	    }

        public void carStoppedReset() 
        {
            foreach(Lane l in this.road.getLanes(Settings.TRAFFIC_EAST_SOUTH)) 
            {
                foreach(Car c in l.getCars()) 
                {
                    c.setStopped(false);
                }
            }
            foreach(Lane l in this.road.getLanes(Settings.TRAFFIC_WEST_NORTH)) 
            {
                foreach(Car c in l.getCars()) 
                {
                    c.setStopped(false);
                }
            }
        }

        public Boolean getRoadOrientation()
        {
            return roadOrientation;
        }

        /**
         * @param roadOrientation the roadOrientation to set
         */
        public void setRoadOrientation(Boolean roadOrientation)
        {
            this.roadOrientation = roadOrientation;
        }
    }
}
