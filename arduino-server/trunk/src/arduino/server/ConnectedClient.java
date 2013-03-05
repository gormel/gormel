package arduino.server;


import com.sun.xml.internal.ws.util.ByteArrayBuffer;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.Properties;
import javax.xml.parsers.ParserConfigurationException;
import messages.Message;
import queryes.LedStateQuery;
import queryes.TempQuery;
import queryes.ToggleBlueQuery;
import queryes.ToggleRedQuery;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Администратор
 */
public class ConnectedClient extends Thread {
    private Socket sock;
    private InputStream input;
    private OutputStream output;
    
    public ConnectedClient(Socket s) throws IOException, ParserConfigurationException {
        sock = s;
        System.out.println("new user connected from " + s.getInetAddress().toString());
        input = sock.getInputStream();
        output = sock.getOutputStream();
    }
    
    @Override
    public void run() {
        try {
	    ByteArrayBuffer buff = new ByteArrayBuffer();
            while (sock.isConnected()) {
		int readed = input.read();
		if (readed != 255) {
		    buff.write(readed);
		} else {
		    Properties message = new Properties();
		    message.loadFromXML(buff.newInputStream());
		    execute(message);
		    buff = new ByteArrayBuffer();
		}
            }
	} catch (IOException e) {
	    System.out.println(e.toString());
        } finally {
            System.out.println("user disconnected from " + sock.getInetAddress().toString());
        }
    }
    
    private void execute(Properties data) {
        switch(Byte.parseByte(data.getProperty("id"))) {
            case Message.toggleRed:
                ArduinoServer.manager.sendQuery(new ToggleRedQuery(this));
                break;
            case Message.toggleBlue:
                ArduinoServer.manager.sendQuery(new ToggleBlueQuery(this));
                break;
            case Message.getLedState:
                ArduinoServer.manager.sendQuery(new LedStateQuery(this));
                break;
            case Message.getTemp:
                ArduinoServer.manager.sendQuery(new TempQuery(this));
                break;
        }
    }
    
    public void send(Message m) throws IOException {
	m.getData().storeToXML(output, "");
	output.write(255);
	output.flush();
    }
}
