/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.image.BufferedImage;
import scheme.elements.SchemeElement;

/**
 *
 * @author User
 */
public class ElementViewer extends Viewer {
    BufferedImage image = new BufferedImage(400, 300, BufferedImage.TYPE_INT_RGB);
    SchemeElement element;
    
    public ElementViewer(SchemeElement element) {
        this.element = element;
        update();
    }

    @Override
    public final void update() {
        Graphics2D g = image.createGraphics();
        int w = image.getWidth();
        int h = image.getHeight();
        int thickness = (int) (w * Math.sqrt(2) / 8);
        g.setStroke(new BasicStroke(thickness));
        g.setColor(Color.white);
        g.fillRect(0, 0, w, h);
        
        g.setColor(element.getLeft().getColor());
        g.drawLine(0, -w / 8, w / 2, h / 2);
        
        g.setColor(element.getRight().getColor());
        g.drawLine(w, -w / 8, w / 2, h / 2);
        
        g.setColor(element.getLeftOutput().getColor());
        g.drawLine(0, h + w / 8, w / 2, h / 2);
        
        g.setColor(element.getRightOutput().getColor());
        g.drawLine(w, h + w / 8, w / 2, h / 2);
        
        int ovalX = w / 4;
        int ovalY = h / 2 - w / 4;
        int ovalW = w / 2;
        int ovalH = w / 2;
        g.setColor(element.mainColor());
        g.fillOval(ovalX, ovalY, ovalW, ovalH);
        
        g.setStroke(new BasicStroke(w / 40));
        g.setColor(Color.black);
        g.drawOval(ovalX, ovalY, ovalW, ovalH);
    }

    @Override
    public Image getPictupe(int width, int height) {
        return image.getScaledInstance(width, height, Image.SCALE_DEFAULT);
    }
    
}
