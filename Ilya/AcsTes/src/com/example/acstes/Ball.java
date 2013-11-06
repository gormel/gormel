package com.example.acstes;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.view.View;

public class Ball extends View {
	float x;
	float y;
	
	public Ball(Context context) {
		super(context);
		x=0;
		y=10;
		// TODO Auto-generated constructor stub
	}
	public float getX()
	{
		return x;
	}
	public void setX(float ix)
	{
		x=ix;
	}
	public void setY(float iy)
	{
		y=iy;
	}
	public float getY()
	{
		return y;
	}
	@Override
	protected void onDraw(Canvas canvas){
	    
		super.onDraw(canvas);
	    Paint paint = new Paint();
	    paint.setStyle(Paint.Style.FILL);

	    // закрашиваем холст белым цветом
	    paint.setColor(Color.WHITE);
	    canvas.drawPaint(paint);
	    paint.setAntiAlias(true);
	    paint.setColor(Color.YELLOW);
	    canvas.drawCircle(x, y, 10, paint);
	}

}
