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
public class SchemeElementB extends SchemeElement {

    public SchemeElementB(SchemeString left, SchemeString right) {
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
    public Color mainColor() {
        return right.getColor();
    }

    @Override
    public int getOffset(SchemeString s) {
        if (s == left)
	    return 1;
	return -1;
    }
    
}
