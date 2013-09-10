#ifndef _list_
#define _list_
#include <triangle.cpp>
#include <metlistp.cpp>
class Cap
	{
	private:
	Triangle trig;
	float height;
	float lenght;
	public:
	Cap(Points positionLeft1,Points positionRight1,float lenght1);
	Cap(const Cap &obj);
	List<Points> PointsList() const;
        void MoveBy(float dx,float dy);
	};
#endif