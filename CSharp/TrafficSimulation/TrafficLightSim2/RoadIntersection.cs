using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class RoadIntersection
   {
        public static short RED_LIGHT = 0;
	    public static short YELLOW_LIGHT = 1;
	    public static short GREEN_LIGHT = 2;
	
	    private Road road;
	    private short lightState;
        private int intersectionCenter;

        public RoadIntersection(int intersectionCenter, short noWestNorthLanes, short noEastSouthLanes, int roadLength)
        {
            this.road = new Road(noEastSouthLanes, noWestNorthLanes, roadLength);
            this.intersectionCenter = intersectionCenter;

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
	        if(lightState < 0 || lightState > 2) 
            {
                lightState = 0;
	        }
            
            this.lightState = lightState;

            if (this.lightState == GREEN_LIGHT)
            {
                carStoppedReset();
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
    }
}
