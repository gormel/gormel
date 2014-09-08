import java.awt.BorderLayout;
import java.awt.Frame;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JTextField;

import dao.DAOFactory;
import dao.Player;
import dao.PlayerDAO;

public class MainFrame extends Frame {
	
	private static final long serialVersionUID = 6933207098183368327L;
	
	private PlayerDAO dao = DAOFactory.getFilePlayerDAO();
	private DefaultListModel<Player> watchListModel = new DefaultListModel<>();
	
	public static void main(String[] args) {
		MainFrame frame = new MainFrame();
		frame.setBounds(10, 10, 300, 600);
		frame.setVisible(true);
	}
	
	public MainFrame() {
		initComponents();
		updatePlayers();
	}
	
	private void initComponents() {
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(WindowEvent e) {
				System.exit(0);
			}
		});
		
		add(container);
		
		container.setLayout(new BorderLayout());
		container.add(watch, BorderLayout.CENTER);
		container.add(control, BorderLayout.SOUTH);
		
		control.setLayout(new BorderLayout());
		control.add(name, BorderLayout.CENTER);
		control.add(buttons, BorderLayout.EAST);
		
		buttons.setLayout(new BorderLayout());
		buttons.add(add, BorderLayout.WEST);
		buttons.add(remove, BorderLayout.EAST);
		
		add.setText("+");
		add.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				add_MouseClecked(e);
			}
		});
		
		remove.setText("-");
		remove.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				remove_MouseClicked(e);
			}
		});
		
		watch.setModel(watchListModel);
	}
	
	private void add_MouseClecked(MouseEvent e) {
		if ("".equals(name.getText()))
				return;
		dao.addPlayer(name.getText());
		name.setText("");
		updatePlayers();
	}
	
	private void remove_MouseClicked(MouseEvent e) {
		dao.removePlayer(watch.getSelectedValue());
		updatePlayers();
	}
	
	private void updatePlayers() {
		watchListModel.clear();
		List<Player> players = dao.fetchAllPlayers();
		for (Player p : players)
			watchListModel.addElement(p);
	}
	
	private JList<Player> watch = new JList<>();
	private JPanel container = new JPanel();
	private JPanel control = new JPanel();
	private JPanel buttons = new JPanel();
	private JTextField name = new JTextField();
	private JButton add = new JButton();
	private JButton remove = new JButton();
}
