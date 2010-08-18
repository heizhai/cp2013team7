package trafficsim;

import java.awt.BorderLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JCheckBoxMenuItem;
import javax.swing.JSeparator;
import javax.swing.JButton;
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
    private JMenuItem runMenuItem;
    private JMenuItem runMultiMenuItem;
    private JCheckBoxMenuItem simRunMenuItem;
    private JMenuItem exitMenuItem;

    //Settings Menu
    private JMenuItem setHLanesMenuItem;
    private JMenuItem setVLanesMenuItem;
    private JMenuItem setHProbMenuItem;
    private JMenuItem setVProbMenuItem;

    //Drawing Area
    private SimulationGUI simGUI;

    //Lower Control Panel
    private JPanel controlPanel;
    private JButton runSimulationButton;
    private JButton stopSimulationButton;
    private JButton lightsCycleButton;

    public mainWindow(Intersection modelIntersection, SimulationEnvironment simulation, String title) {

        super(title);

        //Main Menubar Initialisation
        mainMenuBar = new JMenuBar();
        fileMenu = new JMenu();
        settingsMenu = new JMenu();

        //File Menu Initialisation
        runMenuItem = new JMenuItem();
        runMultiMenuItem = new JMenuItem();
        simRunMenuItem = new JCheckBoxMenuItem();
        exitMenuItem = new JMenuItem();
       
        //Settings Menu Initialisation
        setHLanesMenuItem =  new JMenuItem();
        setVLanesMenuItem = new JMenuItem();
        setHProbMenuItem = new JMenuItem();
        setVProbMenuItem = new JMenuItem();

        //Set Window Settings
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        //Set Main MenuBar Text
        fileMenu.setText("File");
        settingsMenu.setText("Settings");

        //Set File Menu Text
        runMenuItem.setText("Run One Cycle");
        runMultiMenuItem.setText("Run Many Cycles");
        simRunMenuItem.setText("Run Simulation");
        exitMenuItem.setText("Exit");

        //Set Settings Menu Text
        setHLanesMenuItem.setText("No. Horizontal Lanes");
        setVLanesMenuItem.setText("No. Vertical Lanes");
        setHProbMenuItem.setText("Horizontal Car Regularity");
        setVProbMenuItem.setText("Vertical Car Regularity");

        //Construct File Menu
        fileMenu.add(runMenuItem);
        fileMenu.add(runMultiMenuItem);
        fileMenu.add(new JSeparator());
        fileMenu.add(simRunMenuItem);
        fileMenu.add(new JSeparator());
        fileMenu.add(exitMenuItem);

        //Construct Settings Menu
        settingsMenu.add(setHLanesMenuItem);
        settingsMenu.add(setVLanesMenuItem);
        settingsMenu.add(new JSeparator());
        settingsMenu.add(setHProbMenuItem);
        settingsMenu.add(setVProbMenuItem);

        //Construct Menubar
        mainMenuBar.add(fileMenu);
        mainMenuBar.add(settingsMenu);
        setJMenuBar(mainMenuBar);

        simGUI = new SimulationGUI(modelIntersection);
        getContentPane().add(BorderLayout.CENTER, simGUI);

        // Construct Lower JPanel
        controlPanel = new JPanel();
        runSimulationButton = new JButton("Run");
        stopSimulationButton = new JButton("Stop");
        lightsCycleButton = new JButton("Cycle Lights");

        controlPanel.add(BorderLayout.CENTER, runSimulationButton);
        controlPanel.add(BorderLayout.CENTER, stopSimulationButton);
        controlPanel.add(BorderLayout.EAST, lightsCycleButton);
        stopSimulationButton.setVisible(false);
        add(controlPanel, BorderLayout.SOUTH);

        //Add Event Handlers
        stopSimulationButton.addActionListener(new simulationToggle(this, simulation));
        runSimulationButton.addActionListener(new simulationToggle(this, simulation));
        simRunMenuItem.addActionListener(new simulationToggle(this, simulation));
        lightsCycleButton.addActionListener(new lightsCycleListener(this, simulation));
        runMenuItem.addActionListener(new lightsCycleListener(this, simulation));
        runMultiMenuItem.addActionListener(new multiLightsCycleListener(this, simulation));
        setHLanesMenuItem.addActionListener(new settingsHLanesListener(this));
        setVLanesMenuItem.addActionListener(new settingsVLanesListener(this));
        setHProbMenuItem.addActionListener(new settingsHProbListener());
        setVProbMenuItem.addActionListener(new settingsVProbListener());

        lightCycle(false);

        //Pack the window
        pack();
    }
    
    public void toggleSimulation(Boolean condition) {
        simRunMenuItem.setSelected(condition);
        runSimulationButton.setVisible(!condition);
        stopSimulationButton.setVisible(condition);
        settingsMenu.setEnabled(!condition);
    }

    public void lightCycle(Boolean setState) {
        lightsCycleButton.setEnabled(setState);
        runMenuItem.setEnabled(setState);
        runMultiMenuItem.setEnabled(setState);
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


 /**
 * ActionListener implementing class which handles button presses on the buttons to make the traffic lights change
 *
 * @author Tristan Davey
 */
 class lightsCycleListener implements ActionListener {

     mainWindow window;
     SimulationEnvironment simulation;

    public lightsCycleListener(mainWindow window, SimulationEnvironment simulation) {
        this.window = window;
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {
        simulation.lightCycle(false);
        window.lightCycle(false);
    }

}

 /**
 * ActionListener implementing class which handles button presses on the buttons to make the traffic lights change
 *
 * @author Tristan Davey
 */
 class multiLightsCycleListener implements ActionListener {

     mainWindow window;
     SimulationEnvironment simulation;

    public multiLightsCycleListener(mainWindow window, SimulationEnvironment simulation) {
        this.window = window;
        this.simulation = simulation;
    }

    public void actionPerformed(ActionEvent e) {
        String label = "Number of Cycles ("+Settings.LIGHT_CYCLE_BOUNDS[0]+"-"+Settings.LIGHT_CYCLE_BOUNDS[1]+"):";
        int value = Short.parseShort(JOptionPane.showInputDialog(null, label));
        if((value < Settings.LIGHT_CYCLE_BOUNDS[0]) && (value > Settings.LIGHT_CYCLE_BOUNDS[1])) {
            JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.H_LANE_BOUNDS[0]+" and "+Settings.H_LANE_BOUNDS[1]);
        } else {
            simulation.multiLightCycle(value);
            window.lightCycle(false);
        }
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Set Horizontal Lanes" button.
 *
 * @author Tristan Davey
 */
 class settingsHLanesListener implements ActionListener {

    mainWindow window;

    settingsHLanesListener(mainWindow window) {
        this.window = window;
    }

    public void actionPerformed(ActionEvent e) {
        String label = "Horizontal Lanes ("+Settings.H_LANE_BOUNDS[0]+"-"+Settings.H_LANE_BOUNDS[1]+"):";
        Short value = Short.parseShort(JOptionPane.showInputDialog(null, label));
        if((value < Settings.H_LANE_BOUNDS[0]) && (value > Settings.H_LANE_BOUNDS[1])) {
            JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.H_LANE_BOUNDS[0]+" and "+Settings.H_LANE_BOUNDS[1]);
        }
        Settings.getSimSettings().setHLanes(value);
        window.repaint();
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Set Vertical Lanes" button.
 *
 * @author Tristan Davey
 */
 class settingsVLanesListener implements ActionListener {

    mainWindow window;

    settingsVLanesListener(mainWindow window) {
        this.window = window;
    }

    public void actionPerformed(ActionEvent e) {
        String label = "Vertical Lanes ("+Settings.V_LANE_BOUNDS[0]+"-"+Settings.V_LANE_BOUNDS[1]+"):";
        Short value = Short.parseShort(JOptionPane.showInputDialog(null, label));
        if((value < Settings.V_LANE_BOUNDS[0]) && (value > Settings.V_LANE_BOUNDS[1])) {
            JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.V_LANE_BOUNDS[0]+" and "+Settings.V_LANE_BOUNDS[1]);
        }
        Settings.getSimSettings().setVLanes(value);
        window.repaint();
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Horizontal Car " button.
 *
 * @author Tristan Davey
 */
 class settingsHProbListener implements ActionListener {

    public void actionPerformed(ActionEvent e) {
        String label = "Horizontal Car Probability ("+Settings.H_CAR_PROBABILITY_BOUNDS[0]+"-"+Settings.H_CAR_PROBABILITY_BOUNDS[1]+"):";
        Double value = Double.parseDouble(JOptionPane.showInputDialog(null, label));
        if((value < Settings.H_CAR_PROBABILITY_BOUNDS[0]) && (value > Settings.H_CAR_PROBABILITY_BOUNDS[1])) {
            JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.H_CAR_PROBABILITY_BOUNDS[0]+" and "+Settings.H_CAR_PROBABILITY_BOUNDS[1]);
        } else {
            Settings.getSimSettings().setHCarProbability(value);
        }
    }

}

 /**
 * ActionListener implementing class which handles the pressing of the "Vertical Car Regularity" button.
 *
 * @author Tristan Davey
 */
 class settingsVProbListener implements ActionListener {

    public void actionPerformed(ActionEvent e) {
        String label = "Vertical Car Probability ("+Settings.V_CAR_PROBABILITY_BOUNDS[0]+"-"+Settings.V_CAR_PROBABILITY_BOUNDS[1]+"):";
        Double value = Double.parseDouble(JOptionPane.showInputDialog(null, label));
        if((value < Settings.V_CAR_PROBABILITY_BOUNDS[0]) && (value > Settings.V_CAR_PROBABILITY_BOUNDS[1])) {
            JOptionPane.showMessageDialog(null, "Enter a value between "+Settings.V_CAR_PROBABILITY_BOUNDS[0]+" and "+Settings.V_CAR_PROBABILITY_BOUNDS[1]);
        } else {
            Settings.getSimSettings().setVCarProbability(value);
        }
    }

}
