/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package arduino.server;

import com.sun.xml.internal.ws.util.ByteArrayBuffer;
import java.io.IOException;
import java.io.InputStream;
import java.util.logging.Level;
import java.util.logging.Logger;
import jssc.SerialPort;
import jssc.SerialPortEvent;
import jssc.SerialPortEventListener;
import jssc.SerialPortException;

/**
 *
 * @author Администратор
 */
public class SerialStream {
    private final byte endSymbol = (byte) 127;
    ByteArrayBuffer buffer = new ByteArrayBuffer();
    SerialPort port;
    
    SerialStream(SerialPort port) throws SerialPortException {
	this.port = port;
	port.addEventListener(new SerialPortEventListener() {
	    @Override
	    public void serialEvent(SerialPortEvent spe) {
		try {
		    onDataGet(spe);
		} catch (SerialPortException ex) {
		    Logger.getLogger(SerialStream.class.getName()).log(Level.SEVERE, null, ex);
                } catch (IOException e) {
		}
	    }
	});
    }
    
    private void onDataGet(SerialPortEvent e) throws SerialPortException, IOException {
        synchronized(this) {
            if (e.isRXCHAR()) {
                byte[] readedData = port.readBytes(e.getEventValue());
                                
                
                buffer.write(readedData);
                if (messageReady()) {
                    notify();
                }
            }
        }
	
    }
    
    private boolean messageReady() {
        for (int i = 0; i < buffer.size(); i++) {
            byte b = buffer.getRawData()[i];
            if (b == endSymbol) {
                return true;
            }
        }
        return false;
    }
    
    /**
     * читает сообщение из порта, пока сообщение не пришло полностью, вешает поток
     * @return
     * @throws InterruptedException 
     */
    public byte[] readMessage() throws InterruptedException, IOException {
        synchronized(this) {
            while (!messageReady()) {
                wait();
            }
            
            int endPos = 0;
            for (int i = 0; i < buffer.size(); i++) {
                byte b = buffer.getRawData()[i];
                if (b == endSymbol) {
                    endPos = i;
                    break;
                }
            }
            byte[] rv = new byte[endPos];
            InputStream stream = buffer.newInputStream();
            stream.read(rv);
            stream.read();
            
            byte[] remain = new byte[buffer.size() - endPos - 1];
            stream.read(remain);
            buffer = new ByteArrayBuffer(remain);
            
            return rv;
        }
    }
    
    /**
     * отправляет сообщение в порт
     * @param message
     * @throws SerialPortException 
     */
    public void sendMessage(byte[] message) throws SerialPortException {
        port.writeBytes(message);
    }
}
