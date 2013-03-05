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
public class ToggleBlueMessage extends Message{

    public ToggleBlueMessage(Properties data) {
        super(data);
    }

    public ToggleBlueMessage() {
    }
    
    @Override
    protected void setData(Properties data) {
    }

    @Override
    public Properties getData() {
	Properties prop = new Properties();
	prop.setProperty("id", String.valueOf(getId()));
	return prop;
    }

    @Override
    public byte getId() {
        return Message.toggleBlue;
    }
    
}
