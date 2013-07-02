//file: visor.cpp
//visor class realisation
//autor: Tyulin Roman
//date: 20.04.2013

#include "visor.h"
#include <assert.h>

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

CycleList<Point> Visor::GetPoints() const
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