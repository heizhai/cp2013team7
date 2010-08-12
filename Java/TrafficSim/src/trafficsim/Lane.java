package trafficsim;

import java.util.ArrayList;

/**
 * Class to simulate a lane in the Traffic Intersection Simulation
 *
 * @author Tristan Davey
 */
public class Lane {

    private ArrayList<Car> cars = new ArrayList<Car>();

    /**
     * @return an Individual car given the index.
     */
    public ArrayList<Car> getCars() {
        return this.cars;
    }

    public Car getCarInfront(Car car) {
        return this.cars.get(this.cars.indexOf(car));
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
