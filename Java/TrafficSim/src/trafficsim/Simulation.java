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
    public void lightCycle(Boolean automatedExecution) {
        lightTimer = new Timer();
        if(modelIntersection.gethRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().GREEN_LIGHT) {
            modelIntersection.gethRoadIntersection().setLightState(modelIntersection.gethRoadIntersection().YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window, automatedExecution), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        } else if (modelIntersection.getvRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().GREEN_LIGHT) {
            modelIntersection.getvRoadIntersection().setLightState(modelIntersection.getvRoadIntersection().YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window, automatedExecution), (((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)+modelIntersection.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH))*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        }
    }

    public void multiLightCycle(int noCycles) {
        lightMultiCyclesTimer = new Timer();
        lightMultiCyclesTimer.scheduleAtFixedRate(new multiLightCycle(this, window, noCycles), 0, Settings.LIGHT_CYCLE_TIME);
    }

    public void run() throws InterruptedException {
        frameTimer = new Timer();
        this.modelIntersection.reset();
        Settings.getSimSettings().setSimulationRunning(true);
        window.lightCycle(true);
        frameTimer.scheduleAtFixedRate(new simulationFrame(modelIntersection, window), 0, Settings.FRAME_LENGTH);
    }

    public void stop() throws InterruptedException {
        Settings.getSimSettings().setSimulationRunning(false);
        frameTimer.cancel();
        window.lightCycle(false);
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
        if(frameCount%Settings.CAR_FREQUENCY == 0 || frameCount == 1) {
            //Horizontal Road
            if(randGen.nextInt(100) < (Settings.getSimSettings().getHCarProbability()*100)) {
                int lanePositionHWest;
                int lanePositionHEast;
                Road roadH = modelIntersection.gethRoadIntersection().getRoad();
                int randHWest = randGen.nextInt(roadH.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                int randHEast = randGen.nextInt(roadH.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                if(Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
                    lanePositionHWest = roadH.getRoadLength();
                    lanePositionHEast = 0;
                } else {
                    lanePositionHWest = 0;
                    lanePositionHEast = roadH.getRoadLength();
                }

                modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randHWest).addCar(new Car(lanePositionHWest));
                modelIntersection.gethRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randHEast).addCar(new Car(lanePositionHEast));
            }

            if(randGen.nextInt(100) < (Settings.getSimSettings().getVCarProbability()*100)) {
            //Vertical Road
                int lanePositionVNorth;
                int lanePositionVSouth;
                Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                int randVNorth = randGen.nextInt(roadV.getNoLanes(Settings.TRAFFIC_WEST_NORTH));
                int randVSouth = randGen.nextInt(roadV.getNoLanes(Settings.TRAFFIC_EAST_SOUTH));
                if(Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
                    lanePositionVNorth = roadV.getRoadLength();
                    lanePositionVSouth = 0;
                } else {
                    lanePositionVNorth = 0;
                    lanePositionVSouth = roadV.getRoadLength();
                }

                modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_WEST_NORTH, randVNorth).addCar(new Car(lanePositionVNorth));
                modelIntersection.getvRoadIntersection().getRoad().getLane(Settings.TRAFFIC_EAST_SOUTH, randVSouth).addCar(new Car(lanePositionVSouth));
            }
        }
        carFrame(modelIntersection.getvRoadIntersection(), modelIntersection.gethRoadIntersection());
        carFrame(modelIntersection.gethRoadIntersection(), modelIntersection.getvRoadIntersection());

        window.repaint();

    }

    public void carFrame(RoadIntersection roadIntersection,  RoadIntersection intersectingRoadIntersection) {

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
        int stopLine;

        if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
            trafficCycleDistance = Settings.CAR_MOVE;
        } else {
            trafficCycleDistance = (-1*Settings.CAR_MOVE);
        }
        

        //Move The Car Forward
        if(car.getStopped() == false) {
            if(randGen.nextDouble()*Settings.BREAKDOWN_PROBABILITY_LIMIT < Settings.getSimSettings().getBreakdownProbability()) {
                car.setBrokenDown(true);
            } else {
                car.moveCar(trafficCycleDistance);
            }

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
            
            if(car.intersects(stopLine, trafficDirection) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT)) {
                car.setStopped(true);
                car.moveCar(-1*(trafficCycleDistance));
            }


            if(car.intersects(lane.getCarInfront(car, trafficDirection))) {

                boolean lanesChanged = false;

                for(Lane nl: roadIntersection.getRoad().getNeighbouringLanes(lane, trafficDirection)) {
                    if(nl.isLaneClear(car.getLanePosition())) {
                        roadIntersection.getRoad().trafficChangeLane(lane, car, nl);

                        lanesChanged = true;
                        break;
                    }
                }
                car.setStopped(!lanesChanged);
                if(lanesChanged == false) {
                    car.moveCar(-(trafficCycleDistance*2));
                }
            }
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
        Boolean automatedExecution;

        lightCycle(Intersection intersection, mainWindow window, Boolean automatedExecution) {
            this.intersection = intersection;
            this.window = window;
            this.automatedExecution = automatedExecution;
        }

        public void run() {
           if(intersection.gethRoadIntersection().getLightState() == intersection.gethRoadIntersection().YELLOW_LIGHT) {
               intersection.gethRoadIntersection().setLightState(intersection.gethRoadIntersection().RED_LIGHT);
               intersection.getvRoadIntersection().setLightState(intersection.getvRoadIntersection().GREEN_LIGHT);
           } else if (intersection.getvRoadIntersection().getLightState() == intersection.getvRoadIntersection().YELLOW_LIGHT) {
               intersection.getvRoadIntersection().setLightState(intersection.getvRoadIntersection().RED_LIGHT);
               intersection.gethRoadIntersection().setLightState(intersection.gethRoadIntersection().GREEN_LIGHT);
           }
           if(this.automatedExecution == false) {
            // Do not give user feedback if this is an automated cycle.
            window.lightCycle(true);
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

        multiLightCycle(SimulationEnvironment simulation, mainWindow window, int noCycles) {
            this.simulation = simulation;
            this.window = window;
            this.noCycles = noCycles;
        }

        public void run() {
           simulation.lightCycle(true);
           noCycles--;
           if(noCycles <= 0) {
               window.lightCycle(true);
               cancel();
           }
        }
}

