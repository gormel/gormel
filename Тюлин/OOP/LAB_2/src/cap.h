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
	Cap(Point pos, double w1, double w2, double h1, double h2);
	Cap(const Cap &obj);
	~Cap();

	int operator ==(const Cap &obj);
	int operator !=(const Cap &obj);

	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif