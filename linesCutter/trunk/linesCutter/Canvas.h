#ifndef my_canvas
#define my_canvas

#include <vector>
#include <hash_map>
#include <functional>
#include "Core\BaseObject.h"
#include "Core\Vector3.h"
#include "Multiline.h"
#include "IO\Event.h"
#include "DrawingArea.h"

class Canvas : public DrawingArea
{
protected:
	virtual void update(long timeSpend)
	{
	}

	virtual void clipDraw(Multiline &poly)
	{
		glColor3d(poly.Color.X / 255, poly.Color.Y / 255, poly.Color.Z / 255);
		glBegin(GL_LINES);
		for (int i = 0; i < poly.Count(); i++)
		{
			Vector3 v1 = poly[i] + InnerPosition;
			Vector3 v2 = poly[(i + 1) % poly.Count()] + InnerPosition;

			char code1 = Code(v1);
			char code2 = Code(v2);

			double dx = (v2 - v1).X;
			double dy = (v2 - v1).Y;
			double dxdy = 0;
			double dydx = 0;
			if (dx != 0)
				dydx = dy / dx;
			if (dy != 0)
				dxdy = dx / dy;
			while (true)
			{
				if (code1 & code2)
					break;
				if (code1 == code2 && code2 == 0)
				{
					glVertex2d(v1.X, v1.Y);
					glVertex2d(v2.X, v2.Y);
					break;
				}

				if (!code1)
				{
					Swap(code1, code2);
					Swap(v1, v2);
				}

				if (code1 & 1)
				{
					v1.Y = v1.Y - v1.X * dydx;
					v1.X = 0;
				}
				else if (code1 & 2)
				{
					v1.Y = v1.Y + dydx * (Corners.at(2).X - v1.X);
					v1.X = Corners.at(2).X;
				}
				else if (code1 & 4)
				{
					v1.X = v1.X + dxdy * (Corners.at(2).Y - v1.Y);
					v1.Y = Corners.at(2).Y;
				}
				else if (code1 & 8)
				{
					v1.X = v1.X - v1.Y * dxdy;
					v1.Y = 0;
				}

				code1 = Code(v1);
			}
		}
		glEnd();
	}

	char Code(const Vector3 &v)
	{
		char code = 0;
		if (v.X < 0)
			code += 1;
		if (v.X > Corners.at(2).X)
			code += 2;
		if (v.Y > Corners.at(2).Y)
			code += 4;
		if (v.Y < 0)
			code += 8;
		return code;
	}

	template<class T> 
	static void Swap(T &a, T &b)
	{
		auto t = a;
		a = b;
		b = t;
	}
public:
	Canvas(float width, float height)
	{
		Corners.push_back(Vector3());
		Corners.push_back(Vector3(width, 0, 0));
		Corners.push_back(Vector3(width, height, 0));
		Corners.push_back(Vector3(0, height, 0));
	}

	double GetWidth()
	{
		return Corners.at(2).X;
	}

	void SetWidth(double value)
	{
		Corners.at(1).X = value;
		Corners.at(2).X = value;
	}

	double GetHeight()
	{
		return Corners.at(2).Y;
	}

	void SetHeight(double value)
	{
		Corners.at(2).Y = value;
		Corners.at(3).Y = value;
	}

	virtual ~Canvas() {}
};

#endif