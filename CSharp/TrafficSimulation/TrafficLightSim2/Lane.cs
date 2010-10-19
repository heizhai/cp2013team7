using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Lane
    {
        private List<Car> cars = new List<Car>();

        public List<Car> getCars()
        {
            return this.cars;
        }

        public Car getCarInfront(Car car, bool trafficDirection)
        {
            Car closestCar = null;
            foreach (Car c in cars)
            {
                if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
                {
                    if (c.getLanePosition() > car.getLanePosition())
                    {
                        if (closestCar == null)
                        {
                            closestCar = c;
                        }
                        else
                        {
                            if (c.getLanePosition() < closestCar.getLanePosition())
                            {
                                closestCar = c;
                            }
                        }
                    }
                }

                else
                {
                    if (c.getLanePosition() < car.getLanePosition())
                    {
                        if (closestCar == null)
                        {
                            closestCar = c;
                        }
                        else
                        {
                            if (c.getLanePosition() > closestCar.getLanePosition())
                            {
                                closestCar = c;
                            }
                        }
                    }
                }
            }

            if (closestCar == null)
            {
                //If there is not a car, set it to itself
                return car;
            }

            else
            {
                return closestCar;
            }
        }

        public void addCar(Car car)
        {
            this.cars.Add(car);
        }

        public void removeCar(Car car)
        {
            this.cars.Remove(car);
        }

        public int getCarCount()
        {
            return this.cars.Count();
        }

        public bool isLaneClear(int lanePosition)
        {
            foreach (Car c in cars)
            {
                if (c.intersects(new Car(lanePosition)))
                {
                    return false;
                }
            }
            return true;
        }

        public Boolean isLaneClear(int lanePosition, int offset)
        {
            foreach (Car c in cars)
            {
                if (c.intersects(new Car(lanePosition), offset))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
