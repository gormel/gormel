/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.image.BufferedImage;
import scheme.Scheme;
import scheme.SchemeString;
import scheme.elements.SchemeElement;

/**
 *
 * @author Администратор
 */
public class SchemeViewer extends Viewer {

    private Scheme scheme;
    private static final int elementWidth = 400;
    private static final int elementHeight = 300;
    
    public SchemeViewer(Scheme scheme) {
	super(scheme.getWidth() * elementWidth * 3 / 2 - elementWidth / 2, 
	      scheme.getHeight() * elementHeight);
	this.scheme = scheme;
	update();
    }

    
    @Override
    public final void update() {
	BufferedImage image = getImage();
	Graphics2D g = image.createGraphics();
	g.fillRect(0, 0, image.getWidth(), image.getHeight());
	for (SchemeElement e : scheme.getElements()) {
	    ElementViewer viewer = new ElementViewer(e);
	    Image elementImage = viewer.getPicture(elementWidth, elementHeight);
	    int x = e.getIndex() * elementImage.getWidth(null) * 3 / 4;
	    int y = e.getLevel() * elementImage.getHeight(null);
	    g.drawImage(elementImage, x, y, null);
	}
    }
    
}
