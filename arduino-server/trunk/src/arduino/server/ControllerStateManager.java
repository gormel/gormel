/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package arduino.server;

import java.io.IOException;
import queryes.Query;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import jssc.SerialPort;
import jssc.SerialPortEvent;
import jssc.SerialPortEventListener;
import jssc.SerialPortException;
import queryes.LedStateQuery;
import queryes.TempQuery;

/**
 *
 * @author Администратор
 */
public class ControllerStateManager {
    
    private SerialPort port = new SerialPort("COM5");
    ControllerStateManager me = this;
    SerialStream ss;
    List<Query> queryes = Collections.synchronizedList(new ArrayList<Query>());
    
    
    public ControllerStateManager() throws SerialPortException {
        port.openPort();
        port.setParams(9600, 8, 1, 0);
	ss = new SerialStream(port);
	new Thread() {
	    @Override
	    public void run() {
		while (true) {
                    try {
                        byte[] data = ss.readMessage();
                        execute(data);
                    } catch (IOException ex) {
                        Logger.getLogger(ControllerStateManager.class.getName()).log(Level.SEVERE, null, ex);
                    } catch (InterruptedException ex) {
                        Logger.getLogger(ControllerStateManager.class.getName()).log(Level.SEVERE, null, ex);
                    }
		}
	    }
	}.start();
    }
    
    private void execute(byte[] data) {
        if (data.length == 0) {
            return;
        }
        switch (data[0]) {
            case Query.ledStates:
                for (int i = 0; i < queryes.size(); i++) {
                    if (queryes.get(i) instanceof LedStateQuery) {
                        queryes.get(i).request(data);
                        queryes.remove(i);
                        i--;
                    }
                }
                break;
                
            case Query.termState:
                for (int i = 0; i < queryes.size(); i++) {
                    if (queryes.get(i) instanceof TempQuery) {
                        queryes.get(i).request(data);
                        queryes.remove(i);
                        i--;
                    }
                }
                break;
            default:
        }
    }
    
    public void sendQuery(Query q) {
        q.send(ss);
        if (q.hasRequest()) {
            queryes.add(q);
        }
    }
}
