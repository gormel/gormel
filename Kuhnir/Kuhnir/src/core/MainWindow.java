package core;
import java.awt.BorderLayout;
import java.awt.Frame;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

import states.prepair.PrepairState;
import controllers.StateController;
import controllers.StateUpdateListener;

public class MainWindow extends Frame {
	private static final long serialVersionUID = -7018142967304183953L;

	public static void main(String[] args) {
		MainWindow window = new MainWindow();
		window.setBounds(0, 0, 800, 500);
		window.setVisible(true);
	}
	
	public MainWindow() {
		initComponents();
	}
	
	private void initComponents() {
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(WindowEvent e) {
				System.exit(0);
			}
		});
		
		setLayout(new BorderLayout());
		
		StateController.getInstance().addUpdateListener(new StateUpdateListener() {

			@Override
			public void onStateUpdate() {
				MainWindow.this.removeAll();
				MainWindow.this.add(StateController.getInstance().peek().getStatePanel(), 
						BorderLayout.CENTER);
				MainWindow.this.setVisible(false);
				MainWindow.this.setVisible(true);
			}
			
		});
		
		StateController.getInstance().push(new PrepairState());
	}
}
