package trafficsim;

/**
 * This class is designed to make the setting and retrieval of settings for the application easier. NOTE: This class is a singleton and values must be accessed throught the .getSimSettings() function. 
 *
 * @author Tristan Davey
 */
public class Settings {

    //Singleton Instance
    private static Settings simulationSettings = null;

    private short hWestLanes = 3;
    private short hEastLanes = 3;
    private short vNorthLanes = 2;
    private short vSouthLanes = 4;
    private int vLaneLength = 500;
    private int hLaneLength = 500;
    private int vLaneStopLine = 250;
    private int hLaneStopLine = 250;
    private double hCarProbability = 1;
    private double vCarProbability = 1;
    private double breakdownProbability = 0.05;
    private int breakdownTime = 10000;
    private Boolean trafficFlow = false;
    private Boolean simulationRunning = false;

    //Static
    public static final int LANE_WIDTH = 15;
    public static final int DEFAULT_ROAD_LENGTH = 500;
    public static final int CAR_LENGTH = 30;
    public static final int CAR_WIDTH = 12;
    public static final int CAR_MOVE = 1;
    public static final Boolean TRAFFIC_EAST_SOUTH = true;
    public static final Boolean TRAFFIC_WEST_NORTH = false;
    public static final Boolean TRAFFIC_FLOW_LEFT_HAND_TRAFFIC = false;
    public static final Boolean TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC = true;
    public static final Boolean ROAD_SOUTH_NORTH = true;
    public static final Boolean ROAD_EAST_WEST = false;
    public static final int FRAME_LENGTH =  100;
    public static final int[] V_LANE_BOUNDS = {1,4};
    public static final int[] H_LANE_BOUNDS = {1,3};
    public static final double[] H_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] V_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double BREAKDOWN_PROBABILITY_LIMIT = 100.00;
    public static final int CAR_FREQUENCY = 48;
    public static final int[] LIGHT_CYCLE_BOUNDS = {1,10};
    public static final int LIGHT_CYCLE_TIME = 15000;

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
     * @return the vLaneStopLine
     */
    public int getvLaneStopLine() {
        return vLaneStopLine;
    }

    /**
     * @param vLaneStopLine the vLaneStopLine to set
     */
    public void setvLaneStopLine(int vLaneStopLine) {
        this.vLaneStopLine = vLaneStopLine;
    }

    /**
     * @return the hLaneStopLine
     */
    public int gethLaneStopLine() {
        return hLaneStopLine;
    }

    /**
     * @param hLaneStopLine the hLaneStopLine to set
     */
    public void sethLaneStopLine(int hLaneStopLine) {
        this.hLaneStopLine = hLaneStopLine;
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

}
