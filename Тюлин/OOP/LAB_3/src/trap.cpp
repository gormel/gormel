#include "traps.h"

Trapezium::Trapezium(Point pos, double height, double down, double top)
	: lu(pos.MoveBy(down / 2 - top / 2, height)), ru(pos.MoveBy(down / 2 + top / 2, height)), 
	  ld(pos), rd(pos.MoveBy(down, 0))
{
}

void Showcase::MoveBy(Point dxdy)
{
	lu.MoveBy(dxdy);
	ru.MoveBy(dxdy);
	ld.MoveBy(dxdy);
	rd.MoveBy(dxdy);
}

Point Showcase::GetLeftUp()
{
	return lu;
}
Point Showcase::GetRightUp()
{
	return ru;
}
Point Showcase::GetLeftDown()
{
	return ld;
}
Point Showcase::GetRightDown()
{
	return rd;
}