package trafficsim;

import java.util.concurrent.CopyOnWriteArrayList;

/**
 * Class to simulate a lane in the Traffic Intersection Simulation
 *
 * @author Tristan Davey
 */
public class Lane {

    private CopyOnWriteArrayList<Car> cars = new CopyOnWriteArrayList<Car>();

    /**
     * @return an Individual car given the index.
     */
    public CopyOnWriteArrayList<Car> getCars() {
        return this.cars;
    }

    public Car getCarInfront(Car car, Boolean trafficDirection) {
        Car closestCar = null;
        for(Car c: cars) {
            if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                if(c.getLanePosition() > car.getLanePosition()) {
                    if(closestCar == null) {
                        closestCar = c;
                    } else {
                        if (c.getLanePosition() < closestCar.getLanePosition()) {
                            closestCar = c;
                        }
                    }
                }
            } else {
                if(c.getLanePosition() < car.getLanePosition()) {
                    if(closestCar == null) {
                        closestCar = c;
                    } else {
                        if (c.getLanePosition() > closestCar.getLanePosition()) {
                            closestCar = c;
                        }
                    }
                }
            }
        }
        if(closestCar == null) {
            //If there is not a car, set it to itself
            return car;
        } else {
            return closestCar;
        }
    }

    /**
     * @param car the cars to set
     */
    public void addCar(Car car) {
        this.cars.add(car);
    }

    public void removeCar(Car car) {
        this.cars.remove(car);
    }

    public int getCarCount() {
        return this.cars.size();
    }
    
    public Boolean isLaneClear(int lanePosition) {
        for (Car c: cars) {
            if(c.intersects(new Car(lanePosition))) {
                return false;
            }
        }
        return true;
    }

}
