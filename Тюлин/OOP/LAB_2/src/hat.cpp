#include "hat.h"

Hat::Hat(Point position, float len, float height)
	: seg(position, len, height)
{
#ifdef TALKY
	cout << "Hat created!" << endl;
#endif
}

Hat::Hat(const Hat &obj)
	: seg(obj.seg)
{
#ifdef TALKY
	cout << "Hat created!" << endl;
#endif
}

Hat::~Hat()
{
#ifdef TALKY
	cout << "Hat deleted!" << endl;
#endif
}

CycleList<Point> Hat::GetPoints()
{
	CycleList<Point> list;
	
	list.Add(seg.GetStart());
	list.Add(seg.GetStart().MoveBy(Point(seg.GetWidth() / 2, seg.GetHeiht())));
	list.Add(seg.GetStart().MoveBy(Point(seg.GetWidth(), 0)));

	return list;
}

void Hat::MoveBy(Point dxdy)
{
	seg.MoveBy(dxdy);
}