/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package queryes;

import arduino.server.ConnectedClient;
import arduino.server.SerialStream;
import java.util.logging.Level;
import java.util.logging.Logger;
import jssc.SerialPortException;

/**
 *
 * @author Tyulen
 */
public class ToggleBlueQuery extends Query{

    public ToggleBlueQuery(ConnectedClient from) {
        super(from);
    }
    
    @Override
    public void send(SerialStream ss) {
        try {
            ss.sendMessage("b".getBytes());
        } catch (SerialPortException ex) {
            Logger.getLogger(ToggleBlueQuery.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public void request(byte[] data) {
        throw new UnsupportedOperationException("Not supported yet.");
    }

    @Override
    public boolean hasRequest() {
        return false;
    }
    
}
