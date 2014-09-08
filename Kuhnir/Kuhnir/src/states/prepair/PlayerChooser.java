package states.prepair;
import java.awt.BorderLayout;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.ArrayList;
import java.util.List;

import javax.swing.DefaultComboBoxModel;
import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import controllers.PlayerCOntroller;
import dao.DAOFactory;
import dao.Player;
import dao.PlayerDAO;

public class PlayerChooser extends JPanel {
	private static final long serialVersionUID = 4410999314995125723L;
	private DefaultListModel<Player> watchListModel = new DefaultListModel<>();
	private DefaultComboBoxModel<Player> avaliableModel = new DefaultComboBoxModel<>();
	
	PlayerDAO dao = DAOFactory.getFilePlayerDAO();
	
	public PlayerChooser() {
		initComponents();
	}
	
	private void initComponents() {
		watch.setModel(watchListModel);
		avaliable.setModel(avaliableModel);
		for (Player p : dao.fetchAllPlayers())
			avaliableModel.addElement(p);
		
		watchScroll = new JScrollPane(watch);
		setLayout(new BorderLayout());
		add(watchScroll, BorderLayout.CENTER);
		add(control, BorderLayout.SOUTH);
		
		control.setLayout(new BorderLayout());
		control.add(avaliable, BorderLayout.CENTER);
		control.add(buttons, BorderLayout.EAST);
		
		buttons.setLayout(new BorderLayout());
		buttons.add(add, BorderLayout.WEST);
		buttons.add(remove, BorderLayout.EAST);
		
		add.setText("+");
		add.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				add_MouseClicked(e);
			}
		});
		
		remove.setText("-");
		remove.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				remove_MouseClicked(e);
			}
		});
	}
	
	private void add_MouseClicked(MouseEvent e) {
		Player p = (Player)avaliable.getSelectedItem();
		if (!getPlayers().contains(p)) {
			watchListModel.addElement(p);
			PlayerCOntroller.getInstance().selectPlayer(p);
		}
	}
	
	private void remove_MouseClicked(MouseEvent e) {
		Player p = watch.getSelectedValue();
		if (p == null)
			return;
		PlayerCOntroller.getInstance().unselectPlayer(p);
		watchListModel.remove(watch.getSelectedIndex());
	}
	
	public List<Player> getPlayers() {
		List<Player> result = new ArrayList<>();
		
		for (int i = 0; i < watchListModel.getSize(); i++)
			result.add(watchListModel.get(i));
		
		return result;
	}
	
	private JList<Player> watch = new JList<>();
	private JComboBox<Player> avaliable = new JComboBox<>();
	private JPanel control = new JPanel();
	private JPanel buttons = new JPanel(); 
	private JButton add = new JButton();
	private JButton remove = new JButton();
	private JScrollPane watchScroll = new JScrollPane();
}
