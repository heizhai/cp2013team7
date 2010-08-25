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

        
         // Add a lane to this road
        
        public void addLane(Lane lane)
        {
            getLanes().Add(lane);
        }

        public void removeLane()
        {
            getLanes().RemoveAt((this.getLanes().Count() - 1));
        }
 
         // Get the direction the traffic is moving.
         
        public bool getTrafficDirection()
        {
            return trafficDirection;
        }
               
         //Set the direction the traffic is moving.
                  
        public void setTrafficDirection(bool trafficDirection)
        {
            this.trafficDirection = trafficDirection;
        }

         //Get the length of this road.         
         
        public int getRoadLength()
        {
            return roadLength;
        }

         //Set the length of this road.
         
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

        //return the lanes
        
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
