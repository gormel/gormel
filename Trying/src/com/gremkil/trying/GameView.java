package com.gremkil.trying;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Paint.Style;
import android.graphics.RectF;
import android.hardware.SensorManager;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceHolder.Callback;
import android.view.SurfaceView;

import com.gremkil.trying.core.Animation;
import com.gremkil.trying.core.Sprite;
import com.gremkil.trying.utils.IOUtils;
import com.gremkil.trying.utils.MathUtils;

public class GameView extends SurfaceView {
    private final static String TAG = SurfaceView.class.getName();

	private SurfaceHolder holder;
	private GameLoop manager;
	private List<Circule> list = new ArrayList<Circule>();
	private Paint paint = new Paint();

    private Animation animation = null;
    private Sprite sprite = null;
    private RectF destination = new RectF();

    private final float[] rotation = {0, 1, 0};
    private final float[] rotationMatrix = new float[16];
    private final float[] rotationQuternion = new float[4];

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
			public void surfaceCreated(SurfaceHolder holder) {
				manager = new GameLoop(GameView.this);
				manager.setRunning(true);
				manager.start();
			}

            @Override
            public synchronized void surfaceDestroyed(SurfaceHolder holder) {
                manager.setRunning(false);
                try {
                    manager.join();
                } catch (InterruptedException e) {
                    Log.e(GameView.class.getName(), "", e);
                }
            }

			@Override
			public void surfaceChanged(SurfaceHolder holder, int format, int width,	int height) {}
		});
	}
	
	public void onUpdate(float elapsedMs) {
        animation.update(elapsedMs);
	}

    public void onDraw(Canvas canvas) {
        float x = 0;
        float y = 0;
        float scale = 3 * getResources().getDisplayMetrics().density;

        canvas.drawColor(Color.rgb(100, 149, 237));
        destination.set(x, y, x + sprite.getWidth() * scale, y + sprite.getHeight() * scale);
        canvas.drawBitmap(animation.getFrame(), null, destination, null);
    }

    public void onRotate(float[] rotationVector) {
        synchronized (getHolder()) {
            rotation[0] = 0;
            rotation[1] = 1;
            rotation[0] = 0;
            SensorManager.getRotationMatrixFromVector(rotationMatrix, rotationVector);
            SensorManager.getQuaternionFromVector(rotationQuternion, rotationVector);
            //SensorManager.getOrientation(rotationMatrix, rotation);
            MathUtils.rotateVectorByQuaternion(rotation,
                    rotationQuternion[1], rotationQuternion[2], rotationQuternion[3], rotationQuternion[0]);
            MathUtils.norm(rotation);
            ((Activity)getContext()).setTitle(String.format("%.2f %.2f %.2f",
                    rotation[0], rotation[1], rotation[2]));
        }
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

