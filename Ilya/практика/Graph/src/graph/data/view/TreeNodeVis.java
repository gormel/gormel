/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package graph.data.view;

import graph.data.TreeNode;
import java.awt.Color;
import java.awt.Graphics;
import utils.Vector2;

/**
 *
 * @author Илья
 */
public class TreeNodeVis {
    public static final int POINT_RADIUS = 10;
    private int x;
    private int y;
    TreeNode node;
    TreeVis owner;

    public TreeNodeVis(TreeNode node, TreeVis owner) {
        this.node = node;
        this.owner = owner;
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
    
    public void Draw(Graphics g) {
        g.fillOval(x - POINT_RADIUS, y - POINT_RADIUS, 
                POINT_RADIUS * 2, POINT_RADIUS * 2);
        
        for (TreeNode near : node.getChildren()) {
            TreeNodeVis visualisation_of_this_vertex_ololo = owner.getVisualisation(near);
            
            Vector2 me = new Vector2(x, y);
            Vector2 he = new Vector2(visualisation_of_this_vertex_ololo.x, 
                    visualisation_of_this_vertex_ololo.y);
            
            Vector2 markerTo = me.add(he.sub(me).norm().mul(POINT_RADIUS));
            
            g.drawLine(x, y, (int)he.getX(), (int)he.getY());
            Color c = g.getColor();
            g.setColor(Color.WHITE);
            g.drawLine(x, y, (int)markerTo.getX(), (int)markerTo.getY());
            g.setColor(c);
        }
    }
}
