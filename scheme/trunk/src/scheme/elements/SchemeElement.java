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
public abstract class SchemeElement {
    protected SchemeString left;
    protected SchemeString right;
    private int level;
    private int leftIndex;
    
    public SchemeElement(SchemeString left, SchemeString right) {
        if (right.getIndex() - left.getIndex() != 1)
            throw new RuntimeException("Wrong strings! they must be nearly.");
	
        this.left = left;
        this.right = right;
	
	leftIndex = left.getIndex();
        
        left.addElement(this);
        right.addElement(this);
    }

    public SchemeString getLeft() {
        return left;
    }

    public SchemeString getRight() {
        return right;
    }
    
    public void setLevel(int level) {
	if (level > this.level)
	    this.level = level;
    }
    
    public int getLevel() {
	return level;
    }
    
    public int getIndex() {
	return leftIndex;
    }

    public abstract SchemeString getLeftOutput();
    public abstract SchemeString getRightOutput();
    public abstract Color mainColor();
    public abstract int getOffset(SchemeString s);
}
