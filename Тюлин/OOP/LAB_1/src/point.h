#ifndef my_point
#define my_point

#ifdef BORLAND
	#include <iostream.h>
#else
	#include <iostream>
#endif

#ifndef BORLAND
	using namespace std;
#endif

class Point
{
private:
	double x;
	double y;
public:
	Point();
	Point(double x, double y);
	Point(double xy);
	int operator ==(const Point &obj);
	int GetX() const;
	int GetY() const;
	int operator >(double value) const;
	int operator <(double value) const;

	friend ostream &operator <<(ostream &os, Point &p);
	friend istream &operator >>(istream &is, Point &p);
};

Point *ReadPoint(istream &is);
void SkipSpaces(istream &is);

#endif