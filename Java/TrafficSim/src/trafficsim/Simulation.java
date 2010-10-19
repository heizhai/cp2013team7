package trafficsim;

import java.util.Random;
import java.util.Timer;
import java.util.TimerTask;

/**
 * Class which initialises and begins the Traffic Simulation Program
 *
 * @author Tristan Davey
 */
public class Simulation {

    /**
     * Program Constructor
     *
     * @param args the command line arguments
     */
    public static void main(String[] args) throws InterruptedException {

        Intersection modelIntersection = new Intersection();
        SimulationEnvironment simulation = null;
        mainWindow window = null;
        simulation = new SimulationEnvironment(modelIntersection, window);
        window = new mainWindow(modelIntersection, simulation, "Traffic Intersection Simulation");
        simulation.setWindow(window);

        
        window.setSize(700, 700);
        window.setVisible(true);

    }
}

/**
 * Environment in which all the major actions of the traffic simulation are housed
 *
 * @author Tristan Davey
 */
class SimulationEnvironment {

    private Intersection modelIntersection;
    private mainWindow window;
    private Timer frameTimer;
    private Timer lightTimer;
    private Timer lightMultiCyclesTimer;


    SimulationEnvironment(Intersection modelIntersection, mainWindow window) {
        this.modelIntersection = modelIntersection;
        this.window = window;
    }

    /**
     * @param runCycles
     */
    public void lightCycle() {
        lightTimer = new Timer();
        if (modelIntersection.gethRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().GREEN_LIGHT) {
            modelIntersection.gethRoadIntersection().setLightState(modelIntersection.gethRoadIntersection().YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        } else if (modelIntersection.gethRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().TURNING_GREEN_LIGHT) {
            modelIntersection.gethRoadIntersection().setLightState(modelIntersection.gethRoadIntersection().TURNING_YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        } else if (modelIntersection.getvRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().GREEN_LIGHT) {
            modelIntersection.getvRoadIntersection().setLightState(modelIntersection.getvRoadIntersection().YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        } else if (modelIntersection.getvRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().TURNING_GREEN_LIGHT) {
            modelIntersection.getvRoadIntersection().setLightState(modelIntersection.getvRoadIntersection().TURNING_YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        }
    }

    public void multiLightCycle() {
        lightMultiCyclesTimer = new Timer();
        lightMultiCyclesTimer.scheduleAtFixedRate(new multiLightCycle(this, window), 0, Settings.LIGHT_CYCLE_TIME);
    }

    public void run() throws InterruptedException {
        frameTimer = new Timer();
        multiLightCycle();
        Settings.getSimSettings().setSimulationRunning(true);
        frameTimer.scheduleAtFixedRate(new simulationFrame(modelIntersection, window), 0, Settings.FRAME_LENGTH);
    }

    public void stop() throws InterruptedException {
        Settings.getSimSettings().setSimulationRunning(false);
        frameTimer.cancel();
        lightMultiCyclesTimer.cancel();
    }

    public void reset() throws InterruptedException {
        if(Settings.getSimSettings().getSimulationRunning() == true) {
            frameTimer.cancel();
            lightMultiCyclesTimer.cancel();
        }
        modelIntersection.reset();
    }

    void setWindow(mainWindow window) {
        this.window = window;
    }

}

/**
 * Class extended from TimerTask designed to be run as a threaded process to execute the processes in each frame of the simulation.
 *
 * @author Tristan Davey
 */
class simulationFrame extends TimerTask {

    private int frameCount = 0;
    private Random randGen = new Random();
    private Intersection modelIntersection;
    private mainWindow window;

    simulationFrame(Intersection modelIntersection, mainWindow window) {
        this.modelIntersection = modelIntersection;
        this.window = window;
    }

    public void run() {
        frameCount++;

        System.out.println(frameCount);
        // Generate New Cars
        if(frameCount%Settings.CAR_FREQUENCY == 0) {
            //Horizontal Road
            if(randGen.nextInt(100) < (Settings.getSimSettings().getHCarProbability()*100)) {
                Car workingCar = null;
                Road roadH = modelIntersection.gethRoadIntersection().getRoad();
                int randHWest = randGen.nextInt(roadH.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                int randHEast = randGen.nextInt(roadH.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                int lanePositionHWest = roadH.getRoadLength();
                int lanePositionHEast = 0;

                if(this.trafficJam(modelIntersection.gethRoadIntersection().getRoad(), Settings.TRAFFIC_WEST_NORTH) == false) {
                    modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randHWest).addCar(workingCar = new Car(lanePositionHWest));
                
                    if(randGen.nextInt() < (Settings.getSimSettings().getTurnLeftProbability()*100)) {
                        workingCar.setTurningLeft(true);
                    }

                    if (randGen.nextInt() < (Settings.getSimSettings().getTurnRightProbability()*100)) {
                        workingCar.setTurningRight(true);
                    }
                }

                workingCar = null;

                if(this.trafficJam(modelIntersection.gethRoadIntersection().getRoad(), Settings.TRAFFIC_EAST_SOUTH) == false) {
                    modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randHEast).addCar(workingCar = new Car(lanePositionHEast));

                    if(randGen.nextInt() < (Settings.getSimSettings().getTurnLeftProbability()*100)) {
                        workingCar.setTurningLeft(true);
                    }

                    if (randGen.nextInt() < (Settings.getSimSettings().getTurnRightProbability()*100)) {
                        workingCar.setTurningRight(true);
                    }
                }

            }

            if(randGen.nextInt(100) < (Settings.getSimSettings().getVCarProbability()*100)) {
            //Vertical Road
                Car workingCar = null;
                Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                int randVNorth = randGen.nextInt(roadV.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                int randVSouth = randGen.nextInt(roadV.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                int lanePositionVNorth = roadV.getRoadLength();
                int lanePositionVSouth = 0;

                if(this.trafficJam(modelIntersection.getvRoadIntersection().getRoad(), Settings.TRAFFIC_WEST_NORTH) == false) {
                    modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randVNorth).addCar(workingCar = new Car(lanePositionVNorth));

                    if(randGen.nextInt() < (Settings.getSimSettings().getTurnLeftProbability()*100)) {
                        workingCar.setTurningLeft(true);
                    }

                    if (randGen.nextInt() < (Settings.getSimSettings().getTurnRightProbability()*100)) {
                        workingCar.setTurningRight(true);
                    }
                }

                workingCar = null;

                if(this.trafficJam(modelIntersection.getvRoadIntersection().getRoad(), Settings.TRAFFIC_EAST_SOUTH) == false) {
                    modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randVSouth).addCar(workingCar = new Car(lanePositionVSouth));

                    if(randGen.nextInt() < (Settings.getSimSettings().getTurnLeftProbability()*100)) {
                        workingCar.setTurningLeft(true);
                    }

                    if (randGen.nextInt() < (Settings.getSimSettings().getTurnRightProbability()*100)) {
                        workingCar.setTurningRight(true);
                    }

                }

            }
        }
        carFrame(modelIntersection.getvRoadIntersection(), modelIntersection.gethRoadIntersection());
        carFrame(modelIntersection.gethRoadIntersection(), modelIntersection.getvRoadIntersection());

        window.repaint();

    }

    private void carFrame(RoadIntersection roadIntersection,  RoadIntersection intersectingRoadIntersection) {

       for (Lane l: roadIntersection.getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH)) {
            for (Car c: l.getCars()) {
                carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_WEST_NORTH);
            }
       }

       for (Lane l: roadIntersection.getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH)){
            for (Car c: l.getCars()) {
                carFrameProcess(roadIntersection, intersectingRoadIntersection, l, c, Settings.TRAFFIC_EAST_SOUTH);
            }
       }
    }

    private void carFrameProcess(RoadIntersection roadIntersection, RoadIntersection intersectingRoadIntersection, Lane lane, Car car, Boolean trafficDirection) {
        
        int trafficCycleDistance;
        int middleLine;
        int stopLine;

        if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
            trafficCycleDistance = roadIntersection.getRoad().getRoadSpeed();
        } else {
            trafficCycleDistance = (-1*roadIntersection.getRoad().getRoadSpeed());
        }
        

        //Move The Car Forward
        if(car.getStopped() == false) {
            if(randGen.nextDouble()*Settings.BREAKDOWN_PROBABILITY_LIMIT < Settings.getSimSettings().getBreakdownProbability()) {
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

                boolean lanesChanged = false;

                for(Lane nl: roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection)) {
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
                boolean lanesChanged = false;

                for(Lane nl: roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection)) {
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


    private boolean trafficJam(Road road, Boolean trafficDirection) {
        int noBlockedLanes = 0;

        for(Lane l: road.getLanes(trafficDirection)) {
            for(Car c: l.getCars()) {
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    if(c.getLanePosition() <= (1-Settings.TRAFFIC_JAM_THRESHOLD)*road.getRoadLength() && c.getStopped() == true) {
                        noBlockedLanes++;
                    }
                } else {
                    if(c.getLanePosition() >= Settings.TRAFFIC_JAM_THRESHOLD*road.getRoadLength() && c.getStopped() == true) {
                        noBlockedLanes++;
                    }
                }
            }
        }

        if(noBlockedLanes >= (int) (road.getNoLanes(trafficDirection)*Settings.TRAFFIC_JAM_LANES_JAMMED)) {
            return true;
        } else {
            return false;
        }
    }

}

/**
 * Class extended from TimerTask which handles the timed change of traffic lights when a lightChange is processed.
 *
 * @author Tristan Davey
 */
class lightCycle extends TimerTask {

        Intersection intersection;
        mainWindow window;

        lightCycle(Intersection intersection, mainWindow window) {
            this.intersection = intersection;
            this.window = window;
        }

        public void run() {
           if(intersection.gethRoadIntersection().getLightState() == intersection.gethRoadIntersection().YELLOW_LIGHT) {
               intersection.gethRoadIntersection().setLightState(intersection.gethRoadIntersection().RED_LIGHT);
               intersection.getvRoadIntersection().setLightState(intersection.getvRoadIntersection().TURNING_GREEN_LIGHT);
           } else if (intersection.gethRoadIntersection().getLightState() == intersection.gethRoadIntersection().TURNING_YELLOW_LIGHT) {
               intersection.gethRoadIntersection().setLightState(intersection.gethRoadIntersection().GREEN_LIGHT);
           } else if (intersection.getvRoadIntersection().getLightState() == intersection.getvRoadIntersection().YELLOW_LIGHT) {
               intersection.getvRoadIntersection().setLightState(intersection.getvRoadIntersection().RED_LIGHT);
               intersection.gethRoadIntersection().setLightState(intersection.gethRoadIntersection().TURNING_GREEN_LIGHT);
           } else if (intersection.getvRoadIntersection().getLightState() == intersection.getvRoadIntersection().TURNING_YELLOW_LIGHT) {
               intersection.getvRoadIntersection().setLightState(intersection.getvRoadIntersection().GREEN_LIGHT);
           }
        }
}

/**
 * Class extended from TimerTask which handles the timed change of traffic lights when a lightChange is processed.
 *
 * @author Tristan Davey
 */
class multiLightCycle extends TimerTask {

        SimulationEnvironment simulation;
        mainWindow window;
        int noCycles;

        multiLightCycle(SimulationEnvironment simulation, mainWindow window) {
            this.simulation = simulation;
            this.window = window;
        }

        public void run() {
           simulation.lightCycle();
        }
}

