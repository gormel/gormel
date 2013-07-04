/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package graph.data.view;

import graph.data.TreeNode;
import java.awt.Color;
import java.awt.Graphics;
import java.awt.geom.Rectangle2D;
import utils.ColorUtils;
import utils.Vector2;

/**
 *
 * @author Илья
 */
public class TreeNodeVis {
    public static final int POINT_RADIUS = 10;
    public static final Color DEFAULT_COLOR = Color.black;
    private int x;
    private int y;
    private Color color;
    private TreeNode node;
    private TreeVis owner;

    public TreeNodeVis(TreeNode node, TreeVis owner) {
        this.node = node;
        this.owner = owner;
	color = Color.BLACK;
    }
    
    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }

    public Color getColor() {
	return color;
    }

    public void setColor(Color color) {
	this.color = color;
    }
    
    public void Draw(Graphics g) {
	g.setColor(color);
        g.fillOval(x - POINT_RADIUS, y - POINT_RADIUS, 
                POINT_RADIUS * 2, POINT_RADIUS * 2);
	
	g.setColor(ColorUtils.getNegate(color));
	Rectangle2D rect = g.getFontMetrics().getStringBounds(String.valueOf(node.getValue()), g);
	g.drawString(String.valueOf(node.getValue()), 
		-(int)rect.getCenterX() + x, -(int)rect.getCenterY() + y);
        
        for (TreeNode near : node.getChildren()) {
            TreeNodeVis visualisation_of_this_vertex_ololo = owner.getVisualisation(near);
            
            Vector2 me = new Vector2(x, y);
            Vector2 he = new Vector2(visualisation_of_this_vertex_ololo.x, 
                    visualisation_of_this_vertex_ololo.y);
            
            Vector2 markerTo = me.add(he.sub(me).norm().mul(POINT_RADIUS));
            he = he.add(me.sub(markerTo));
	    g.setColor(DEFAULT_COLOR);
	    
            g.drawLine(x, y, (int)he.getX(), (int)he.getY());
            g.setColor(ColorUtils.getNegate(color));
            g.drawLine(x, y, (int)markerTo.getX(), (int)markerTo.getY());
        }
    }
}
