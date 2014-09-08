package states;

import java.awt.BorderLayout;
import java.awt.GridLayout;
import java.awt.event.MouseListener;

import javax.swing.JButton;
import javax.swing.JPanel;

public class StatePanel extends JPanel {
	private static final long serialVersionUID = -5839411360902856669L;
	protected JPanel action;
	private JPanel buttons = new JPanel();
	private GridLayout gridLayout = new GridLayout(1, 0);
	
	public StatePanel(JPanel actionPanel) {
		action = actionPanel;
		initComponents();
	}
	
	private void initComponents() {
		setLayout(new BorderLayout());
		add(action, BorderLayout.CENTER);
		add(buttons, BorderLayout.SOUTH);
		buttons.setLayout(gridLayout);
	}
	
	protected void addButton(String text, MouseListener listener) {
		JButton b = new JButton();
		b.setText(text);
		if (listener != null)
			b.addMouseListener(listener);
		gridLayout.setColumns(gridLayout.getColumns() + 1);
		buttons.add(b);
	}
}
