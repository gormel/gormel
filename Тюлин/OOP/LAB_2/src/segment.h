#ifndef my_segment
#define my_segment

class Segment
{
private:
	Point center;
	Point start;
	float angle;
public:
	Segment(Point center, Point start, float angle);
	Segment(const Segment &obj);
	~Segment();

	Point GetCenter();
	Point GetStart();
	float GetAngle();

	void MoveBy(Point dxdy);
	void RotateStart(float radians);
	void SetAngle(float angle);
};

#endif