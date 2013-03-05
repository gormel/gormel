/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package messages;

import java.util.Properties;

/**
 * сообщение, посылаемое с клиента на сервер и обратно
 * @author Tyulen
 */
public abstract class Message {
    
    /**
     * создает новое сообщение из его представления в виде объекта Properties
     * @param data 
     */
    public Message(Properties data) {
        setData(data);
    }
    
    public Message() {
	
    }
    
    /**
     * формирует сообщение из его представления в виде объекта Properties
     * @param data 
     */
    protected abstract void setData(Properties data);
    
    /**
     * возвращает сообщение в его представлении в виде объекта Properties
     * @return 
     */
    public abstract Properties getData();
    
    /**
     * id сообщения
     * @return 
     */
    public abstract byte getId();
    
    public static final byte toggleRed = 0;
    public static final byte toggleBlue = 1;
    public static final byte getLedState = 2;
    public static final byte ledState = 3;
    public static final byte getTemp = 4;
    public static final byte temp = 6;
}