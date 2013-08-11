#ifndef _triangle_
#define _triangle_
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


#endif
