#ifndef my_visor
#define my_visor

#include "cyclist.h"
#include "point.h"
#include "line.h"

class Visor
{
private:
	Line line;
public:
	Visor(Point position, double dx, double dy);
	Visor(const Visor &obj);
	~Visor();

	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif