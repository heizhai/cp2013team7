using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace TrafficLightSim2
{
    class Simulation
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            AllocConsole();
            SimulationEnvironment simulation = new SimulationEnvironment();
        }
    }
    public class SimulationEnvironment
    {
        private Intersection modelIntersection;
        private mainWindow window;
        private System.Windows.Forms.Timer frameTimer;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.Timer lightMultiCyclesTimer;

        public SimulationEnvironment()
        {
            this.modelIntersection = new Intersection();
            this.window = new mainWindow(modelIntersection, this);
            Application.Run(window);
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

            else if (modelIntersection.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
            {
                lightTimer.Interval = (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) + modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)) * Settings.LANE_WIDTH) / Settings.CAR_MOVE) + Settings.CAR_LENGTH) * Settings.FRAME_LENGTH);
                modelIntersection.gethRoadIntersection().setLightState(RoadIntersection.TURNING_YELLOW_LIGHT);
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

            else if (modelIntersection.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
            {
                lightTimer.Interval = (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) + modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)) * Settings.LANE_WIDTH) / Settings.CAR_MOVE) + Settings.CAR_LENGTH) * Settings.FRAME_LENGTH);
                modelIntersection.getvRoadIntersection().setLightState(RoadIntersection.TURNING_YELLOW_LIGHT);
                lightTimer.Start();
                lightTimer.Tick += new EventHandler(lightTimer_Tick);
            }
        }

        public void multiLightCycle()
        {
            lightMultiCyclesTimer = new System.Windows.Forms.Timer();
            lightMultiCyclesTimer.Interval = Settings.LIGHT_CYCLE_TIME;
            lightMultiCyclesTimer.Start();
            lightMultiCyclesTimer.Tick += new EventHandler(multiCycle_Tick);
        }

        public void multiCycle_Tick(object sender, EventArgs eArgs)
        {
            Settings.automate = true;
            new multiLightCycle(this, window);
        }

        public void lightTimer_Tick(object sender, EventArgs eArgs)
        {
            new lightCycle(modelIntersection, window);
            lightTimer.Stop();
        }

        public void run()
        {
            frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = Settings.FRAME_LENGTH;
            Settings.getSimSettings().setSimulationRunning(true);
            frameTimer.Start();
            frameTimer.Tick += new EventHandler(frameTimer_Tick);
            multiLightCycle();
        }

        public void stop()
        {
            Settings.getSimSettings().setSimulationRunning(false);
            frameTimer.Stop();
            lightMultiCyclesTimer.Stop();
        }

        public void reset()
        {
            if (Settings.getSimSettings().getSimulationRunning() == true)
            {
                frameTimer.Stop();
                lightMultiCyclesTimer.Stop();
            }
            modelIntersection.reset();
        }

        public void frameTimer_Tick(object sender, EventArgs eArgs)
        {
            lock (this)
            {
                new SimulationFrame(modelIntersection, window);
            }
        }

        public void fileOutput(string outputFile)
        {
            try
            {
                Intersection writeData = modelIntersection;
                FileStream writeStream = new FileStream(outputFile, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
           //     formatter.Serialize(writeStream, Settings.getSimSettings().outputSettings());
                formatter.Serialize(writeStream, writeData);
                writeStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

          public void fileInput(string inputFile)
          {
              try
              {
                  this.reset();
                  FileStream readStream = new FileStream(inputFile, FileMode.Open);
                  BinaryFormatter formatter = new BinaryFormatter();
                  Intersection readData = (Intersection)formatter.Deserialize(readStream);
                  readStream.Close();
                  modelIntersection = readData;
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.StackTrace);
              }
              window.refreshSimulationReference(modelIntersection, this);
          }
    }

    public class SimulationFrame
    {
        static public int frameCount = 0;
        private Random randGen = new Random();
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
            frameCount++;

            // Generate New Cars
            if (frameCount % Settings.CAR_FREQUENCY == 0)
            {
                //Horizontal Road
                if (randGen.Next(100) < (Settings.getSimSettings().getHCarProbability() * 100))
                {
                    Car workingCar = null;
                    Road roadH = modelIntersection.gethRoadIntersection().getRoad();
                    int randHWest = randGen.Next(roadH.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                    int randHEast = randGen.Next(roadH.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                    int lanePositionHWest = roadH.getRoadLength();
                    int lanePositionHEast = 0;

                    if (this.trafficJam(modelIntersection.gethRoadIntersection().getRoad(), Settings.TRAFFIC_WEST_NORTH) == false)
                    {
                        modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randHWest).addCar(workingCar = new Car(lanePositionHWest));

                        if (randGen.Next() < (Settings.getSimSettings().getTurnLeftProbability() * 100))
                        {
                            workingCar.setTurningLeft(true);
                        }

                        if (randGen.Next() < (Settings.getSimSettings().getTurnRightProbability() * 100))
                        {
                            workingCar.setTurningRight(true);
                        }
                    }

                    workingCar = null;

                    if (this.trafficJam(modelIntersection.gethRoadIntersection().getRoad(), Settings.TRAFFIC_EAST_SOUTH) == false)
                    {
                        modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randHEast).addCar(workingCar = new Car(lanePositionHEast));

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnLeftProbability() * 100))
                        {
                            workingCar.setTurningLeft(true);
                        }

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnRightProbability() * 100))
                        {
                            workingCar.setTurningRight(true);
                        }
                    }

                }

                if (randGen.Next(100) < (Settings.getSimSettings().getVCarProbability() * 100))
                {
                    //Vertical Road
                    Car workingCar = null;
                    Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                    int randVNorth = randGen.Next(roadV.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                    int randVSouth = randGen.Next(roadV.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                    int lanePositionVNorth = roadV.getRoadLength();
                    int lanePositionVSouth = 0;

                    if (this.trafficJam(modelIntersection.getvRoadIntersection().getRoad(), Settings.TRAFFIC_WEST_NORTH) == false)
                    {
                        modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randVNorth).addCar(workingCar = new Car(lanePositionVNorth));

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnLeftProbability() * 100))
                        {
                            workingCar.setTurningLeft(true);
                        }

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnRightProbability() * 100))
                        {
                            workingCar.setTurningRight(true);
                        }
                    }

                    workingCar = null;

                    if (this.trafficJam(modelIntersection.getvRoadIntersection().getRoad(), Settings.TRAFFIC_EAST_SOUTH) == false)
                    {
                        modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randVSouth).addCar(workingCar = new Car(lanePositionVSouth));

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnLeftProbability() * 100))
                        {
                            workingCar.setTurningLeft(true);
                        }

                        if (randGen.Next(100) < (Settings.getSimSettings().getTurnRightProbability() * 100))
                        {
                            workingCar.setTurningRight(true);
                        }

                    }

                }
            }
            carFrame(modelIntersection.getvRoadIntersection(), modelIntersection.gethRoadIntersection());
            carFrame(modelIntersection.gethRoadIntersection(), modelIntersection.getvRoadIntersection());

            window.Refresh();

        }

        public void carFrame(RoadIntersection roadIntersection, RoadIntersection intersectingRoadIntersection)
        {
            for (int i = 0; i < roadIntersection.getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH).Count(); i++)
            {
                Lane l = roadIntersection.getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH)[i];

                for (int j = 0; j < l.getCars().Count(); j++)
                {
                    Car c = l.getCars()[j];
                    carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_WEST_NORTH);
                }
            }

            for (int i = 0; i < roadIntersection.getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH).Count(); i++)
            {
                Lane l = roadIntersection.getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH)[i];

                for (int j = 0; j < l.getCars().Count(); j++)
                {
                    Car c = l.getCars()[j];
                    carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_EAST_SOUTH);
                }
            }
        }

        private void carFrameProcess(RoadIntersection roadIntersection, RoadIntersection intersectingRoadIntersection, Lane lane, Car car, Boolean trafficDirection)
        {

            int trafficCycleDistance;
            int middleLine;
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
        if(car.getStopped() == false) {
            if(randGen.NextDouble()*Settings.BREAKDOWN_PROBABILITY_LIMIT < Settings.getSimSettings().getBreakdownProbability()) {
                car.breakdown();
            } else {
                car.moveCar(trafficCycleDistance);
            }

            //Calculated the Middle Line Position

            middleLine = intersectingRoadIntersection.getIntersectionCenter();

            //Calculate the Stop Line Position
            
            if(Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    stopLine = roadIntersection.getIntersectionCenter()-(intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
                } else {
                    stopLine = roadIntersection.getIntersectionCenter()+(intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
                }
            } else {
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    stopLine = roadIntersection.getIntersectionCenter()-(intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
                } else {
                    stopLine = roadIntersection.getIntersectionCenter()+(intersectingRoadIntersection.getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
                }
            }

            //-------------------------------------------//
            // Intersection Rules - Stopping and Turning //
            //-------------------------------------------//
            // Note: many of the conditional statements in this section are
            // multi-lined and indented for easier reading and understanding

            int directionMultiplier = 0;

            if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                directionMultiplier = 1;
            } else {
                directionMultiplier = -1;
            }

            if(car.intersects(stopLine, roadIntersection.getRoad().getRoadSpeed(), trafficDirection)) {
                //Rules for when cars should stop.
                if (
                     roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT
                ) {
                    car.stopLight();
                    car.moveCar(-directionMultiplier*Settings.CAR_MOVE);
                }
                
                if (
                     roadIntersection.getLightState() != RoadIntersection.TURNING_GREEN_LIGHT
                     && car.getTurningRight() == true
                     && Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC
                     && ((roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH && trafficDirection == Settings.TRAFFIC_EAST_SOUTH) || (roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST && trafficDirection == Settings.TRAFFIC_WEST_NORTH))
                     && lane == roadIntersection.getRoad().getLane(trafficDirection, 0)
                ) {
                    car.stopLight();
                } else if (
                     roadIntersection.getLightState() != RoadIntersection.TURNING_GREEN_LIGHT
                     && car.getTurningRight() == true
                     && Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC
                     && ((roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH && trafficDirection == Settings.TRAFFIC_WEST_NORTH) || (roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST && trafficDirection == Settings.TRAFFIC_EAST_SOUTH))
                     && lane == roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1))
                ) {
                    car.stopLight();
                } else if (
                     roadIntersection.getLightState() != RoadIntersection.TURNING_GREEN_LIGHT
                     && car.getTurningLeft() == true
                     && Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC
                     && ((roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH && trafficDirection == Settings.TRAFFIC_WEST_NORTH) || (roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST && trafficDirection == Settings.TRAFFIC_EAST_SOUTH))
                     && lane == roadIntersection.getRoad().getLane(trafficDirection, 0)
                ) {
                    car.stopLight();
                } else if (
                     roadIntersection.getLightState() != RoadIntersection.TURNING_GREEN_LIGHT
                     && car.getTurningLeft() == true
                     && Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC
                     && ((roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH && trafficDirection == Settings.TRAFFIC_EAST_SOUTH) || (roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST && trafficDirection == Settings.TRAFFIC_WEST_NORTH))
                     && lane == roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1))
                ) {
                    car.stopLight();
                }

            } else  if(
                    car.intersects((stopLine+((Settings.CAR_LENGTH/2)*directionMultiplier)), roadIntersection.getRoad().getRoadSpeed(), trafficDirection)
                    && (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                    && (car.getTurningLeft() == true)
                    && (roadIntersection.getLightState() == RoadIntersection.GREEN_LIGHT)
            ) {
                //Car is turning left into first lane

                Lane turningLane = null;
                int laneLocation = 0;
                
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1))) {
                        turningLane = intersectingRoadIntersection.getRoad().getLane(trafficDirection, 0);
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()+(((roadIntersection.getRoad().getNoLanes(trafficDirection)-1)*Settings.LANE_WIDTH));
                        car.turn(turningLane, lane, laneLocation);
                    }
                } else  {
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, 0)) {
                        turningLane = intersectingRoadIntersection.getRoad().getLane(trafficDirection, (intersectingRoadIntersection.getRoad().getNoLanes(trafficDirection)-1));
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()-(((roadIntersection.getRoad().getNoLanes(trafficDirection)-1)*Settings.LANE_WIDTH))-Settings.CAR_LENGTH;
                        car.turn(turningLane, lane, laneLocation);
                    }
                }
                
            } else if(
                    car.intersects((stopLine+(Settings.CAR_LENGTH/2)*directionMultiplier), roadIntersection.getRoad().getRoadSpeed(), trafficDirection)
                    && (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC)
                    && (car.getTurningRight() == true)
                    && (roadIntersection.getLightState() == RoadIntersection.GREEN_LIGHT)
            ) {
                //Car is turning right into first lane

                Lane turningLane = null;
                int laneLocation = 0;

                if((trafficDirection == Settings.TRAFFIC_EAST_SOUTH && roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) || (trafficDirection == Settings.TRAFFIC_WEST_NORTH && roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST)) {
                    // Traffic heading South or West
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, 0)) {
                        // Traffic in the Left/Top Lane
                        if(roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) {
                            // Traffic Heading South turning West
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0);
                        } else {
                            // Traffic Heading West turning North
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (intersectingRoadIntersection.getRoad().getNoLanes(trafficDirection)-1));
                        }                        
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()-(((roadIntersection.getRoad().getNoLanes(trafficDirection))*Settings.LANE_WIDTH))-(Settings.CAR_LENGTH/2);
                        car.turn(turningLane, lane, laneLocation);
                    }
                } else {
                    // Traffic heading North or East
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1))) {
                        // Traffic in the Bottom/Right Lane
                        if(roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) {
                            // Traffic heading North turning East
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (intersectingRoadIntersection.getRoad().getNoLanes(trafficDirection)-1));
                        } else {
                            // Traffic heading East turning South
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0);
                        }                        
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()+(((roadIntersection.getRoad().getNoLanes(trafficDirection))*Settings.LANE_WIDTH)-(Settings.CAR_LENGTH/2));
                        car.turn(turningLane, lane, laneLocation);
                    }
                }

            } else if(
                    car.intersects((middleLine+(Settings.CAR_LENGTH/2)*directionMultiplier), roadIntersection.getRoad().getRoadSpeed(), trafficDirection)
                    && (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                    && (car.getTurningRight() == true)
                    && (roadIntersection.getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
            ) {
                //Car is turning right into second lane

                Lane turningLane = null;
                int laneLocation = 0;

                if((trafficDirection == Settings.TRAFFIC_EAST_SOUTH && roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) || (trafficDirection == Settings.TRAFFIC_WEST_NORTH && roadIntersection.getRoadOrientation() == Settings.ROAD_EAST_WEST)) {
                    // Traffic heading South or West
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, 0)) {
                        // Traffic in the Left/Top Lane
                        if(roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) {
                            // Traffic Heading South turning West
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, 0);
                        } else {
                            // Traffic Heading West turning North
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, (intersectingRoadIntersection.getRoad().getNoLanes(trafficDirection)-1));
                        }
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()-(Settings.CAR_LENGTH/2);
                        car.turn(turningLane, lane, laneLocation);
                    }
                } else {
                    // Traffic heading North or East
                    if(lane == roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1))) {
                        // Traffic in the Bottom/Right Lane
                        if(roadIntersection.getRoadOrientation() == Settings.ROAD_SOUTH_NORTH) {
                            // Traffic heading North turning East
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, (intersectingRoadIntersection.getRoad().getNoLanes(trafficDirection)-1));
                        } else {
                            // Traffic heading East turning South
                            turningLane = intersectingRoadIntersection.getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, 0);
                        }
                        laneLocation = intersectingRoadIntersection.getIntersectionCenter()+(Settings.CAR_LENGTH/2);
                        car.turn(turningLane, lane, laneLocation);
                    }
                }
                
            } else if(
                    car.intersects((middleLine+(Settings.CAR_LENGTH/2)), roadIntersection.getRoad().getRoadSpeed(), trafficDirection)
                    && (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC)
                    && (car.getTurningLeft() == true)
                    //&& (roadIntersection.getRoad().getLane(trafficDirection, (roadIntersection.getRoad().getNoLanes(trafficDirection)-1)) == lane)
                    && (roadIntersection.getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
            ) {
                //TO BE IMPLEMENTED
            }

            if(car.intersects(lane.getCarInfront(car, trafficDirection))) {

                bool lanesChanged = false;

                foreach (Lane nl in roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection)) {
                    if(nl.isLaneClear((car.getLanePosition()+(trafficCycleDistance*2)))) {
                        roadIntersection.getRoad().trafficChangeLane(lane, car, nl);

                        lanesChanged = true;
                        break;
                    }
                }
                car.setStopped(!lanesChanged);
                if(lanesChanged == false) {
                    car.moveCar(-2*trafficCycleDistance);
                }
            }
        } else {

            Car fakeCar = new Car(car.getLanePosition());
            fakeCar.moveCar(trafficCycleDistance*2);

            if(fakeCar.intersects(lane.getCarInfront(car, trafficDirection)) && lane.getCarInfront(car, trafficDirection).getBrokenDown() == true) {
                bool lanesChanged = false;

                foreach (Lane nl in roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection)) {
                    if(nl.isLaneClear(car.getLanePosition()+trafficCycleDistance)) {
                        roadIntersection.getRoad().trafficChangeLane(lane, car, nl);

                        lanesChanged = true;
                        break;
                    }
                }
                
                if(lanesChanged == true) {
                    car.setStopped(false);
                }

            } else if(car.getBrokenDown() != true && car.getLightStopped() != true) {
                if(!fakeCar.intersects(lane.getCarInfront(car, trafficDirection))) {
                    //car infront has moved!
                    car.setStopped(false);
                }
            }
        }
        }

        private bool trafficJam(Road road, Boolean trafficDirection)
        {
            int noBlockedLanes = 0;

            for (int i = 0; i < road.getLanes(trafficDirection).Count(); i++)
            // foreach (Lane l in road.getLanes(trafficDirection)) 
            {
                Lane l = road.getLanes(trafficDirection)[i];
                //   foreach (Car c in l.getCars()) 
                for (int j = 0; j < l.getCars().Count(); j++)
                {
                    Car c = l.getCars()[j];
                    if (trafficDirection == Settings.TRAFFIC_EAST_SOUTH)
                    {
                        if (c.getLanePosition() <= (1 - Settings.TRAFFIC_JAM_THRESHOLD) * road.getRoadLength() && c.getStopped() == true)
                        {
                            noBlockedLanes++;
                        }
                    }
                    else
                    {
                        if (c.getLanePosition() >= Settings.TRAFFIC_JAM_THRESHOLD * road.getRoadLength() && c.getStopped() == true)
                        {
                            noBlockedLanes++;
                        }
                    }
                }
            }

            if (noBlockedLanes >= (int)(road.getNoLanes(trafficDirection) * Settings.TRAFFIC_JAM_LANES_JAMMED))
            {
                return true;
            }
            else
            {
                return false;
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
                intersection.gethRoadIntersection().setLightState(RoadIntersection.TURNING_GREEN_LIGHT);
            }

            else if (intersection.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
            {
                intersection.gethRoadIntersection().setLightState(RoadIntersection.TURNING_RED_LIGHT);
                intersection.getvRoadIntersection().setLightState(RoadIntersection.GREEN_LIGHT);
            }

            else if (intersection.getvRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
            {
                intersection.getvRoadIntersection().setLightState(RoadIntersection.RED_LIGHT);
                intersection.getvRoadIntersection().setLightState(RoadIntersection.TURNING_GREEN_LIGHT);
            }

            else if (intersection.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
            {
                intersection.getvRoadIntersection().setLightState(RoadIntersection.TURNING_RED_LIGHT);
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
        }
    }

    
}

