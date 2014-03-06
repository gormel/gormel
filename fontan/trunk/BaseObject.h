#ifndef my_base_object
#define my_base_object

#include "gl\glut.h"
#include "Vector3.h"
#include "Quatarnion.h"
#include "Rotation.h"

#include <iostream>

class BaseObject
{
protected:
	virtual void draw(long timeSpend) = 0;
	virtual void update(long timeSpend) = 0;
public:
	BaseObject()
		: Scale(1, 1, 1)
	{
	}
	Vector3 Position;
	Vector3 Scale;
	mutable Quternion Rotations;

	virtual ~BaseObject() {}

	void Draw(long timeSpend)
	{
		glPushMatrix();

		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
				
		Rotations = Rotations.Normalize();
		Rotation rot(Rotations);
		
		glTranslated(Position.X, Position.Y, Position.Z);

		if (!(rot.X == 0 && rot.Y == 0 && rot.Z == 0))
			glRotated(rot.Angle, rot.X, rot.Y, rot.Z);
		
		if (!(Scale.X == 0 || Scale.Y == 0 || Scale.Z == 0))
			glScaled(Scale.X, Scale.Y, Scale.Z);

		draw(timeSpend);

		glPopMatrix();
	}

	void Update(long timeSpend)
	{
		update(timeSpend);
	}
};

#endif