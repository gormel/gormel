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

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
		f->Draw(timeSpend);
		//screen.Draw(timeSpend);
	}

	virtual void update(long timeSpend)
	{
		KeyboardState kb = KeyboardState::Current();
		
		if (kb.IsKeyDown(VK_UP))
			f->Height += 10.0 * timeSpend / 1000;
		if (kb.IsKeyDown(VK_DOWN))
			f->Height -= 10.0 * timeSpend / 1000;
		if (kb.IsKeyDown(VK_LEFT))
			f->Rotations *= Rotation(0, 1, 0, -90.0 * timeSpend / 1000).ToQuternion();
		if (kb.IsKeyDown(VK_RIGHT))
			f->Rotations *= Rotation(0, 1, 0,  90.0 * timeSpend / 1000).ToQuternion();

		if (kb.IsKeyDown(VK_OEM_PLUS))
			f->Transparency = f->Transparency < 255 ? f->Transparency + 1 : f->Transparency;
		if (kb.IsKeyDown(VK_OEM_MINUS))
			f->Transparency = f->Transparency > 0 ? f->Transparency - 1 : f->Transparency;

		f->Update(timeSpend);

		if (kb.IsKeyDown('1'))
			screen.setPrimetiveType(GL_POINTS);
		if (kb.IsKeyDown('2'))
			screen.setPrimetiveType(GL_LINES);
		if (kb.IsKeyDown('3'))
			screen.setPrimetiveType(GL_LINE_STRIP);
		if (kb.IsKeyDown('4'))
			screen.setPrimetiveType(GL_TRIANGLES);
		if (kb.IsKeyDown('5'))
			screen.setPrimetiveType(GL_TRIANGLE_STRIP);
		if (kb.IsKeyDown('6'))
			screen.setPrimetiveType(GL_QUADS);
		if (kb.IsKeyDown('7'))
			screen.setPrimetiveType(GL_QUAD_STRIP);
		if (kb.IsKeyDown('8'))
			screen.setPrimetiveType(GL_POLYGON);
	}
private:
	static Program *inst;

	Fontan *f;
	PrimetivesScreen screen;

	Program()
	{
		f = new Fontan(1000, 1, 100, 15, 15);
		f->Position -= Vector3(0, 10, 50);

		screen.Position = Vector3(0, 0, -1);

		screen.AddPoint(Vector3(-1, -1, -1));
		screen.AddPoint(Vector3(-1,  1, -1));
		screen.AddPoint(Vector3( 1,  1, -1));
		screen.AddPoint(Vector3( 1, -1, -1));
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
		delete f;
	}
};

Program *Program::inst = nullptr;

#endif