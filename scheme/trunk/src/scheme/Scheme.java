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
    
    public List<SchemeElement> getElements() {
        return elements;
    }
    
    public void addString(Color c) {
	strings.add(new SchemeString(strings.size(), c));
    }
    
    public void setStringColor(int index, Color c) {
	strings.get(index).setColor(c);
    }
    
    public void removeString() {
	strings.remove(strings.size() - 1);
    }
    
    public SchemeString getString(int index) {
	if (index < 0 || index > strings.size())
	    throw new RuntimeException("Index out of range.");
	
	for (SchemeString s : strings) {
	    if (s.getIndex() == index)
		return s;
	}
	return null;
    }
    
    public int getStringCount() {
	return strings.size();
    }
    
    public int getWidth() {
	return strings.size() / 2;
    }
    
    public int getHeight() {
	int height = 0;
	for (SchemeElement e : elements) {
	    if (e.getLevel() > height)
		height = e.getLevel();
	}
	return height + 1;
    }
}
