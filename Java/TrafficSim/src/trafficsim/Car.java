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
    private Boolean lightStopped = false;
    private Boolean brokenDown = false;
    private Boolean turningLeft = false;
    private Boolean turningRight = false;

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

    public Boolean intersects(int lanePosition, int laneSpeed, Boolean direction) {
        if((((this.getLanePosition() <= lanePosition && this.getLanePosition() >= lanePosition-laneSpeed) && direction == Settings.TRAFFIC_WEST_NORTH)) || ((this.getLanePosition()+Settings.CAR_LENGTH <= lanePosition && this.getLanePosition()+Settings.CAR_LENGTH+laneSpeed >= lanePosition) && direction == Settings.TRAFFIC_EAST_SOUTH)) {
            return true;
        } else {
            return false;
        }
    }

    public Boolean intersects(Car car, int offset) {

        int lanePositionStart = this.getLanePosition()-Math.abs(offset);
        int lanePositionEnd = this.getLanePosition()+Settings.CAR_LENGTH+Math.abs(offset);

        if(((car.getLanePosition() < lanePositionEnd) && (lanePositionEnd < (car.getLanePosition()+Settings.CAR_LENGTH)) || ((lanePositionStart < this.getLanePosition()) && (lanePositionStart < (car.getLanePosition()+Settings.CAR_LENGTH))))) {
            return true;
        } else {
            return false;
        }
    }

    public Boolean intersects(int lanePosition, Boolean direction, int offset) {
        if(((this.getLanePosition()-Math.abs(offset) == lanePosition) && (direction == Settings.TRAFFIC_WEST_NORTH)) || ((lanePosition == (this.getLanePosition()+Math.abs(offset)+Settings.CAR_LENGTH) && (direction == Settings.TRAFFIC_EAST_SOUTH)))) {
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
        System.out.println("setStopped: "+stopped);
        if(this.brokenDown != true) {
            this.stopped = stopped;
        } else {
            this.stopped = true;
        }
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
        if (brokenDown == true) {
            System.out.println("BREAKDOWN: "+Settings.getSimSettings().getBreakdownTime());
            this.setStopped(true);
        } else {
            System.out.println("BREAKDOWN END");
            this.setStopped(false);
        }
    }

    public void breakdown() {
        Timer breakdownTimer = new Timer();
        setBrokenDown(true);
        breakdownTimer.schedule(new carBreakdownTask(this), Settings.getSimSettings().getBreakdownTime());
    }

    public void stopLight() {
        stopped = true;
        lightStopped = true;
    }

    public void startLight() {
        stopped = false;
        lightStopped = false;
    }

    /**
     * @return the lightStopped
     */
    public Boolean getLightStopped() {
        return lightStopped;
    }

    /**
     * @param lightStopped the lightStopped to set
     */
    public void setLightStopped(Boolean lightStopped) {
        this.lightStopped = lightStopped;
    }

    /**
     * @return the turningLeft
     */
    public Boolean getTurningLeft() {
        return turningLeft;
    }

    /**
     * @param turningLeft the turningLeft to set
     */
    public void setTurningLeft(Boolean turningLeft) {
        this.turningLeft = turningLeft;
    }

    /**
     * @return the turningRight
     */
    public Boolean getTurningRight() {
        return turningRight;
    }

    /**
     * @param turningRight the turningRight to set
     */
    public void setTurningRight(Boolean turningRight) {
        this.turningRight = turningRight;
    }

    void turn(Lane turningLane, Lane originatingLane, int lanePosition) {
        originatingLane.removeCar(this);
        this.lanePosition = lanePosition;
        turningLane.addCar(this);
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
            this.car.setBrokenDown(false);
        }
}

