/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import scheme.elements.SchemeElement;

/**
 *
 * @author User
 */
public class ElementViewer extends Viewer {
    BufferedImage image = new BufferedImage(100, 100, BufferedImage.TYPE_INT_RGB);
    SchemeElement element;
    
    public ElementViewer(SchemeElement element) {
        this.element = element;
        update();
    }

    @Override
    public final void update() {
        Graphics g = image.getGraphics();
        g.fillRect(0, 0, 100, 100);
        g.setColor(element.getLeft().getColor());
        
    }

    @Override
    public Image getPictupe(int width, int height) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    
}
