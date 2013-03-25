/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package scheme;

import java.awt.Color;
import java.awt.Image;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import scheme.elements.SchemeElement;

/**
 *
 * @author User
 */
public class Scheme {
    
    private List<SchemeString> strings = new ArrayList<SchemeString>();
    private List<SchemeElement> elements = new ArrayList<SchemeElement>();
    
    public Scheme() {
    }

    public List<SchemeString> getStrings() {
        return strings;
    }
    
    public List<SchemeElement> getElements() {
        return elements;
    }
}
