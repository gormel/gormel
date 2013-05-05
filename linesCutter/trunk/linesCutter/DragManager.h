#ifndef my_dragmanager
#define my_dragmanager

#include <vector>
#include "Core\Vector3.h"
#include "IO\MouseState.h"

class DragManager
{
private:
	std::vector<Vector3 *> drags;
	MouseState lms;
public:
	DragManager()
	{
	}

	void StartDrag(Vector3 &v)
	{
		drags.push_back(&v);
	}

	void StopDrag()
	{
		drags.clear();
	}

	void Update(long timeSpend)
	{
		MouseState ms = MouseState::Current();

		double dx = ms.X - lms.X;
		double dy = ms.Y - lms.Y;

		for (auto v : drags)
		{
			v->X += dx;
			v->Y += dy;
		}

		lms = ms;
	}
};

#endif