#include "goodcap.h"

Goodcap::Goodcap(Point pos, double w1, double w2, double h1, double h2)
	: Cap(pos, w1, w2, h1, h2), 
	sticker(pos.MoveBy(Point(0.2 * (w1 - w2), -0.4 * h2)), 0.6 * (w1 - w2), 0.4 * h2)
{
}

CycleList<Point> Goodcap::GetPoints() const
{
	CycleList<Point> result = Cap::GetPoints();
	CycleList<Point> stickerPoints = sticker.GetPoints();
	int i;
	for (i = 0; i < stickerPoints.Count(); i++)
	{
		result.Add(stickerPoints.Get(i));
	}
	return result;
}

void Goodcap::MoveBy(Point dxdy)
{
	Cap::MoveBy(dxdy);
	sticker.MoveBy(dxdy);
}