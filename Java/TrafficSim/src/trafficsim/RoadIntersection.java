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
            carStoppedResetForward();
        } else if (this.lightState == TURNING_GREEN_LIGHT) {
            if(Settings.getSimSettings().getTrafficFlow() ==  Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
                if(this.getRoadOrientation() == Settings.ROAD_EAST_WEST) {
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0), true);
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)-1)), true);
                } else {
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)-1)), true);
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0), true);
                }
            } else {
                if(this.getRoadOrientation() == Settings.ROAD_EAST_WEST) {
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0), true);
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)-1)), true);
                } else {
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (this.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)-1)), true);
                    this.carLaneStoppedReset(this.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0), true);
                }
            }
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
     * Resets the stop setting of cars after light changes
     *
     * @param intersectionCenter the intersectionCenter to set
     */
    public void carStoppedResetForward() {
        for(Lane l: this.road.getLanes(Settings.TRAFFIC_EAST_SOUTH)) {
            carLaneStoppedReset(l, false);
        }
        for(Lane l: this.road.getLanes(Settings.TRAFFIC_WEST_NORTH)) {
            carLaneStoppedReset(l, false);
        }
    }

    /**
     * resets the stop setting of
     *
     * @param intersectionCenter the intersectionCenter to set
     */
    public void carLaneStoppedReset(Lane lane, Boolean turningCars) {
        for(Car c: lane.getCars()) {
            if(turningCars) {
                if((Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC && c.getTurningRight() == true) || (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC && c.getTurningLeft() == true)) {
                    c.setStopped(false);
                }
            } else {
                c.setStopped(turningCars);
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
