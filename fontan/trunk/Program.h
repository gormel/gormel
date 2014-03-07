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

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
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



		lastKbState = kb;

		screen1.Update(timeSpend);
		screen2.Update(timeSpend);
	}
private:
	static Program *inst;

	PrimetivesScreen screen;
	AlphaScreen screen1;
	BlendScreen screen2;
	
	KeyboardState lastKbState;

	Program()
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