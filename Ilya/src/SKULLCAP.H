#ifndef _skulcaps_
#define _skulcaps_
#include <skull.cpp>
#include <cap.cpp>
class SkullCaps
{
	private:
	Skull skull;
	Cap cap;
	public:
	SkullCaps(Points position,float width,float height);
	SkullCaps(const SkullCaps &n);
	List<Points> GetCap();
	List<Points> GetSkull();
	List<Points> GetPoints() const;
	void MoveBy(float dx,float dy);
};
#endif