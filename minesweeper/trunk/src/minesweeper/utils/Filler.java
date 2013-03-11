/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper.utils;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.layout.AnchorPane;

/**
 *
 * @author User
 */
public class Filler extends AnchorPane{

    public Filler() {
        FXMLLoader loader = new FXMLLoader(getClass().getResource("Filler.fxml"));
        loader.setRoot(this);
        loader.setController(this);
        try {
            loader.load();
        } catch (IOException ex) {
            throw new RuntimeException(ex);
        }
        
        for(Node o : getChildren()) {
            AnchorPane.setBottomAnchor(o, 0.0);
            AnchorPane.setLeftAnchor(o, 0.0);
            AnchorPane.setRightAnchor(o, 0.0);
            AnchorPane.setTopAnchor(o, 0.0);
        }
    }
}
