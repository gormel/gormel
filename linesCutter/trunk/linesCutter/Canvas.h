#ifndef my_canvas
#define my_canvas

#include <vector>
#include <hash_map>
#include <functional>
#include "Core\BaseObject.h"
#include "Core\Vector3.h"
#include "Multiline.h"
#include "IO\Event.h"

class Canvas : public BaseObject
{
protected:
	virtual void draw(long timeSpend) 
	{
		glColor3d(1, 1, 1);
		glBegin(GL_LINE_LOOP);
			glVertex2d(0, 0);
			glVertex2d(Width, 0);
			glVertex2d(Width, Height);
			glVertex2d(0, Height);
		glEnd();

		for (auto p : polys)
		{
			algoritms[cutterAlg](p);
		}
	}

	virtual void update(long timeSpend)
	{
	}
private:
	std::vector<Multiline> polys;
	int cutterAlg;
	std::hash_map<int, std::function<void(Multiline &)>> algoritms;

	void KSClip(Multiline &poly)
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
					v1.Y = v1.Y + dydx * (Width - v1.X);
					v1.X = Width;
				}
				else if (code1 & 4)
				{
					v1.X = v1.X + dxdy * (Height - v1.Y);
					v1.Y = Height;
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
		if (v.X > Width)
			code += 2;
		if (v.Y > Height)
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
	const static int KS_CLIP = 0;
	const static int KB_CLIP = 1;

	float Width;
	float Height;

	Vector3 InnerPosition;

	Event<double, double> Drag;
	Canvas(float width, float height)
		: Width(width), Height(height), cutterAlg(KS_CLIP)
	{
		algoritms[KS_CLIP] = [&](Multiline &poly) { KSClip(poly); };
	}

	void Clear()
	{
		polys.clear();
	}

	void AddLine(const Vector3 &v1, const Vector3 &v2, const Vector3 &color)
	{
		Vector3 verts[] = { v1, v2 };
		polys.push_back(Multiline(verts, 2, color));
	}

	void AddPolygon(Vector3 *vertices, int vertCount, const Vector3 &color)
	{
		polys.push_back(Multiline(vertices, vertCount, color));
	}

	void SetCutterAlgoritm(int algoritm)
	{
		cutterAlg = algoritm;
	}
};

#endif