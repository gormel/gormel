#ifndef _skullcaps_
#define _skullcaps_
#include <skullcap.h>
SkullCaps::SkullCaps(Points position,float width,float height)
:cap(position,position.MoveBy(width,0),height*0.75),skull(position, height*(-0.25),width)
	{
#ifdef TALKY
	cout<<"SkullCaps created"<<endl;
#endif
	}
SkullCaps::SkullCaps(const SkullCaps &n)
:cap(n.cap),skull(n.skull)
	{
#ifdef TALKY
	cout<<"SkullCaps created"<<endl;
#endif
	}

List<Points> SkullCaps::GetPoints() const
	{
	List<Points> list;
	List<Points> capPoints(cap.PointsList());
	int i=0;
	for(i=0;i<capPoints.Count();i++)
		{
		list.Add(capPoints.Get(i));
		}
	List<Points> skullPoints(skull.GetPoints());
	for(i=0;i<skullPoints.Count();i++)
		{
		list.Add(skullPoints.Get(i));
		}
	return list;
	}

void SkullCaps::MoveBy(float dx,float dy)
{
	skull.MoveBy(dx, dy);
	cap.MoveBy(dx, dy);
}
#endif