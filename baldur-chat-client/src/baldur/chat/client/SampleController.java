/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package baldur.chat.client;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.Initializable;

/**
 *
 * @author Tyulen
 */
public class SampleController implements Initializable {
    
    ObservableList<String> items = FXCollections.observableArrayList();
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
        items.add("1");
        items.add("2");
    }    
}
