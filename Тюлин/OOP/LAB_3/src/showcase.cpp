#include "showcase.h"

Showcase::Showcase(Point pos, double height, double down, double top)
	: trap(pos, height, down, top)
{
}
void Showcase::MoveBy(Point dxdy)
{
	trap.MoveBy(dxdy);
}

List<Point> Showcase::GetPoints()
{
	List<Point> result;
	result.Add(trap.GetLeftUp());
	result.Add(trap.GetRightUp());
	result.Add(trap.GetLeftDown());
	result.Add(trap.GetLRightDown());
	return result;
}