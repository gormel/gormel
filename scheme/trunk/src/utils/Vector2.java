/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package utils;

/**
 *
 * @author User
 */
public class Vector2 {

    private double x;
    private double y;
    private double len;
    
    public Vector2(double x, double y) {
        this.x = x;
        this.y = y;
        len = Math.sqrt(x * x + y * y);
    }

    public Vector2() {
    }
    
    public Vector2 add(Vector2 v) {
        return new Vector2(x + v.getX(), y + v.getY());
    }
    
    public Vector2 sub(Vector2 v) {
        return add(v.neg());
    }
    
    public Vector2 mul(double value) {
        return new Vector2(x * value, y * value);
    }
    
    public Vector2 div(double value) {
        return mul(1 / value);
    }
    
    public Vector2 neg() {
        return mul(-1);
    }
    
    public Vector2 rot(double angle) {
        return new Vector2(
                y * Math.cos(angle) + x * Math.sin(angle), 
                y * Math.sin(angle) - x * Math.cos(angle));
    }
    
    public double dot(Vector2 v) {
        return x * v.getX() + y * v.getY();
    }
    
    public double crossZ(Vector2 v) {
        return x * v.getY() - y * v.getX();
    }
    
    public double len() {
        return len;
    }
    
    public Vector2 nor() {
        return div(len());
    }
    
    public double lenSq() {
        return len * len;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }
    
    public Vector2 setX(double x) {
        return new Vector2(x, y);
    }
    
    public Vector2 setY(double y) {
        return new Vector2(x, y);
    }
}
