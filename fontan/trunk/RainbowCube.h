#ifndef my_rnbcube
#define my_rnbcube

#include "Cube.h"
#include "Color.h"

class RainbowCube : public Cube
{
protected:
	virtual Color *GetColors()
	{
		Color *result = new Color[vertCount];

		for (int i = 0; i < vertCount; i++)
			result[i] = Color((vertices[i].X + 0.5), 
							  (vertices[i].Y + 0.5), 
							  (vertices[i].Z + 0.5), Alpha);

		return result;
	}
public:
	double Alpha;
};

#endif