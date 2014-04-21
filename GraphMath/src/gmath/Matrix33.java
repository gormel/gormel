/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gmath;

/**
 *
 * @author User
 */
public class Matrix33 {
    public double m11;
    public double m12;
    public double m13;
    public double m21;
    public double m22;
    public double m23;
    public double m31;
    public double m32;
    public double m33;

    public Matrix33(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33) {
        this.m11 = m11;
        this.m12 = m12;
        this.m13 = m13;
        this.m21 = m21;
        this.m22 = m22;
        this.m23 = m23;
        this.m31 = m31;
        this.m32 = m32;
        this.m33 = m33;
    }
    
    public Vector2 transform(Vector2 value) {
        double x = value.getX();
        double y = value.getY();
        double w = 1;
        
        double x1 = m11 * x + m12 * y + m13 * w;
        double y1 = m21 * x + m22 * y + m23 * w;
        return new Vector2(x1, y1);
    }
    
    public static Matrix33 getIdentity() {
        return new Matrix33(1, 0, 0, 
                            0, 1, 0,
                            0, 0, 1);
    }
    
    public static Matrix33 getRotation(double angle) {
        return new Matrix33(Math.sin(angle), Math.cos(angle), 0,
                            Math.cos(angle), -Math.sin(angle), 0,
                            0, 0, 1);
    }
    
    public static Matrix33 getTranslation(double dx, double dy) {
        return new Matrix33(1, 0, dx, 
                            0, 1, dy, 
                            0, 0, 1);
    }
    
    public Matrix33 mul(Matrix33 value) {
        double _m11 = m11 * value.m11 + m12 * value.m21 + m13 * value.m31;
        double _m12 = m11 * value.m12 + m12 * value.m22 + m13 * value.m32;
        double _m13 = m11 * value.m13 + m12 * value.m23 + m13 * value.m33;
        double _m21 = m21 * value.m11 + m22 * value.m21 + m23 * value.m31;
        double _m22 = m21 * value.m12 + m22 * value.m22 + m23 * value.m32;
        double _m23 = m21 * value.m13 + m22 * value.m23 + m23 * value.m33;
        double _m31 = m31 * value.m11 + m32 * value.m21 + m33 * value.m31;
        double _m32 = m31 * value.m12 + m32 * value.m22 + m33 * value.m32;
        double _m33 = m31 * value.m13 + m32 * value.m23 + m33 * value.m33;
        
        return new Matrix33(_m11, _m12, _m13, _m21, _m22, _m23, _m31, _m32, _m33);
    }
}
