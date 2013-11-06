package com.example.acsgame;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.TextView;
 @SuppressLint("WrongCall")
public class GameView extends SurfaceView implements OnTouchListener  {  
    /**Загружаем спрайт*/
    private Bitmap bmp;
    TextView tv;
    float x;
    float y;
    /**Поле рисования*/
    private SurfaceHolder holder;
    
    /**объект класса GameView*/
    private GameManager gameLoopThread;
    
    /**Объект класса Sprite*/
    private Sprite sprite;
   
    /**Конструктор*/
    public GameView(Context context) 
    {
    	  
          super(context);
          gameLoopThread = new GameManager(this);
          holder = getHolder();
          
          /*Рисуем все наши объекты и все все все*/
          holder.addCallback(new SurfaceHolder.Callback() 
          {
                        /*** Уничтожение области рисования */
                 public void surfaceDestroyed(SurfaceHolder holder) 
                 {
                        boolean retry = true;
                        gameLoopThread.setRunning(false);
                        while (retry) 
                        {
                               try 
                               {
                                     gameLoopThread.join();
                                     retry = false;
                               } catch (InterruptedException e) 
                               {
                               }
                        }
                 }

                 /** Создание области рисования */
                 public void surfaceCreated(SurfaceHolder holder) 
                 {
                        gameLoopThread.setRunning(true);
                        gameLoopThread.start();
                 }

                 /** Изменение области рисования */
                 public void surfaceChanged(SurfaceHolder holder, int format,
                               int width, int height) 
                 {
                 }
          });
          bmp = BitmapFactory.decodeResource(getResources(), R.drawable.image);
          sprite = new Sprite(this,bmp);
    }

    /**Функция рисующая все спрайты и фон
     * @return */
   
    protected void onDraw(Canvas canvas) 
    {
          canvas.drawColor(Color.BLACK);
          sprite.onDraw(canvas);
    }

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		
	      return false;
		// TODO Auto-generated method stub
	}

	
   
}