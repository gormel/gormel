#ifndef _qwerectagle_
#define _qwerectangle_
#include <rectangl.h>
Rectangle::Rectangle(const Points topLeft1,const Points topRight1,const Points bottomLeft1,const Points bottomRight1)
{
 topLeft=topLeft1;
 topRight=topRight1;
 bottomLeft=bottomLeft1;
 bottomRight=bottomRight1;
}
Points Rectangle::GetTopLeft() const
{return topLeft;}
Points Rectangle::GetTopRight() const
{return topRight;}
Points Rectangle::GetBottomLeft() const
{return bottomLeft;}
Points Rectangle::GetBottomRight() const
{return bottomRight;}
void Rectangle::MoveBy(float dx,float dy)
{
topRight=topRight.MoveBy(dx,dy);
topLeft=topLeft.MoveBy(dx,dy);
bottomLeft=bottomLeft.MoveBy(dx,dy);
bottomRight=bottomRight.MoveBy(dx,dy);
}
#endif