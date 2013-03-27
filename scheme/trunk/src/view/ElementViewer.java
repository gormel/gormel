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
    SchemeElement element;
    
    public ElementViewer(SchemeElement element) {
	super(400, 300);
        this.element = element;
        update();
    }

    @Override
    public final void update() {
        Graphics2D g = getImage().createGraphics();
        int w = getImage().getWidth();
        int h = getImage().getHeight();
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
        
        int arrowHVSize = (int) (w * Math.sqrt(2) / 10);
        int arrowStartX = 0;
        int arrowStartY = h / 2 - arrowHVSize;
        
        if (element.getLeft().getColor().equals(element.mainColor())) {
            arrowStartX = w / 2 - arrowHVSize;
        }
        
        if (element.getRight().getColor().equals(element.mainColor())) {
            arrowStartX = w / 2 + arrowHVSize;
        }
        
        int arrowEndX = 0;
        int arrowEndY = h / 2 + arrowHVSize;
        
        if (element.getLeftOutput().getColor().equals(element.mainColor())) {
            arrowEndX = w / 2 - arrowHVSize;
        }
        
        if (element.getRightOutput().getColor().equals(element.mainColor())) {
            arrowEndX = w / 2 + arrowHVSize;
        }
        
        g.drawLine(arrowStartX, arrowStartY, w / 2, h / 2);
        drawArrow(g, w / 2, h / 2, arrowEndX, arrowEndY, w / 40, w / 20);
    }
}
