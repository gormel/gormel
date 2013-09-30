#include "sticker.h"

Sticker::Sticker(Point pos, double width, double height)
	: rect(pos, width, height)
{
}

CycleList<Point> Sticker::GetPoints() const
{
	CycleList<Point> result;
	result.Add(rect.GetLeftUp());
	result.Add(rect.GetRightUp());
	result.Add(rect.GetLeftDown());
	result.Add(rect.GetRightDown());
	return result;
}

void Sticker::MoveBy(Point dxdy)
{
	rect.MoveBy(dxdy);
}