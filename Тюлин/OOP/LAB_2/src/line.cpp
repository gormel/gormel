#include "line.h"

Line::Line(Point start, Point end)
	: start(start), end(end)
{
#ifdef TALKY
	cout << "Line created!" << endl;
#endif
}

Line::Line(const Line &obj)
	: start(obj.start), end(obj.end)
{
#ifdef TALKY
	cout << "Line created!" << endl;
#endif
}

Line::~Line()
{
#ifdef TALKY
	cout << "Line removed!" << endl;
#endif
}

Point Line::GetStart() const
{
	return start;
}

Point Line::GetEnd() const
{
	return end;
}

void Line::MoveBy(Point dxdy)
{
	start = start.MoveBy(dxdy);
	end = end.MoveBy(dxdy);
}