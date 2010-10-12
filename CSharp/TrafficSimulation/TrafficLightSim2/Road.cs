using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Road
    {
        private List<Lane> eastSouthLanes = new List<Lane>();
        private List<Lane> westNorthLanes = new List<Lane>();
        private int roadLength;
        private int roadSpeed;

        public Road(short noEastSouthLanes, short noWestNorthLanes, int roadLength)
        {
            for (int i = 0; i < noEastSouthLanes; i++)
            {
                this.addLane(new Lane(), Settings.TRAFFIC_EAST_SOUTH);
            }
            for (int i = 0; i < noWestNorthLanes; i++)
            {
                this.addLane(new Lane(), Settings.TRAFFIC_WEST_NORTH);
            }
            this.roadLength = roadLength;
        }

     /*   public Road(short noLanes, int roadLength)
        {
            for (int i = 0; i < noLanes; i++)
            {
                addLane(new Lane());
            }

            trafficDirection = Settings.TRAFFIC_EAST_SOUTH;
            this.roadLength = roadLength;
        }*/
        public List<Lane> getNeighbouringLanes(Lane lane, bool trafficDirection)
        {
            List<Lane> neighbours = new List<Lane>();
            if ((this.getLanes(trafficDirection).IndexOf(lane) - 1) >= 0)
            {
                neighbours.Add(this.getLanes(trafficDirection)[this.getLanes(trafficDirection).IndexOf(lane) - 1]);
            }
            if ((this.getLanes(trafficDirection).IndexOf(lane) + 1 < this.getLanes(trafficDirection).Count()))
            {
                neighbours.Add(this.getLanes(trafficDirection)[this.getLanes(trafficDirection).IndexOf(lane) + 1]);
            }
            return neighbours;
        }

 /*       public List<Lane> getNeighbouringLanes(Lane lane)
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
        }*/

        
         // Add a lane to this road
        
        public void addLane(Lane lane, bool trafficDirection) 
        {
            getLanes(trafficDirection).Add(lane);
        }

        public void removeLane(bool trafficDirection) 
        {
            getLanes(trafficDirection).RemoveAt((this.getLanes(trafficDirection).Count()-1));
        }
 /*       public void removeLane()
        {
            getLanes().RemoveAt((this.getLanes().Count() - 1));
        }
 */
         // Get the direction the traffic is moving.
    /*     
        public bool getTrafficDirection()
        {
            return trafficDirection;
        }
        */
      
         //Set the direction the traffic is moving.
                  
     /*   public void setTrafficDirection(bool trafficDirection)
        {
            this.trafficDirection = trafficDirection;
        }*/

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

        public List<Lane> getLanes(bool trafficDirection)
        {
            if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
            {
                return eastSouthLanes;
            }
            else
            {
                return westNorthLanes;
            }
        }

        public int getNoLanes(bool trafficDirection)
        {
            return getLanes(trafficDirection).Count();
        }

        public Lane getLane(bool trafficDirection, int laneNo)
        {
            return getLanes(trafficDirection)[laneNo];
        }

        public int getRoadSpeed()
        {
            return roadSpeed;
        }

        public void setRoadSpeed(int roadSpeed)
        {
            this.roadSpeed = roadSpeed;
        }
    }
}
