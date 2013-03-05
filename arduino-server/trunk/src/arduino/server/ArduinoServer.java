/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package arduino.server;

import com.sun.xml.internal.ws.util.ByteArrayBuffer;
import java.io.IOException;
import java.net.ServerSocket;
import javax.xml.parsers.ParserConfigurationException;
import jssc.SerialPortException;

/**
 *
 * @author Tyulen
 */
public class ArduinoServer {

    /**
     * @param args the command line arguments
     */
    static ServerSocket socket;
    public static ControllerStateManager manager;
    
    public static void main(String[] args) throws IOException, SerialPortException, IOException, ParserConfigurationException {
        ByteArrayBuffer b = new ByteArrayBuffer();
        b.write(1);
        b.write(2);
        byte[] d = b.getRawData();
	
	socket = new ServerSocket(2000);
        manager = new ControllerStateManager();
        
        while (true) {
            new ConnectedClient(socket.accept()).start();
        }
    }
}