#ifndef my_hat
#define my_hat

#include "segment.h"
#include "cyclist.h"

class Hat
{
private:
	Segment seg;
public:
	Hat(Point position, float len, float height);
	Hat(const Hat &obj);
	~Hat();

	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif