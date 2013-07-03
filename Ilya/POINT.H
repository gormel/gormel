//Fail point.h
//class point
//avtor:Obmanov Ilja
//data: 10.04.2013

#include <iostream.h>
#include <ctype.h>
class Points
	{
private:
	int x;
	int y;
public:
	Points *Next;
	Points();
	Points(double ,double);
	~Points();
	int getX();
	int getY();
	void setX(int);
	void setY(int);
	void showP();
	Points MoveBy(float dx,float dy);
	int Points::operator == (const Points &p);
	void Points::operator = (const Points &p);
	friend ostream &operator <<(ostream &os, const Points &p)
	{
		return os << "{" << p.x << ", " << p.y << "}";
	}
	friend istream &operator >> (istream &is,Points &p);

};
Points *ReadPoint(istream &is);
void SkipSpaces(istream &is);