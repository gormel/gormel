#ifndef my_line
#define my_line

class Line
{
private:
	Point start;
	Point end;
public:
	Line(Point start, Point end);
	Line(const Line &obj);
	~Line();
	Point GetStart();
	Point GetEnd();
	void MoveBy(Point dxdy);
	void RotateEnd(float radians);
};

#endif