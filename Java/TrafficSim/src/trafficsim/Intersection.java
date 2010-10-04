/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package trafficsim;

/**
 * Class which represents the Intersection in the simulation.
 *
 * @author Tristan Davey
 */
public class Intersection {

    private RoadIntersection vRoadIntersection;
    private RoadIntersection hRoadIntersection;

    Intersection() {
        vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvIntersectionCenter(), Settings.getSimSettings().getVNorthLanes(), Settings.getSimSettings().getVSouthLanes(), Settings.getSimSettings().getvLaneLength());
        hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethIntersectionCenter(), Settings.getSimSettings().getHWestLanes(), Settings.getSimSettings().getHEastLanes(), Settings.getSimSettings().gethLaneLength());
        vRoadIntersection.setLightState(vRoadIntersection.GREEN_LIGHT);
    }

    /**
     * @return the vRoadIntersection
     */
    public RoadIntersection getvRoadIntersection() {
        return vRoadIntersection;
    }

    /**
     * @param vRoadIntersection the vRoadIntersection to set
     */
    public void setvRoadIntersection(RoadIntersection vRoadIntersection) {
        this.vRoadIntersection = vRoadIntersection;
    }

    /**
     * @return the hRoadIntersection
     */
    public RoadIntersection gethRoadIntersection() {
        return hRoadIntersection;
    }

    /**
     * @param hRoadIntersection the hRoadIntersection to set
     */
    public void sethRoadIntersection(RoadIntersection hRoadIntersection) {
        this.hRoadIntersection = hRoadIntersection;
    }

    public void reset() {
        vRoadIntersection = new RoadIntersection(Settings.getSimSettings().getvIntersectionCenter(), Settings.getSimSettings().getVNorthLanes(), Settings.getSimSettings().getVSouthLanes(), Settings.getSimSettings().getvLaneLength());
        hRoadIntersection = new RoadIntersection(Settings.getSimSettings().gethIntersectionCenter(), Settings.getSimSettings().getHWestLanes(), Settings.getSimSettings().getHEastLanes(), Settings.getSimSettings().gethLaneLength());
        vRoadIntersection.setLightState(vRoadIntersection.GREEN_LIGHT);
    }


}
