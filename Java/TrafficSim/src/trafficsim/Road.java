package trafficsim;

import java.io.Serializable;
import java.util.concurrent.CopyOnWriteArrayList;

/**
 * Class which represents the roads for the simulation, containing Lanes of traffic and associated with the relevant RoadIntersection
 *
 * @author Tristan Davey
 */
class Road  implements Serializable {
    private CopyOnWriteArrayList<Lane> eastSouthLanes = new CopyOnWriteArrayList<Lane>();
    private CopyOnWriteArrayList<Lane> westNorthLanes = new CopyOnWriteArrayList<Lane>();
    private int roadLength;
    private int roadSpeed;

    /**
     * Constructor for Road
     *
     * @return the lanes
     */
    public  Road(short noEastSouthLanes, short noWestNorthLanes, int roadLength, int roadSpeed) {
        for(int i = 0; i < noEastSouthLanes; i++) {
            this.addLane(new Lane(), Settings.TRAFFIC_EAST_SOUTH);
        }
        for(int i = 0; i < noWestNorthLanes; i++) {
            this.addLane(new Lane(), Settings.TRAFFIC_WEST_NORTH);
        }
        this.roadLength = roadLength;
        this.roadSpeed = roadSpeed;
    }

    /**
     * Finds lanes which neighbour this with traffic flowing in the same direction.
     *
     * @return the lanes
     */
    public CopyOnWriteArrayList<Lane> getNeighbouringLanes(Lane lane, Boolean trafficDirection) {
        
            CopyOnWriteArrayList<Lane> neighbours = new CopyOnWriteArrayList<Lane>();
            if ((this.getLanes(trafficDirection).indexOf(lane)-1) >= 0) {
                neighbours.add(this.getLanes(trafficDirection).get(this.getLanes(trafficDirection).indexOf(lane)-1));
            }
            if ((this.getLanes(trafficDirection).indexOf(lane)+1 < this.getLanes(trafficDirection).size())) {
                neighbours.add(this.getLanes(trafficDirection).get(this.getLanes(trafficDirection).indexOf(lane)+1));
            }
            return neighbours;
    }

    /**
     * Add a lane to this road
     *
     * @param lanes the lanes to set
     */
    public synchronized void addLane(Lane lane, Boolean trafficDirection) {
        getLanes(trafficDirection).add(lane);
    }

    /**
     * Remove a lane from this road
     *
     * @param lanes the lanes to set
     */
    public synchronized void removeLane(Boolean trafficDirection) {
        getLanes(trafficDirection).remove((this.getLanes(trafficDirection).size()-1));
    }

    /**
     * Get the length of this road.
     *
     * @return the roadLength
     */
    public int getRoadLength() {
        return roadLength;
    }

    /**
     * Set the length of this road.
     *
     * @param roadLength the roadLength to set
     */
    public void setRoadLength(int roadLength) {
        this.roadLength = roadLength;
    }

    public void trafficChangeLane(Lane l, Car c, Lane nl) {
        Car tempCar = c;
        l.removeCar(c);
        nl.addCar(tempCar);
    }

    /**
     * @return the lanes
     */
    public CopyOnWriteArrayList<Lane> getLanes(Boolean trafficDirection) {
        if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
            return eastSouthLanes;
        } else {
            return westNorthLanes;
        }
    }

    /**
     * @return the number of lanes
     */
    public int getNoLanes(Boolean trafficDirection) {
        return getLanes(trafficDirection).size();
    }

    /**
     * @return the lane with the given index
     */
    public Lane getLane(Boolean trafficDirection, int laneNo) {
        return getLanes(trafficDirection).get(laneNo);
    }

    /**
     * @return the roadSpeed
     */
    public int getRoadSpeed() {
        return roadSpeed;
    }

    /**
     * @param roadSpeed the roadSpeed to set
     */
    public void setRoadSpeed(int roadSpeed) {
        this.roadSpeed = roadSpeed;
    }

}