package states.process;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import controllers.StateController;
import states.StatePanel;
import states.timer.TimerState;

public class ProcessStatePanel extends StatePanel {
	private static final long serialVersionUID = 6233058653845256886L;
	public ProcessStatePanel() {
		super(new PairWatchPanel());
		initComponents();
	}
	
	private void initComponents() {
		addButton("Back", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				back_MouceClick(e);
			}
		});
		
		addButton("Strat tour", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				start_MouceClick(e);
			}
		});
		
		addButton("Repair", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				repair_MouceClick(e);
			}
		});
	}
	
	private void repair_MouceClick(MouseEvent e) {
		StateController.getInstance().pop();
		StateController.getInstance().push(new ProcessState());
	}
	
	private void start_MouceClick(MouseEvent e) {
		StateController.getInstance().pop();
		StateController.getInstance().push(new TimerState());
	}
	
	private void back_MouceClick(MouseEvent e) {
		StateController.getInstance().pop();
	}
}
