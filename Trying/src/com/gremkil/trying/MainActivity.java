package com.gremkil.trying;

import android.annotation.TargetApi;
import android.os.Build;
import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;

import com.gremkil.server.packages.IsData;

import java.lang.annotation.Annotation;
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

}
