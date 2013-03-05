/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package queryes;

import arduino.server.ConnectedClient;
import arduino.server.SerialStream;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import jssc.SerialPortException;
import messages.LedStateMessage;

/**
 *
 * @author Tyulen
 */
public class LedStateQuery extends Query {

    public LedStateQuery(ConnectedClient from) {
        super(from);
    }
    
    @Override
    public void send(SerialStream ss) {
        try {
            ss.sendMessage("s".getBytes());
        } catch (SerialPortException ex) {
            Logger.getLogger(LedStateQuery.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public void request(byte[] data) {
        try {
            LedStateMessage m = new LedStateMessage();
	    m.red = data[1] == 1;
	    m.blue = data[2] == 1;
	    from.send(m);
	} catch (IOException ex) {
            Logger.getLogger(LedStateQuery.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public boolean hasRequest() {
        return true;
    }
    
}
