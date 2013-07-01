#include "visor.h"

Visor::Visor(Point position, double dx, double dy)
	: line(position, position.MoveBy(Point(dx, dy)))
{
#ifdef TALKY
	cout << "Visor created!" << endl;
#endif
}

Visor::Visor(const Visor &obj)
	: line(obj.line)
{
#ifdef TALKY
	cout << "Visor created!" << endl;
#endif
}

Visor::~Visor()
{
#ifdef TALKY
	cout << "Visor removed!" << endl;
#endif
}

CycleList<Point> Visor::GetPoints()
{
	CycleList<Point> list;

	list.Add(line.GetStart());
	list.Add(line.GetEnd());

	return list;
}

void Visor::MoveBy(Point dxdy)
{
	line.MoveBy(dxdy);
}