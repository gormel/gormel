/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package queryes;

import arduino.server.ConnectedClient;
import arduino.server.SerialStream;

/**
 * запрос, отправляемый на микроконтроллер
 * @author Tyulen
 */
public abstract class Query {
    protected ConnectedClient from;
    
    /**
     * создает новый запрос, 
     * @param from - клиент, которому будет предоставлен ответ на этот запрос
     */
    public Query(ConnectedClient from) {
        this.from = from;
    }
    
    /**
     * отправляет запрос
     * @param ss 
     */
    public abstract void send(SerialStream ss);
    
    /**
     * вызывается при получении ответных данных на этот запрос
     * @param data 
     */
    public abstract void request(byte[] data);
    
    /**
     * если true, то запрос будет ожидать ответа
     * @return 
     */
    public abstract boolean hasRequest();
    
    public static final byte ledStates = 0;
    public static final byte termState = 1;
}
