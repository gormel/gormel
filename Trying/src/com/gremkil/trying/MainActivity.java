package com.gremkil.trying;

import android.annotation.TargetApi;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.MenuItem;

import java.nio.ByteBuffer;
import java.util.Arrays;

public class MainActivity extends Activity {
    @TargetApi(Build.VERSION_CODES.GINGERBREAD)
    @Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
        //test area

        byte[] buff = new byte[] { 1, 2, 3, 4, 5 };
        ByteBuffer buffer = ByteBuffer.wrap(buff);
        byte b = buffer.get();
        short s = buffer.getShort();
        byte[] arr = Arrays.copyOfRange(buffer.array(), buffer.position(), buffer.capacity());

		setContentView(R.layout.activity_main);
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
            ((GameView) findViewById(R.id.game)).setColor(color);
        }
        return super.onMenuItemSelected(featureId, item);
    }
}
