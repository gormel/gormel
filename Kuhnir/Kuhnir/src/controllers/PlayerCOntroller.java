package controllers;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import core.Paring;
import dao.DAOFactory;
import dao.Player;


public class PlayerCOntroller {
	private static PlayerCOntroller inst = null;
	public static PlayerCOntroller getInstance() {
		if (inst == null)
			inst = new PlayerCOntroller();
		return inst;
	}
	
	private List<Player> players = new ArrayList<>();
	private List<Paring> parings = null;
	
	public List<Player> avaliablePlayers() {
		return DAOFactory.getFilePlayerDAO().fetchAllPlayers();
	}
	
	public synchronized List<Player> selectedPlayers() {
		Collections.sort(players, new Comparator<Player>() {
			@Override
			public int compare(Player o1, Player o2) {
				return Integer.compare(o1.score, o2.score);
			}
		});
		return players;
	}
	
	public Player addPlayer(String name) {
		DAOFactory.getFilePlayerDAO().addPlayer(name);
		return DAOFactory.getFilePlayerDAO().findPlayer(name);
	}
	
	public synchronized void selectPlayer(Player p) {
		if (p == null)
			return;
		players.add(p);
	}
	
	public synchronized void unselectPlayer(Player p) {
		if (players.contains(p))
			players.remove(p);
	}
	
	private synchronized List<Paring> pair() {
		List<Paring> result = new ArrayList<>();
		
		if (players.size() % 2 == 1)
			players.add(Player.BYE);
		Collections.sort(players, new Comparator<Player>() {
			@Override
			public int compare(Player o1, Player o2) {
				if (o1.score == o2.score)
					return Integer.compare(o1.totalScore, o2.totalScore);
				return Integer.compare(o1.score, o2.score);
			}		
		});
		
		for (int i = 0; i < players.size() - 1; i += 2) {
			Paring par = new Paring();
			par.player1 = players.get(i);
			par.player2 = players.get(i + 1);
			result.add(par);
		}
		players.remove(Player.BYE);
		return result;
	}
	
	public synchronized List<Paring> getParings(boolean repair) {
		if (repair || parings == null)
			parings = pair();
		Collections.sort(parings, new Comparator<Paring>() {
			@Override
			public int compare(Paring o1, Paring o2) {
				return -(o1.player1.score + o1.player2.score - o2.player1.score - o2.player2.score);
			}
		});
		return parings;
	}
}
