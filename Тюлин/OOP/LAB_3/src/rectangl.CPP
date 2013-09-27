#include "rectangl.h"
#include <assert.h>

Rectangle::Rectangle(Point p, double width, double height)
	: lu(p), ru(p.MoveBy(Point(width, 0))), ld(p.MoveBy(Point(0, height))), rd(p.MoveBy(Point(width, height)))
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

Point Rectangle::GetLeftUp() const
{
	return lu;
}
Point Rectangle::GetRightUp() const
{
	return ru;
}
Point Rectangle::GetLeftDown() const
{
	return ld;
}
Point Rectangle::GetRightDown() const
{
	return rd;
}

