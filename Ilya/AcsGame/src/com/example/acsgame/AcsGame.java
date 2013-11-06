package com.example.acsgame;


import android.hardware.SensorListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.app.Activity;
import android.util.Log;
import android.view.Menu;
import android.view.MotionEvent;
import android.view.Window;
import android.content.pm.ActivityInfo;
import android.view.View.OnTouchListener;
import android.view.View;



@SuppressWarnings("deprecation")
public class AcsGame extends Activity implements SensorListener,OnTouchListener {

	  final String tag = "IBMEyes";
	    SensorManager sm = null;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		GameView gv=new GameView(this);
		gv.setOnTouchListener(this);
		setContentView(gv);;
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.acs_game, menu);
		return true;
	}

	@Override
	public void onAccuracyChanged(int sensor, int accuracy) {
		Log.d(tag,"onAccuracyChanged: " + sensor + ", accuracy: " + accuracy);

		// TODO Auto-generated method stub
		
	}

	public void onSensorChanged(int sensor, float[] values) {
		float x=0;
		float y=0;
		float z=0;
		synchronized (this) {
            Log.d(tag, "onSensorChanged: " + sensor + ", x: " +
values[0] + ", y: " + values[1] + ", z: " + values[2]);
            if (sensor == SensorManager.SENSOR_ACCELEROMETER) {
                x=values[0];
                y=values[1];
                z=values[2];
            }
            if (x>2||x<-2)
            {
            Sprite.setxSpeed((int) y);
            }
            if (y<-4 || y>-2)
            {
            Sprite.setySpeed((int) x);
            }
        }
		// TODO Auto-generated method stub
		
	}

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		// TODO Auto-generated method stub
		return false;
	}
	@Override
    protected void onResume() {
        super.onResume();
      // register this class as a listener for the orientation and accelerometer sensors
        sm.registerListener(this,
                SensorManager.SENSOR_ORIENTATION |SensorManager.SENSOR_ACCELEROMETER,
                SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onStop() {
        // unregister listener
        sm.unregisterListener(this);
        super.onStop();
    }
}
