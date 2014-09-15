package states.settings;

import java.awt.BorderLayout;
import java.awt.GridLayout;
import java.util.Date;

import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JSpinner;
import javax.swing.SpinnerDateModel;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

import org.joda.time.DateTime;
import org.joda.time.LocalDate;
import org.joda.time.LocalDateTime;
import org.joda.time.Period;

import controllers.SettingsController;

public class SettingsPanel extends JPanel {
	private static final long serialVersionUID = 2445656545415150478L;
	private static final LocalDateTime startTime = new LocalDateTime(1970, 1, 1, 0, 0);
	
	public SettingsPanel() {
		initComponents();
	}
	
	private void initComponents() {
		setLayout(new GridLayout(10, 2));
		
		p.setLayout(new BorderLayout());
		p.add(new JLabel("Tour time"), BorderLayout.WEST);
		timePicker.setModel(new SpinnerDateModel());
		timePicker.setEditor(new JSpinner.DateEditor(timePicker, "KK:mm:ss"));
		Date d = startTime.plus(SettingsController.getInstance().getTourTime()).toDate();
		timePicker.setValue(d);
		timePicker.addChangeListener(new ChangeListener() {
			
			@Override
			public void stateChanged(ChangeEvent arg0) {
				timePicker_StateChanged(arg0);
			}
		});
		p.add(timePicker, BorderLayout.EAST);
		add(p);
	}
	
	private void timePicker_StateChanged(ChangeEvent e) {
		Date d = (Date)timePicker.getValue();
		LocalDateTime newDateTime = new LocalDateTime(d);
		SettingsController.getInstance().setTourTime(new Period(startTime, newDateTime));
	}
	
	private JSpinner timePicker = new JSpinner();
	private JPanel p = new JPanel();
}
