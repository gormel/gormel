package states.tournamentresult;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import controllers.PlayerCOntroller;
import controllers.StateController;
import dao.Player;

import states.StatePanel;

public class TournamentResultStatePanel extends StatePanel {
	private static final long serialVersionUID = -1439019139197964127L;

	public TournamentResultStatePanel() {
		super(new TournamentResultPanel());
		initComponents();
	}

	private void initComponents() {
		addButton("OK", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				ok_MouseClicked(e);
			}
		});
	}
	
	private void ok_MouseClicked(MouseEvent e) {
		for (Player p : PlayerCOntroller.getInstance().selectedPlayers())
			p.flushScore();
		PlayerCOntroller.getInstance().updateSelected();
		PlayerCOntroller.getInstance().unselectAll();
		StateController.getInstance().pop();
	}
}
