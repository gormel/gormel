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
import messages.TempMessage;

/**
 *
 * @author Tyulen
 */
public class TempQuery extends Query {

    public TempQuery(ConnectedClient from) {
        super(from);
    }
    
    @Override
    public void send(SerialStream ss) {
        try {
            ss.sendMessage("t".getBytes());
        } catch (SerialPortException ex) {
            Logger.getLogger(TempQuery.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public void request(byte[] data) {
        try {
            TempMessage m = new TempMessage();
            m.value = Float.intBitsToFloat((data[1] << 24) + (data[2] << 16) + (data[3] << 8) + data[4]);
            from.send(m);
        } catch (IOException ex) {
            Logger.getLogger(TempQuery.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @Override
    public boolean hasRequest() {
        return true;
    }
    
}
