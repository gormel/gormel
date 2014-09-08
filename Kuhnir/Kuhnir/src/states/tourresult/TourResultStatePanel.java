package states.tourresult;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import controllers.PlayerCOntroller;
import controllers.StateController;
import dao.Player;
import states.StatePanel;
import states.addresult.AddResultState;
import states.process.ProcessState;

public class TourResultStatePanel extends StatePanel {
	private static final long serialVersionUID = 6498423307660393818L;

	public TourResultStatePanel() {
		super(new TourResultPanel());
		initComponents();
	}

	private void initComponents() {
		addButton("Add result", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				add_MouseClick(e);
			}
		});
		
		addButton("Next tour", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				next_MouseClick(e);
			}
		});
		
		addButton("End tournament", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				end_MouseClick(e);
			}
		});
	}
	
	private void add_MouseClick(MouseEvent e) {
		StateController.getInstance().push(new AddResultState());
	}
	
	private void next_MouseClick(MouseEvent e) {
		for (Player p : PlayerCOntroller.getInstance().selectedPlayers()) {
			p.flushTour();
		}
		StateController.getInstance().pop();
		StateController.getInstance().push(new ProcessState());
	}
	
	private void end_MouseClick(MouseEvent e) {
		for (Player p : PlayerCOntroller.getInstance().selectedPlayers()) {
			p.flushScore();
			PlayerCOntroller.getInstance().unselectPlayer(p);
		}
		
		StateController.getInstance().pop();
	}
}
