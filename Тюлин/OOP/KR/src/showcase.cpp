#include "showcase.h"

Showcase::Showcase(Point pos, double height, double down, double top)
	: trap(pos, height, down, top)
{
	k1 = 2 * height / (down - top);
	b1 = pos.GetY() - pos.GetX() * k1;
	k2 = -k1;
	b2 = b1 + k1 * down;
	
}
void Showcase::MoveBy(Point dxdy)
{
	trap.MoveBy(dxdy);
}

int Showcase::Inside(const Point &p) const
{
	if (p.GetY() < trap.GetLeftDown().GetY())
		return 0;
	if (p.GetY() > trap.GetLeftUp().GetY())
		return 0;
	if (p.GetY() > k1 * p.GetX() + b1)
		return 0;
	if (p.GetY() > k2 * p.GetX() + b2)
		return 0;
	return 1;
}

CycleList<Point> Showcase::GetPoints() const
{
	CycleList<Point> result;
	result.Add(trap.GetLeftUp());
	result.Add(trap.GetRightUp());
	result.Add(trap.GetLeftDown());
	result.Add(trap.GetRightDown());
	return result;
}

int Showcase::Validate(const Cap &cap) const
{
	CycleList<Point> capPoints = cap.GetPoints();
	int i;
	for (i = 0; i < capPoints.Count(); i++)
	{
		if (!Inside(capPoints.Get(i)))
			return 0;
	}
	return 1;
}