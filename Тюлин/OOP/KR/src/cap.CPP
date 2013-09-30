//file: cap.cpp
//Cap class realisation
//autor: Tyulin Roman
//date: 20.04.2013

#include "cap.h"
#include <assert.h>

Cap::Cap(Point pos, double w1, double w2, double h1, double h2)
	: hat(pos, w1 - w2, h2), visor(pos, -w2, h1)
{
	assert(w1 > w2 && h2 > 0 && w1 > 0 && w2 > 0);
#ifdef TALKY
	cout << "Cap created!" << endl;
#endif
}

Cap::Cap(const Cap &obj)
	: hat(obj.hat), visor(obj.visor)
{
#ifdef TALKY
	cout << "Cap created!" << endl;
#endif
}

Cap::~Cap()
{
#ifdef TALKY
	cout << "Cap deleted!" << endl;
#endif
}

int Cap::operator ==(const Cap &obj)
{
	CycleList<Point> my = GetPoints();
	CycleList<Point> his = obj.GetPoints();
	if (my.Count() != his.Count())
		return 0;
	for (int i = 0; i < my.Count(); ++i)
	{
		if (my.Get(i) != his.Get(i))
			return 0;
	}
	return 1;
}

int Cap::operator !=(const Cap &obj)
{
	return !(*this == obj);
}

CycleList<Point> Cap::GetPoints() const
{
	CycleList<Point> list;

	CycleList<Point> hatPoints = hat.GetPoints();
	int i = 0;
	for (i = 0; i < hatPoints.Count(); ++i)
	{
		list.Add(hatPoints.Get(i));
	}

	CycleList<Point> visorPoints = visor.GetPoints();
	for (i = 0; i < visorPoints.Count(); ++i)
	{
		list.Add(visorPoints.Get(i));
	}

	return list;
}

void Cap::MoveBy(Point dxdy)
{
	hat.MoveBy(dxdy);
	visor.MoveBy(dxdy);
}