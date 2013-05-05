#ifndef my_program
#define my_program

#include <gl\GLAux.h>
#include <iostream>
#include <unordered_map>

#include "Core\BaseObject.h"
#include "Core\Quatarnion.h"
#include "IO\MouseState.h"
#include "IO\KeyboardState.h"
#include "Canvas.h"

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
		c.Draw(timeSpend);
	}

	virtual void update(long timeSpend)
	{
		MouseState ms = MouseState::Current();
		double dx = ms.X - lms.X;
		double dy = ms.Y - lms.Y;

		if (!lms.Left && ms.Left)
		{
			if (ms.X > c.Position.X && ms.X < c.Position.X + c.Width &&
				ms.Y > c.Position.Y && ms.Y < c.Position.Y + c.Height)
				pictDragMode = true;
			else
			{
				bool notX = false;
				if (abs(ms.X - c.Position.X) < quanlily)
					leftDragMode = true;
				else if (abs(ms.X - c.Position.X - c.Width) < quanlily)
					rightDragMode = true;
				else
					notX = true;
				if (abs(ms.Y - c.Position.Y) < quanlily)
					topDragMode = true;
				else if (abs(ms.Y - c.Position.Y - c.Height) < quanlily)
					botDragMode = true;
				else if (notX)
					fullDragMode = true;
			}
		}

		if (lms.Left && !ms.Left)
			DisableDragModes();

		if (pictDragMode)
		{
			c.InnerPosition += Vector3(dx, dy, 0);
		}

		if (leftDragMode)
		{
			c.Width -= dx;
			c.Position.X += dx;
		}

		if (rightDragMode)
		{
			c.Width += dx;
		}

		if (topDragMode)
		{
			c.Height -= dy;
			c.Position.Y += dy;
		}

		if (botDragMode)
		{
			c.Height += dy;
		}

		if (fullDragMode)
		{
			c.Position += Vector3(dx, dy, 0);
		}

		lms = ms;
	}
private:
	MouseState lms;
	bool pictDragMode;
	bool leftDragMode;
	bool rightDragMode;
	bool topDragMode;
	bool botDragMode;
	bool fullDragMode;

	const static int quanlily = 4;

	void DisableDragModes()
	{
		fullDragMode = topDragMode = botDragMode = leftDragMode = rightDragMode = pictDragMode = false;
	}

	static Program *inst;

	Canvas c;

	Program()
		: c(100, 100)
	{
		c.AddLine(Vector3(-10, 10, 0), Vector3(50, 50, 0), Vector3(255, 255, 255));
		c.Position += Vector3(50, 50, 0);
		DisableDragModes();
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