/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package scheme;

import scheme.elements.SchemeElement;
import java.awt.Color;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author User
 */
public class SchemeString {

    private Color color;
    private List<SchemeElement> elements = new ArrayList<SchemeElement>();
    private int startIndex;
    
    public SchemeString(int startIndex, Color color) {
        this(startIndex);
        this.color = color;
    }

    public SchemeString(int startIndex) {
        this.startIndex = startIndex;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }
    
    public int getIndex() {
        int index = startIndex;
        for (SchemeElement element : elements) {
            index += element.getOffset();
        }
        return index;
    }
    
    public void addElement(SchemeElement element) {
        elements.add(element);
    }
}
