#ifndef my_showview
#define my_showview

#include "cyclist.h"
#include "showcase.h"

class ShowcaseView : public Showcase, public CycleList<Cap *>
{
public:
	ShowcaseView(Point pos, double height, double down, double top)
		: Showcase(pos, height, down, top)
	{
		
	}
	
	void Add(Cap * const &cap)
	{
		if (!Validate(*cap))
			return;
		CycleList<Cap *>::Add(cap);
	}
};

#endif