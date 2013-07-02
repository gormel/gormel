//file: segment.cpp
//segment class realisation
//autor: Tyulin Roman
//date: 20.04.2013

#include "segment.h"

Segment::Segment(Point start, double width, double height)
	: start(start), width(width), height(height)
{
#ifdef TALKY
	cout << "Segment created!" << endl;
#endif
}

Segment::Segment(const Segment &obj)
	: start(obj.start), width(obj.width), height(obj.height)
{
#ifdef TALKY
	cout << "Segment created!" << endl;
#endif
}

Segment::~Segment()
{
#ifdef TALKY
	cout << "Segment removed!" << endl;
#endif
}

Point Segment::GetStart() const
{
	return start;
}

double Segment::GetWidth() const
{
	return width;
}

double Segment::GetHeight() const
{
	return height;
}

void Segment::MoveBy(Point dxdy)
{
	start = start.MoveBy(dxdy);
}