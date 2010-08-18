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
            lightTimer.schedule(new lightCycle(modelIntersection, window, automatedExecution), ((((modelIntersection.getvRoadIntersection().getRoad().getNoLanes()*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        } else if (modelIntersection.getvRoadIntersection().getLightState() == modelIntersection.gethRoadIntersection().GREEN_LIGHT) {
            modelIntersection.getvRoadIntersection().setLightState(modelIntersection.getvRoadIntersection().YELLOW_LIGHT);
            lightTimer.schedule(new lightCycle(modelIntersection, window, automatedExecution), ((((modelIntersection.gethRoadIntersection().getRoad().getNoLanes()*Settings.LANE_WIDTH)/Settings.CAR_MOVE)+Settings.CAR_LENGTH)*Settings.FRAME_LENGTH));
        }
    }

    public void multiLightCycle(int noCycles) {
        lightMultiCyclesTimer = new Timer();
        lightMultiCyclesTimer.scheduleAtFixedRate(new multiLightCycle(this, window, noCycles), 0, Settings.LIGHT_CYCLE_TIME);
    }

    public void run() throws InterruptedException {
        frameTimer = new Timer();
        Settings.getSimSettings().setSimulationRunning(true);
        window.lightCycle(true);
        frameTimer.scheduleAtFixedRate(new simulationFrame(modelIntersection, window), 0, 100);
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
    Intersection modelIntersection;
    mainWindow window;

    simulationFrame(Intersection modelIntersection, mainWindow window) {
        this.modelIntersection = modelIntersection;
        this.window = window;
    }

    public void run() {
        Random randGen = new Random();
        frameCount++;

        System.out.println(frameCount);
        // Generate New Cars
        if(frameCount%Settings.CAR_FREQUENCY == 0 || frameCount == 1) {
            //Horizontal Road
            if(randGen.nextInt(100) < (Settings.getSimSettings().getHCarProbability()*100)) {
                Road roadH = modelIntersection.gethRoadIntersection().getRoad();
                int randH = randGen.nextInt(roadH.getNoLanes());
                int lanePositionH;
                if(roadH.getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH) {
                    lanePositionH = 0;
                } else {
                    lanePositionH = roadH.getRoadLength();
                }
                modelIntersection.gethRoadIntersection().getRoad().getLane(randH).addCar(new Car(lanePositionH));
            }

            if(randGen.nextInt(100) < (Settings.getSimSettings().getVCarProbability()*100)) {
            //Vertical Road
                Road roadV = modelIntersection.getvRoadIntersection().getRoad();
                int randV = randGen.nextInt(roadV.getNoLanes());
                int lanePositionV;
                if(roadV.getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH) {
                    lanePositionV = 0;
                } else {
                    lanePositionV = roadV.getRoadLength();
                }
                modelIntersection.getvRoadIntersection().getRoad().getLane(randV).addCar(new Car(lanePositionV));
            }
        }
        //TODO: Add Probability
        carFrame(modelIntersection.getvRoadIntersection());
        carFrame(modelIntersection.gethRoadIntersection());

        window.repaint();

    }

    public void carFrame(RoadIntersection roadIntersection) {

       int trafficCycleDistance = Settings.CAR_MOVE;

       for (Lane l: roadIntersection.getRoad().getLanes()) {
            for (Car c: l.getCars()) {
                //Move The Car Forward
                if(c.getStopped() == false) {
                    c.moveCar(trafficCycleDistance);

                    if(c.intersects(roadIntersection.getIntersectionStopLine(), roadIntersection.getRoad().getTrafficDirection()) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT)) {
                        c.setStopped(true);
                        c.moveCar(-(trafficCycleDistance));
                    }


                    if(c.intersects(l.getCarInfront(c, roadIntersection.getRoad().getTrafficDirection()))) {

                        boolean lanesChanged = false;

                        for(Lane nl: roadIntersection.getRoad().getNeighbouringLanes(l)) {
                            if(nl.isLaneClear(c.getLanePosition())) {
                                roadIntersection.getRoad().trafficChangeLane(l, c, nl);
                                
                                lanesChanged = true;
                                break;
                            }
                        }
                        c.setStopped(!lanesChanged);
                        if(lanesChanged == false) {
                            c.moveCar(-(trafficCycleDistance*2));
                        }
                    }
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

