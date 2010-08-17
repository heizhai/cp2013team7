package trafficsim;

import java.util.Random;
import java.util.Timer;
import java.util.TimerTask;

/**
 *
 * @author Tristan Davey
 */
public class Simulation {

    private int frameCount  = 0;
    /**
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

class SimulationEnvironment {

    private Intersection modelIntersection;
    private mainWindow window;
    private Timer frameTimer = new Timer();
    private Timer lightTimer;


    SimulationEnvironment(Intersection modelIntersection, mainWindow window) {
        this.modelIntersection = modelIntersection;
        this.window = window;
    }

    /**
     * @param runCycles
     */
    public void lightCycle(int runCycles) {
        
    }

    public void run() throws InterruptedException {
        Settings.getSimSettings().setSimulationRunning(true);
        frameTimer.scheduleAtFixedRate(new simulationFrame(modelIntersection, window), 0, 100);
    }

    public void stop() throws InterruptedException {
        Settings.getSimSettings().setSimulationRunning(false);
        frameTimer.cancel();
        modelIntersection.reset();
        frameTimer = new Timer();
    }

    void setWindow(mainWindow window) {
        this.window = window;
    }

}

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
        if(frameCount%48 == 0 || frameCount == 1) {
            //Horizontal Road
            Road roadH = modelIntersection.gethRoadIntersection().getRoad();
            int randH = randGen.nextInt(roadH.getNoLanes());
            int lanePositionH;
            if(roadH.getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH) {
                lanePositionH = 0;
            } else {
                lanePositionH = roadH.getRoadLength();
            }
            modelIntersection.gethRoadIntersection().getRoad().getLane(randH).addCar(new Car(lanePositionH));

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

                    if(c.intersects(roadIntersection.getIntersectionStopLine()) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT)) {
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
