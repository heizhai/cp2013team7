package trafficsim;

/**
 * This class is designed to make the setting and retrieval of settings for the application easier. NOTE: This class is a singleton and values must be accessed throught the .getSimSettings() function. 
 *
 * @author Tristan Davey
 */
public class Settings {

    //Singleton Instance
    private static Settings simulationSettings = null;

    //Static
    public static final int LANE_WIDTH = 15;
    public static final int CAR_LENGTH = 30;
    public static final int CAR_WIDTH = 12;
    public static final int CAR_MOVE = 1;
    public static final Boolean TRAFFIC_EAST_SOUTH = true;
    public static final Boolean TRAFFIC_WEST_NORTH = false;
    public static final Boolean TRAFFIC_FLOW_LEFT_HAND_TRAFFIC = false;
    public static final Boolean TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC = true;
    public static final Boolean ROAD_SOUTH_NORTH = true;
    public static final Boolean ROAD_EAST_WEST = false;
    public static final int FRAME_LENGTH =  41; //Running at 24 frames per second.
    public static final int[] V_LANE_BOUNDS = {1,4};
    public static final int[] H_LANE_BOUNDS = {1,3};
    public static final double[] H_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] V_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] TURN_LEFT_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] TURN_RIGHT_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double BREAKDOWN_PROBABILITY_LIMIT = 100.00;
    public static final int CAR_FREQUENCY = 48;
    public static final int[] LIGHT_CYCLE_BOUNDS = {1,10};
    public static final int LIGHT_CYCLE_TIME = 15000;
    public static final double TRAFFIC_JAM_THRESHOLD = .8;
    public static final double TRAFFIC_JAM_LANES_JAMMED = .5;

    public static final int DEFAULT_ROAD_LENGTH = 500;
    public static final int DEFAULT_ROAD_SPEED = 1;
    public static final boolean DEFAULT_TRAFFIC_FLOW = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
    
    private short hWestLanes = 3;
    private short hEastLanes = 3;
    private short vNorthLanes = 3;
    private short vSouthLanes = 4;
    private int vLaneLength = this.DEFAULT_ROAD_LENGTH;
    private int hLaneLength = this.DEFAULT_ROAD_LENGTH;
    private int vRoadSpeed = 4;//this.DEFAULT_ROAD_SPEED;
    private int hRoadSpeed = 2;//this.DEFAULT_ROAD_SPEED;
    private int vIntersectionCenter = 250;
    private int hIntersectionCenter = 250;
    private double hCarProbability = .9;
    private double vCarProbability = .9;
    private double turnLeftProbability = .40;
    private double turnRightProbability = .40;
    private double breakdownProbability = .01;
    private int breakdownTime = 5000;
    private Boolean trafficFlow = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
    private Boolean simulationRunning = false;

    protected void Settings() {
        //Prevents Instantiation
    }
 
    public static synchronized Settings getSimSettings() {
        if (simulationSettings == null) {
            simulationSettings = new Settings();
        }
        return simulationSettings;
    }

    /**
     * @return the number of westbound lanes
     */
    public short getHWestLanes() {
        return hWestLanes;
    }

    /**
     * @param the number of westbound lanes
     */
    public void setHWestLanes(short hWestLanes) {
        this.hWestLanes = hWestLanes;
    }

    /**
     * @return the number of westbound lanes
     */
    public short getHEastLanes() {
        return hEastLanes;
    }

    /**
     * @param the number of westbound lanes
     */
    public void setHEastLanes(short hEastLanes) {
        this.hEastLanes = hEastLanes;
    }

    /**
     * @return the number of northbound lanes
     */
    public short getVNorthLanes() {
        return vNorthLanes;
    }

    /**
     * @param the number of northbound lanes
     */
    public void setVNorthLanes(short vNorthLanes) {
        this.vNorthLanes = vNorthLanes;
    }

    /**
     * @return the number of southbound vertical lanes
     */
    public short getVSouthLanes() {
        return vSouthLanes;
    }

    /**
     * @param the number of southbound vertical lanes
     */
    public void setVSouthLanes(short vSouthLanes) {
        this.vSouthLanes = vSouthLanes;
    }

    /**
     * @return the hCarProbability
     */
    public double getHCarProbability() {
        return hCarProbability;
    }

    /**
     * @param hCarProbability the hCarProbability to set
     */
    public void setHCarProbability(double hCarProbability) {
        this.hCarProbability = hCarProbability;
    }

    /**
     * @return the vCarProbability
     */
    public double getVCarProbability() {
        return vCarProbability;
    }

    /**
     * @param vCarProbability the vCarProbability to set
     */
    public void setVCarProbability(double vCarProbability) {
        this.vCarProbability = vCarProbability;
    }

    /**
     * @return the vLaneLength
     */
    public int getvLaneLength() {
        return vLaneLength;
    }

    /**
     * @param vLaneLength the vLaneLength to set
     */
    public void setvLaneLength(int vLaneLength) {
        this.vLaneLength = vLaneLength;
    }

    /**
     * @return the hLaneLength
     */
    public int gethLaneLength() {
        return hLaneLength;
    }

    /**
     * @param hLaneLength the hLaneLength to set
     */
    public void sethLaneLength(int hLaneLength) {
        this.hLaneLength = hLaneLength;
    }

    /**
     * @return the simulationRunning
     */
    public Boolean getSimulationRunning() {
        return simulationRunning;
    }

    /**
     * @param simulationRunning the simulationRunning to set
     */
    public void setSimulationRunning(Boolean simulationRunning) {
        this.simulationRunning = simulationRunning;
    }

    /**
     * @return the trafficFlow
     */
    public Boolean getTrafficFlow() {
        return trafficFlow;
    }

    /**
     * @param trafficFlow the trafficFlow to set
     */
    public void setTrafficFlow(Boolean trafficFlow) {
        this.trafficFlow = trafficFlow;
    }

    /**
     * @return the breakdownProbability
     */
    public double getBreakdownProbability() {
        return breakdownProbability;
    }

    /**
     * @param breakdownProbability the breakdownProbability to set
     */
    public void setBreakdownProbability(double breakdownProbability) {
        if(breakdownProbability < 100.00) {
            this.breakdownProbability = breakdownProbability;
        }
    }

    /**
     * @return the breakdownTime
     */
    public int getBreakdownTime() {
        return breakdownTime;
    }

    /**
     * @param breakdownTime the breakdownTime to set
     */
    public void setBreakdownTime(int breakdownTime) {
        this.breakdownTime = breakdownTime;
    }

    /**
     * @return the vIntersectionCenter
     */
    public int getvIntersectionCenter() {
        return vIntersectionCenter;
    }

    /**
     * @param vIntersectionCenter the vIntersectionCenter to set
     */
    public void setvIntersectionCenter(int vIntersectionCenter) {
        this.vIntersectionCenter = vIntersectionCenter;
    }

    /**
     * @return the hIntersectionCenter
     */
    public int gethIntersectionCenter() {
        return hIntersectionCenter;
    }

    /**
     * @param hIntersectionCenter the hIntersectionCenter to set
     */
    public void sethIntersectionCenter(int hIntersectionCenter) {
        this.hIntersectionCenter = hIntersectionCenter;
    }

    /**
     * @return the vRoadSpeed
     */
    public int getvRoadSpeed() {
        return vRoadSpeed;
    }

    /**
     * @param vRoadSpeed the vRoadSpeed to set
     */
    public void setvRoadSpeed(int vRoadSpeed) {
        this.vRoadSpeed = vRoadSpeed;
    }

    /**
     * @return the hRoadSpeed
     */
    public int gethRoadSpeed() {
        return hRoadSpeed;
    }

    /**
     * @param hRoadSpeed the hRoadSpeed to set
     */
    public void sethRoadSpeed(int hRoadSpeed) {
        this.hRoadSpeed = hRoadSpeed;
    }

    /**
     * @return the turnLeftProbability
     */
    public double getTurnLeftProbability() {
        return turnLeftProbability;
    }

    /**
     * @param turnLeftProbability the turnLeftProbability to set
     */
    public void setTurnLeftProbability(double turnLeftProbability) {
        if(turnLeftProbability >= TURN_LEFT_PROBABILITY_BOUNDS[0] && turnLeftProbability <= TURN_LEFT_PROBABILITY_BOUNDS[1]) {
            this.turnLeftProbability = turnLeftProbability;
        }
    }

    /**
     * @return the turnRightProbability
     */
    public double getTurnRightProbability() {
        return turnRightProbability;
    }

    /**
     * @param turnRightProbability the turnRightProbability to set
     */
    public void setTurnRightProbability(double turnRightProbability) {
        if(turnRightProbability >= TURN_RIGHT_PROBABILITY_BOUNDS[0] && turnRightProbability <= TURN_RIGHT_PROBABILITY_BOUNDS[1]) {
            this.turnRightProbability = turnRightProbability;
        }
    }

}
