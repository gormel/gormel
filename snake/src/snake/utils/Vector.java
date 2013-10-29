/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package snake.utils;

/**
 *
 * @author User
 */
public class Vector {
    private int x;
    private int y;

    public Vector() {
    }

    public Vector(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }
    
    public Vector add(Vector v) {
        return new Vector(x + v.x, y + v.y);
    }
    
    public Vector sub(Vector v) {
        return new Vector(x - v.x, y - v.y);
    }
    
    public Vector norm() {
        double len = Math.sqrt(x * x + y * y);
        return new Vector((int)(x / len), (int)(y / len));
    }
}
