package states.addresult;

import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import controllers.StateController;
import states.StatePanel;

public class AddResultStatePanel extends StatePanel {
	private static final long serialVersionUID = -5977327374204102310L;

	public AddResultStatePanel() {
		super(new AddResultPanel());
		initComponents();
	}
	
	private void initComponents() {
		addButton("OK", new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				ok_mouseClicked(e);
			}
		});
	}
	
	private void ok_mouseClicked(MouseEvent e) {
		StateController.getInstance().pop();
	}

}
