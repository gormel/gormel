/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gmath;

/**
 *
 * @author User
 */
public class Rectangle {
    private double x;
    private double y;
    private double w;
    private double h;

    public Rectangle(double x, double y, double w, double h) {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
    
    public boolean hitTest(Vector2 vector) {
        double _x = vector.getX();
        double _y = vector.getY();
        
        return _x >= x && _x <= x + w && _y >= y && _y <= y + h;
    }

    public double getH() {
        return h;
    }

    public double getW() {
        return w;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }
    
    public Rectangle intersection(Rectangle rect) {
        double _x = Math.min(x, rect.x);
        double _y = Math.min(y, rect.y);
        double _w = 0;
        double _h = 0;
        if (_w <= 0 || _h <=0)
            return null;
        return new Rectangle(_x, _y, _w, _h);
    }
}
