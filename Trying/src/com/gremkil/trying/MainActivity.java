package com.gremkil.trying;

import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;

import com.gremkil.server.packages.IsData;

import java.lang.annotation.Annotation;

public class MainActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
        Annotation[] as = getClass().getAnnotations();
        Annotation a = getClass().getAnnotation(IsData.class);
		setContentView(R.layout.activity_main);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

}
