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
#include "CurveCanvas.h"
#include "DragManager.h"
#include "IO\Font.h"

class Program : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{
		c.Draw(timeSpend);
		cc.Draw(timeSpend);
	}

	virtual void update(long timeSpend)
	{
		MouseState ms = MouseState::Current();
		double dx = ms.X - lms.X;
		double dy = ms.Y - lms.Y;

		if (!lms.Left && ms.Left)
		{
			bool dr = false;
			for (int i = 0; i < cc.Corners.size(); i++)
			{
				auto c = cc.Corners.at(i);
				if ((c + cc.Position - Vector3(ms.X, ms.Y, 0)).Lenght() < quanlily)
				{
					dragManager.StartDrag<CurveCanvas>(cc, [=](CurveCanvas &cc, double dx, double dy) { cc.Corners.at(i) += Vector3(dx, dy, 0); });
					dr = true;
				}
			}

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
				else
					notX = true;

				if (abs(ms.Y - c.Position.Y) < quanlily)
				{
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetHeight(c.GetHeight() - dy); });
					dragManager.StartDrag<double>(c.Position.Y, [&](double &v, double dx, double dy) { v += dy; });
				}
				else if (abs(ms.Y - c.Position.Y - c.GetHeight()) < quanlily)
					dragManager.StartDrag<Canvas>(c, [&](Canvas &c, double dx, double dy) { c.SetHeight(c.GetHeight() + dy); });
				else if(notX && !dr)
				{
					dragManager.StartDrag<Vector3>(cc.InnerPosition, [&](Vector3 &v, double dx, double dy) { v += Vector3(dx, dy, 0); });
					dr = true;
				}
			}

		}

		if (!lms.Right && ms.Right)
		{
			if (ms.X > c.Position.X && ms.X < c.Position.X + c.GetWidth() &&
				ms.Y > c.Position.Y && ms.Y < c.Position.Y + c.GetHeight())
				dragManager.StartDrag<Vector3>(c.Position, [&](Vector3 &v, double dx, double dy) { v += Vector3(dx, dy, 0); });
			else
				dragManager.StartDrag<Vector3>(cc.Position, [&](Vector3 &v, double dx, double dy) { v += Vector3(dx, dy, 0); });

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
	CurveCanvas cc;

	Program()
		: c(100, 100)
	{
		Vector3 poly1[4];
		Vector3 poly2[4];

		poly1[0] = Vector3(0, 30, 0);
		poly1[1] = Vector3(30, 0, 0);
		poly1[2] = Vector3(60, 90, 0);
		poly1[3] = Vector3(90, 60, 0);
		
		poly2[0] = Vector3(60, 0, 0);
		poly2[1] = Vector3(90, 30, 0);
		poly2[2] = Vector3(0, 60, 0);
		poly2[3] = Vector3(30, 90, 0);

		Vector3 color(255, 255, 255);

		c.AddPolygon(poly1, 4, color);
		c.AddPolygon(poly2, 4, color);
		c.Position += Vector3(50, 50, 0);

		cc.Corners.push_back(Vector3(-30, 0, 0));
		cc.Corners.push_back(Vector3(60, -60, 0));
		cc.Corners.push_back(Vector3(180, 30, 0));
		cc.Corners.push_back(Vector3(60, 180, 0));
		cc.Corners.push_back(Vector3(-10, 120, 0));
		
		cc.AddPolygon(poly1, 4, color);
		cc.AddPolygon(poly2, 4, color);
		cc.Position += Vector3(200, 200, 0);
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