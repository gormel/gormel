/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gmath;

/**
 *
 * @author User
 */
public class Vector2 {
    private double x;
    private double y;

    public Vector2() {
    }

    public Vector2(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }
    
    public double dot(Vector2 value) {
        return x * value.x + y * value.y;
    }
    
    public Vector2 mul(double value) {
        return new Vector2(value * x, value * y);
    }
    
    public Vector2 div(double value) {
        return mul(1 / value);
    }
    
    public Vector2 add(Vector2 value) {
        return new Vector2(x + value.x, y + value.y);
    }
    
    public Vector2 sub(Vector2 value) {
        return add(value.mul(-1));
    }
    
    public Vector2 norm() {
        return div(len());
    }
    
    public double len() {
        return Math.sqrt(x * x + y * y);
    }
}
