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
            vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvLaneStopLine(), Settings.getSimSettings().getVLanes(), Settings.getSimSettings().getvLaneLength());
            hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethLaneStopLine(), Settings.getSimSettings().getHLanes(), Settings.getSimSettings().gethLaneLength());
            vRoadIntersection.setLightState(RoadIntersection.GREEN_LIGHT);
        }

        public RoadIntersection getvRoadIntersection()
        {
            return vRoadIntersection;
        }

        public void setvRoadIntersection(RoadIntersection vRoadIntersection)
        {
            this.vRoadIntersection = vRoadIntersection;
        }

        public RoadIntersection gethRoadIntersection()
        {
            return hRoadIntersection;
        }

        public void sethRoadIntersection(RoadIntersection hRoadIntersection)
        {
            this.hRoadIntersection = hRoadIntersection;
        }

        public void reset()
        {
            vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvLaneStopLine(), Settings.getSimSettings().getVLanes(), Settings.getSimSettings().getvLaneLength());
            hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethLaneStopLine(), Settings.getSimSettings().getHLanes(), Settings.getSimSettings().gethLaneLength());
            vRoadIntersection.setLightState(RoadIntersection.GREEN_LIGHT);
        }
    }
}
