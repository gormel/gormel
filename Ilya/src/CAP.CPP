#ifndef _cap_
#define _cap_
#include <iostream.h>
#include <cap.h>
Cap::Cap(Points positionLeft1,Points positionRight1,float lenght1)
:trig(positionLeft1,positionRight1,lenght1)
{
cout<<"Cap created"<<endl;
}
Cap::Cap(const Cap&obj)
:trig(obj.trig)
{
cout<<"Cap created"<<endl;
}
List<Points> Cap::PointsList()
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