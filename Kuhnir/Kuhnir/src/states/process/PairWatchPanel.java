package states.process;

import java.awt.GridLayout;
import java.util.List;

import javax.swing.JLabel;
import javax.swing.JPanel;

import controllers.PlayerCOntroller;
import core.Paring;

public class PairWatchPanel extends JPanel {
	private static final long serialVersionUID = -4742285464330316243L;	
	public PairWatchPanel() {
		initComponents();
	}
	
	private void initComponents() {
		List<Paring> pairs = PlayerCOntroller.getInstance().getParings(true);
		
		setLayout(new GridLayout(pairs.size(), 2));
		for (Paring p : pairs) {
			add(new JLabel(p.player1.name + " - " + p.player2.name));
			add(new JLabel(String.valueOf(p.player1.score) + " - " + String.valueOf(p.player2.score)));
		}
	}
}
