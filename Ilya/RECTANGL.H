#include <metpoint.cpp>
class Rectangle
	{
	private:
	Points topLeft;
	Points topRight;
	Points bottomLeft;
	Points bottomRight;
	public:
	Rectangle(const Points topLeft1,const Points topRight2,const Points bottomLeft1,const Points bottomRight1);
	Points GetTopLeft();
	Points GetTopRight();
	Points GetBottomLeft();
	Points GetBottomRight();
	}
;