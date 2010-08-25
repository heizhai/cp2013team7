using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Car
    {
        private int lanePosition;
        private bool stopped;

        public Car(int lanePosition)
        {
            stopped = false;
            this.lanePosition = lanePosition;
        }

        public bool intersects(Car car)
        {
            if (((car.getLanePosition() < (this.getLanePosition() + Settings.CAR_LENGTH)) && ((this.getLanePosition() + Settings.CAR_LENGTH) < (car.getLanePosition() + Settings.CAR_LENGTH)) || ((car.getLanePosition() < this.getLanePosition()) && (this.getLanePosition() < (car.getLanePosition() + Settings.CAR_LENGTH)))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool intersects(int lanePosition, bool direction)
        {
            if (((this.getLanePosition() == lanePosition) && (direction == Settings.TRAFFIC_WEST_NORTH)) || ((lanePosition == (this.getLanePosition()+Settings.CAR_LENGTH) && (direction == Settings.TRAFFIC_EAST_SOUTH))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getLanePosition()
        {
            return this.lanePosition;
        }

        public void setLanePostion(int lanePosition)
        {
            this.lanePosition = lanePosition;
        }

        public void setStopped(bool stopped)
        {
            this.stopped = stopped;
        }

        public bool getStopped()
        {
            return this.stopped;
        }

        public void moveCar(int units)
        {
            lanePosition += units;
        }
    }
}
