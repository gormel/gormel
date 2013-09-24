#include "goodcap.h"

Goodcap::Goodcap(Point pos, double w1, double w2, double h1, double h2)
	: Cap(pos, w1, w2, h1, h2), 
	rect(pos.MoveBy(Point(0.2 * (w1 - w2), -0.4 * h2)), 0.6 * (w1 - w2), 0.4 * h2)
{
}

CycleList<Point> Goodcap::GetPoints()
{
	CycleList<Point> result = Cap::GetPoints();
	result.Add(rect.GetLeftUp());
	result.Add(rect.GetRightUp());
	result.Add(rect.GetLeftDown());
	result.Add(rect.GetRightDown());
	return result;
}

void Goodcap::MoveBy(Point dxdy)
{
	Cap::MoveBy(dxdy);
	rect.MoveBy(dxdy);
}