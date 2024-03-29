﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrafficLightSim2 
{
    [Serializable()]
    public class Road : ISerializable
    {
        private List<Lane> eastSouthLanes = new List<Lane>();
        private List<Lane> westNorthLanes = new List<Lane>();
        private int roadLength;
        private int roadSpeed;

        public Road(short noEastSouthLanes, short noWestNorthLanes, int roadLength, int roadSpeed)
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
            this.roadSpeed = roadSpeed;
        }

        public Road(SerializationInfo info, StreamingContext ctxt)
       {
          this.eastSouthLanes = (List<Lane>)info.GetValue("eastSouthLanes", typeof(List<Lane>));
          this.westNorthLanes = (List<Lane>)info.GetValue("westNorthLanes", typeof(List<Lane>));
          this.roadLength = (int)info.GetValue("roadLength", typeof(int));
          this.roadSpeed = (int)info.GetValue("roadSpeed", typeof(int));
       }

       public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
       {
          info.AddValue("eastSouthLanes", this.eastSouthLanes);
          info.AddValue("westNorthLanes", this.westNorthLanes);
          info.AddValue("roadLength", this.roadLength);
          info.AddValue("roadSpeed", this.roadSpeed);
       }

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

         // Add a lane to this road
        
        public void addLane(Lane lane, bool trafficDirection) 
        {
            getLanes(trafficDirection).Add(lane);
        }

        public void removeLane(bool trafficDirection) 
        {
            getLanes(trafficDirection).RemoveAt((this.getLanes(trafficDirection).Count()-1));
        }      
         
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
