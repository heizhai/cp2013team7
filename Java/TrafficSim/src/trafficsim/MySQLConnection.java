package trafficsim;


import java.io.*;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 * Class which neatly handles all the MySQL Transactions, Configurations and Settings for MySQL Connectivity.
 *
 * @author Tristan Davey
 *
 */
class MySQLConnection implements Serializable {

    private String hostname;
    private String username;
    private String password;
    private String database;
    private Connection conn;

    MySQLConnection(String hostname, String username, String password, String database) throws InstantiationException, IllegalAccessException, SQLException, ClassNotFoundException {
        this.hostname = hostname;
        this.username = username;
        this.password = password;
        this.database = database;
        this.openConnection();
    }

    public void openConnection() throws InstantiationException, IllegalAccessException, SQLException, ClassNotFoundException {
            Class.forName("com.mysql.jdbc.Driver").newInstance();
            this.conn = DriverManager.getConnection("jdbc:mysql://" + this.hostname + "/" + this.database + "?" + "user=" + this.username + "&password=" + this.password);
    }

    public ResultSet selectQuery(String queryString) {
      try {
        Statement statement = conn.createStatement();
        ResultSet result = statement.executeQuery(queryString);
        return result;
      }
      catch (SQLException s){
        System.out.println(s.getMessage());
        return null;
      }
    }

    public void insertUpdateQuery(String queryString) {
        try{
            Statement st = conn.createStatement();
            int val = st.executeUpdate(queryString);
        }
        catch (SQLException s){
            System.out.println(s.getMessage());
        }
    }

}
