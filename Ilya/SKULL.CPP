#ifndef _skull_
#define _skull_
#include <skull.h>
#include <iostream.h>
Skull::Skull(Points position1,float height1,float width1)
:rec(position1,position1.MoveBy(width1,0),position1.MoveBy(0,height1*(-1)),position1.MoveBy(width1,height1*(-1)))
{
cout<<"skull created"<<endl;
}
Skull::Skull(const Skull &b)
:rec(b.rec)
{
cout<<"Skull created"<<endl;
}
float Skull::GetHeight()
{
return height;
}
List<Points> Skull::GetPoints()
{
List<Points> list;
list.Add(rec.GetTopLeft());
list.Add(rec.GetTopRight());
list.Add(rec.GetBottomLeft());
list.Add(rec.GetBottomRight());
return list;
}
#endif