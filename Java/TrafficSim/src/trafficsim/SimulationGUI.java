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

    @SuppressWarnings("empty-statement")
    public void paintComponent(Graphics g) {

        //Setup Background
        g.setColor(Color.green);
        g.fillRect(0,0,this.getWidth(),this.getHeight());
        
        //Draw the Horizontal Road
        //Calculate the Road Values
        int hRoadWidth = (Settings.getSimSettings().getHEastLanes()*Settings.LANE_WIDTH)+(Settings.getSimSettings().getHWestLanes()*Settings.LANE_WIDTH);
        int vRoadWidth = (Settings.getSimSettings().getVNorthLanes()*Settings.LANE_WIDTH)+(Settings.getSimSettings().getVSouthLanes()*Settings.LANE_WIDTH);

        int hRoadY, hRoadX;

        hRoadX = (this.getWidth()-Settings.getSimSettings().gethLaneLength())/2;

        if(Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
            hRoadY = (this.getHeight()/2)-(model.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
        } else {
            hRoadY = (this.getHeight()/2)-(model.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
        }

        int vRoadY, vRoadX;

        int xOffset = (this.getWidth()-Settings.getSimSettings().gethLaneLength())/2;

        if(Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
            vRoadX = xOffset+model.gethRoadIntersection().getIntersectionCenter()-(model.getvRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
        } else {
            vRoadX = xOffset+model.gethRoadIntersection().getIntersectionCenter()-(model.getvRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
        }

        vRoadY = ((this.getHeight()/2)-model.gethRoadIntersection().getIntersectionCenter());
        //Render Road
        g.setColor(Color.BLACK);
        g.fillRect( hRoadX, hRoadY, Settings.getSimSettings().gethLaneLength(), hRoadWidth);
        g.fillRect( vRoadX, vRoadY, vRoadWidth, Settings.getSimSettings().getvLaneLength());

        //Render Horizontal Road Cars
        renderCars(model.gethRoadIntersection().getRoad(), g, hRoadX, hRoadY, Settings.ROAD_EAST_WEST, Settings.getSimSettings().getTrafficFlow());
        //Render Vertical Road Cars
        renderCars(model.getvRoadIntersection().getRoad(), g, vRoadX, vRoadY, Settings.ROAD_SOUTH_NORTH, Settings.getSimSettings().getTrafficFlow());

        //Render Traffic Lights
        g.setColor(Color.BLACK);
        int lightPadding = 20;
        int lightWidth = 25;
        int lightHeight = 50;
        int lightCorner = 5;
        int lightDiameter = 10;

        //Traffic Light rendering is highly inefficient in this code. If anybody wants to rework it, please do!

        if(model.gethRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH).size() > 0) {
            double[] hLightOrderEast = {0.25, 0.5, 0.75};
            int hLightXEast = vRoadX+lightPadding+vRoadWidth;
            int hLightYEast = hRoadY-lightPadding-lightWidth;

            //Green Horizontal Light
            g.setColor(Color.BLACK);
            g.fillRoundRect(hLightXEast, hLightYEast, lightHeight, lightWidth, lightCorner, lightCorner);
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().GREEN_LIGHT) {
                g.setColor(Color.GREEN);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXEast+(hLightOrderEast[0]*lightHeight-.5*lightDiameter)), (int) (hLightYEast+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);

            // Yellow Horizontal Light
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().YELLOW_LIGHT) {
                g.setColor(Color.ORANGE);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXEast+(hLightOrderEast[1]*lightHeight-.5*lightDiameter)), (int) (hLightYEast+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);

            // Red Horizontal Light
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().RED_LIGHT) {
                g.setColor(Color.RED);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXEast+(hLightOrderEast[2]*lightHeight-.5*lightDiameter)), (int) (hLightYEast+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);
        }
            
        if(model.gethRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH).size() > 0) {
            double[] hLightOrderWest = {0.75, 0.5, 0.25};
            int hLightXWest = vRoadX-(lightHeight+lightPadding);
            int hLightYWest = hRoadY+hRoadWidth+lightPadding;

            //Green Horizontal Light
            g.setColor(Color.BLACK);
            g.fillRoundRect(hLightXWest, hLightYWest, lightHeight, lightWidth, lightCorner, lightCorner);
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().GREEN_LIGHT) {
                g.setColor(Color.GREEN);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXWest+(hLightOrderWest[0]*lightHeight-.5*lightDiameter)), (int) (hLightYWest+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);

            // Yellow Horizontal Light
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().YELLOW_LIGHT) {
                g.setColor(Color.ORANGE);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXWest+(hLightOrderWest[1]*lightHeight-.5*lightDiameter)), (int) (hLightYWest+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);

            // Red Horizontal Light
            if(model.gethRoadIntersection().getLightState() == model.gethRoadIntersection().RED_LIGHT) {
                g.setColor(Color.RED);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (hLightXWest+(hLightOrderWest[2]*lightHeight-.5*lightDiameter)), (int) (hLightYWest+(.5*lightWidth-.5*lightDiameter)) , lightDiameter, lightDiameter);
        }

        if(model.getvRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH).size() > 0) {
            double[] vLightOrderSouth = {0.25, 0.5, 0.75};
            int vLightXSouth = vRoadX+lightPadding+vRoadWidth;
            int vLightYSouth = hRoadY+lightPadding+hRoadWidth;

            //Green Vertical Light
            g.setColor(Color.BLACK);
            g.fillRoundRect(vLightXSouth, vLightYSouth, lightWidth, lightHeight, lightCorner, lightCorner);
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().GREEN_LIGHT) {
                g.setColor(Color.GREEN);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXSouth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYSouth+(vLightOrderSouth[0]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);

            // Yellow Vertical Light
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().YELLOW_LIGHT) {
                g.setColor(Color.ORANGE);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXSouth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYSouth+(vLightOrderSouth[1]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);

            // Red Vertical Light
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().RED_LIGHT) {
                g.setColor(Color.RED);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXSouth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYSouth+(vLightOrderSouth[2]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);

        }

        if(model.gethRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH).size() > 0) {
            double[] vLightOrderNorth = {0.75, 0.5, 0.25};
            int vLightXNorth = vRoadX-(lightWidth+lightPadding);
            int vLightYNorth = hRoadY-(lightHeight+lightPadding);

            //Green Vertical Light
            g.setColor(Color.BLACK);
            g.fillRoundRect(vLightXNorth, vLightYNorth, lightWidth, lightHeight, lightCorner, lightCorner);
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().GREEN_LIGHT) {
                g.setColor(Color.GREEN);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXNorth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYNorth+(vLightOrderNorth[0]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);

            // Yellow Vertical Light
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().YELLOW_LIGHT) {
                g.setColor(Color.ORANGE);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXNorth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYNorth+(vLightOrderNorth[1]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);

            // Red Vertical Light
            if(model.getvRoadIntersection().getLightState() == model.gethRoadIntersection().RED_LIGHT) {
                g.setColor(Color.RED);
            } else {
                g.setColor(Color.GRAY);
            }
            g.fillOval((int) (vLightXNorth+(.5*lightWidth-.5*lightDiameter)), (int) (vLightYNorth+(vLightOrderNorth[2]*lightHeight-.5*lightDiameter)), lightDiameter, lightDiameter);


        }

        g.setColor(Color.LIGHT_GRAY);
        //g.fillRect(0, 0, hRoadX, this.getHeight());
        //g.fillRect((hRoadX+Settings.getSimSettings().gethLaneLength()), 0, this.getWidth(), this.getHeight());
        //g.fillRect(0, 0, this.getWidth(), vRoadY);
        //g.fillRect(0, (vRoadY+Settings.getSimSettings().gethLaneLength()), this.getWidth(), this.getHeight());
    }

    private void renderCars(Road renderRoad, Graphics g, int roadX, int roadY, Boolean roadOrient, Boolean trafficFlowDirection) {
        g.setColor(Color.WHITE);
        int lNum = 0;
        int rectW, rectH, rectX, rectY, lanesXEastSouth, lanesYEastSouth, lanesXWestNorth, lanesYWestNorth;
        
        if(roadOrient == Settings.ROAD_EAST_WEST) {
            rectW = Settings.CAR_LENGTH;;
            rectH = Settings.CAR_WIDTH;
        } else {
            rectW = Settings.CAR_WIDTH;
            rectH = Settings.CAR_LENGTH;
        }        

        if(trafficFlowDirection == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
            if(roadOrient == Settings.ROAD_SOUTH_NORTH) {
                lanesXEastSouth = roadX+(renderRoad.getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
                lanesXWestNorth = roadX;
                lanesYEastSouth = roadY;
                lanesYWestNorth = roadY;
            } else {
                lanesXEastSouth = roadX;
                lanesXWestNorth = roadX;
                lanesYEastSouth = roadY;
                lanesYWestNorth = roadY+(renderRoad.getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
            }
        } else {
            if(roadOrient == Settings.ROAD_SOUTH_NORTH) {
                lanesXEastSouth = roadX;
                lanesXWestNorth = roadX;
                lanesYEastSouth = roadY;
                lanesYWestNorth = roadY+(renderRoad.getNoLanes(Settings.TRAFFIC_WEST_NORTH)*Settings.LANE_WIDTH);
            } else {
                lanesXEastSouth = roadX;
                lanesXWestNorth = roadX;
                lanesYEastSouth = roadY+(renderRoad.getNoLanes(Settings.TRAFFIC_EAST_SOUTH)*Settings.LANE_WIDTH);
                lanesYWestNorth = roadY;
            }
        }
        
        lanesYEastSouth = roadY;
        lanesYWestNorth = roadY;
        
        for(Lane l: renderRoad.getLanes(Settings.TRAFFIC_EAST_SOUTH)) {
            lNum++;
            for(Car c: l.getCars()) {
                if(roadOrient == Settings.ROAD_EAST_WEST) { 
                    rectX = lanesXEastSouth+c.getLanePosition();
                    rectY = lanesYEastSouth+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                } else {
                    rectX = lanesXEastSouth+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                    rectY = lanesYEastSouth+c.getLanePosition();
                }
                g.fillRect(rectX, rectY, rectW, rectH);
            }
        }
        
        for(Lane l: renderRoad.getLanes(Settings.TRAFFIC_WEST_NORTH)) {
            lNum++;
            for(Car c: l.getCars()) {
                if(roadOrient == Settings.ROAD_EAST_WEST) { 
                    rectX = lanesXWestNorth+c.getLanePosition();
                    rectY = lanesYWestNorth+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                } else {
                    rectX = lanesXWestNorth+((lNum-1)*Settings.LANE_WIDTH)+((Settings.LANE_WIDTH-Settings.CAR_WIDTH)/2);
                    rectY = lanesYWestNorth+c.getLanePosition();
                }
                g.fillRect(rectX, rectY, rectW, rectH);
            }
        }
    }
}