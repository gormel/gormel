#include <metpoint.cpp>
class Triangle
	{
	private:
	Points Left;
	Points Right;
	float height;
	public:
	Triangle(const Points Left1,const Points Right1,float height1);
	Points GetLeft();
	Points GetRight();
	float GetHeight();
	}
;