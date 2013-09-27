#ifndef my_rectangle
#define my_rectangle

#include "point.h"

class Rectangle
{
private:
	Point lu;
	Point ru;
	Point ld;
	Point rd;
public:
	Rectangle(Point lu, double length, double height);
	void MoveBy(Point dxdy);
	Point GetLeftUp() const;
	Point GetRightUp() const;
	Point GetLeftDown() const;
	Point GetRightDown() const;
};

#endif