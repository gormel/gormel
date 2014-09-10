package states.tournamentresult;

import java.awt.GridLayout;

import javax.swing.DefaultListModel;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import controllers.PlayerCOntroller;
import dao.Player;

public class TournamentResultPanel extends JPanel {
	private static final long serialVersionUID = -7048456018426180376L;
	public TournamentResultPanel() {
		initComponents();
	}
	
	private void initComponents() {
		setLayout(new GridLayout(1, 1));
		add(watchScroll);
		
		for (Player p : PlayerCOntroller.getInstance().selectedPlayers()) {
			watchModel.addElement(String.format("%s - %s", p, p.score));
		}
	}
	
	private DefaultListModel<String> watchModel = new DefaultListModel<String>();
	private JList<String> watch = new JList<String>(watchModel);
	private JScrollPane watchScroll = new JScrollPane(watch);
}
