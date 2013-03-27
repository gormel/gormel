/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import java.awt.Color;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.imageio.ImageIO;
import scheme.Scheme;
import scheme.SchemeString;
import scheme.elements.SchemeElementA;
import scheme.elements.SchemeElementB;
import scheme.elements.SchemeElementC;
import scheme.elements.SchemeElementD;
import view.ElementViewer;
import view.SchemeResultViewer;
import view.SchemeViewer;
import view.Viewer;

/**
 *
 * @author User
 */
public class Main {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws IOException {
//        try {
//            // TODO code application logic here
//
//            BufferedImage im = ImageIO.read(Main.class.getResource("input.bmp"));
//            BufferedImage t1b = ImageIO.read(Main.class.getResource("t1b.bmp"));
//            BufferedImage t1w = ImageIO.read(Main.class.getResource("t1w.bmp"));
//            BufferedImage t2b = ImageIO.read(Main.class.getResource("t2b.bmp"));
//            BufferedImage t2w = ImageIO.read(Main.class.getResource("t2w.bmp"));
//            BufferedImage result = new BufferedImage(im.getWidth() * t1b.getWidth() * 3 / 4, im.getHeight() * t1b.getHeight(), BufferedImage.TYPE_INT_RGB);
//            Graphics resulGraphics = result.getGraphics();
//            resulGraphics.fillRect(0, 0, result.getWidth(), result.getHeight());
//            
//            for (int y = 0; y < im.getHeight(); y++) {
//                BufferedImage b = t2b;
//                BufferedImage w = t1w;
//                
//                int x = 0;
//                if (y % 2 == 1) {
//                    x = 1;
//                    b = t1b;
//                    w = t2w;
//                }
//                for (; x < im.getWidth(); x += 2) {
//                    Color pixel = new Color(im.getRGB(x, y));
//                    BufferedImage icon = b;
//                    if (pixel.equals(Color.white)) {
//                        icon = w;
//                    }
//                    
//                    int drawX = x * (icon.getWidth() * 3 / 4);
//                    int drawY = y * (icon.getHeight());
//                    resulGraphics.drawImage(icon, drawX, drawY, null);
//                }
//            }
//            
//            File outFile = new File("result.bmp");
//            ImageIO.write(result, "bmp", outFile);
//        } catch (IOException ex) {
//            Logger.getLogger(Main.class.getName()).log(Level.SEVERE, null, ex);
//        }
                
        Scheme s = new Scheme();
	s.addString(Color.red);
	s.addString(Color.blue);
	s.addString(Color.green);
	s.addString(Color.green);
	s.addString(Color.blue);
	s.addString(Color.red);
	
	s.getElements().add(new SchemeElementA(s.getString(0), s.getString(1)));
	s.getElements().add(new SchemeElementB(s.getString(2), s.getString(3)));
	s.getElements().add(new SchemeElementA(s.getString(4), s.getString(5)));
	
	s.getElements().add(new SchemeElementC(s.getString(1), s.getString(2)));
	s.getElements().add(new SchemeElementD(s.getString(3), s.getString(4)));
	
	s.getElements().add(new SchemeElementA(s.getString(0), s.getString(1)));
	s.getElements().add(new SchemeElementB(s.getString(2), s.getString(3)));
	s.getElements().add(new SchemeElementA(s.getString(4), s.getString(5)));
	
	s.getElements().add(new SchemeElementC(s.getString(1), s.getString(2)));
	s.getElements().add(new SchemeElementD(s.getString(3), s.getString(4)));	
	
	s.getElements().add(new SchemeElementA(s.getString(0), s.getString(1)));
	s.getElements().add(new SchemeElementB(s.getString(2), s.getString(3)));
	s.getElements().add(new SchemeElementA(s.getString(4), s.getString(5)));
	
	s.getElements().add(new SchemeElementC(s.getString(1), s.getString(2)));
	s.getElements().add(new SchemeElementD(s.getString(3), s.getString(4)));
	
	s.getElements().add(new SchemeElementA(s.getString(0), s.getString(1)));
	s.getElements().add(new SchemeElementB(s.getString(2), s.getString(3)));
	s.getElements().add(new SchemeElementA(s.getString(4), s.getString(5)));
	
	s.getElements().add(new SchemeElementC(s.getString(1), s.getString(2)));
	s.getElements().add(new SchemeElementD(s.getString(3), s.getString(4)));
	Viewer v = new SchemeViewer(s);
	Viewer rv = new SchemeResultViewer(s);
	
        Image image = v.getPicture();
        BufferedImage im = new BufferedImage(image.getWidth(null), image.getHeight(null), BufferedImage.TYPE_INT_BGR);
        im.createGraphics().drawImage(image, 0, 0, null);
        
        ImageIO.write(im, "bmp", new File("file.bmp"));
	
	Image result = rv.getPicture();
        im = new BufferedImage(result.getWidth(null), result.getHeight(null), BufferedImage.TYPE_INT_BGR);
        im.createGraphics().drawImage(result, 0, 0, null);
        
        ImageIO.write(im, "bmp", new File("result.bmp"));
    }
}
