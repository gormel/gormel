package states.timer;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import states.StatePanel;
import states.addresult.AddResultState;
import controllers.StateController;

public class TimerStatePanel extends StatePanel {
	private static final long serialVersionUID = -8380934444972553179L;

	public TimerStatePanel() {
		super(new TimerPanel());
		initComponents();
	}

	private void initComponents() {
		addButton("Add result", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				addResult_mouseClicked(e);
			}
		});
		
		addButton("End tour", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				endTour_mouseClicked(e);
			}
		});
	}
	
	private void addResult_mouseClicked(MouseEvent e) {
		StateController.getInstance().push(new AddResultState());
	}
	
	private void endTour_mouseClicked(MouseEvent e) {
		TimerPanel action = (TimerPanel) super.action;
		action.endTour();
	}
}
