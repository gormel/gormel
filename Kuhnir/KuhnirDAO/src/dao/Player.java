package dao;

public class Player {
	public static String NAME = "Player.name";
	public static String TOTAL_SCORE = "Player.totalScore";
	public static final Player BYE = new Player("bye", 0);
	public Player(String name, int score) {
		this.name = name;
		this.score = score;
	}
	public String name;
	public int score;
	public int totalScore;
	public int tourScore;
	
	public void flushScore() {
		score += tourScore;
		totalScore += score;
		score = 0;
		tourScore = 0;
	}
	
	public void flushTour() {
		score += tourScore;
		tourScore = 0;
	}
	
	@Override
	public String toString() {
		return name;
	}
}
