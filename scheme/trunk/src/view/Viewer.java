/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.Image;

/**
 *
 * @author User
 */
public abstract class Viewer {
    public abstract void update();
    public abstract Image getPictupe(int width, int height);
}
