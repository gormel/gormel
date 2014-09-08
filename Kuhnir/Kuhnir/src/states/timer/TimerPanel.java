package states.timer;

import java.awt.BorderLayout;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.Timer;

import org.joda.time.DateTime;
import org.joda.time.Period;
import org.joda.time.format.PeriodFormatterBuilder;

import states.tourresult.TourResultState;
import controllers.SettingsController;
import controllers.StateController;

public class TimerPanel extends JPanel {
	private static final long serialVersionUID = 1518379850704020499L;
	private DateTime startTime = DateTime.now();
	private JLabel timeLabel = new JLabel();
	private Timer timer = new Timer(100, new ActionListener() {
		
		@Override
		public void actionPerformed(ActionEvent arg0) {
			DateTime now = DateTime.now();
			Period difference = new Period(startTime, now);
			Period visible = SettingsController.getInstance().getTourTime()
					.minus(difference).toPeriod().normalizedStandard();
			if (visible.toStandardSeconds().getSeconds() <= 0) {
				endTour();
			}
			
			String value = visible.toString(new PeriodFormatterBuilder().appendHours()
							.appendSeparator(":").appendMinutes()
							.appendSeparator(":").appendSeconds().toFormatter());
			timeLabel.setText(value);
		}
	});
	
	
	public TimerPanel() {
		initComponents();
	}
	
	private void initComponents() {
		setLayout(new BorderLayout());
		timeLabel.setFont(new Font(timeLabel.getFont().getName(), Font.PLAIN, 100));
		timeLabel.setHorizontalAlignment(JLabel.CENTER);
		add(timeLabel, BorderLayout.CENTER);
		timer.start();
	}
	
	public void endTour() {
		timer.stop();
		StateController.getInstance().pop();
		StateController.getInstance().push(new TourResultState());
	}
}
