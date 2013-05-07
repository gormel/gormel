#ifndef my_drawingarea
#define my_drawingarea

#include <vector>
#include "Core\BaseObject.h"
#include "Core\Vector3.h"
#include "Multiline.h"

class DrawingArea : public BaseObject
{
protected:
	virtual void draw(long timeSpend)
	{		
		glColor3d(1, 1, 1);
		glBegin(GL_LINE_LOOP);
			for (auto v : Corners)
				glVertex2d(v.X, v.Y);
		glEnd();

		glPointSize(3);
		glBegin(GL_POINTS);
			for (auto v : Corners)
				glVertex2d(v.X, v.Y);
		glEnd();

		for (auto p : polys)
			clipDraw(p);
	}

	virtual void clipDraw(Multiline &poly) = 0;
private:
	std::vector<Multiline> polys;
public:
	Vector3 InnerPosition;
	std::vector<Vector3> Corners;
	DrawingArea()
	{
	}

	virtual ~DrawingArea() {}

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
};

#endif