#ifndef _skullcaps_
#define _skullcaps_
#include <skullcap.h>
SkullCaps::SkullCaps(Points position,float width,float height)
:cap(position,position.MoveBy(width,0),height*0.75),skull(position, height*(-0.25),width)
	{
	cout<<"SkullCaps created"<<endl;
	}
SkullCaps::SkullCaps(const SkullCaps &n)
:cap(n.cap),skull(n.skull)
	{
	cout<<"SkullCaps created"<<endl;
	}
List<Points> SkullCaps::GetCap()
	{
	List<Points> list;
	list=cap.PointsList();
	return list;
	}
List<Points> SkullCaps::GetSkull()
	{
	List<Points> list;
	list=skull.GetPoints();
	return list;
	}
List<Points> SkullCaps::GetPoints()
	{
	List<Points> list;
	List<Points> capPoints= cap.PointsList();
	int i=0;
	for(i=0;i<capPoints.Count();i++)
		{
		list.Add(capPoints.Get(i));
		}
	List<Points> skullPoints=skull.GetPoints();
	for(i=0;i<skullPoints.Count();i++)
		{
		list.Add(skullPoints.Get(i));
		}
	return list;
	}
#endif