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
public class SchemeElementE extends SchemeElement {

    public SchemeElementE(SchemeString string) {
        super(string, string);
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
        return right.getColor();
    }

    @Override
    public int getOffset(SchemeString s) {
        return 0;
    }
    
}
