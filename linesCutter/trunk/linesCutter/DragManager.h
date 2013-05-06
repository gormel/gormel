#ifndef my_dragmanager
#define my_dragmanager

#include <vector>
#include <functional>
#include "Core\Vector3.h"
#include "IO\MouseState.h"
#include "DragItem.h"

class DragManager
{
private:
	std::vector<IDragItem *> drags;
	MouseState lms;
public:
	DragManager()
	{
	}

	~DragManager()
	{
		for (auto i : drags)
			delete i;
	}

	template<class T>
	void StartDrag(T &value, std::function<void(T &, double, double)> func)
	{
		drags.push_back(new DragItem<T>(value, func));
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
			v->Add(dx, dy);
		}

		lms = ms;
	}
};

#endif