package trafficsim;

/**
 *
 * @author Tristan Davey
 */
class Car {
    

    private int lanePosition;
    private Boolean stopped = false;

    public Car(int lanePosition) {
        this.lanePosition = lanePosition;
    }

    public Boolean intersects(Car car) {
        if(((car.getLanePosition() < (this.getLanePosition()+Settings.CAR_LENGTH)) && ((this.getLanePosition()+Settings.CAR_LENGTH) < (car.getLanePosition()+Settings.CAR_LENGTH)) || ((car.getLanePosition() < this.getLanePosition()) && (this.getLanePosition() < (car.getLanePosition()+Settings.CAR_LENGTH))))) {
            return true;
        } else {
            return false;
        }
    }

    public Boolean intersects(int lanePosition) {
        if((this.getLanePosition() < lanePosition) && (lanePosition < (this.getLanePosition()+Settings.CAR_LENGTH))) {
            return true;
        } else {
            return false;
        }
    }

    public void moveCar(int units) {
        lanePosition += units;
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

    public void setStopped(boolean stopped) {
        this.stopped = stopped;
    }

    public Boolean getStopped() {
        return this.stopped;
    }

}
