using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;

namespace TrafficLightSim2
{
    class Simulation
    {
        static void Main()
        {
            Intersection ourIntersection = new Intersection();
            SimulationEnvironment simulation = null;
            mainWindow window = null;
            simulation = new SimulationEnvironment(ourIntersection, window);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainWindow(ourIntersection, simulation));
        }

     /*   public void carFrame(RoadIntersection roadIntersection)
        {
            int trafficCycleDistance = Settings.CAR_MOVE;

            foreach (Lane l in roadIntersection.getRoad().getLanes())
            {
                foreach (Car c in l.getCars())
                {
                    //Move The Car Forward
                    if (c.getStopped() == false)
                    {
                        c.moveCar(trafficCycleDistance);

                        if (c.intersects(roadIntersection.getIntersectionStopLine(), roadIntersection.getRoad().getTrafficDirection()) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT))
                        {
                            c.setStopped(true);
                            c.moveCar(-(trafficCycleDistance));
                        }


                        if (c.intersects(l.getCarInfront(c, roadIntersection.getRoad().getTrafficDirection())))
                        {

                            bool lanesChanged = false;

                            foreach (Lane nl in roadIntersection.getRoad().getNeighbouringLanes(l))
                            {
                                if (nl.isLaneClear(c.getLanePosition()))
                                {
                                    roadIntersection.getRoad().trafficChangeLane(l, c, nl);

                                    lanesChanged = true;
                                    break;
                                }
                            }
                            c.setStopped(!lanesChanged);
                            if (lanesChanged == false)
                            {
                                c.moveCar(-(trafficCycleDistance * 2));
                            }
                        }
                    }
                }
            }
        }*/
    }
    public class SimulationEnvironment
    {
        private Intersection modelIntersection;
        private mainWindow window;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.Timer lightMultiCyclesTimer;

        public SimulationEnvironment(Intersection modelIntersection, mainWindow window)
        {
            this.modelIntersection = modelIntersection;
            this.window = window;
        }

    }

    public class SimulationFrame
    {
        
    }
}