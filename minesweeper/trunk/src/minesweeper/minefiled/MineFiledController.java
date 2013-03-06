/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper.minefiled;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.beans.property.ListProperty;
import javafx.beans.property.SimpleListProperty;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.RowConstraints;

/**
 * FXML Controller class
 *
 * @author Tyulen
 */
public class MineFiledController implements Initializable {
    
    @FXML
    ListProperty<RowConstraints> rowConstraints = new SimpleListProperty<>();
    
    @FXML
    GridPane pane;
    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    
}
