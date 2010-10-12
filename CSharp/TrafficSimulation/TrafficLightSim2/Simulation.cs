using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace TrafficLightSim2
{
    class Simulation
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        static void Main(string[] args)
        {
            AllocConsole();

            Intersection ourIntersection = new Intersection();
            SimulationEnvironment simulation = null;
            mainWindow window = null;
            simulation = new SimulationEnvironment(ourIntersection, window);
            Application.EnableVisualStyles();
            window = new mainWindow(ourIntersection, simulation);
            simulation.setWindow(window);
            Application.Run(window);
        }
    }
    public class SimulationEnvironment
    {
        private Intersection modelIntersection;
        private mainWindow window;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.Timer lightMultiCyclesTimer;
      //  private int noCycles;
        

        public SimulationEnvironment(Intersection modelIntersection, mainWindow window)
        {
            this.modelIntersection = modelIntersection;
            this.window = window;
            
        }

        public void setWindow(mainWindow window)
        {
            this.window = window;
        }

        public void lightCycle()
        {
            lightTimer = new System.Windows.Forms.Timer();

            if (modelIntersection.gethRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
            {
                lightTimer.Interval = (((((modelIntersection.getvRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) + modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)) * Settings.LANE_WIDTH) / Settings.CAR_MOVE) + Settings.CAR_LENGTH) * Settings.FRAME_LENGTH);
                modelIntersection.gethRoadIntersection().setLightState(RoadIntersection.YELLOW_LIGHT);
                lightTimer.Start();
                lightTimer.Tick += new EventHandler(lightTimer_Tick);                
            }
            else if (modelIntersection.getvRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
            {
                lightTimer.Interval = (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) + modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)) * Settings.LANE_WIDTH) / Settings.CAR_MOVE) + Settings.CAR_LENGTH) * Settings.FRAME_LENGTH);
                modelIntersection.getvRoadIntersection().setLightState(RoadIntersection.YELLOW_LIGHT);
                lightTimer.Start();
                lightTimer.Tick += new EventHandler(lightTimer_Tick);
            }
            
        }

        public void multiLightCycle(/*int noCycles*/)
        {
            lightMultiCyclesTimer = new System.Windows.Forms.Timer();
            lightMultiCyclesTimer.Interval = Settings.LIGHT_CYCLE_TIME;
            lightMultiCyclesTimer.Start();
            lightMultiCyclesTimer.Tick +=new EventHandler(multiCycle_Tick);
        }

        public void multiCycle_Tick(object sender, EventArgs eArgs)
        {
            Settings.automate = true;
            new multiLightCycle(this, window);
            if (!Settings.automate)
            {
                  lightMultiCyclesTimer.Stop();
            }
        }

        public void lightTimer_Tick(object sender, EventArgs eArgs)
        {
            new lightCycle(modelIntersection, window);
            lightTimer.Stop();
        }

        public void run()
        {
            SimulationFrame.frameCount = 0;
            modelIntersection.reset();
            frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = Settings.FRAME_LENGTH;
            Settings.getSimSettings().setSimulationRunning(true);
            frameTimer.Start();
            frameTimer.Tick += new EventHandler(frameTimer_Tick);
            this.multiLightCycle();
        }

        public void stop()
        {
            Settings.getSimSettings().setSimulationRunning(false);
            frameTimer.Stop();
            if (Settings.automate)
            {
                lightTimer.Stop();
            }
            lightMultiCyclesTimer.Stop();
            SimulationFrame.frameCount = 0;
            modelIntersection.reset();
        }

        public void frameTimer_Tick(object sender, EventArgs eArgs)
        {
            lock (this)
            {
                new SimulationFrame(modelIntersection, window);
            }
        }

       /* public int getNoCycles()
        {
            return this.noCycles;
        }

        public void setNoCycles(int noCycles)
        {
            this.noCycles = noCycles;
        }*/
    }

    public class SimulationFrame
    {
        static public int frameCount = 0;
        Intersection modelIntersection;
        mainWindow window;

        public SimulationFrame(Intersection modelIntersection, mainWindow window)
        {
            this.modelIntersection = modelIntersection;
            this.window = window;
            run();
        }

        public void run()
        {
            Random randGen = new Random();
            frameCount++;

            // Generate New Cars
            if (frameCount % Settings.CAR_FREQUENCY == 0 || frameCount == 1)
            {
                //Horizontal Road
                if (randGen.Next(100) < (Settings.getSimSettings().getHCarProbability() * 100))
                {
                    int lanePositionHWest;
                    int lanePositionHEast;
                    Road roadH = modelIntersection.gethRoadIntersection().getRoad();
                    int randHWest = randGen.Next(roadH.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                    int randHEast = randGen.Next(roadH.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));

                    if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                    {
                        lanePositionHWest = roadH.getRoadLength();
                        lanePositionHEast = 0;
                    }
                    else
                    {
                        lanePositionHWest = 0;
                        lanePositionHEast = roadH.getRoadLength();
                    }

                    modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randHWest).addCar(new Car(lanePositionHWest));
                    modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randHEast).addCar(new Car(lanePositionHEast));
                }

                if (randGen.Next(100) < (Settings.getSimSettings().getVCarProbability() * 100))
                {
                    //Vertical Road
                    int lanePositionVNorth;
                    int lanePositionVSouth;
                    Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                    int randVNorth = randGen.Next(roadV.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                    int randVSouth = randGen.Next(roadV.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                    if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                    {
                        lanePositionVNorth = roadV.getRoadLength();
                        lanePositionVSouth = 0;
                    }
                    else
                    {
                        lanePositionVNorth = 0;
                        lanePositionVSouth = roadV.getRoadLength();
                    }

                    modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randVNorth).addCar(new Car(lanePositionVNorth));
                    modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randVSouth).addCar(new Car(lanePositionVSouth));
                }
                /*                  randH = randGen.Next(roadH.getNoLanes());
                                  int lanePositionH;
                                  if (roadH.getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
                                  {
                                      lanePositionH = 0;
                                  }

                                  else
                                  {
                                      lanePositionH = roadH.getRoadLength();
                                  }
                                  modelIntersection.gethRoadIntersection().getRoad().getLane(randH).addCar(new Car(lanePositionH));
                              }

                              if (randGen.Next(100) < (Settings.getSimSettings().getVCarProbability() * 100))
                              {
                                  //Vertical Road
                                  Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                                  int randV = randGen.Next(roadV.getNoLanes());
                                  int lanePositionV;

                                  if (roadV.getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
                                  {
                                      lanePositionV = 0;
                                  }

                                  else
                                  {
                                      lanePositionV = roadV.getRoadLength();
                                  }
                                  modelIntersection.getvRoadIntersection().getRoad().getLane(randV).addCar(new Car(lanePositionV));
                              }
              */
            }

     //       Thread oThread = new Thread(new ThreadStart(this.doStuff));
      //      oThread.Start();

      //      Thread.Sleep(1);
            carFrame(modelIntersection.getvRoadIntersection(), modelIntersection.gethRoadIntersection());
            carFrame(modelIntersection.gethRoadIntersection(), modelIntersection.getvRoadIntersection());

            window.Refresh();
        }

     //   public void doStuff()
     //   {
      //      carFrame(modelIntersection.getvRoadIntersection(), modelIntersection.gethRoadIntersection());
      //      carFrame(modelIntersection.gethRoadIntersection(), modelIntersection.getvRoadIntersection());
     //   }

        public void carFrame(RoadIntersection roadIntersection, RoadIntersection intersectingRoadIntersection)
        {
            foreach (Lane l in roadIntersection.getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH))
            {
                foreach (Car c in l.getCars())
                {
                    carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_WEST_NORTH);
                }
            }

            foreach (Lane l in roadIntersection.getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH))
            {
                foreach (Car c in l.getCars())
                {
                    carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_EAST_SOUTH);
                }
            }
            /*
                        int trafficCycleDistance = Settings.CAR_MOVE;

                        for (int i = 0; i < roadIntersection.getRoad().getLanes().Count(); i++)
                        {
                            Lane l = roadIntersection.getRoad().getLanes()[i];

                            for (int j = 0; j < l.getCars().Count(); j++)
                            {
                                Car c = l.getCars()[j];
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
                        }*/

        }

        private void carFrameProcess(RoadIntersection roadIntersection, RoadIntersection intersectingRoadIntersection, Lane lane, Car car, Boolean trafficDirection)
        {

            Random randGen = new Random();
            int trafficCycleDistance;
            int stopLine;

            if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
            {
                trafficCycleDistance = roadIntersection.getRoad().getRoadSpeed();
            }
            else
            {
                trafficCycleDistance = (-1 * roadIntersection.getRoad().getRoadSpeed());
            }


            //Move The Car Forward
            if (car.getStopped() == false)
            {
                if (randGen.NextDouble() * Settings.BREAKDOWN_PROBABILITY_LIMIT < Settings.getSimSettings().getBreakdownProbability())
                {
                    car.setBrokenDown(true);
                }
                else
                {
                    car.moveCar(trafficCycleDistance);
                }

                //Calculate the Stop Line Position

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
                    {
                        stopLine = roadIntersection.getIntersectionCenter() - (intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
                    }
                    else
                    {
                        stopLine = roadIntersection.getIntersectionCenter() + (intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
                    }
                }
                else
                {
                    if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
                    {
                        stopLine = roadIntersection.getIntersectionCenter() - (intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
                    }
                    else
                    {
                        stopLine = roadIntersection.getIntersectionCenter() + (intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
                    }
                }

                if (car.intersects(stopLine, trafficDirection) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT))
                {
                    car.setStopped(true);
                    car.moveCar(-1 * (trafficCycleDistance));
                }


                if (car.intersects(lane.getCarInfront(car, trafficDirection)))
                {

                    bool lanesChanged = false;

                    foreach (Lane nl in roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection))
                    {
                        if (nl.isLaneClear(car.getLanePosition()))
                        {
                            roadIntersection.getRoad().trafficChangeLane(lane, car, nl);

                            lanesChanged = true;
                            break;
                        }
                    }
                    car.setStopped(!lanesChanged);
                    if (lanesChanged == false)
                    {
                        car.moveCar(-(trafficCycleDistance * 2));
                    }
                }
            }
        }

    }
    class lightCycle
    {
        Intersection intersection;
        mainWindow window;

        public lightCycle(Intersection intersection, mainWindow window)
        {
            this.intersection = intersection;
            this.window = window;
            run();
        }

        public void run()
        {
            if (intersection.gethRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
            {
                intersection.gethRoadIntersection().setLightState(RoadIntersection.RED_LIGHT);
                intersection.getvRoadIntersection().setLightState(RoadIntersection.GREEN_LIGHT);
            }
            else
            {
                intersection.getvRoadIntersection().setLightState(RoadIntersection.RED_LIGHT);
                intersection.gethRoadIntersection().setLightState(RoadIntersection.GREEN_LIGHT);
            }
        }
    }

    class multiLightCycle
    {
        SimulationEnvironment simulation;
        mainWindow window;

        public multiLightCycle(SimulationEnvironment simulation, mainWindow window)
        {
            this.simulation = simulation;
            this.window = window;
            run();
        }

        public void run()
        {
            simulation.lightCycle();
            //int numCycles = simulation.getNoCycles();
            
           // numCycles--;
         //   simulation.setNoCycles(numCycles);

            
           /* if (numCycles <= 0)
            {
                Settings.automate = false;
            }*/
        }
    }
}

