package com.gremkil.trying;

import android.annotation.TargetApi;
import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorEventListener2;
import android.hardware.SensorManager;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.MenuItem;

import java.nio.Buffer;

public class MainActivity extends Activity {
    private SensorManager sensorManager;
    private Sensor rotationSensor;

    private final float[] sensorValues = new float[5];
    private final SensorEventListener sensorListener = new SensorEventListener() {
        @Override
        public void onSensorChanged(SensorEvent sensorEvent) {
            System.arraycopy(sensorEvent.values, 0, sensorValues, 0, sensorValues.length);
            getGameView().onRotate(sensorValues);
        }

        @Override
        public void onAccuracyChanged(Sensor sensor, int i) {}
    };

    @TargetApi(Build.VERSION_CODES.JELLY_BEAN)
    @Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
        sensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
        rotationSensor = sensorManager.getDefaultSensor(Sensor.TYPE_ROTATION_VECTOR);
		setContentView(R.layout.activity_main);
	}

    @Override
    protected void onResume() {
        super.onResume();
        sensorManager.registerListener(sensorListener, rotationSensor, SensorManager.SENSOR_DELAY_GAME);
    }

    @Override
    protected void onPause() {
        super.onPause();
        sensorManager.unregisterListener(sensorListener);
    }

    @Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

    @Override
    public boolean onMenuItemSelected(int featureId, MenuItem item) {
        if (item.getItemId() == R.id.action_settings) {}
        else {
            int color = -1;
            switch (item.getItemId()) {
                case R.id.action_green:
                    color = Color.GREEN;
                    break;
                case R.id.action_red:
                    color = Color.RED;
                    break;
                case R.id.action_white:
                    color = Color.WHITE;
                    break;
            }
            getGameView().setColor(color);
        }
        return super.onMenuItemSelected(featureId, item);
    }

    private GameView getGameView() {
        return (GameView) findViewById(R.id.game);
    }
}
