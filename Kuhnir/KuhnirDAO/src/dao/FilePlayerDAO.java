package dao;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class FilePlayerDAO implements PlayerDAO {
	
	private static FilePlayerDAO inst = null;
	public static FilePlayerDAO getInstance() {
		if (inst == null)
			inst = new FilePlayerDAO();
		return inst;
	}
	
	private List<Player> cashedPlayers = new ArrayList<>();
	
	public FilePlayerDAO() {
		updateCash();
	}
	
	private File[] getSourceFiles() {
		File daoFolder = new File("./dao");
		if (!daoFolder.exists()) {
			daoFolder.mkdirs();
			return new File[0];
		}
		return daoFolder.listFiles();
	}
	
	private void updateCash() {
		cashedPlayers.clear();
		for (File f : getSourceFiles()) {
			if (f.isFile()) {
				Player p = parsePlayer(f);
				cashedPlayers.add(p);
			}
		}
	}
	
	private void flush() {
		for (File f : getSourceFiles())
			try {
				Files.deleteIfExists(f.toPath());
			} catch (IOException e) {
				e.printStackTrace();
			}
		for (Player p : cashedPlayers)
			savePlayer(p);
	}
	
	private void savePlayer(Player p) {
		Properties prop = new Properties();
		prop.setProperty(Player.NAME, p.name);
		prop.setProperty(Player.TOTAL_SCORE, String.valueOf(p.totalScore));
		try {
			prop.storeToXML(new FileOutputStream(new File("./dao/" + p.name + ".xml")), null);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	private Player parsePlayer(File file) {
		Properties p = new Properties();
		try {
			p.loadFromXML(new FileInputStream(file));
		} catch (IOException e) {
			e.printStackTrace();
		}
		String name = p.getProperty(Player.NAME);
		int score = Integer.parseInt(p.getProperty(Player.TOTAL_SCORE));
		Player pl = new Player(name, 0);
		pl.totalScore = score;
		return pl;
	}
	
	@Override
	public List<Player> fetchAllPlayers() {
		return cashedPlayers;
	}

	@Override
	public Player findPlayer(String name) {
		for (Player p : cashedPlayers) {
			if (name.equals(p.name))
				return p;
		}
		return null;
	}

	@Override
	public void addPlayer(String name) {
		if (findPlayer(name) != null)
			throw new RuntimeException("Player already exist.");
		Player p = new Player(name, 0);
		cashedPlayers.add(p);
		flush();
	}

	@Override
	public void UpdatePlayer(Player player) {
		if (findPlayer(player.name) == null)
			throw new RuntimeException("Player does not exist.");
		savePlayer(player);
	}

	@Override
	public void removePlayer(Player p) {
		cashedPlayers.remove(p);
		flush();
	}
}
