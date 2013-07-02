//file: point.cpp
//point class realisation
//autor: Tyulin Roman
//date: 20.04.2013

#include "point.h"
#include <ctype.h>

Point::Point()
	: x(0), y(0) 
{
#ifdef TALKY
	cout << "Point created!" << endl;
#endif
}

Point::Point(double x, double y)
	: x(x), y(y)
{
#ifdef TALKY
	cout << "Point created!" << endl;
#endif
}

Point::Point(double xy)
	: x(xy), y(xy)
{
#ifdef TALKY
	cout << "Point created!" << endl;
#endif
}

Point &Point::operator =(const Point &obj)
{
	x = obj.x;
	y = obj.y;
	return *this;
}

int Point::operator ==(const Point &obj)
{
	return x == obj.x && y == obj.y;
}

int Point::operator !=(const Point &obj)
{
	return x != obj.x || y != obj.y;
}

int Point::GetX() const
{
	return x;
}

int Point::GetY() const
{
	return y;
}

int Point::operator >(double value) const
{
	return x * y > value;
}

int Point::operator <(double value) const
{
	return x * y < value;
}

Point Point::MoveBy(Point dxdy)
{
	return Point(x + dxdy.x, y + dxdy.y);
}

ostream &operator <<(ostream &os, Point &p)
{
	return os << "{ " << p.GetX() << ", " << p.GetY() << " }";
}

istream &operator >>(istream &is, Point &p)
{
	Point *readed = ReadPoint(is);
	is.clear(istream::goodbit);
	if (!readed)
	{
		is.clear(istream::eofbit);
		return is;
	}
	p = *readed;
	return is;
}

Point *ReadPoint(istream &is)
{
	SkipSpaces(is);
	if(is.peek() != '{')
		return 0;
	is.get();
	SkipSpaces(is);
	double x = 0;
	is >> x;
	SkipSpaces(is);
	if(is.peek() != ',')
		return 0;
	is.get();
	SkipSpaces(is);
	double y = 0;
	is >> y;
	SkipSpaces(is);
	if(is.peek() != '}')
		return 0;
	is.get();
	return new Point(x, y);
}

void SkipSpaces(istream &is)
{
	while(isspace(is.peek()))
		is.get();
}