/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package scheme.elements;

import java.awt.Color;
import scheme.SchemeString;

/**
 *
 * @author User
 */
public class SchemeElementA extends SchemeElement {

    public SchemeElementA(SchemeString left, SchemeString right) {
        super(left, right);
    }
    
    @Override
    public SchemeString getLeftOutput() {
        return right;
    }

    @Override
    public SchemeString getRightOutput() {
        return left;
    }

    @Override
    public int getOffset() {
        return 1;
    }

    @Override
    public Color mainColor() {
        return left.getColor();
    }
    
}
