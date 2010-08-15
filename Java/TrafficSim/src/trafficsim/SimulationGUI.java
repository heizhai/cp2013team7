package trafficsim;

import java.awt.*;
import javax.swing.*;

/**
 *
 * @author Tristan Davey
 */
public class SimulationGUI extends JPanel {
    
    private Intersection model;

    SimulationGUI(Intersection modelIntersection) {
        this.model = modelIntersection;
    }

    public void paintComponent(Graphics g) {

        //Setup Background
        g.setColor(Color.green);
        g.fillRect(0,0,this.getWidth(),this.getHeight());
        
        //Draw the Horizontal Road
        //Calculate the Road Values
        int hRoadWidth = Settings.getSimSettings().getHLanes()*Settings.LANE_WIDTH;
        int vRoadWidth = Settings.getSimSettings().getVLanes()*Settings.LANE_WIDTH;

        int hRoadY = (this.getHeight()/2)-(hRoadWidth/2);
        int hRoadX = (this.getWidth()-Settings.getSimSettings().gethLaneLength())/2;

        int vRoadY;
        int vRoadX;

        int xOffset = (this.getWidth()-Settings.getSimSettings().gethLaneLength())/2;
        int yOffset = (this.getHeight()-Settings.getSimSettings().getvLaneLength())/2;

        if(model.gethRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH) {
            vRoadX = xOffset+model.gethRoadIntersection().getIntersectionStopLine();
        } else {
            vRoadX = xOffset+(model.gethRoadIntersection().getIntersectionStopLine()-vRoadWidth);
        }

        if(model.getvRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH) {
            vRoadY = hRoadY-model.getvRoadIntersection().getIntersectionStopLine();
        } else {
            vRoadY = hRoadY-(model.getvRoadIntersection().getIntersectionStopLine()+hRoadWidth);
        }

        //Render Road
        g.setColor(Color.BLACK);
        g.fillRect( hRoadX, hRoadY, Settings.getSimSettings().gethLaneLength(), hRoadWidth);
        g.fillRect( vRoadX, vRoadY, vRoadWidth, Settings.getSimSettings().getvLaneLength());

        //Render Horizontal Road Cars
        renderCars(model.gethRoadIntersection().getRoad(), g, hRoadX, hRoadY, Settings.ROAD_EAST_WEST);
        //Render Vertical Road Cars
        renderCars(model.getvRoadIntersection().getRoad(), g, vRoadX, vRoadY, Settings.ROAD_SOUTH_NORTH);
        
    }

    private void renderCars(Road renderRoad, Graphics g, int roadX, int roadY, Boolean roadOrient) {
        g.setColor(Color.WHITE);
        int lNum = 0;
        int rectW, rectH, rectX, rectY;
        if(roadOrient == Settings.ROAD_EAST_WEST) {
            rectW = Settings.CAR_LENGTH;
            rectH = Settings.CAR_WIDTH;
        } else {
            rectW = Settings.CAR_WIDTH;
            rectH= Settings.CAR_LENGTH;
        }
        for(Lane l: renderRoad.getLanes()) {
            lNum++;
            for(Car c: l.getCars()) {
                if(roadOrient == Settings.ROAD_EAST_WEST) {
                    rectX = roadX+c.getLanePosition();
                    rectY = roadY+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                } else {
                    rectX = roadX+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                    rectY = roadY+c.getLanePosition();
                }
                g.fillRect(rectX, rectY, rectW, rectH);
            }
        }
    }
}
