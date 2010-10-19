﻿using System;
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
        public SimulationEnvironment simulation;
        private Intersection model;

        private MainMenu mainMenu;
        private MenuItem myMenuItemFile;
        private MenuItem myMenuItemSettings;

        //File Menu
        private MenuItem simRunMenuItem;
        private MenuItem exitMenuItem;

        //Settings Menu
        private MenuItem setHLanesMenuItem;
        private MenuItem setVLanesMenuItem;
        private MenuItem setHProbMenuItem;
        private MenuItem setVProbMenuItem;

        //Lower Control Panel
        private Button runSimulationButton;
        private Button stopSimulationButton;

        public mainWindow(Intersection modelIntersection, SimulationEnvironment simulation)
        {
            InitializeComponent();
            this.simulation = simulation;
            this.model = modelIntersection;

            //Main Menus 
            mainMenu = new MainMenu();
            this.Menu = mainMenu;

            //File Menu
            myMenuItemFile = new MenuItem("&File");
            simRunMenuItem = new MenuItem("&Run/Stop Simulation");
            exitMenuItem = new MenuItem("&Exit");
            mainMenu.MenuItems.Add(myMenuItemFile);
            myMenuItemFile.MenuItems.Add(simRunMenuItem);
            myMenuItemFile.MenuItems.Add(exitMenuItem);

            //Settings Menu
            myMenuItemSettings = new MenuItem("&Settings");
            setHLanesMenuItem = new MenuItem("&Set No. Horizontal Lanes");
            setVLanesMenuItem = new MenuItem("&Set No. Vertical Lanes");
            setHProbMenuItem = new MenuItem("&Set Horizontal Lane Car Regularity");
            setVProbMenuItem = new MenuItem("&Set Vertical Lane Car Regularity");
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

            runSimulationButton.Location = new Point(250, 530);
            stopSimulationButton.Location = new Point(250, 530);
            stopSimulationButton.Hide();

            Controls.Add(runSimulationButton);
            Controls.Add(stopSimulationButton);

            //Event Handlers
            exitMenuItem.Click += new System.EventHandler(OnClickExit);
            setHLanesMenuItem.Click += new System.EventHandler(OnClickHLanes);
            setVLanesMenuItem.Click += new System.EventHandler(OnClickVLanes);
            simRunMenuItem.Click += new System.EventHandler(simulationToggle);
            runSimulationButton.Click += new System.EventHandler(simulationToggle);
            stopSimulationButton.Click += new System.EventHandler(simulationToggle);
            setHProbMenuItem.Click += new EventHandler(settingsHProbListener);
            setVProbMenuItem.Click += new EventHandler(settingsVProbListener);
        }

        private void OnClickExit(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void settingsHProbListener(object sender, System.EventArgs e)
        {
            setHReg subForm = new setHReg(this);
            subForm.Show();
        }

        private void settingsVProbListener(object sender, System.EventArgs e)
        {
            setVReg subForm = new setVReg(this);
            subForm.Show();
        }

        private void OnClickHLanes(object sender, System.EventArgs e)
        {
            setHLanes subForm = new setHLanes(this);
            subForm.Show();
            this.Refresh();
        }

        private void OnClickVLanes(object sender, System.EventArgs e)
        {
            setVLanes subForm = new setVLanes(this);
            subForm.Show();
            this.Refresh();
        }

        private void simulationToggle(object sender, System.EventArgs e)
        {
            if (Settings.getSimSettings().getSimulationRunning())
            {
                myMenuItemSettings.Visible = true;
                simulation.stop();
                stopSimulationButton.Hide();
                runSimulationButton.Show();
                Settings.automate = false;

            }
            else
            {
                myMenuItemSettings.Visible = false;
                simulation.run();
                stopSimulationButton.Show();
                runSimulationButton.Hide();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsObj;
            Font f = new Font("Arial", 10, FontStyle.Regular);

            graphicsObj = this.CreateGraphics();
            Pen myPen = new Pen(Color.Black, 1);
            SolidBrush myBrush = new SolidBrush(Color.Black);

            //Draw Road
            //First finding road values
            int hRoadWidth = (Settings.getSimSettings().getHEastLanes() * Settings.LANE_WIDTH) + (Settings.getSimSettings().getHWestLanes() * Settings.LANE_WIDTH);
            int vRoadWidth = (Settings.getSimSettings().getVNorthLanes() * Settings.LANE_WIDTH) + (Settings.getSimSettings().getVSouthLanes() * Settings.LANE_WIDTH);

            int hRoadY, hRoadX;

            hRoadX = (this.Width - Settings.getSimSettings().gethLaneLength()) / 2;

            if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
            {
                hRoadY = (this.Height / 2) - (model.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
            }
            else
            {
                hRoadY = (this.Height / 2) - (model.gethRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
            }

            int vRoadY, vRoadX;

            int xOffset = (this.Width - Settings.getSimSettings().gethLaneLength()) / 2;

            if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
            {
                vRoadX = xOffset + model.gethRoadIntersection().getIntersectionCenter() - (model.getvRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
            }
            else
            {
                vRoadX = xOffset + model.gethRoadIntersection().getIntersectionCenter() - (model.getvRoadIntersection().getRoad().getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
            }

            vRoadY = ((this.Height / 2) - model.gethRoadIntersection().getIntersectionCenter());

            //Render Road
            graphicsObj.FillRectangle(myBrush, hRoadX, hRoadY, Settings.getSimSettings().gethLaneLength(), hRoadWidth);
            graphicsObj.FillRectangle(myBrush, vRoadX, vRoadY, vRoadWidth, Settings.getSimSettings().getvLaneLength());

            //Render Horizontal Road Cars
            renderCars(model.gethRoadIntersection().getRoad(), graphicsObj, hRoadX, hRoadY, Settings.ROAD_EAST_WEST, Settings.getSimSettings().getTrafficFlow());
            //Render Vertical Road Cars
            renderCars(model.getvRoadIntersection().getRoad(), graphicsObj, vRoadX, vRoadY, Settings.ROAD_SOUTH_NORTH, Settings.getSimSettings().getTrafficFlow());

            //Render Traffic Lights
            int lightPadding = 20;
            int lightWidth = 50;
            int lightHeight = 60;
            int lightDiameter = 10;

            if (model.gethRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH).Count() > 0)
            {
                double[] hLightOrderEast = { 0.25, 0.5, 0.75 };
                int hLightXEast = vRoadX + lightPadding + vRoadWidth;
                int hLightYEast = hRoadY - lightPadding - lightWidth;

                //Green Horizontal Light=
                myBrush.Color = Color.Black;
                graphicsObj.FillRectangle(myBrush, hLightXEast, hLightYEast, lightHeight, lightWidth);
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
        
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.7 * lightWidth - .5 * lightDiameter) - lightPadding), lightDiameter, lightDiameter);
                }

                //Green Horizontal Turning Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                   // graphicsObj.DrawString("i", 
                    graphicsObj.DrawString("↓", f, myBrush, (int) (hLightXEast+(hLightOrderEast[0]*lightHeight-.5*lightDiameter)), (int) (hLightYEast+(0.5*lightWidth)));
                }
                else
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int) (hLightXEast+(hLightOrderEast[0]*lightHeight-.5*lightDiameter)), (int) (hLightYEast+(.25*lightWidth-.5*lightDiameter)));
                }

                // Yellow Horizontal Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.7 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

               //Yellow Turning Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("↓", f, myBrush, (int)(hLightXEast + (hLightOrderEast[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.5 * lightWidth)));
                }
                else
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int)(hLightXEast + (hLightOrderEast[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.25 * lightWidth - .5 * lightDiameter)));
                }

                // Red Horizontal Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_RED_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXEast + (hLightOrderEast[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.7 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Red Horizontal Turning Light
                if (model.gethRoadIntersection().getLightState() != RoadIntersection.TURNING_GREEN_LIGHT && model.gethRoadIntersection().getLightState() != RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("↓", f, myBrush, (int)(hLightXEast + (hLightOrderEast[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.5 * lightWidth)));
                }
                else
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int)(hLightXEast + (hLightOrderEast[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYEast + (.25 * lightWidth - .5 * lightDiameter)));
                }
            }

            if (model.gethRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH).Count() > 0)
            {
                double[] hLightOrderWest = { 0.75, 0.5, 0.25 };
                int hLightXWest = vRoadX - (lightHeight + lightPadding);
                int hLightYWest = hRoadY + hRoadWidth + lightPadding;

                //Green Horizontal Light
                myBrush.Color = Color.Black;
                graphicsObj.FillRectangle(myBrush, hLightXWest, hLightYWest, lightHeight, lightWidth);
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.7 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Green Horizontal Turning Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int)(hLightXWest + (hLightOrderWest[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.2 * lightWidth - .5 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("↓", f, myBrush, (int)(hLightXWest + (hLightOrderWest[0] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.5 * lightWidth - .5 * lightDiameter)));
                }

                // Yellow Horizontal Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.7 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Yellow Horizontal Turning Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int)(hLightXWest + (hLightOrderWest[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.2 * lightWidth - .5 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("↓", f, myBrush, (int)(hLightXWest + (hLightOrderWest[1] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.5 * lightWidth - .5 * lightDiameter)));
                }

                // Red Horizontal Light
                if (model.gethRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT
                    || model.gethRoadIntersection().getLightState() == RoadIntersection.TURNING_RED_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.7 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(hLightXWest + (hLightOrderWest[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.3 * lightWidth - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Red Horizontal Turning Light
                if (model.gethRoadIntersection().getLightState() != RoadIntersection.TURNING_GREEN_LIGHT && model.gethRoadIntersection().getLightState() != RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("↑", f, myBrush, (int)(hLightXWest + (hLightOrderWest[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.2 * lightWidth - .5 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("↓", f, myBrush, (int)(hLightXWest + (hLightOrderWest[2] * lightHeight - .5 * lightDiameter)), (int)(hLightYWest + (.5 * lightWidth - .5 * lightDiameter)));
                }
            }

            if (model.getvRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_EAST_SOUTH).Count() > 0)
            {
                double[] vLightOrderSouth = { 0.25, 0.5, 0.75 };
                int vLightXSouth = vRoadX + lightPadding + vRoadWidth;
                int vLightYSouth = hRoadY + lightPadding + hRoadWidth;

                //Green Vertical Light
                myBrush.Color = Color.Black;
                graphicsObj.FillRectangle(myBrush, vLightXSouth, vLightYSouth, lightWidth, lightHeight);
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[0] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[0] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Green Veritcal Turning Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("←", f, myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[0] * lightHeight + -0.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[0] * lightHeight + .4 * lightDiameter)));
                }

                // Yellow Vertical Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[1] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[1] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Yellow Vertical Turning Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("←", f, myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[1] * lightHeight + -.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[1] * lightHeight + .4 * lightDiameter)));
                }

                // Red Vertical Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_RED_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[2] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[2] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Red Vertical Turning Light
                if (model.getvRoadIntersection().getLightState() != RoadIntersection.TURNING_GREEN_LIGHT && model.getvRoadIntersection().getLightState() != RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("←", f, myBrush, (int)(vLightXSouth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[2] * lightHeight + -.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXSouth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYSouth + (vLightOrderSouth[2] * lightHeight + .4 * lightDiameter)));
                }
            }

            if (model.getvRoadIntersection().getRoad().getLanes(Settings.TRAFFIC_WEST_NORTH).Count() > 0)
            {
                double[] vLightOrderNorth = { 0.75, 0.5, 0.25 };
                int vLightXNorth = vRoadX - (lightWidth + lightPadding);
                int vLightYNorth = hRoadY - (lightHeight + lightPadding);

                //Green Vertical Light
                myBrush.Color = Color.Black;
                graphicsObj.FillRectangle(myBrush, vLightXNorth, vLightYNorth, lightWidth, lightHeight);
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[0] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[0] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Green Vertical Turning Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT)
                {
                    myBrush.Color = Color.Green;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[0] * lightHeight + -0.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("←", f, myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[0] * lightHeight + .5 * lightDiameter)));
                }

                // Yellow Vertical Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[1] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[1] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Yellow Vertical Turning Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Orange;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[1] * lightHeight + -.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("←", f, myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[1] * lightHeight + .5 * lightDiameter)));
                }

                // Red Vertical Light
                if (model.getvRoadIntersection().getLightState() == RoadIntersection.RED_LIGHT 
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_GREEN_LIGHT
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_YELLOW_LIGHT
                    || model.getvRoadIntersection().getLightState() == RoadIntersection.TURNING_RED_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }

                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[2] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }
                else
                {
                    graphicsObj.FillEllipse(myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[2] * lightHeight - .5 * lightDiameter)), lightDiameter, lightDiameter);
                }

                //Red Vertical Turning Light
                if (model.getvRoadIntersection().getLightState() != RoadIntersection.TURNING_GREEN_LIGHT && model.getvRoadIntersection().getLightState() != RoadIntersection.TURNING_YELLOW_LIGHT)
                {
                    myBrush.Color = Color.Red;
                }
                else
                {
                    myBrush.Color = Color.Gray;
                }
                if (Settings.getSimSettings().getTrafficFlow() == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
                {
                    graphicsObj.DrawString("→", f, myBrush, (int)(vLightXNorth + (.7 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[2] * lightHeight + -.9 * lightDiameter)));
                }
                else
                {
                    graphicsObj.DrawString("←",f , myBrush, (int)(vLightXNorth + (.3 * lightWidth - .5 * lightDiameter)), (int)(vLightYNorth + (vLightOrderNorth[2] * lightHeight + .5 * lightDiameter)));
                }
            }
        }

        private void renderCars(Road renderRoad, Graphics g, int roadX, int roadY, Boolean roadOrient, Boolean trafficFlowDirection)
        {
            SolidBrush myBrush2 = new SolidBrush(Color.White);
            myBrush2.Color = Color.White;
            int lNum = 0;
            int rectW, rectH, rectX, rectY, lanesXEastSouth, lanesYEastSouth, lanesXWestNorth, lanesYWestNorth;

            if (roadOrient == Settings.ROAD_EAST_WEST)
            {
                rectW = Settings.CAR_LENGTH; ;
                rectH = Settings.CAR_WIDTH;
            }
            else
            {
                rectW = Settings.CAR_WIDTH;
                rectH = Settings.CAR_LENGTH;
            }

            if (trafficFlowDirection == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC)
            {
                if (roadOrient == Settings.ROAD_SOUTH_NORTH)
                {
                    lanesXEastSouth = roadX + (renderRoad.getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
                    lanesXWestNorth = roadX;
                    lanesYEastSouth = roadY;
                    lanesYWestNorth = roadY;
                }
                else
                {
                    lanesXEastSouth = roadX;
                    lanesXWestNorth = roadX;
                    lanesYEastSouth = roadY;
                    lanesYWestNorth = roadY + (renderRoad.getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
                }
            }
            else
            {
                if (roadOrient == Settings.ROAD_SOUTH_NORTH)
                {
                    lanesXEastSouth = roadX;
                    lanesXWestNorth = roadX + (renderRoad.getNoLanes(Settings.TRAFFIC_EAST_SOUTH) * Settings.LANE_WIDTH);
                    lanesYEastSouth = roadY;
                    lanesYWestNorth = roadY;
                }
                else
                {
                    lanesXEastSouth = roadX;
                    lanesXWestNorth = roadX;
                    lanesYEastSouth = roadY + (renderRoad.getNoLanes(Settings.TRAFFIC_WEST_NORTH) * Settings.LANE_WIDTH);
                    lanesYWestNorth = roadY;
                }
            }

            foreach (Lane l in renderRoad.getLanes(Settings.TRAFFIC_EAST_SOUTH))
            {
                lNum++;
                foreach (Car c in l.getCars())
                {
                    if (roadOrient == Settings.ROAD_EAST_WEST)
                    {
                        rectX = lanesXEastSouth + c.getLanePosition();
                        rectY = lanesYEastSouth + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                    }
                    else
                    {
                        rectX = lanesXEastSouth + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                        rectY = lanesYEastSouth + c.getLanePosition();
                    }
                    g.FillRectangle(myBrush2, rectX, rectY, rectW, rectH);
                }
            }

            lNum = 0;

            foreach (Lane l in renderRoad.getLanes(Settings.TRAFFIC_WEST_NORTH))
            {
                lNum++;
                foreach (Car c in l.getCars())
                {
                    if (roadOrient == Settings.ROAD_EAST_WEST)
                    {
                        rectX = lanesXWestNorth + c.getLanePosition();
                        rectY = lanesYWestNorth + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                    }
                    else
                    {
                        rectX = lanesXWestNorth + ((lNum - 1) * Settings.LANE_WIDTH) + ((Settings.LANE_WIDTH - Settings.CAR_WIDTH) / 2);
                        rectY = lanesYWestNorth + c.getLanePosition();
                    }
                    g.FillRectangle(myBrush2, rectX, rectY, rectW, rectH);
                }
            }
        }
    }
}
