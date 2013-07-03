/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package utils;

/**
 *
 * @author Илья
 */
public class Vector2 {
    private float x;
    private float y;
    public Vector2() {
        x = y = 0;
    }

    public Vector2(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public float getX() {
        return x;
    }

    public float getY() {
        return y;
    }
    
    public Vector2 add(Vector2 v)
    {
        return new Vector2(x + v.x, y + v.y);
    }
    
    public Vector2 mul(float v)
    {
        return new Vector2((x * v), (y * v));
    }
    
    public Vector2 sub(Vector2 v)
    {
        return add(v.mul(-1));
    }
    
    public Vector2 div(float v)
    {
        return mul(1 / v);
    }
    
    public float dot(Vector2 v)
    {
        return x * v.x + y * v.y;
    }
    
    public Vector2 rot(float radians)
    {
        return new Vector2((float)(x * Math.sin(radians) + y * Math.cos(radians)),
                           (float)(x * Math.cos(radians) - y * Math.sin(radians)));
    }
    
    public float len()
    {
        return (float)Math.sqrt(x * x + y * y);
    }
    
    public Vector2 norm()
    {
        return div(len());
    }
}
