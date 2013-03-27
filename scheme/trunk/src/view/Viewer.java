/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package view;

import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.image.BufferedImage;
import utils.Vector2;

/**
 *
 * @author User
 */
public abstract class Viewer {
    private BufferedImage image;

    public Viewer(int width, int height) {
	image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
    }
    
    protected BufferedImage getImage() {
	return image;
    }
    
    public Image getPictupe(int width, int height) {
	return image.getScaledInstance(width, height, Image.SCALE_DEFAULT);
    }
    
    protected void drawArrow(Graphics2D g, int x1, int y1, int x2, int y2, int width, int length) {
        g.drawLine(x1, y1, x2, y2);
        
        Vector2 start = new Vector2(x1, y1);
        Vector2 end = new Vector2(x2, y2);
        Vector2 line = end.sub(start);
        Vector2 neckEnd = line.nor().mul(line.len() - length);
        Vector2 neckWidthLeft = line.nor().mul(width).rot(Math.PI / 2);
        Vector2 neckLeft = neckEnd.add(neckWidthLeft);
        Vector2 neckRight = neckEnd.sub(neckWidthLeft);
        Vector2 neckLeftTrue = start.add(neckLeft);
        Vector2 neckRightTrue = start.add(neckRight);
        
        g.drawLine(x2, y2, (int) neckLeftTrue.getX(), (int) neckLeftTrue.getY());
        g.drawLine(x2, y2, (int) neckRightTrue.getX(), (int) neckRightTrue.getY());
    }
    
    public abstract void update();
}
