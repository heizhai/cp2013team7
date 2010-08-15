package trafficsim;

import java.util.ArrayList;

/**
 * Class which represents the roads for the simulation, containing Lanes of traffic and associated with the relevant RoadIntersection
 *
 * @author Twistie
 */
class Road {
    private ArrayList<Lane> lanes = new ArrayList<Lane>();
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

        this.roadLength = roadLength;
    }

    public ArrayList<Lane> getNeighbouringLanes(Lane lane) {
        ArrayList<Lane> neighbours = new ArrayList<Lane>();
        neighbours.add(this.getLanes().get(this.getLanes().indexOf(lane)-1));
        neighbours.add(this.getLanes().get(this.getLanes().indexOf(lane)+1));
        return neighbours;
    }

    /**
     * Add a lane to this road
     *
     * @param lanes the lanes to set
     */
    public void addLane(Lane lane) {
        getLanes().add(lane);
    }

    /**
     * Remove a lane to this road
     *
     * @param lanes the lanes to set
     */
    public void removeLane() {
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
    public ArrayList<Lane> getLanes() {
        return lanes;
    }

}