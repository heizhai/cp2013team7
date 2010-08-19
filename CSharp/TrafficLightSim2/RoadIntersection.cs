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
	    private int intersectionStopLine;

        public RoadIntersection(int stopLine, short noLanes, int roadLength)
        {
            this.road = new Road(noLanes, roadLength);
            this.intersectionStopLine = stopLine;
        }
	
	    /**
	     * Returns the road element which this section of the intersection belongs to
	     *
	     * @return the road
	     */
	    public Road getRoad() 
        {
	        return road;
	    }
	
	    /**
        * Sets the road element which this section of the intersection belongs to
	     *
	     * @param road the road to set
	     */
	    public void setRoad(Road road) 
        {
            this.road = road;
        }
	
        /**
         * Returns the state that the lights are in (Red, Yellow or Green). This can be compared to the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT from the Simulation class
        *
        * @return the lightState
        */
        public short getLightState() 
        {
	        return lightState;
	    }
	
        /**
	     * Sets the state that the lights are in (Red, Yellow or Green). This can be set using the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT. Note, values that do not match the constants will be assumed to be 0 (red).
        *
        * @param lightState the lightState to set
	     */
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

	    /**
        * Returns the location of the Intersection's stop line on the road.
        *
        * @return the intersectionStopLine
	     */
        public int getIntersectionStopLine() 
        {
	        return intersectionStopLine;
	    }
	
	    /**
	     * Sets the location of the Intersection's stop line on the road.
	     *
	     * @param intersectionStopLine the intersectionStopLine to set
	     */
	    public void setIntersectionStopLine(int intersectionStopLine) 
        {
	        this.intersectionStopLine = intersectionStopLine;
	    }

        public void carStoppedReset() 
        {
        foreach (Lane l in this.road.getLanes()) 
        {
            foreach (Car c in l.getCars()) 
            {
                c.setStopped(false);
            }
        }
    }
    }
}
