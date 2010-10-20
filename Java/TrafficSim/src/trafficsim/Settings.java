package trafficsim;

import java.io.*;
import java.lang.Object;
import java.util.HashMap;
import java.util.Map;

/**
 * This class is designed to make the setting and retrieval of settings for the application easier. NOTE: This class is a singleton and values must be accessed throught the .getSimSettings() function. 
 *
 * @author Tristan Davey
 */
public class Settings implements Serializable {

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
    public static final int CAR_FREQUENCY = 48;
    public static final int LIGHT_CYCLE_TIME = 15000;
    public static final double TRAFFIC_JAM_THRESHOLD = .8;
    public static final double TRAFFIC_JAM_LANES_JAMMED = .5;
    
    // Settings Bounds
    public static final int[] V_LANE_BOUNDS = {1,5};
    public static final int[] H_LANE_BOUNDS = {1,5};
    public static final double[] H_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] V_CAR_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] TURN_LEFT_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final double[] TURN_RIGHT_PROBABILITY_BOUNDS = {0.0, 1.0};
    public static final int[] LIGHT_CYCLE_BOUNDS = {1,10};
    public static final double BREAKDOWN_PROBABILITY_LIMIT = 100.00;
    
    // Default Settings Values
    public static final int DEFAULT_ROAD_LENGTH = 500;
    public static final int DEFAULT_ROAD_CENTER = 250;
    public static final int DEFAULT_ROAD_SPEED = 1;
    public static final double DEFAULT_CAR_PROBABILITY = .9;
    public static final double DEFAULT_TURN_PROBABILITY = .4;
    public static final double DEFAULT_BREAKDOWN_PROBABILITY = .01;
    public static final int DEFAULT_BREAKDOWN_TIME = 5000;
    public static final boolean DEFAULT_TRAFFIC_FLOW = TRAFFIC_FLOW_LEFT_HAND_TRAFFIC;
    public static final int DEFAULT_LANE_NUMBER = 3;
    
    private short hWestLanes = DEFAULT_LANE_NUMBER;
    private short hEastLanes = DEFAULT_LANE_NUMBER;
    private short vNorthLanes = DEFAULT_LANE_NUMBER;
    private short vSouthLanes = DEFAULT_LANE_NUMBER;
    private int vLaneLength = DEFAULT_ROAD_LENGTH;
    private int hLaneLength = DEFAULT_ROAD_LENGTH;
    private int vRoadSpeed = 4;//this.DEFAULT_ROAD_SPEED;
    private int hRoadSpeed = 2;//this.DEFAULT_ROAD_SPEED;
    private int vIntersectionCenter = DEFAULT_ROAD_CENTER;
    private int hIntersectionCenter = DEFAULT_ROAD_CENTER;
    private double hCarProbability = DEFAULT_CAR_PROBABILITY;
    private double vCarProbability = DEFAULT_CAR_PROBABILITY;
    private double turnLeftProbability = DEFAULT_TURN_PROBABILITY;
    private double turnRightProbability = DEFAULT_TURN_PROBABILITY;
    private double breakdownProbability = DEFAULT_BREAKDOWN_PROBABILITY;
    private int breakdownTime = DEFAULT_BREAKDOWN_TIME;
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


    public synchronized void inputSettings(Map inputSettings) {
        if(inputSettings.containsKey("hWestLanes")) {
            setHWestLanes((Short) inputSettings.get("hWestLanes"));
        }
        if(inputSettings.containsKey("hEastLanes")) {
            setHEastLanes((Short) inputSettings.get("hEastLanes"));
        }
        if(inputSettings.containsKey("vNorthLanes")) {
            setVNorthLanes((Short) inputSettings.get("vNorthLanes"));
        }
        if(inputSettings.containsKey("vSouthLanes")) {
            setVSouthLanes((Short) inputSettings.get("vSouthLanes"));
        }
        if(inputSettings.containsKey("vLaneLength")) {
            setvLaneLength((Integer) inputSettings.get("vLaneLength"));
        }
        if(inputSettings.containsKey("hLaneLength")) {
            sethLaneLength((Integer) inputSettings.get("hLaneLength"));
        }
        if(inputSettings.containsKey("hRoadSpeed")) {
            sethRoadSpeed((Integer) inputSettings.get("hRoadSpeed"));
        }
        if(inputSettings.containsKey("vRoadSpeed")) {
            setvRoadSpeed((Integer) inputSettings.get("vRoadSpeed"));
        }
        if(inputSettings.containsKey("vIntersectionCenter")) {
            setvIntersectionCenter((Integer) inputSettings.get("vIntersectionCenter"));
        }
        if(inputSettings.containsKey("hIntersectionCenter")) {
            sethIntersectionCenter((Integer) inputSettings.get("hIntersectionCenter"));
        }
        if(inputSettings.containsKey("hCarProbability")) {
            setHCarProbability((Double) inputSettings.get("hCarProbability"));
        }
        if(inputSettings.containsKey("vCarProbability")) {
            setVCarProbability((Double) inputSettings.get("vCarProbability"));
        }
        if(inputSettings.containsKey("turnLeftProbability")) {
            setTurnLeftProbability((Double) inputSettings.get("turnLeftProbability"));
        }
        if(inputSettings.containsKey("turnRightProbability")) {
            setTurnRightProbability((Double) inputSettings.get("turnRightProbability"));
        }
        if(inputSettings.containsKey("breakdownProbability")) {
            setBreakdownProbability((Double) inputSettings.get("breakdownProbability"));
        }
        if(inputSettings.containsKey("breakdownTime")) {
            setBreakdownTime((Integer) inputSettings.get("breakdownTime"));
        }
        if(inputSettings.containsKey("trafficFlow")) {
            setTrafficFlow((Boolean) inputSettings.get("trafficFlow"));
        }
    }
    
    public synchronized Map outputSettings() {
        Map outputSettings = new HashMap();

        if(getHWestLanes() != DEFAULT_LANE_NUMBER) {
            outputSettings.put("hWestLanes", getHWestLanes());
        }
        if(getHEastLanes() != DEFAULT_LANE_NUMBER) {
            outputSettings.put("hEastLanes", getHEastLanes());
        }
        if(getVNorthLanes() != DEFAULT_LANE_NUMBER) {
            outputSettings.put("vNorthLanes", getVNorthLanes());
        }
        if(getVSouthLanes() != DEFAULT_LANE_NUMBER) {
            outputSettings.put("vSouthLanes", getVSouthLanes());
        }
        if(getvLaneLength() != DEFAULT_ROAD_LENGTH) {
            outputSettings.put("vLaneLength", getvLaneLength());
        }
        if(gethLaneLength() != DEFAULT_ROAD_LENGTH) {
            outputSettings.put("hLaneLength", getVSouthLanes());
        }
        if(getvRoadSpeed() != DEFAULT_ROAD_SPEED) {
            outputSettings.put("vRoadSpeed", getvRoadSpeed());
        }
        if(gethRoadSpeed() != DEFAULT_ROAD_SPEED) {
            outputSettings.put("hRoadSpeed", gethRoadSpeed());
        }
        if(gethIntersectionCenter() != DEFAULT_ROAD_CENTER) {
            outputSettings.put("hIntersectionCenter", gethIntersectionCenter());
        }
        if(getvIntersectionCenter() != DEFAULT_ROAD_CENTER) {
            outputSettings.put("vIntersectionCenter", getvIntersectionCenter());
        }
        if(getHCarProbability() != DEFAULT_CAR_PROBABILITY) {
            outputSettings.put("hCarProbability", getHCarProbability());
        }
        if(getVCarProbability() != DEFAULT_CAR_PROBABILITY) {
            outputSettings.put("vCarProbability", getVCarProbability());
        }
        if(getTurnLeftProbability() != DEFAULT_CAR_PROBABILITY) {
            outputSettings.put("turnLeftProbability", getTurnLeftProbability());
        }
        if(getTurnRightProbability() != DEFAULT_CAR_PROBABILITY) {
            outputSettings.put("turnRightProbability", getTurnRightProbability());
        }
        if(getBreakdownProbability() != DEFAULT_CAR_PROBABILITY) {
            outputSettings.put("breakdownProbability", getBreakdownProbability());
        }
        if(getBreakdownTime() != DEFAULT_BREAKDOWN_TIME) {
            outputSettings.put("breakdownTime", getBreakdownTime());
        }
        if(getTrafficFlow() != TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
            outputSettings.put("trafficFlow", getTrafficFlow());
        }

        return outputSettings;
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
