package dao;
import java.util.List;

public interface PlayerDAO {
	List<Player> fetchAllPlayers();
	Player findPlayer(String name);
	void addPlayer(String name);
	void UpdatePlayer(Player player);
	void removePlayer(Player p);
}
