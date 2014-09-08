package states.tourresult;

import java.awt.GridLayout;

import javax.swing.JLabel;
import javax.swing.JPanel;

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
		updateScore();
		StateController.getInstance().addUpdateListener(new StateUpdateListener() {
			
			@Override
			public void onStateUpdate() {
				updateScore();
			}
		});
	}
	
	private void updateScore() {
		removeAll();
		setLayout(new GridLayout(PlayerCOntroller.getInstance().getParings(false).size(), 2));
		for (Paring p : PlayerCOntroller.getInstance().getParings(false)) {
			add(new JLabel(p.toString()));
			add(new JLabel(String.format("%s - %s", p.player1.tourScore, p.player2.tourScore)));
		}
		revalidate();
	}
}
