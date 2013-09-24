#ifndef my_showcase
#define my_showcase

#include "trap.h"
#include "cyclist.h"

class Showcase
{
private:
	Trapezium trap;
public:
	Showcase(Point pos, double height, double down, double top);
	void MoveBy(Point dxdy);
	CycleList<Point> GetPoints();
}

#endif