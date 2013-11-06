package com.example.acsgame;

import android.R.bool;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;
import java.util.Random;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;
import android.hardware.SensorManager;
import android.util.Log;
import android.view.MotionEvent;
 public class Sprite {
       /**Рядков в спрайте = 4*/
    private static final int BMP_ROWS = 4;
    
    /**Колонок в спрайте = 3*/
    private static final int BMP_COLUMNS = 3;
    
    /**Объект класса GameView*/
    private GameView gameView;
    
    /**Картинка*/
    private Bitmap bmp;
    
    /**Позиция по Х=0*/
    private static int x = 5;
    
    /**Позиция по У=0*/
    private static int y = 0;
    
    /**Скорость по Х=5*/
    private static int xSpeed = 0;
    
    private static int ySpeed = 0;
    
    /**Текущий кадр = 0*/
    private int currentFrame = 0;
    
    /**Ширина*/
    private int width;
    
    /**Ввыоста*/
    private int height;
    final String tag = "IBMEyes";
    private static boolean end; 

    
    /**Конструктор*/
    public static void setxSpeed(int rasx)
    {
    	xSpeed=rasx;
    }
    public static void setySpeed(int rasy)
    {
    	ySpeed=rasy;
    }
    public Sprite(GameView gameView, Bitmap bmp) 
     {
    		this.gameView = gameView;
           this.bmp = bmp;
           this.width = bmp.getWidth() / BMP_COLUMNS;
           this.height = bmp.getHeight() / BMP_ROWS;
           end=false;
      //     Random rnd = new Random();
      //     xSpeed = rnd.nextInt(10)-5;
      //     ySpeed = rnd.nextInt(10)-5;
     }
    public static boolean getend()
    {
    	return end;
    }
    public static int getX()
    {
		return x;
    }
    public static int getY()
    {
		return y;
    }
     /**Перемещение объекта, его направление*/
     private void update() 
     {
         if (x >= gameView.getWidth() - width - xSpeed || x + xSpeed <= 0) 
         {
             xSpeed = -xSpeed;
             end=true;
         }
         
         x = x + xSpeed;
         
         if (y >= gameView.getHeight() - height - ySpeed || y + ySpeed <= 0) 
         {
             ySpeed = -ySpeed;
             end=true;
         }
         
         y = y + ySpeed;
         currentFrame = ++currentFrame % BMP_COLUMNS;
     }
     public static void setend()
     {
		end=false;    	 
     }

     /**Рисуем наши спрайты
     * @return */
     public void onDraw(Canvas canvas) 
     {
         update();
         int srcX = currentFrame * width;
         int srcY = getAnimationRow() * height;
         if (xSpeed==0 & ySpeed==0)
         {
        	 srcY = 0 * height;
        	 srcX=1 * width;
         }
         Rect src = new Rect(srcX, srcY, srcX + width, srcY + height);
         Rect dst = new Rect((int)x, (int)y, (int)x + width, (int)y + height);
         canvas.drawBitmap(bmp, src, dst, null);
     }
  // direction = 0 up, 1 left, 2 down, 3 right,
  // animation = 3 up, 1 left, 0 down, 2 right
  int[] DIRECTION_TO_ANIMATION_MAP = { 3, 1, 0, 2 };
   private int getAnimationRow() {
   double dirDouble = (Math.atan2(xSpeed, ySpeed) / (Math.PI / 2) + 2);
   int direction = (int) Math.round(dirDouble) % BMP_ROWS;
   return DIRECTION_TO_ANIMATION_MAP[direction];
  }
   
	}