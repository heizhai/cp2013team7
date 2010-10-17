package trafficsim;

/**
 * Class which handles the elements of an intersection which are explicitly associated with the road, rather than the intersection itself.
 *
 * @author Tristan Davey
 */
public class RoadIntersection {

    static final short RED_LIGHT = 0;
    static final short YELLOW_LIGHT = 1;
    static final short GREEN_LIGHT = 2;
    static final short TURNING_YELLOW_LIGHT = 3;
    static final short TURNING_GREEN_LIGHT = 4;

    private Road road;
    private short lightState;
    private Boolean roadOrientation;
    private int intersectionCenter;

    RoadIntersection(int intersectionCenter, short noWestNorthLanes, short noEastSouthLanes, int roadLength, int roadSpeed, Boolean roadOrientation) {
        this.road = new Road(noEastSouthLanes, noWestNorthLanes, roadLength, roadSpeed);
        this.intersectionCenter = intersectionCenter;
        this.roadOrientation = roadOrientation;
    }

    /**
     * Returns the road element which this section of the intersection belongs to
     *
     * @return the road
     */
    public Road getRoad() {
        return road;
    }

    /**
     * Sets the road element which this section of the intersection belongs to
     *
     * @param road the road to set
     */
    public void setRoad(Road road) {
        this.road = road;
    }

    /**
     * Returns the state that the lights are in (Red, Yellow or Green). This can be compared to the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT from the Simulation class
     *
     * @return the lightState
     */
    public short getLightState() {
        return lightState;
    }

    /**
     * Sets the state that the lights are in (Red, Yellow or Green). This can be set using the constants RED_LIGHT, YELLOW_LIGHT and GREEN_LIGHT. Note, values that do not match the constants will be assumed to be 0 (red).
     *
     * @param lightState the lightState to set
     */
    public void setLightState(short lightState) {
        if(lightState < 0 || lightState > 4) {
            lightState = 0;
        }
        this.lightState = lightState;
        if (this.lightState == GREEN_LIGHT) {
            carStoppedReset();
        }
    }

    /**
     * Returns the location of the Intersection's stop line on the road.
     *
     * @return the intersectionCenter
     */
    public int getIntersectionCenter() {
        return intersectionCenter;
    }

    /**
     * Sets the location of the Intersection's stop line on the road.
     *
     * @param intersectionCenter the intersectionCenter to set
     */
    public void setIntersectionCenter(int intersectionCenter) {
        this.intersectionCenter = intersectionCenter;
    }

    /**
     * Sets the location of the Intersection's stop line on the road.
     *
     * @param intersectionCenter the intersectionCenter to set
     */
    public void carStoppedReset() {
        for(Lane l: this.road.getLanes(Settings.TRAFFIC_EAST_SOUTH)) {
            for(Car c: l.getCars()) {
                c.setStopped(false);
            }
        }
        for(Lane l: this.road.getLanes(Settings.TRAFFIC_WEST_NORTH)) {
            for(Car c: l.getCars()) {
                c.setStopped(false);
            }
        }
    }

    /**
     * @return the roadOrientation
     */
    public Boolean getRoadOrientation() {
        return roadOrientation;
    }

    /**
     * @param roadOrientation the roadOrientation to set
     */
    public void setRoadOrientation(Boolean roadOrientation) {
        this.roadOrientation = roadOrientation;
    }

}
