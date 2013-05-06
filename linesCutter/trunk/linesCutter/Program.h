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
#include "DragManager.h"

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
			if (ms.X > c.Position.X && ms.X < c.Position.X + c.GetWidth() &&
				ms.Y > c.Position.Y && ms.Y < c.Position.Y + c.GetHeight())
				dragManager.StartDrag<Vector3>(c.InnerPosition, [&](Vector3 &v, double dx, double dy) { v += Vector3(dx, dy, 0); });
			else
			{
				bool notX = false;
				if (abs(ms.X - c.Position.X) < quanlily)
				{
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetWidth(c.GetWidth() - dx); });
					dragManager.StartDrag<double>(c.Position.X, [&](double &v, double dx, double dy) { v += dx; });
				}
				else if (abs(ms.X - c.Position.X - c.GetWidth()) < quanlily)
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetWidth(c.GetWidth() + dx); });

				if (abs(ms.Y - c.Position.Y) < quanlily)
				{
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetHeight(c.GetHeight() - dy); });
					dragManager.StartDrag<double>(c.Position.Y, [&](double &v, double dx, double dy) { v += dy; });
				}
				else if (abs(ms.Y - c.Position.Y - c.GetHeight()) < quanlily)
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetHeight(c.GetHeight() + dy); });
			}
		}

		if (!lms.Right && ms.Right)
		{
			if (ms.X > c.Position.X && ms.X < c.Position.X + c.GetWidth() &&
				ms.Y > c.Position.Y && ms.Y < c.Position.Y + c.GetHeight())
				dragManager.StartDrag<Vector3>(c.Position, [&](Vector3 &v, double dx, double dy) { v += Vector3(dx, dy, 0); });
		}

		if (lms.Left && !ms.Left)
			dragManager.StopDrag();

		if (lms.Right && !ms.Right)
			dragManager.StopDrag();

		dragManager.Update(timeSpend);
		
		lms = ms;
	}
private:
	MouseState lms;
	DragManager dragManager;

	const static int quanlily = 4;

	static Program *inst;

	Canvas c;

	Program()
		: c(100, 100)
	{
		c.AddLine(Vector3(-10, 10, 0), Vector3(50, 50, 0), Vector3(255, 255, 255));
		c.Position += Vector3(50, 50, 0);
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