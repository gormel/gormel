#ifndef my_cap
#define my_cap

#include "hat.h"
#include "visor.h"
#include "point.h"
#include "cyclist.h"

class Cap
{
private:
	Hat hat;
	Visor visor;
public:
	Cap(Point pos, double width, double h1, double h2);
	Cap(const Cap &obj);
	~Cap();

	CycleList<Point> GetPoints();
	void MoveBy(Point dxdy);
};

#endif