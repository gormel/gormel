#ifndef _qwetriangle_
#define _qwetriangle_
#include <triangle.h>
Triangle::Triangle(const Points Left1,const Points Right1,float height1)
{
Left=Left1;
Right=Right1;
height=height1;
}

Points Triangle::GetLeft() const
{return Left;}
Points Triangle::GetRight() const
{return Right;}
float Triangle::GetHeight() const
{return height;}
void Triangle::MoveBy(float dx,float dy)
{
Left=Left.MoveBy(dx,dy);
Right=Right.MoveBy(dx,dy);
}

#endif
