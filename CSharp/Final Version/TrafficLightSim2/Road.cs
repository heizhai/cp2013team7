using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Road
    {
        private List<Lane> lanes = new List<Lane>();
        private bool trafficDirection;
        private int roadLength;

        public Road(short noLanes, int roadLength)
        {
            for (int i = 0; i < noLanes; i++)
            {
                addLane(new Lane());
            }

            trafficDirection = Settings.TRAFFIC_EAST_SOUTH;
            this.roadLength = roadLength;
        }

        public List<Lane> getNeighbouringLanes(Lane lane)
        {
            List<Lane> neighbours = new List<Lane>();

            if ((this.getLanes().IndexOf(lane) - 1) > 0)
            {
                neighbours.Add(this.getLanes()[this.getLanes().IndexOf(lane) - 1]);
            }
            if ((this.getLanes().IndexOf(lane) + 1 < this.getLanes().Count()))
            {
                neighbours.Add(this.getLanes()[this.getLanes().IndexOf(lane) + 1]);
            }

            return neighbours;
        }

        /**
         * Add a lane to this road
         *
         * @param lanes the lanes to set
         */
        public void addLane(Lane lane)
        {
            getLanes().Add(lane);
        }

        public void removeLane()
        {
            getLanes().RemoveAt((this.getLanes().Count() - 1));
        }

        /**
         * Get the direction the traffic is moving.
         *
         * @return the trafficDirection
         */
        public bool getTrafficDirection()
        {
            return trafficDirection;
        }

        /**
         * Set the direction the traffic is moving.
         *
         * @param trafficDirection the trafficDirection to set
         */
        public void setTrafficDirection(bool trafficDirection)
        {
            this.trafficDirection = trafficDirection;
        }

        /**
         * Get the length of this road.
         *
         * @return the roadLength
         */
        public int getRoadLength()
        {
            return roadLength;
        }

        /**
         * Set the length of this road.
         *
         * @param roadLength the roadLength to set
         */
        public void setRoadLength(int roadLength)
        {
            this.roadLength = roadLength;
        }

        public void trafficChangeLane(Lane l, Car c, Lane nl)
        {
            Car tempCar = c;
            l.removeCar(c);
            nl.addCar(tempCar);
        }

        /**
         * @return the lanes
         */
        public List<Lane> getLanes()
        {
            return lanes;
        }

        public int getNoLanes()
        {
            return lanes.Count();
        }

        public Lane getLane(int laneNo)
        {
            return lanes[laneNo];
        }
    }
}
