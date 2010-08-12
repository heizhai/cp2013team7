package trafficsim;

/**
 *
 * @author Tristan Davey
 */
class Car {
    static final int CAR_LENGTH = 10;

    private int lanePosition;

    public Car(int lanePosition) {
        this.lanePosition = lanePosition;
    }

    public Boolean intersects(Car car) {
        if(((this.getLanePosition() < (car.getLanePosition()+CAR_LENGTH)) && ((car.getLanePosition()+CAR_LENGTH) > this.getLanePosition())) || ((car.getLanePosition() < (this.getLanePosition()+CAR_LENGTH)) && ((this.getLanePosition()+CAR_LENGTH) > this.getLanePosition()))) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * @return the lanePosition
     */
    public int getLanePosition() {
        return lanePosition;
    }

    /**
     * @param lanePosition the lanePosition to set
     */
    public void setLanePosition(int lanePosition) {
        this.lanePosition = lanePosition;
    }

}
