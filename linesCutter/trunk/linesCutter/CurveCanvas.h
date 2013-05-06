#ifndef my_curvecanvas
#define my_curvecanvas

#include "DrawingArea.h"
#include "Multiline.h"

class CurveCanvas : public DrawingArea
{
protected:
	virtual void update(long timeSpend)
	{
	}

	virtual void drawClip(Multiline &poly)
	{
	}
};

#endif