using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrafficLightSim2
{
    public partial class mainWindow : Form
    {

        private MainMenu mainMenu;
        private MenuItem myMenuItemFile;
        private MenuItem myMenuItemSettings;

        //File Menu
        private MenuItem simRunMenuItem;
        private MenuItem runMenuItem;
        private MenuItem runMultiMenuItem;
        private MenuItem exitMenuItem;

        //Settings Menu
        private MenuItem setHLanesMenuItem;
        private MenuItem setVLanesMenuItem;
        private MenuItem setHProbMenuItem;
        private MenuItem setVProbMenuItem;

        //Lower Control Panel
        private Panel controlPanel;
        private Button runSimulationButton;
        private Button stopSimulationButton;
        private Button lightsCycleButton;

        public mainWindow(Intersection modelIntersection, SimulationEnvironment simulation)
        {
            InitializeComponent();

             //Main Menus 
            mainMenu = new MainMenu();
            this.Menu = mainMenu;

            //File Menu
            myMenuItemFile = new MenuItem("&File");
            simRunMenuItem = new MenuItem("&Run Simulation");
            runMenuItem = new MenuItem("&Run One Cycle");
            runMultiMenuItem = new MenuItem("&Run Multiple Cycles");
            exitMenuItem = new MenuItem("&Exit");
            mainMenu.MenuItems.Add(myMenuItemFile);
            myMenuItemFile.MenuItems.Add(simRunMenuItem);
            myMenuItemFile.MenuItems.Add(runMenuItem);
            myMenuItemFile.MenuItems.Add(runMultiMenuItem);
            myMenuItemFile.MenuItems.Add(exitMenuItem);

            //Settings Menu
            myMenuItemSettings = new MenuItem("&Settings");
            setHLanesMenuItem = new MenuItem("&Set No. Horizontal Lanes");
            setVLanesMenuItem = new MenuItem("&Set No. Vertical Lanes");
            setHProbMenuItem = new MenuItem("&Set Horizontal Lane Car Regularity");
            setVProbMenuItem = new MenuItem("&Set Horizontal Lane Car Regularity");
            mainMenu.MenuItems.Add(myMenuItemSettings);
            myMenuItemSettings.MenuItems.Add(setHLanesMenuItem);
            myMenuItemSettings.MenuItems.Add(setVLanesMenuItem);
            myMenuItemSettings.MenuItems.Add(setHProbMenuItem);
            myMenuItemSettings.MenuItems.Add(setVProbMenuItem);

            //Control Buttons
            runSimulationButton = new Button();
            runSimulationButton.Text = "Run";
            runSimulationButton.BackColor = Color.White;
            stopSimulationButton = new Button();
            stopSimulationButton.Text = "Stop";
            stopSimulationButton.BackColor = Color.White;
            lightsCycleButton = new Button();
            lightsCycleButton.Text = "Cycle Lights";
            lightsCycleButton.BackColor = Color.White;

            runSimulationButton.Location = new Point(250, 630);
            stopSimulationButton.Location = new Point(250, 630);
            stopSimulationButton.Hide();
            lightsCycleButton.Location = new Point(400, 630);

            Controls.Add(runSimulationButton);
            Controls.Add(stopSimulationButton);
            Controls.Add(lightsCycleButton);       
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Intersection model = new Intersection();
            Graphics graphicsObj;

            graphicsObj = this.CreateGraphics();
            Pen myPen = new Pen(Color.Black, 1);
            SolidBrush myBrush = new SolidBrush(Color.Black);

            //Draw Road
            //First finding road values
            int hRoadWidth = Settings.getSimSettings().getHLanes() * Settings.LANE_WIDTH;
            int vRoadWidth = Settings.getSimSettings().getVLanes() * Settings.LANE_WIDTH;

            int hRoadY = (700 / 2) - (hRoadWidth / 2);
            int hRoadX = (700 - Settings.getSimSettings().gethLaneLength()) / 2;

            int vRoadY, vRoadX;

            int xOffset = (700 - Settings.getSimSettings().gethLaneLength()) / 2;
            int yOffset = (700 - Settings.getSimSettings().getvLaneLength()) / 2;

            if (model.gethRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
            {
                vRoadX = xOffset + model.gethRoadIntersection().getIntersectionStopLine();
            }
            else
            {
                vRoadX = xOffset + (model.gethRoadIntersection().getIntersectionStopLine() - vRoadWidth);
            }

            if (model.getvRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
            {
                vRoadY = hRoadY - model.getvRoadIntersection().getIntersectionStopLine();
            }
            else
            {
                vRoadY = hRoadY - (model.getvRoadIntersection().getIntersectionStopLine() + hRoadWidth);
            }

            //Render Road
            graphicsObj.FillRectangle(myBrush, hRoadX, hRoadY, Settings.getSimSettings().gethLaneLength(), hRoadWidth);
            graphicsObj.FillRectangle(myBrush, vRoadX, vRoadY, vRoadWidth, Settings.getSimSettings().getvLaneLength());

            //Render Horizontal Road Cars
            renderCars(model.gethRoadIntersection().getRoad(), graphicsObj, hRoadX, hRoadY, Settings.ROAD_EAST_WEST);
            //Render Vertical Road Cars
            renderCars(model.getvRoadIntersection().getRoad(), graphicsObj, vRoadX, vRoadY, Settings.ROAD_SOUTH_NORTH);

            //Render Traffic Lights
            int lightPadding = 20;
            int lightWidth = 25;
            int lightHeight = 50;
            int lightDiameter = 10;
            int hLightX, hLightY, vLightX, vLightY;
            double[] hLightOrder = { 0.75, 0.5, 0.25 }, vLightOrder = { 0.75, 0.5, 0.25 };

            if (model.gethRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
            {
                hLightX = vRoadX + lightPadding + vRoadWidth;
                hLightY = hRoadY - lightPadding - lightWidth;
                hLightOrder[0] = 0.25;
                hLightOrder[2] = 0.75;
            }

            else
            {
                hLightX = vRoadX - (lightHeight + lightPadding);
                hLightY = hRoadY + hRoadWidth + lightPadding;
                hLightOrder[0] = 0.75;
                hLightOrder[2] = 0.25;
            }

            //Green Horizontal Level
            graphicsObj.FillRectangle(myBrush, hLightX, hLightY, lightHeight, lightWidth);

            if (model.gethRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
            {
                myBrush.Color = Color.Green;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(hLightX + (hLightOrder[0] * lightHeight - .5 * lightDiameter)), (int)(hLightY + (.5 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);

            // Yellow Horizontal Light
            if (model.gethRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
            {
                myBrush.Color = Color.Orange;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(hLightX + (hLightOrder[1] * lightHeight - .5 * lightDiameter)), (int)(hLightY + (.5 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);


            // Red Horizontal Light
            if (model.gethRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT)
            {
                myBrush.Color = Color.Red;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(hLightX + (hLightOrder[2] * lightHeight - .5 * lightDiameter)), (int)(hLightY + (.5 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);


            if (model.gethRoadIntersection().getRoad().getTrafficDirection() == Settings.TRAFFIC_EAST_SOUTH)
            {
                vLightX = vRoadX + lightPadding + vRoadWidth;
                vLightY = hRoadY + lightPadding + hRoadWidth;
                vLightOrder[0] = 0.25;
                vLightOrder[2] = 0.75;
            }

            else
            {
                vLightX = vRoadX - (lightWidth + lightPadding);
                vLightY = hRoadY - (lightHeight + lightPadding);
                vLightOrder[0] = 0.75;
                vLightOrder[2] = 0.25;
            }

            //Green Vertical Level
            myBrush.Color = Color.Black;

            graphicsObj.FillRectangle(myBrush, vLightX, vLightY, lightWidth, lightHeight);
            if (model.getvRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
            {
                myBrush.Color = Color.Green;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(vLightX + (.5 * lightWidth - .5 * lightDiameter)), (int)(vLightY + (vLightOrder[0] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);

            // Yellow Vertical Light
            if (model.getvRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
            {
                myBrush.Color = Color.Orange;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(vLightX + (.5 * lightWidth - .5 * lightDiameter)), (int)(vLightY + (vLightOrder[1] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);

            // Red Vertical Light
            if (model.getvRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT)
            {
                myBrush.Color = Color.Red;
            }

            else
            {
                myBrush.Color = Color.Gray;
            }
            graphicsObj.FillEllipse(myBrush, (int)(vLightX + (.5 * lightWidth - .5 * lightDiameter)), (int)(vLightY + (vLightOrder[2] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
        }

        private void renderCars(Road renderRoad, Graphics g, int roadX, int roadY, bool roadOrient)
        {
            SolidBrush myBrush = new SolidBrush(Color.White);

            int lNum = 0;
            int rectW, rectH, rectX, rectY;
            if (roadOrient == Settings.ROAD_EAST_WEST)
            {
                rectW = Settings.CAR_LENGTH;
                rectH = Settings.CAR_WIDTH;
            }

            else
            {
                rectW = Settings.CAR_WIDTH;
                rectH = Settings.CAR_LENGTH;
            }
            foreach (Lane l in renderRoad.getLanes())
            {
                lNum++;
                foreach (Car c in l.getCars())
                {
                    if (roadOrient == Settings.ROAD_EAST_WEST)
                    {
                        rectX = roadX + c.getLanePosition();
                        rectY = roadY + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                    }

                    else
                    {
                        rectX = roadX + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                        rectY = roadY + c.getLanePosition();
                    }

                    g.FillRectangle(myBrush, rectX, rectY, rectW, rectH);

                }
            }
        }
    }
}
