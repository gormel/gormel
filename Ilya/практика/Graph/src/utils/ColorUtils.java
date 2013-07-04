/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package utils;

import java.awt.Color;

/**
 *
 * @author Администратор
 */
public class ColorUtils {
    public static Color getNegate(Color c) {
	int r = Color.WHITE.getRed() - c.getRed();
	int g = Color.WHITE.getGreen() - c.getGreen();
	int b = Color.WHITE.getBlue() - c.getBlue();
	return new Color(r, g, b);
    }
}
