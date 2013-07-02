#ifndef my_segment
#define my_segment

#include "point.h"

class Segment
{
private:
	Point start;
	double width;
	double height;
public:
	Segment(Point start, double width, double height);
	Segment(const Segment &obj);
	~Segment();

	Point GetStart() const;
	double GetWidth() const;
	double GetHeight() const;

	void MoveBy(Point dxdy);
};

#endif