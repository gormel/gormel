#ifndef my_goodcap
#define my_goodcap

#include "cap.h"
#include "sticker.h"
#include "cyclist.h"

class Goodcap : public Cap
{
private:
	Sticker sticker;
public:
	Goodcap(Point pos, double w1, double w2, double h1, double h2);
	CycleList<Point> GetPoints() const;
	void MoveBy(Point dxdy);
};

#endif