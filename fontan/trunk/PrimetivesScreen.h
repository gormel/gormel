#ifndef my_primetivesscreen
#define my_primetivesscreen

#include "BaseObject.h"
#include "GL\GL.H"
#include "Vector3.h"
#include <vector>

class PrimetivesScreen : public BaseObject
{
private:
	std::vector<Vector3> points;
	GLenum primetiveType;
protected:
	virtual void draw(long timeSpend) 
	{
		glPointSize(5);
		glLineWidth(5);
		glPolygonMode(GL_FILL, GL_FRONT);
		glBegin(primetiveType);
		
		for (auto v : points)
		{
			glVertex3d(v.X, v.Y, v.Z);
		}

		glEnd();
	}
	virtual void update(long timeSpend) {}
public:
	PrimetivesScreen()
		: primetiveType(GL_POINTS)
	{
	}

	void AddPoint(const Vector3 &p)
	{
		points.push_back(p);
	}

	void ClearPoints()
	{
		points.clear();
	}

	void setPrimetiveType(GLenum type)
	{
		primetiveType = type;
	}
};

#endif