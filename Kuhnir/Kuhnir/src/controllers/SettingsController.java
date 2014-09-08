package controllers;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Properties;

import org.joda.time.Period;

public class SettingsController {
	private static SettingsController inst = null;
	public static SettingsController getInstance() {
		if (inst == null)
			inst = new SettingsController();
		return inst;
	}
	
	private Period tourTime = new Period(0, 0, 10, 0);
	
	private static final File FILE_DIR = new File("./conf");
	private SettingsController() {
		loadFromFile();
		save();
	}
	
	private void loadFromFile() {
		String fileName = FILE_DIR + "/config.xml";
		File settingsFile = new File(fileName);
		if (settingsFile.exists()) {
			Properties prop = new Properties();
			try {
				prop.loadFromXML(new FileInputStream(settingsFile));
				String tourTime = prop.getProperty("tourTime");
				this.tourTime = Period.parse(tourTime);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}
	
	public void load() {
		loadFromFile();
	}
	
	public void save() {
		Properties prop = new Properties();
		prop.setProperty("tourTime", tourTime.toString());
		
		File file = new File(FILE_DIR + "/config.xml");
		if (!file.exists())
			try {
				FILE_DIR.mkdirs();
				file.createNewFile();
			} catch (IOException e1) {
				e1.printStackTrace();
			}
		try {
			prop.storeToXML(new FileOutputStream(file), "");
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public Period getTourTime() {
		return tourTime;
	}
	
	public void setTourTime(Period tourTime) {
		this.tourTime = tourTime;
	}
}
