package states.tourresult;

import java.awt.GridLayout;

import javax.swing.DefaultListModel;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import controllers.PlayerCOntroller;
import controllers.StateController;
import controllers.StateUpdateListener;
import core.Paring;

public class TourResultPanel extends JPanel {
	private static final long serialVersionUID = -2428636550604423592L;
	
	public TourResultPanel() {
		initComponents();
	}
	
	private void initComponents() {
		watch.setModel(watchModel);
		setLayout(new GridLayout(1, 1));
		watchScroll = new JScrollPane(watch);
		add(watchScroll);
		updateScore();
		StateController.getInstance().addUpdateListener(new StateUpdateListener() {
			
			@Override
			public void onStateUpdate() {
				updateScore();
			}
		});
	}
	
	private void updateScore() {		
		watchModel.clear();
		for (Paring p : PlayerCOntroller.getInstance().getParings(false)) {
			String value = String.format("%s %s - %s", p, p.player1.tourScore, p.player2.tourScore);
			watchModel.addElement(value);
		}
	}
	
	private JList<String> watch = new JList<>();
	private DefaultListModel<String> watchModel = new DefaultListModel<String>();
	
	private JScrollPane watchScroll;
}
