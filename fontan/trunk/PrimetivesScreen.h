#ifndef my_primetivesscreen
#define my_primetivesscreen

#include "BaseObject.h"
#include "GL\GL.H"
#include "Vector3.h"

class PrimetivesScreen : public BaseObject
{
private:
	Vector3 v1;
	Vector3 v2;
	Vector3 v3;
	Vector3 v4;
	GLenum primetiveType;
protected:
	virtual void draw(long timeSpend) 
	{
		glBegin(primetiveType);

		glVertex3d(v1.X, v1.Y, v1.Z);
		glVertex3d(v2.X, v2.Y, v2.Z);
		glVertex3d(v3.X, v3.Y, v3.Z);
		glVertex3d(v4.X, v4.Y, v4.Z);

		glEnd();
	}
	virtual void update(long timeSpend) {}
public:
	PrimetivesScreen()
		: primetiveType(GL_POINT),
		v1(0, 0, 0), v2(), v3(), v4()
	{
	}


};

#endif