/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import scheme.Scheme;
import scheme.elements.SchemeElement;

/**
 *
 * @author Администратор
 */
public class SchemeResultViewer extends Viewer{

    private Scheme scheme;
    public SchemeResultViewer(Scheme s) {
	super(s.getWidth() - 1 + s.getHeight(), 
		s.getWidth() - 1 + s.getHeight());
	this.scheme = s;
	update();
    }
    
    @Override
    public final void update() {
	BufferedImage image = getImage();
	Graphics2D g = image.createGraphics();
	for (SchemeElement e : scheme.getElements()) {
	    image.setRGB(e.getIndex(), e.getLevel(), e.mainColor().getRGB());
	}
    }
    
}
