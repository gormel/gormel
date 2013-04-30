#ifndef my_program
#define my_program

#include <gl\GLAux.h>
#include <iostream>
#include <unordered_map>
#include "BaseObject.h"
#include "Quatarnion.h"
#include "MouseState.h"
#include "KeyboardState.h"

#include "Geosphere.h"
#include "Fontan.h"

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
		f->Draw(timeSpend);
	}

	virtual void update(long timeSpend)
	{
		KeyboardState kb = KeyboardState::Current();

		if (kb.IsKeyDown(VK_UP))
			f->Height += 0.01;
		if (kb.IsKeyDown(VK_DOWN))
			f->Height -= 0.01;
		if (kb.IsKeyDown(VK_LEFT))
			Rotations *= Rotation(0, 1, 0, -1).ToQuternion();
		if (kb.IsKeyDown(VK_RIGHT))
			Rotations *= Rotation(0, 1, 0, 1).ToQuternion();

		f->Update(timeSpend);
	}
private:
	static Program *inst;

	Fontan *f;

	Program()
	{
		Position -= Vector3(0, 30, 100);
		f = new Fontan(1000, 1, 100, 15, 15);
	}
public:
	static Program *Instance()
	{
		if (!inst)
			inst = new Program();
		return inst;
	}
	virtual ~Program() {}
};

Program *Program::inst = nullptr;

#endif