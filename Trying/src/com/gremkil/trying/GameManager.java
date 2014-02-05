package com.gremkil.trying;

import android.graphics.Canvas;

public class GameManager extends Thread {
	
	GameView view;
	private boolean running = false;
	
	private static long fps = 60;
	
	public GameManager(GameView view) {
		this.view = view;
	}
	
	@Override
	public void run() {
		long lastTime = System.currentTimeMillis();
		long time = 0;
		while (running) {
			long beforeTime = System.currentTimeMillis();
			do {
				time = System.currentTimeMillis();
				synchronized (view.getHolder()) {
					view.onUpdate(time - beforeTime);
				}
				beforeTime = time;
			} while (time - lastTime < 1000 / fps);
			
			Canvas canvas = view.getHolder().lockCanvas();
			synchronized (view.getHolder()) {
				view.onDraw(canvas);
			}
			view.getHolder().unlockCanvasAndPost(canvas);
			
		}
	}
	
	public void setRunning(boolean running) {
		this.running = running;
	}
}
