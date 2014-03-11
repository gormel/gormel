#ifndef my_alphablend
#define my_alphablend

#include "GL\GL.H"
#include "BaseObject.h"
#include "Cube.h"
#include "Geosphere.h"
#include "RainbowGeosphere.h"
#include "MouseState.h"
#include "RainbowCube.h"

class AlphaBlendScreen : public BaseObject
{
protected:
	virtual void draw(long ellapsedTime)
	{
		model.Alpha = Alpha;
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
	RainbowCube model;
	
	MouseState lastMsState;
	bool dragMode;
public:
	double Alpha;

	AlphaBlendScreen()
		: model(), Alpha(0.5)
	{
		model.Scale = Vector3(3);
		dragMode = false;
	}

	virtual ~AlphaBlendScreen() {}
};

#endif