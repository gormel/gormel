package core;
import dao.Player;


public class Paring {
	public Player player1;
	public Player player2;
	
	@Override
	public String toString() {
		return String.format("%s - %s", player1, player2);
	}
}
