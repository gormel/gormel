/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package messages;

import java.util.Properties;

/**
 *
 * @author Tyulen
 */
public class LedStateMessage extends Message{

    public boolean red;
    public boolean blue;
    
    public LedStateMessage(Properties data) {
        super(data);
    }
    
    public LedStateMessage() {
	
    }
    
    @Override
    protected void setData(Properties data) {
	red = Boolean.parseBoolean(data.getProperty("red"));
	blue = Boolean.parseBoolean(data.getProperty("blue"));
    }

    @Override
    public Properties getData() {
	Properties prop = new Properties();
	prop.setProperty("id", String.valueOf(getId()));
	prop.setProperty("red", String.valueOf(red));
	prop.setProperty("blue", String.valueOf(blue));
	return prop;
    }

    @Override
    public byte getId() {
        return Message.ledState;
    }    
}
