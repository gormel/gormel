#include "rectangl.h"

Rectangle::Rectangle(Point p, double width, double height)
	: lu(p), ru(p.MoveBy(width, 0)), ld(p.MoveBy(0, height)), rd(p.MoveBy(width, height))
{
	assert(width > 0 && height > 0);
}

void Rectangle::MoveBy(Point dxdy)
{
	lu.MoveBy(dxdy);
	ru.MoveBy(dxdy);
	ld.MoveBy(dxdy);
	rd.MoveBy(dxdy);
}

Point Rectangle::GetLeftUp()
{
	return lu;
}
Point Rectangle::GetRightUp()
{
	return ru;
}
Point Rectangle::GetLeftDown()
{
	return ld;
}
Point Rectangle::GetRightDown()
{
	return rd;
}

