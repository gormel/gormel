package states.settings;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import controllers.SettingsController;
import controllers.StateController;

import states.StatePanel;

public class SettingStatePanel extends StatePanel {
	private static final long serialVersionUID = 368581817206157087L;

	public SettingStatePanel() {
		super(new SettingsPanel());
		initComponents();
	}
	
	private void initComponents() {
		addButton("Save", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				save_MouseClicked(e);
			}
		});

		addButton("Cancel", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				cancel_MouseClicked(e);
			}
		});

		addButton("OK", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				ok_MouseClicked(e);
			}
		});
	}
	
	private void save_MouseClicked(MouseEvent e) {
		SettingsController.getInstance().save();
	}
	
	private void cancel_MouseClicked(MouseEvent e) {
		SettingsController.getInstance().load();
		StateController.getInstance().pop();
	}
	
	private void ok_MouseClicked(MouseEvent e) {
		SettingsController.getInstance().save();
		StateController.getInstance().pop();
	}

}
