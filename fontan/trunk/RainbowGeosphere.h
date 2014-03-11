#ifndef my_rainbowgeo
#define my_rainbowgeo

#include "Geosphere.h"
#include "Vector3.h"
#include "Color.h"
#include <math.h>

class RainbowGeosphere : public Geosphere
{
private:
	double f(double t)
	{
		double a = 0;
		double value = -abs(3 - 6 * modf(t / 6, &a)) + 2;
		if (value < 0)
			return 0;
		if (value > 1)
			return 1;
		return value;
	}

	Color *GenerateColors()
	{
		Color *colors = new Color[GetVertCount()]; 

		int i = 0;
		for (double t = 0; t <= 5; t += 6.0 / GetVertCount())
		{
			Color value(f(t + 3), f(t + 1), f(t + 5));
			colors[i++] = value;
		}

		return colors;

	}
public:
	RainbowGeosphere(int level)
		: Geosphere(level - 1)
	{
		LvlUp();
	}

	virtual void LvlUp()
	{
		Geosphere::LvlUp();
		Color *colors = GenerateColors();
		initList(vert, vert, colors, GetVertCount(), GL_FRONT, GL_FILL, GL_TRIANGLES);
		delete[] colors;
	}
	virtual ~RainbowGeosphere() {};
};

#endif