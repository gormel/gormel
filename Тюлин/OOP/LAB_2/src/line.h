#ifndef my_line
#define my_line

#include "point.h"

class Line
{
private:
	Point start;
	Point end;
public:
	Line(Point start, Point end);
	Line(const Line &obj);
	~Line();

	Point GetStart() const;
	Point GetEnd() const;

	void MoveBy(Point dxdy);
};

#endif