package states.process;

import java.awt.GridLayout;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import controllers.PlayerCOntroller;
import core.Paring;

public class PairWatchPanel extends JPanel {
	private static final long serialVersionUID = -4742285464330316243L;	
	public PairWatchPanel() {
		initComponents();
	}
	
	private void initComponents() {
		List<Paring> pairs = PlayerCOntroller.getInstance().getParings(true);
		pairsList.setModel(listModel);
		setLayout(new GridLayout(1, 1));
		listScroler = new JScrollPane(pairsList);
		
		for (Paring p : pairs) {
			String value = p.player1.name + " - " + p.player2.name + " " +
					String.valueOf(p.player1.score) + " - " + String.valueOf(p.player2.score);
			listModel.addElement(value);
		}
		add(listScroler);
	}
	
	private JList<String> pairsList = new JList<String>();
	private DefaultListModel<String> listModel = new DefaultListModel<String>();
	
	private JScrollPane listScroler;
}
