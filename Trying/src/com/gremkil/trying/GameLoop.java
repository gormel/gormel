package com.gremkil.trying;

import android.graphics.Canvas;
import android.util.Log;

public class GameLoop extends Thread {
    public static final long FPS = 60;
    public static final long NS_PER_FRAME = 1000000000 / FPS;

	GameView view;
	private boolean running = false;
	
	private final static long fps = 60;
	
	public GameLoop(GameView view) {
		this.view = view;
	}
	
	@Override
	public void run() {
        long time = 0;
		while (running) {
			long beforeTime = System.nanoTime();
            long lastUpdateTime = beforeTime;
			do {
				time = System.nanoTime();
                long dt = time - lastUpdateTime;
                if (dt < NS_PER_FRAME / 10) { continue; }
                if (dt <= 0 || dt > NS_PER_FRAME) {
                    dt = NS_PER_FRAME;
                }
				synchronized (view.getHolder()) {
					view.onUpdate(dt / 1000000.0f);
                }
				lastUpdateTime = time;
			} while (time - beforeTime < NS_PER_FRAME);
			
			Canvas canvas = view.getHolder().lockCanvas();
			if (canvas != null) {
                synchronized (view.getHolder()) {
                    view.onDraw(canvas);
                }
                view.getHolder().unlockCanvasAndPost(canvas);
            }
		}
	}
	
	public void setRunning(boolean running) {
		this.running = running;
	}
}
