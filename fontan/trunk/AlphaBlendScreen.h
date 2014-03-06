#ifndef my_alphablend
#define my_alphablend

#include "GL\GL.H"
#include "BaseObject.h"
#include "Cube.h"
#include "Geosphere.h"
#include "MouseState.h"

class AlphaBlendScreen : public BaseObject
{
protected:
	virtual void draw(long ellapsedTime)
	{
		glColor4d(0.5, 0.4, 0.2, Alpha);
		model.Position = Position;
		model.Draw(ellapsedTime);
	}

	virtual void update(long ellapsedTime)
	{
		MouseState ms = MouseState::Current();

		if (ms.Left && !lastMsState.Left)
			dragMode = true;

		if (!ms.Left && lastMsState.Left)
			dragMode = false;

		if (dragMode)
		{
			if (ms.Y != lastMsState.Y)
				model.Rotations = Rotation(1, 0, 0, ms.Y - lastMsState.Y).ToQuternion() * model.Rotations;
			if (ms.X != lastMsState.X)
				model.Rotations = Rotation(0, 1, 0, ms.X - lastMsState.X).ToQuternion() * model.Rotations;
		}

		lastMsState = ms;
	}
private:
	Geosphere model;
	
	MouseState lastMsState;
	bool dragMode;
public:
	double Alpha;

	AlphaBlendScreen()
		: model(3), Alpha(0.5)
	{
		model.Scale = Vector3(3);
		dragMode = false;
	}
};

#endif