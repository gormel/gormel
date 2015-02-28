package com.gremkil.trying;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.Rect;
import android.graphics.RectF;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;

public class GameView extends SurfaceView {
    private final static String TAG = SurfaceView.class.getName();

	private SurfaceHolder holder;
	private GameManager manager;
	private List<Circule> list = new ArrayList<Circule>();
	private Paint paint = new Paint();

    private Animation animation = null;
    private Sprite sprite = null;
    private RectF destination = new RectF();

	public GameView(Context context, AttributeSet attrs) {
		super(context, attrs);
		initialize();
	}

    private void initialize() {
		paint.setStyle(Style.STROKE);
		paint.setColor(Color.WHITE);
		paint.setStrokeWidth(3);

        InputStream is = null;
        try {
            is = getContext().getAssets().open("sprite.png");
            Bitmap big = BitmapFactory.decodeStream(is);
            sprite = Sprite.load(big, 5, 4);
            animation = new Animation(sprite, 100, 5, 9);
        } catch (IOException ex) {
            throw new RuntimeException(ex);
        } finally {
            IOUtils.close(is);
        }

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
			public void surfaceChanged(SurfaceHolder holder, int format, int width,	int height) {}
		});
	}
	
	public void onUpdate(float elapsedMs) {
        animation.update(elapsedMs);
//		List<Circule> removeing = new ArrayList<Circule>();
//		for (Circule c : list) {
//			c.r += 0.03 * elapsedMs;
//			if (c.r > 40)
//				removeing.add(c);
//		}
//		for (Circule c : removeing) {
//			list.remove(c);
//		}
	}

    protected void onDraw(Canvas canvas) {
        float x = 0;
        float y = 0;
        float scale = 3;

        canvas.drawColor(Color.rgb(100, 149, 237));
        destination.set(x, y, x + sprite.getWidth() * scale, y + sprite.getHeight() * scale);
        canvas.drawBitmap(animation.getFrame(), null, destination, null);
//        for (Circule c : list) {
//            //canvas.drawCircle(c.x, c.y, c.r, paint);
//            canvas.drawRect(new RectF(c.x - c.r /2, c.y - c.r / 2, c.x + c.r, c.y + c.r), paint);
//        }
    }
	
	@Override
	public boolean onTouchEvent(MotionEvent event) {
		synchronized (getHolder()) {
			switch (event.getAction()) {
			case MotionEvent.ACTION_DOWN:
			case MotionEvent.ACTION_MOVE:
				list.add(new Circule(event.getX(), event.getY()));
				return true;
			}
			return false;
		}
	}

    public void setColor(int color) {
        paint.setColor(color);
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
}

