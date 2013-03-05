/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package messages;

import java.util.Properties;

/**
 *
 * @author User
 */
public class TempMessage extends Message {

    public double value = 0;
    
    @Override
    protected void setData(Properties data) {
        value = Double.parseDouble(data.getProperty("value"));
    }

    public TempMessage() {
    }

    public TempMessage(Properties data) {
        value = Double.parseDouble(data.getProperty("value"));
    }

    @Override
    public Properties getData() {
        Properties prop = new Properties();
        prop.setProperty("id", String.valueOf(getId()));
        prop.setProperty("value", String.valueOf(value));
        return prop;
    }

    @Override
    public byte getId() {
        return Message.temp;
    }
    
}
