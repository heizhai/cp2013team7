package trafficsim;

import java.awt.BorderLayout;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;

/**
 *
 * @author Tristan Davey
 */
public class mainWindow extends javax.swing.JFrame {



    //Main Menu Bar
    private JMenuBar mainMenuBar;
    private JMenu fileMenu;
    
    //File Menu
    private JMenuItem runMenuItem;
    private JMenuItem runMultiMenuItem;
    private JMenuItem exitMenuItem;

    //Drawing Area
    private SimulationGUI simGUI;

    public mainWindow(Intersection modelIntersection) {

        //Main Menubar Initialisation
        mainMenuBar = new javax.swing.JMenuBar();
        fileMenu = new javax.swing.JMenu();

        //File Menu Initialisation
        runMenuItem = new javax.swing.JMenuItem();
        runMultiMenuItem = new javax.swing.JMenuItem();
        exitMenuItem = new javax.swing.JMenuItem();
       
        //Set Window Settings
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        //Set Main MenuBar Text
        fileMenu.setText("File");

        //Set File Menu Text
        runMenuItem.setText("Run One Cycle");
        runMultiMenuItem.setText("Run Many Cycles");
        exitMenuItem.setText("Exit");

        //Construct File Menu
        fileMenu.add(runMenuItem);
        fileMenu.add(runMultiMenuItem);
        fileMenu.add(exitMenuItem);

        //Construct Menubar
        mainMenuBar.add(fileMenu);
        setJMenuBar(mainMenuBar);

        this.setName("CP2013 Traffic Simulation");

        simGUI = new SimulationGUI(modelIntersection);
        getContentPane().add(BorderLayout.CENTER, simGUI);

        //Pack the window
        pack();
    }
}
