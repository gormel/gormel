//Fail metpoint.cpp
//metodi point
//avtor:Obmanov Ilja
//data: 10.04.2013

#ifndef point
#define point
#include <iostream.h>
#include <point.h>
Points::Points(double x,double y)
:x(x),y(y)
{}
Points::Points()
{x=0;
y=0;
Next=NULL;
}

Points::~Points()
{Next=NULL;
}

int Points::getX()
	{
	return x;
	}

int Points::getY()
	{
	return y;
	}

void Points::setX(int x)
	{
	this->x=x;
	}
void Points::setY(int y)
	{
	this->y=y;
	}
int Points::operator == (const Points &p)
{
return x==p.x&y==p.y;
}
void Points::operator = (const Points &p)
{
 x=p.x;
 y=p.y;
}
Points Points::MoveBy(float dx,float dy)
{
x=x+dx;
y=y+dy;
return Points(x,y);
}

void Points::showP()
{
cout<<"("<<x<<";"<<y<<")"<<endl;
}

istream &operator >>(istream &is, Points &p)
{
	Points *readed = ReadPoint(is);
	is.clear(istream::goodbit);
	if (!readed)
	{
		is.clear(istream::eofbit);
		return is;
	}
	p = *readed;
	return is;
}

Points *ReadPoint(istream &is)
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
	return new Points(x, y);
}

void SkipSpaces(istream &is)
{
	while(isspace(is.peek()))
		is.get();
}

#endif