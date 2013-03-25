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
public class SchemeElementC extends SchemeElement {

    public SchemeElementC(SchemeString left, SchemeString right) {
        super(left, right);
    }
    
    @Override
    public SchemeString getLeftOutput() {
        return left;
    }

    @Override
    public SchemeString getRightOutput() {
        return right;
    }

    @Override
    public Color mainColor() {
        return left.getColor();
    }

    @Override
    public int getOffset() {
        return 0;
    }
    
}
