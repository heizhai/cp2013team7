package trafficsim;

import java.util.concurrent.CopyOnWriteArrayList;

/**
 * Class which represents the roads for the simulation, containing Lanes of traffic and associated with the relevant RoadIntersection
 *
 * @author Twistie
 */
class Road {
    private CopyOnWriteArrayList<Lane> lanes = new CopyOnWriteArrayList<Lane>();
    private Boolean trafficDirection;
    private int roadLength;

    /**
     * Constructor for Road
     *
     * @return the lanes
     */
    public  Road(short noLanes, int roadLength) {
        for(int i = 0; i < noLanes; i++) {
            addLane(new Lane());
        }
        trafficDirection = Settings.TRAFFIC_EAST_SOUTH;
        this.roadLength = roadLength;
    }

    public CopyOnWriteArrayList<Lane> getNeighbouringLanes(Lane lane) {
        
            CopyOnWriteArrayList<Lane> neighbours = new CopyOnWriteArrayList<Lane>();
            if ((this.getLanes().indexOf(lane)-1) > 0) {
                neighbours.add(this.getLanes().get(this.getLanes().indexOf(lane)-1));
            }
            if ((this.getLanes().indexOf(lane)+1 < this.getLanes().size())) {
                neighbours.add(this.getLanes().get(this.getLanes().indexOf(lane)+1));
            }
            return neighbours;
    }

    /**
     * Add a lane to this road
     *
     * @param lanes the lanes to set
     */
    public synchronized void addLane(Lane lane) {
        getLanes().add(lane);
    }

    /**
     * Remove a lane to this road
     *
     * @param lanes the lanes to set
     */
    public synchronized void removeLane() {
        getLanes().remove((this.getLanes().size()-1));
    }

    /**
     * Get the direction the traffic is moving.
     *
     * @return the trafficDirection
     */
    public Boolean getTrafficDirection() {
        return trafficDirection;
    }

    /**
     * Set the direction the traffic is moving.
     *
     * @param trafficDirection the trafficDirection to set
     */
    public void setTrafficDirection(Boolean trafficDirection) {
        this.trafficDirection = trafficDirection;
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
    public CopyOnWriteArrayList<Lane> getLanes() {
        return lanes;
    }

    /**
     * @return the number of lanes
     */
    public int getNoLanes() {
        return lanes.size();
    }

    /**
     * @return the lane with the given index
     */
    public Lane getLane(int laneNo) {
        return lanes.get(laneNo);
    }

}