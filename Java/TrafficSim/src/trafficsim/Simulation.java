/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package trafficsim;

/**
 *
 * @author Tristan Davey
 */
public class Simulation {

    private short hLanes;
    private short vLanes;
    private float hCarProbability;
    private float vCarProbability;
    private Intersection modelIntersection;

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
    }

    /**
     * @param runCycles
     */
    public void run(int runCycles) {

    }

    /**
     * @return the hLanes
     */
    public short getHLanes() {
        return hLanes;
    }

    /**
     * @param hLanes the hLanes to set
     */
    public void setHLanes(short hLanes) {
        this.hLanes = hLanes;
    }

    /**
     * @return the vLanes
     */
    public short getvLanes() {
        return vLanes;
    }

    /**
     * @param vLanes the vLanes to set
     */
    public void setvLanes(short vLanes) {
        this.vLanes = vLanes;
    }

    /**
     * @return the hCarProbability
     */
    public float gethCarProbability() {
        return hCarProbability;
    }

    /**
     * @param hCarProbability the hCarProbability to set
     */
    public void sethCarProbability(float hCarProbability) {
        this.hCarProbability = hCarProbability;
    }

    /**
     * @return the vCarProbability
     */
    public float getvCarProbability() {
        return vCarProbability;
    }

    /**
     * @param vCarProbability the vCarProbability to set
     */
    public void setvCarProbability(float vCarProbability) {
        this.vCarProbability = vCarProbability;
    }

}
