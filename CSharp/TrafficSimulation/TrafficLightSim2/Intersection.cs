using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLightSim2
{
    public class Intersection
    {
        private RoadIntersection vRoadIntersection;
        private RoadIntersection hRoadIntersection;

        public Intersection()
        {
            vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvIntersectionCenter(), Settings.getSimSettings().getVNorthLanes(), Settings.getSimSettings().getVSouthLanes(), Settings.getSimSettings().getvLaneLength());
            hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethIntersectionCenter(), Settings.getSimSettings().getHWestLanes(), Settings.getSimSettings().getHEastLanes(), Settings.getSimSettings().gethLaneLength());
            vRoadIntersection.setLightState(RoadIntersection.GREEN_LIGHT);
        }

        public RoadIntersection getvRoadIntersection()
        {
            return vRoadIntersection;
        }

        public RoadIntersection gethRoadIntersection()
        {
            return hRoadIntersection;
        }

        public void setvRoadIntersection(RoadIntersection vRoadIntersection)
        {
            this.vRoadIntersection = vRoadIntersection;
        }

        public void sethRoadIntersection(RoadIntersection hRoadIntersection)
        {
            this.hRoadIntersection = hRoadIntersection;
        }

        public void reset()
        {
            vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvIntersectionCenter(), Settings.getSimSettings().getVNorthLanes(), Settings.getSimSettings().getVSouthLanes(), Settings.getSimSettings().getvLaneLength());
            hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethIntersectionCenter(), Settings.getSimSettings().getHWestLanes(), Settings.getSimSettings().getHEastLanes(), Settings.getSimSettings().gethLaneLength());
            vRoadIntersection.setLightState(RoadIntersection.GREEN_LIGHT);
        }
    }
}
