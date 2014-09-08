package states.prepair;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import states.StatePanel;
import states.process.ProcessState;
import states.settings.SettingsState;
import controllers.StateController;


public class PrepirStatePanel extends StatePanel {
	private static final long serialVersionUID = 8869281401234348689L;
	
	public PrepirStatePanel() {
		super(new PlayerChooser());
		initComponents();
	}
	
	private void initComponents() {
		
		addButton("Settings", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				settings_MouceClick(e);
			}
		});
		
		addButton("Start", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				start_MouseClick(e);
			}
		});
		
		addButton("Exit", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				exit_MouseClick(e);
			}
		});
	}
	
	private void start_MouseClick(MouseEvent e) {
		StateController.getInstance().push(new ProcessState());
	}
	
	private void settings_MouceClick(MouseEvent e) {
		StateController.getInstance().push(new SettingsState());
	}
	
	private void exit_MouseClick(MouseEvent e) {
		System.exit(0);
	}
}
