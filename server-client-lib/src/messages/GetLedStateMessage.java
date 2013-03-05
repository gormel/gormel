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
public class GetLedStateMessage extends Message {

    public GetLedStateMessage(Properties data) {
        super(data);
    }

    public GetLedStateMessage() {
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
        return Message.getLedState;
    }
}
