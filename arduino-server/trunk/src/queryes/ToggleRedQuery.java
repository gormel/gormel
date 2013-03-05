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
public class ToggleRedQuery extends Query{

    public ToggleRedQuery(ConnectedClient from) {
        super(from);
    }
    
    @Override
    public void send(SerialStream ss) {
        try {
            ss.sendMessage("r".getBytes());
        } catch (SerialPortException ex) {
            Logger.getLogger(ToggleRedQuery.class.getName()).log(Level.SEVERE, null, ex);
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
