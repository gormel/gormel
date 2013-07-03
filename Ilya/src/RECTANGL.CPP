#ifndef _rectagle_
#define _rectangle_
#include <rectangl.h>
Rectangle::Rectangle(const Points topLeft1,const Points topRight1,const Points bottomLeft1,const Points bottomRight1)
{
 topLeft=topLeft1;
 topRight=topRight1;
 bottomLeft=bottomLeft1;
 bottomRight=bottomRight1;
}
Points Rectangle::GetTopLeft()
{return topLeft;}
Points Rectangle::GetTopRight()
{return topRight;}
Points Rectangle::GetBottomLeft()
{return bottomLeft;}
Points Rectangle::GetBottomRight()
{return bottomRight;}
#endif