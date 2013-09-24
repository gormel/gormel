//file: hat.h
//hat class header
//autor: Tyulin Roman
//date: 20.04.2013

#ifndef my_hat
#define my_hat

#include "segment.h"
#include "cyclist.h"

class Hat
{
private:
	Segment seg;
public:
	Hat(Point position, float len, float height);
	Hat(const Hat &obj);
	~Hat();

	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif