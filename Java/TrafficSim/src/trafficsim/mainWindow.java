package trafficsim;

import java.awt.BorderLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JCheckBoxMenuItem;
import javax.swing.JSeparator;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JOptionPane;


/**
 * JFrame Class which builds the main window of the Traffic Simulation Program *
 *
 * @author Tristan Davey
 */
public class mainWindow extends javax.swing.JFrame {



    //Main Menu Bar
    private JMenuBar mainMenuBar;
    private JMenu fileMenu;
    private JMenu settingsMenu;
    
    //File Menu
    private JMenuItem mySQLConfMenuItem;
    private JMenuItem openFileMenuItem;
    private JMenuItem saveFileMenuItem;
    private JMenuItem openMySQLMenuItem;
    private JMenuItem saveMySQLMenuItem;
    private JCheckBoxMenuItem simRunMenuItem;
    private JMenuItem exitMenuItem;

    //Settings Menu
    private JMenuItem setSouthLanesMenuItem;
    private JMenuItem setNorthLanesMenuItem;
    private JMenuItem setEastLanesMenuItem;
    private JMenuItem setWestLanesMenuItem;
    private JMenuItem setHProbMenuItem;
    private JMenuItem setVProbMenuItem;
    private JMenuItem setHSpeed;
    private JMenuItem setVSpeed;
    private JMenuItem setBreakdownProbability;

    private JCheckBoxMenuItem setLHD;
    private JCheckBoxMenuItem setRHD;

    //Drawing Area
    private SimulationGUI simGUI;

    //Lower Control Panel
    private JPanel controlPanel;
    private JButton runSimulationButton;
    private JButton stopSimulationButton;
    private JButton resetSimulationButton;

    public mainWindow(Intersection modelIntersection, SimulationEnvironment simulation, String title) {

        super(title);

        //Main Menubar Initialisation
        mainMenuBar = new JMenuBar();
        fileMenu = new JMenu();
        settingsMenu = new JMenu();

        //File Menu Initialisation
        simRunMenuItem = new JCheckBoxMenuItem();
        //mySQLConfMenuItem = new JMenuItem();
        openFileMenuItem = new JMenuItem();
        saveFileMenuItem = new JMenuItem();
        //openMySQLMenuItem = new JMenuItem();
        //saveMySQLMenuItem = new JMenuItem();
        exitMenuItem = new JMenuItem();
       
        //Settings Menu Initialisation
        setSouthLanesMenuItem =  new JMenuItem();
        setNorthLanesMenuItem =  new JMenuItem();
        setEastLanesMenuItem = new JMenuItem();
        setWestLanesMenuItem = new JMenuItem();
        setHProbMenuItem = new JMenuItem();
        setVProbMenuItem = new JMenuItem();
        setLHD = new JCheckBoxMenuItem();
        setRHD = new JCheckBoxMenuItem();
        setHSpeed = new JMenuItem();
        setVSpeed = new JMenuItem();
        setBreakdownProbability = new JMenuItem();

        //Set Window Settings
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        //Set Main MenuBar Text
        fileMenu.setText("File");
        settingsMenu.setText("Settings");

        //Set File Menu Text
        simRunMenuItem.setText("Run Simulation");
        //mySQLConfMenuItem.setText("Configure Database Connection");
        openFileMenuItem.setText("Open File");
        saveFileMenuItem.setText("Save File");
        //openMySQLMenuItem.setText("Open from Database");
        //saveMySQLMenuItem.setText("Save to Database");
        exitMenuItem.setText("Exit");

        //Set Settings Menu Text
        setSouthLanesMenuItem.setText("Southbound Lanes");
        setNorthLanesMenuItem.setText("Northbound Lanes");
        setEastLanesMenuItem.setText("Eastbound Lanes");
        setWestLanesMenuItem.setText("Westbound Lanes");
        setHProbMenuItem.setText("Horizontal Car Regularity");
        setVProbMenuItem.setText("Vertical Car Regularity");
        setLHD.setText("Left Hand Drive");
        setLHD.setSelected(true);
        setRHD.setText("Right Hand Drive");
        setHSpeed.setText("Horizontal Road Speed");
        setVSpeed.setText("Vertical Road Speed");
        setBreakdownProbability.setText("Breakdown Probability");


        //Construct File Menu
        fileMenu.add(new JSeparator());
        fileMenu.add(simRunMenuItem);
        fileMenu.add(new JSeparator());
        //fileMenu.add(mySQLConfMenuItem);
        //fileMenu.add(new JSeparator());
        fileMenu.add(openFileMenuItem);
        //fileMenu.add(openMySQLMenuItem);
        fileMenu.add(new JSeparator());
        fileMenu.add(saveFileMenuItem);
        //fileMenu.add(saveMySQLMenuItem);
        fileMenu.add(new JSeparator());
        fileMenu.add(exitMenuItem);

        //Construct Settings Menu
        settingsMenu.add(setLHD);
        settingsMenu.add(setRHD);
        settingsMenu.add(new JSeparator());
        settingsMenu.add(setNorthLanesMenuItem);
        settingsMenu.add(setSouthLanesMenuItem);
        settingsMenu.add(setWestLanesMenuItem);
        settingsMenu.add(setEastLanesMenuItem);
        settingsMenu.add(new JSeparator());
        settingsMenu.add(setHProbMenuItem);
        settingsMenu.add(setVProbMenuItem);
        settingsMenu.add(setBreakdownProbability);
        settingsMenu.add(new JSeparator());
        settingsMenu.add(setHSpeed);
        settingsMenu.add(setVSpeed);

        //Construct Menubar
        mainMenuBar.add(fileMenu);
        mainMenuBar.add(settingsMenu);
        setJMenuBar(mainMenuBar);

        simGUI = new SimulationGUI(modelIntersection);
        getContentPane().add(BorderLayout.CENTER, simGUI);

        // Construct Lower JPanel
        controlPanel = new JPanel();
        runSimulationButton = new JButton("Run");
        stopSimulationButton = new JButton("Pause");
        resetSimulationButton = new JButton("Reset");

        controlPanel.add(BorderLayout.CENTER, runSimulationButton);
        controlPanel.add(BorderLayout.CENTER, stopSimulationButton);
        controlPanel.add(BorderLayout.CENTER, resetSimulationButton);
        stopSimulationButton.setVisible(false);
        add(controlPanel, BorderLayout.SOUTH);

        //Add Event Handlers
        openFileMenuItem.addActionListener(new fileOpenListener(simulation, this));
        saveFileMenuItem.addActionListener(new fileSaveListener(simulation, this));
        //openMySQLMenuItem.addActionListener(new mysqlOpenListener(simulation));
        //saveMySQLMenuItem.addActionListener(new mysqlSaveListener(simulation));
        stopSimulationButton.addActionListener(new simulationToggle(this, simulation));
        runSimulationButton.addActionListener(new simulationToggle(this, simulation));
        resetSimulationButton.addActionListener(new simulationReset(this, simulation));
        simRunMenuItem.addActionListener(new simulationToggle(this, simulation));
        setLHD.addActionListener(new setTrafficFlowListener(Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC, setLHD, setRHD, simGUI));
        setRHD.addActionListener(new setTrafficFlowListener(Settings.TRAFFIC_FLOW_RIGHT_HAND_TRAFFIC, setLHD, setRHD, simGUI));
        setNorthLanesMenuItem.addActionListener(new settingsLanesListener(this, simulation, Settings.TRAFFIC_WEST_NORTH, Settings.ROAD_SOUTH_NORTH));
        setSouthLanesMenuItem.addActionListener(new settingsLanesListener(this, simulation, Settings.TRAFFIC_EAST_SOUTH, Settings.ROAD_SOUTH_NORTH));
        setEastLanesMenuItem.addActionListener(new settingsLanesListener(this, simulation, Settings.TRAFFIC_EAST_SOUTH, Settings.ROAD_EAST_WEST));
        setWestLanesMenuItem.addActionListener(new settingsLanesListener(this, simulation, Settings.TRAFFIC_WEST_NORTH, Settings.ROAD_EAST_WEST));
        setHProbMenuItem.addActionListener(new settingsHProbListener(simulation));
        setVProbMenuItem.addActionListener(new settingsVProbListener(simulation));
        setBreakdownProbability.addActionListener(new settingsBreakdownProbListener(simulation));
        setHSpeed.addActionListener(new settingsHSpeedListener(simulation));
        setVSpeed.addActionListener(new settingsVSpeedListener(simulation));

        //Pack the window
        pack();
    }
    
    public void toggleSimulation(Boolean condition) {
        simRunMenuItem.setSelected(condition);
        runSimulationButton.setVisible(!condition);
        stopSimulationButton.setVisible(condition);
        settingsMenu.setEnabled(!condition);
        openFileMenuItem.setEnabled(!condition);
        //openMySQLMenuItem.setEnabled(!condition);
        saveFileMenuItem.setEnabled(!condition);
        //saveMySQLMenuItem.setEnabled(!condition);
    }

    public void redrawSimulation() {
        this.simGUI.repaint();
    }

    public void refreshSimulationReference(Intersection modelIntersection) {
        //There seems to be a bug or issue where simGUI uses the old modelIntersection object reference unless we remove it from the window, recreate and readd.
        getContentPane().remove(this.simGUI);
        this.simGUI = new SimulationGUI(modelIntersection);
        getContentPane().add(BorderLayout.CENTER, simGUI);
        this.simGUI.repaint();
    }

    void exceptionWindow(String exceptionString) {
        JOptionPane.showMessageDialog(this, exceptionString);
    }

    void disableMySQL() {
        openMySQLMenuItem.setEnabled(false);
        saveMySQLMenuItem.setEnabled(false);
    }

}

/**
 * ActionListener implementing class to toggle the simulation from running to stopped or vica-versa.
 *
 * @author Tristan Davey
 */
 class simulationToggle implements ActionListener {

     mainWindow window;
     SimulationEnvironment simulation;

    simulationToggle(mainWindow window, SimulationEnvironment simulation) {
        this.window = window;
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {
        window.toggleSimulation(!Settings.getSimSettings().getSimulationRunning());
        if(Settings.getSimSettings().getSimulationRunning()) {
            try {
                simulation.stop();
            } catch (InterruptedException ex) {
                Logger.getLogger(simulationToggle.class.getName()).log(Level.SEVERE, null, ex);
            }
        } else {
            try {
                simulation.run();
            } catch (InterruptedException ex) {
                Logger.getLogger(simulationToggle.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}

 /* ActionListener implementing class to toggle the simulation from running to stopped or vica-versa.
 *
 * @author Tristan Davey
 */
 class simulationReset implements ActionListener {

     mainWindow window;
     SimulationEnvironment simulation;

    simulationReset(mainWindow window, SimulationEnvironment simulation) {
        this.window = window;
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {
        try {
            simulation.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(simulationReset.class.getName()).log(Level.SEVERE, null, ex);
        }
        window.toggleSimulation(false);
        window.redrawSimulation();
    }
}

 /**
 * ActionListener implementing class which handles the pressing of the "Set Horizontal Lanes" button.
 *
 * @author Tristan Davey
 */
 class settingsLanesListener implements ActionListener {

    mainWindow window;
    SimulationEnvironment simulation;
    Boolean trafficDirection;
    Boolean roadOrientation;

    settingsLanesListener(mainWindow window, SimulationEnvironment simulation, Boolean trafficDirection, Boolean roadOrientation) {
        this.window = window;
        this.simulation = simulation;
        this.trafficDirection = trafficDirection;
        this.roadOrientation = roadOrientation;

    }

    public void actionPerformed(ActionEvent e) {
        short value = 0;
        Boolean fieldComplete = false;
        String message = null;
        
        while(fieldComplete != true) {
            if(roadOrientation == Settings.ROAD_EAST_WEST) {
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    message = "Set Number of Eastbound Lanes ("+Settings.H_LANE_BOUNDS[0]+"-"+Settings.H_LANE_BOUNDS[1]+"):";
                } else {
                    message = "Set Number of Westbound Lanes ("+Settings.H_LANE_BOUNDS[0]+"-"+Settings.H_LANE_BOUNDS[1]+"):";
                }
                try {
                    value = Short.parseShort(JOptionPane.showInputDialog(null, message));
                } catch (NumberFormatException ex) {
                    //Nothing was entered or cancel was hit
                    fieldComplete = true;
                }
                if(fieldComplete == false) {
                    if((value < Settings.H_LANE_BOUNDS[0]) || (value > Settings.H_LANE_BOUNDS[1])) {
                        JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.H_LANE_BOUNDS[0]+" and "+Settings.H_LANE_BOUNDS[1]);
                    } else {
                       if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                           Settings.getSimSettings().setHEastLanes(value);
                       } else {
                           Settings.getSimSettings().setHWestLanes(value);
                       }
                       fieldComplete = true;
                    }
                }
            } else {
                if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                    message = "Set Number of Southbound Lanes ("+Settings.V_LANE_BOUNDS[0]+"-"+Settings.V_LANE_BOUNDS[1]+"):";
                } else {
                    message = "Set Number of Northbound Lanes ("+Settings.V_LANE_BOUNDS[0]+"-"+Settings.V_LANE_BOUNDS[1]+"):";
                }
                try {
                    value = Short.parseShort(JOptionPane.showInputDialog(null, message));
                } catch (NumberFormatException ex) {
                    //Nothing was entered or cancel was hit
                    fieldComplete = true;
                }
                if(fieldComplete == false) {
                    if((value < Settings.V_LANE_BOUNDS[0]) || (value > Settings.V_LANE_BOUNDS[1])) {
                        JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.V_LANE_BOUNDS[0]+" and "+Settings.V_LANE_BOUNDS[1]);
                    } else {
                       if(trafficDirection == Settings.TRAFFIC_EAST_SOUTH) {
                           Settings.getSimSettings().setVSouthLanes(value);
                       } else {
                           Settings.getSimSettings().setVNorthLanes(value);
                       }
                       fieldComplete = true;
                    }
                }  
            }
        }
        try {
            this.simulation.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsLanesListener.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }

}


 /**
 * ActionListener implementing class which handles the pressing of the "Horizontal Car Regularity" button.
 *
 * @author Tristan Davey
 */
 class settingsHProbListener implements ActionListener {

     SimulationEnvironment sim = null;

     settingsHProbListener(SimulationEnvironment sim) {
         this.sim = sim;
     }

    public void actionPerformed(ActionEvent e) {
        String label = "Horizontal Car Probability ("+Settings.H_CAR_PROBABILITY_BOUNDS[0]+"-"+Settings.H_CAR_PROBABILITY_BOUNDS[1]+"):";
        boolean fieldComplete = false;

        while(fieldComplete != true) {
            try {
                Double value = Double.parseDouble(JOptionPane.showInputDialog(null, label));
                if((value < Settings.H_CAR_PROBABILITY_BOUNDS[0]) || (value > Settings.H_CAR_PROBABILITY_BOUNDS[1])) {
                    JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.H_CAR_PROBABILITY_BOUNDS[0]+" and "+Settings.H_CAR_PROBABILITY_BOUNDS[1]);
                } else {
                    Settings.getSimSettings().setHCarProbability(value);
                    fieldComplete = true;
                }
            } catch (NumberFormatException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            } catch (NullPointerException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            }
        }
        try {
            this.sim.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsHProbListener.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Vertical Car Regularity" button.
 *
 * @author Tristan Davey
 */
 class settingsVProbListener implements ActionListener {

     SimulationEnvironment sim = null;

     settingsVProbListener(SimulationEnvironment sim) {
         this.sim = sim;
     }

    public void actionPerformed(ActionEvent e) {
        String label = "Vertical Car Probability ("+Settings.V_CAR_PROBABILITY_BOUNDS[0]+"-"+Settings.V_CAR_PROBABILITY_BOUNDS[1]+"):";
        boolean fieldComplete = false;

        while(fieldComplete != true) {
            try {
                Double value = Double.parseDouble(JOptionPane.showInputDialog(null, label));
                if((value < Settings.V_CAR_PROBABILITY_BOUNDS[0]) || (value > Settings.V_CAR_PROBABILITY_BOUNDS[1])) {
                    JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.V_CAR_PROBABILITY_BOUNDS[0]+" and "+Settings.V_CAR_PROBABILITY_BOUNDS[1]);
                } else {
                    Settings.getSimSettings().setVCarProbability(value);
                    fieldComplete = true;
                }
            } catch (NumberFormatException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            } catch (NullPointerException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            }
        }
        try {
            this.sim.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsVProbListener.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}

 /**
 * ActionListener implementing class which handles the pressing of the "Breakdown Probability" button.
 *
 * @author Tristan Davey
 */
 class settingsBreakdownProbListener implements ActionListener {

     SimulationEnvironment sim = null;

    settingsBreakdownProbListener(SimulationEnvironment sim) {
        this.sim = sim;;
    }

    public void actionPerformed(ActionEvent e) {
        String label = "Car Breakdown Probability ("+Settings.BREAKDOWN_PROBABILITY_BOUNDS[0]+"-"+Settings.BREAKDOWN_PROBABILITY_BOUNDS[1]+"):";
        boolean fieldComplete = false;

        while(fieldComplete != true) {
            try {
                Double value = Double.parseDouble(JOptionPane.showInputDialog(null, label));
                if((value < Settings.BREAKDOWN_PROBABILITY_BOUNDS[0]) || (value > Settings.BREAKDOWN_PROBABILITY_BOUNDS[1])) {
                    JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.BREAKDOWN_PROBABILITY_BOUNDS[0]+" and "+Settings.BREAKDOWN_PROBABILITY_BOUNDS[1]);
                } else {
                    Settings.getSimSettings().setBreakdownProbability(value);
                    fieldComplete = true;
                }
            } catch (NumberFormatException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            } catch (NullPointerException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            }
        }
        try {
            this.sim.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsBreakdownProbListener.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}


 /**
 * ActionListener implementing class which handles the pressing of the "Breakdown Probability" button.
 *
 * @author Tristan Davey
 */
 class settingsHSpeedListener implements ActionListener {
     
     SimulationEnvironment sim = null;

     settingsHSpeedListener (SimulationEnvironment sim) {
         this.sim = sim;;
     }

    public void actionPerformed(ActionEvent e) {
        String label = "Horizontal Lane Speed ("+Settings.ROAD_SPEED_BOUNDS[0]+"-"+Settings.ROAD_SPEED_BOUNDS[1]+"):";
        boolean fieldComplete = false;

        while(fieldComplete != true) {
            try {
                int value = Integer.parseInt(JOptionPane.showInputDialog(null, label));
                if((value < Settings.ROAD_SPEED_BOUNDS[0]) || (value > Settings.ROAD_SPEED_BOUNDS[1])) {
                    JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.ROAD_SPEED_BOUNDS[0]+" and "+Settings.ROAD_SPEED_BOUNDS[1]);
                } else {
                    Settings.getSimSettings().sethRoadSpeed(value);
                    fieldComplete = true;
                }
            } catch (NumberFormatException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            } catch (NullPointerException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            }
        }
        try {
            this.sim.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsHSpeedListener.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}

 /**
 * ActionListener implementing class which handles the pressing of the "Breakdown Probability" button.
 *
 * @author Tristan Davey
 */
 class settingsVSpeedListener implements ActionListener {

     SimulationEnvironment sim = null;

     settingsVSpeedListener (SimulationEnvironment sim) {
         this.sim = sim;
     }

    public void actionPerformed(ActionEvent e) {
        String label = "Vertical Lane Speed ("+Settings.ROAD_SPEED_BOUNDS[0]+"-"+Settings.ROAD_SPEED_BOUNDS[1]+"):";
        boolean fieldComplete = false;

        while(fieldComplete != true) {
            try {
                int value = Integer.parseInt(JOptionPane.showInputDialog(null, label));
                if((value < Settings.ROAD_SPEED_BOUNDS[0]) || (value > Settings.ROAD_SPEED_BOUNDS[1])) {
                    JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.ROAD_SPEED_BOUNDS[0]+" and "+Settings.ROAD_SPEED_BOUNDS[1]);
                } else {
                    Settings.getSimSettings().setvRoadSpeed(value);
                    fieldComplete = true;
                }
            } catch (NumberFormatException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            } catch (NullPointerException ex) {
                //Nothing was entered or cancel was hit
                fieldComplete = true;
            }
        }
        try {
            this.sim.reset();
        } catch (InterruptedException ex) {
            Logger.getLogger(settingsVSpeedListener.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}

  /**
 * ActionListener implementing class which handles the pressing of the "Save to Database" button.
 *
 * @author Tristan Davey
 */
 /*class mysqlSaveListener implements ActionListener {

    SimulationEnvironment simulation;

    mysqlSaveListener(SimulationEnvironment simulation) {
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {
        boolean fieldComplete = false;
        String saveNameValue = null;
        
        while(fieldComplete == false) {
            saveNameValue = JOptionPane.showInputDialog(null, "Enter the save name: ");
            if(saveNameValue == null) {
                JOptionPane.showMessageDialog(null, "Enter a valid save name.");
            } else {
                try {
                    this.simulation.mySQLoutput(saveNameValue);
                } catch (IOException ex) {
                    Logger.getLogger(mysqlSaveListener.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
        
    }

}*/

  /**
 * ActionListener implementing class which handles the pressing of the "Save to Database" button.
 *
 * @author Tristan Davey
 */
 /*class mysqlOpenListener implements ActionListener {

    SimulationEnvironment simulation;

    mysqlOpenListener(SimulationEnvironment simulation) {
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {

    }

}*/

  /**
 * ActionListener implementing class which handles the pressing of the "Open from Database" button.
 *
 * @author Tristan Davey
 */
 class setTrafficFlowListener implements ActionListener {

     Boolean trafficFlow;
     JCheckBoxMenuItem leftHandDriveMenuItem;
     JCheckBoxMenuItem rightHandDriveMenuItem;
     SimulationGUI simGUI;
     
     setTrafficFlowListener(Boolean trafficFlowValue, JCheckBoxMenuItem leftHandDriveMenuItem,  JCheckBoxMenuItem rightHandDriveMenuItem, SimulationGUI simGUI) {
         this.trafficFlow = trafficFlowValue;
         this.leftHandDriveMenuItem = leftHandDriveMenuItem;
         this.rightHandDriveMenuItem = rightHandDriveMenuItem;
         this.simGUI = simGUI;
     }

    public void actionPerformed(ActionEvent e) {
        Settings.getSimSettings().setTrafficFlow(this.trafficFlow);
        if(this.trafficFlow == Settings.TRAFFIC_FLOW_LEFT_HAND_TRAFFIC) {
            this.leftHandDriveMenuItem.setSelected(true);
            this.rightHandDriveMenuItem.setSelected(false);
        } else {
            this.leftHandDriveMenuItem.setSelected(false);
            this.rightHandDriveMenuItem.setSelected(true);
        }
        simGUI.repaint();
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Save File" button.
 *
 * @author Tristan Davey
 */
 class fileSaveListener implements ActionListener {

    final JFileChooser fc = new JFileChooser();
    private mainWindow window = null;
    private SimulationEnvironment simulation = null;

    fileSaveListener(SimulationEnvironment simulation, mainWindow window) {
        this.window = window;
        this.simulation = simulation;
        fc.addChoosableFileFilter(new simFilter());
    }

    public void actionPerformed(ActionEvent e) {
        int returnVal = fc.showSaveDialog(window);

        if (returnVal == JFileChooser.APPROVE_OPTION) {
            File file = fc.getSelectedFile();
            simulation.fileOutput(file);
        }

    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Open File" button.
 *
 * @author Tristan Davey
 */
 class fileOpenListener implements ActionListener {

    final JFileChooser fc = new JFileChooser();
    private mainWindow window = null;
    private SimulationEnvironment simulation = null;

    fileOpenListener(SimulationEnvironment simulation, mainWindow window) {
        this.window = window;
        this.simulation = simulation;
        fc.addChoosableFileFilter(new simFilter());
    }

    public void actionPerformed(ActionEvent e) {
        int returnVal = fc.showOpenDialog(window);

        if (returnVal == JFileChooser.APPROVE_OPTION) {
            File file = fc.getSelectedFile();
            simulation.fileInput(file);
        }
    }
}

class simFilter extends javax.swing.filechooser.FileFilter {

    public boolean accept(File file) {
        if (file.isDirectory()) {
            return true;
        } else {
            String filename = file.getName();
            return filename.endsWith(".sim");
        }
    }

    public String getDescription() {
        return "*.sim";
    }
}