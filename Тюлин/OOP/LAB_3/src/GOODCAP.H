#ifndef my_goodcap
#define my_goodcap

#include "cap.h"
#include "rectangl.h"
#include "cyclist.h"

class Goodcap : public Cap
{
private:
	Rectangle rect;
public:
	Goodcap(Point pos, double w1, double w2, double h1, double h2);
	CycleList<Point> GetPoints();
	void MoveBy(Point dxdy);
};

#endif