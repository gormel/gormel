#include "trap.h"

Trapezium::Trapezium(Point pos, double height, double down, double top)
	: lu(pos.MoveBy(Point(down / 2 - top / 2, height))), ru(pos.MoveBy(Point(down / 2 + top / 2, height))),
	  ld(pos), rd(pos.MoveBy(Point(down, 0)))
{
}

void Trapezium::MoveBy(Point dxdy)
{
	lu.MoveBy(dxdy);
	ru.MoveBy(dxdy);
	ld.MoveBy(dxdy);
	rd.MoveBy(dxdy);
}

Point Trapezium::GetLeftUp() const
{
	return lu;
}
Point Trapezium::GetRightUp() const
{
	return ru;
}
Point Trapezium::GetLeftDown() const
{
	return ld;
}
Point Trapezium::GetRightDown() const
{
	return rd;
}