#ifndef my_prt
#define my_prt

#include "Vector3.h"
#include "Geosphere.h"

class Part
{
public:
	Vector3 Velocity;
	Vector3 Color;
	Vector3 Position;
	float g;

	Part(float gravity)
		: g(gravity)
	{
	}
	
	void Update(long timeSpend)
	{
		Velocity -= Vector3(0, g * timeSpend / 1000, 0);
		Position += Velocity * timeSpend / 1000;
	}
};

#endif