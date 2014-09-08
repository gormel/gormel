package states;
import javax.swing.JPanel;


public class State {
	
	private JPanel panel = null;
	protected State(JPanel p) {
		panel = p;
	}
	
	public final JPanel getStatePanel() {
		return panel;
	}
}
