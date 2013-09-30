#ifndef my_sticker
#define my_sticker

#include "rectangl.h"
#include "point.h"
#include "cyclist.h"

class Sticker
{
private:
	Rectangle rect;
public:
	Sticker(Point pos, double width, double height);
	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif