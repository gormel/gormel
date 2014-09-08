package states.addresult;

import java.awt.GridLayout;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JPanel;

import controllers.PlayerCOntroller;
import controllers.StateController;
import core.Paring;

public class AddResultPanel extends JPanel {
	private static final long serialVersionUID = 3864881668186771719L;
	
	private JComboBox<Paring> pairs = new JComboBox<>();
	private JButton button30 = new JButton();
	private JButton button11 = new JButton();
	private JButton button03 = new JButton();
	
	public AddResultPanel() {
		initComponents();
	}
	
	private void initComponents() {
		DefaultComboBoxModel<Paring> model = new DefaultComboBoxModel<Paring>();
		for (Paring p : PlayerCOntroller.getInstance().getParings(false))
			model.addElement(p);
		pairs.setModel(model);
		
		button30.setText("3 - 0");
		button30.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				button30_mouseClicked(e);
			}
		});
		
		button11.setText("1 - 1");
		button11.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				button11_mouseClicked(e);
			}
		});
		
		button03.setText("0 - 3");
		button03.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				button03_mouseClicked(e);
			}
		});
		
		setLayout(new GridLayout(4, 1));
		add(button30);
		add(button11);
		add(button03);
		add(pairs);
	}
	
	private void button30_mouseClicked(MouseEvent e) {
		if (pairs.getSelectedItem() == null)
			return;
		((Paring)pairs.getSelectedItem()).player1.tourScore = 3;
		((Paring)pairs.getSelectedItem()).player2.tourScore = 0;
		StateController.getInstance().pop();
	}
	
	private void button11_mouseClicked(MouseEvent e) {
		if (pairs.getSelectedItem() == null)
			return;
		((Paring)pairs.getSelectedItem()).player1.tourScore = 1;
		((Paring)pairs.getSelectedItem()).player2.tourScore = 1;
		StateController.getInstance().pop();
	}

	private void button03_mouseClicked(MouseEvent e) {
		if (pairs.getSelectedItem() == null)
			return;
		((Paring)pairs.getSelectedItem()).player1.tourScore = 0;
		((Paring)pairs.getSelectedItem()).player2.tourScore = 3;
		StateController.getInstance().pop();
	}
}
