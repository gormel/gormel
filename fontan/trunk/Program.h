#ifndef my_program
#define my_program

#include "gl\GLAux.h"
#include <iostream>
#include <unordered_map>
#include "BaseObject.h"
#include "Quatarnion.h"
#include "MouseState.h"
#include "KeyboardState.h"

#include "Geosphere.h"
#include "Fontan.h"
#include "PrimetivesScreen.h"
#include "AlphaScreen.h"
#include "BlendScreen.h"

#include "Timer.h"

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
		glClearColor(117.0 / 255, 192.0 / 255, 253.0 / 255, 1);
		screen1.Draw(timeSpend);
		screen2.Draw(timeSpend);
	}

	virtual void update(long timeSpend)
	{
		KeyboardState kb = KeyboardState::Current();
		
		if (kb.IsKeyDown('1'))
			screen.SetPrimetiveType(GL_POINTS);
		if (kb.IsKeyDown('2'))
			screen.SetPrimetiveType(GL_LINES);
		if (kb.IsKeyDown('3'))
			screen.SetPrimetiveType(GL_LINE_STRIP);
		if (kb.IsKeyDown('4'))
			screen.SetPrimetiveType(GL_LINE_LOOP);
		if (kb.IsKeyDown('5'))
			screen.SetPrimetiveType(GL_TRIANGLES);
		if (kb.IsKeyDown('6'))
			screen.SetPrimetiveType(GL_TRIANGLE_STRIP);
		if (kb.IsKeyDown('7'))
			screen.SetPrimetiveType(GL_TRIANGLE_FAN);
		if (kb.IsKeyDown('8'))
			screen.SetPrimetiveType(GL_QUADS);
		if (kb.IsKeyDown('9'))
			screen.SetPrimetiveType(GL_QUAD_STRIP);
		if (kb.IsKeyDown('0'))
			screen.SetPrimetiveType(GL_POLYGON);

		if (kb.IsKeyDown(VK_OEM_MINUS))
			screen.SetMode(GL_FILL);
		if (kb.IsKeyDown(VK_OEM_PLUS))
			screen.SetMode(GL_LINE);

		if (kb.IsKeyDown(VK_ADD) && timer1.IsOk())
		{
			screen1.Alpha = screen1.Alpha < 1 ? screen1.Alpha + 0.02 : screen1.Alpha;
			screen2.Alpha = screen2.Alpha < 1 ? screen2.Alpha + 0.02 : screen2.Alpha;
		}

		if (kb.IsKeyDown(VK_SUBTRACT) && timer1.IsOk())
		{
			screen1.Alpha = screen1.Alpha > 0 ? screen1.Alpha - 0.02 : screen1.Alpha;
			screen2.Alpha = screen2.Alpha > 0 ? screen2.Alpha - 0.02 : screen2.Alpha;
		}

		if (kb.IsKeyDown(VK_LSHIFT))
		{
			if (kb.IsKeyDown('1'))
				screen1.SetAlphaFunc(GL_ALWAYS);
			if (kb.IsKeyDown('2'))
				screen1.SetAlphaFunc(GL_LESS);
			if (kb.IsKeyDown('3'))
				screen1.SetAlphaFunc(GL_EQUAL);
			if (kb.IsKeyDown('4'))
				screen1.SetAlphaFunc(GL_LEQUAL);
			if (kb.IsKeyDown('5'))
				screen1.SetAlphaFunc(GL_GREATER);
			if (kb.IsKeyDown('6'))
				screen1.SetAlphaFunc(GL_NOTEQUAL);
			if (kb.IsKeyDown('7'))
				screen1.SetAlphaFunc(GL_GEQUAL);
			if (kb.IsKeyDown('8'))
				screen1.SetAlphaFunc(GL_NEVER);
		}
		else if(kb.IsKeyDown(VK_LCONTROL))
		{
			if (kb.IsKeyDown('1'))
				screen2.SetSfactor(GL_ZERO);
			if (kb.IsKeyDown('2'))
				screen2.SetSfactor(GL_ONE);
			if (kb.IsKeyDown('3'))
				screen2.SetSfactor(GL_DST_COLOR);
			if (kb.IsKeyDown('4'))
				screen2.SetSfactor(GL_ONE_MINUS_DST_COLOR);
			if (kb.IsKeyDown('5'))
				screen2.SetSfactor(GL_SRC_ALPHA);
			if (kb.IsKeyDown('6'))
				screen2.SetSfactor(GL_ONE_MINUS_SRC_ALPHA);
			if (kb.IsKeyDown('7'))
				screen2.SetSfactor(GL_DST_ALPHA);
			if (kb.IsKeyDown('8'))
				screen2.SetSfactor(GL_ONE_MINUS_DST_ALPHA);
		}
		else if(kb.IsKeyDown(VK_LMENU))
		{
			if (kb.IsKeyDown('1'))
				screen2.SetDfactor(GL_ZERO);
			if (kb.IsKeyDown('2'))
				screen2.SetDfactor(GL_ONE);
			if (kb.IsKeyDown('3'))
				screen2.SetDfactor(GL_DST_COLOR);
			if (kb.IsKeyDown('4'))
				screen2.SetDfactor(GL_ONE_MINUS_DST_COLOR);
			if (kb.IsKeyDown('5'))
				screen2.SetDfactor(GL_SRC_ALPHA);
			if (kb.IsKeyDown('6'))
				screen2.SetDfactor(GL_ONE_MINUS_SRC_ALPHA);
			if (kb.IsKeyDown('7'))
				screen2.SetDfactor(GL_DST_ALPHA);
			if (kb.IsKeyDown('8'))
				screen2.SetDfactor(GL_ONE_MINUS_DST_ALPHA);
		}

		lastKbState = kb;

		screen1.Update(timeSpend);
		screen2.Update(timeSpend);
		timer1.Update(timeSpend);
	}
private:
	static Program *inst;

	PrimetivesScreen screen;
	AlphaScreen screen1;
	BlendScreen screen2;
	
	KeyboardState lastKbState;

	Timer timer1;

	Program()
		: timer1(50)
	{
		screen.Position = Vector3(0, 0, -1);

		screen.AddPoint(Vector3(-1, -1, -1));
		screen.AddPoint(Vector3(-1,  1, -1));
		screen.AddPoint(Vector3( 1,  1, -1));
		screen.AddPoint(Vector3( 1, -1, -1));

		screen1.Position = Vector3(-4, 0, -20);
		screen2.Position = Vector3(4, 0, -20);

		lastKbState = KeyboardState::Current();
	}
public:
	static Program *Instance()
	{
		if (!inst)
			inst = new Program();
		return inst;
	}
	virtual ~Program() 
	{
	}
};

Program *Program::inst = nullptr;

#endif