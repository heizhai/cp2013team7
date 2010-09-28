package trafficsim;

import java.util.Timer;
import java.util.TimerTask;

/**
 *
 * @author Tristan Davey
 */
class Car {
    

    private int lanePosition;
    private Boolean stopped = false;
    private Boolean brokenDown = false;

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

    public Boolean intersects(int lanePosition, Boolean direction) {
        if(((this.getLanePosition() == lanePosition) && (direction == Settings.TRAFFIC_WEST_NORTH)) || ((lanePosition == (this.getLanePosition()+Settings.CAR_LENGTH) && (direction == Settings.TRAFFIC_EAST_SOUTH)))) {
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

    /**
     * @return the brokenDown
     */
    public Boolean getBrokenDown() {
        return brokenDown;
    }

    /**
     * @param brokenDown the brokenDown to set
     */
    public void setBrokenDown(Boolean brokenDown) {
        this.brokenDown = brokenDown;
    }

    public void breakdown() {
        Timer breakdownTimer = new Timer();
        setBrokenDown(true);
        breakdownTimer.schedule(new carBreakdownTask(this), Settings.getSimSettings().getBreakdownTime());
    }

}

/**
 * Class extended from TimerTask which handles the timed change of traffic lights when a lightChange is processed.
 *
 * @author Tristan Davey
 */
class carBreakdownTask extends TimerTask {

        Car car;

        carBreakdownTask(Car car) {
            this.car = car;
        }

        public void run() {
        }
}
