/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper.minefiled;

import java.io.IOException;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.GridPane;

/**
 *
 * @author User
 */
public class MineFiled extends AnchorPane {

    @FXML
    GridPane grid;
    
    public MineFiled() {
        FXMLLoader loader = new FXMLLoader(getClass().getResource("MineFiled.fxml"));
        loader.setRoot(this);
        loader.setController(this);
        
        try {
            loader.load();
        } catch (IOException ex) {
            throw new RuntimeException(ex);
        }
    }
}
