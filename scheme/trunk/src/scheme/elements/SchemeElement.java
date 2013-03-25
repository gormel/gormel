/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package scheme.elements;

import java.awt.Color;
import java.awt.Image;
import scheme.SchemeString;

/**
 *
 * @author User
 */
public abstract class SchemeElement {
    protected SchemeString left;
    protected SchemeString right;
    public SchemeElement(SchemeString left, SchemeString right) {
        this.left = left;
        this.right = right;
        
        if (right.getIndex() - left.getIndex() != 1)
            throw new RuntimeException("Wrong strings! they must be nearly.");
        
        left.addElement(this);
        right.addElement(this);
    }

    public SchemeString getLeft() {
        return left;
    }

    public SchemeString getRight() {
        return right;
    }

    public abstract SchemeString getLeftOutput();
    public abstract SchemeString getRightOutput();
    public abstract Color mainColor();
    public abstract int getOffset();
}
