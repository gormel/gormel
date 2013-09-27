#ifndef my_showcase
#define my_showcase

#include "trap.h"
#include "cyclist.h"
#include "goodcap.h"

class Showcase
{
private:
	Trapezium trap;
	double k1;
	double k2;
	double b1;
	double b2;
	int Inside(const Point &p) const;
public:
	Showcase(Point pos, double height, double down, double top);
	void MoveBy(Point dxdy);
	CycleList<Point> GetPoints() const;
	int Validate(const Goodcap &cap) const;
};

#endif