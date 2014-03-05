#ifndef _qwcap_
#define _qwcap_
#include <iostream.h>
#include <cap.h>
Cap::Cap(Points positionLeft1,Points positionRight1,float lenght1)
:trig(positionLeft1,positionRight1,lenght1)
{
#ifdef TALKY
cout<<"Cap created"<<endl;
#endif
}

void Cap::MoveBy(float dx,float dy)
	{
        trig.MoveBy(dx,dy);
	}
	
Cap::Cap(const Cap&obj)
:trig(obj.trig)
{
#ifdef TALKY
cout<<"Cap created"<<endl;
#endif
}
List<Points> Cap::PointsList() const
{
 List<Points> list;
 list.Add(trig.GetLeft());
 list.Add(trig.GetRight());
 Points poin;
 poin=trig.GetLeft();
 list.Add(poin.MoveBy((trig.GetRight().getX()-trig.GetLeft().getX())/2,trig.GetHeight()));
 return list;
}

#endif