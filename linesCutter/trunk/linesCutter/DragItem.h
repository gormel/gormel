#ifndef my_dragitem
#define my_dragitem

#include <functional>

class IDragItem
{
public:
	virtual void Add(double dx, double dy) = 0;
};

template<class T>
class DragItem : public IDragItem
{
private:
	T *value;
	std::function<void(T &, double, double)> func;
public:
	DragItem(T &value, std::function<void(T &, double, double)> func)
		: value(&value), func(func)
	{
	}

	virtual void Add(double dx, double dy)
	{
		func(*value, dx, dy);
	}
};

#endif