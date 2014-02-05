package com.gremkil.trying;

import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.util.AttributeSet;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;

public class GameView extends SurfaceView {
	private SurfaceHolder holder;
	private GameManager manager;
	private List<Circule> list = new ArrayList<Circule>();
	private Paint paint = new Paint();

	public GameView(Context context, AttributeSet attrs) {
		super(context, attrs);
		initialize();
	}
	
	private void initialize() {
		paint.setStyle(Style.STROKE);
		paint.setColor(Color.WHITE);
		paint.setStrokeWidth(3);
		
		holder = getHolder();
		holder.addCallback(new Callback() {
			
			@Override
			public void surfaceDestroyed(SurfaceHolder holder) {
				manager.setRunning(false);
				try {
					manager.join();
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
			
			@Override
			public void surfaceCreated(SurfaceHolder holder) {
				manager = new GameManager(GameView.this);
				manager.setRunning(true);
				manager.start();
			}
			
			@Override
			public void surfaceChanged(SurfaceHolder holder, int format, int width,	int height) {
				
			}
		});
	}
	
	protected void onDraw(Canvas canvas) {
		canvas.drawColor(Color.BLACK);
		for (Circule c : list) {
			canvas.drawCircle(c.x, c.y, c.r, paint);
		}
	}
	
	public void onUpdate(long elapsedMilis) {
		List<Circule> removeing = new ArrayList<Circule>();
		for (Circule c : list) {
			c.r += 0.3;
			if (c.r > 40)
				removeing.add(c);
		}
		for (Circule c : removeing) {
			list.remove(c);
		}
	}
	
	@Override
	public boolean onTouchEvent(MotionEvent event) {
		synchronized (getHolder()) {
			if (event.getAction() == MotionEvent.ACTION_DOWN) {
				return true;
			}
			switch (event.getAction()) {
			case MotionEvent.ACTION_DOWN:
			case MotionEvent.ACTION_MOVE:
				list.add(new Circule(event.getX(), event.getY()));
				return true;
			}
			return false;
		}
	}
}

class Circule {
	public float x;
	public float y;
	public float r;
	
	public Circule(float x, float y) {
		this.x = x;
		this.y = y;
		this.r = 0;
	}
}
