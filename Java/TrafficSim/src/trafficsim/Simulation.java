package trafficsim;

/**
 *
 * @author Tristan Davey
 */
public class Simulation {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {

        Intersection modelIntersection = new Intersection();

        mainWindow window = new mainWindow(modelIntersection);
        window.setSize(700, 700);
        window.setVisible(true);
        
    }

    public void init() {

    }


    public void simCycle(Intersection modelIntersection) {
        // Generate New Cars
            //TODO: Add Probability



        // Move all Cars

    }

    /**
     * @param runCycles
     */
    public void lightCycle(int runCycles) {

    }

    public void carSimCycle(RoadIntersection roadIntersection) {

       int trafficCycleDistance = Settings.CAR_MOVE;

       for (Lane l: roadIntersection.getRoad().getLanes()) {
            for (Car c: l.getCars()) {
                //Move The Car Forward
                if(c.getStopped() == false) {
                    if(c.intersects(roadIntersection.getIntersectionStopLine()) && (roadIntersection.getLightState() != RoadIntersection.GREEN_LIGHT)) {
                        c.setStopped(true);
                        c.moveCar(-trafficCycleDistance);
                    }
                    c.moveCar(trafficCycleDistance);
                }

                if(c.intersects(l.getCarInfront(c))) {
                    for(Lane nl: roadIntersection.getRoad().getNeighbouringLanes(l)) {
                        if(nl.isLaneClear(c.getLanePosition())) {
                            roadIntersection.getRoad().trafficChangeLane(l, c, nl);
                            break;
                        } else {
                            c.moveCar(-trafficCycleDistance);
                            c.setStopped(true);
                        }
                    }
                } else {

                }
            }
       }
    }

}
