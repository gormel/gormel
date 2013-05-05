#ifndef my_multiline
#define my_multiline

#include <vector>
#include "Core\Vector3.h"

class Multiline
{
private:
	std::vector<Vector3> verts;
public:
	Vector3 Color;
	Multiline(Vector3 *vectices, int vertCount, const Vector3 &color)
		: Color(color)
	{
		for (int i = 0; i < vertCount; i++)
		{
			verts.push_back(vectices[i]);
		}
	}

	int Count()
	{
		return verts.size();
	}

	Vector3 &operator [](int index)
	{
		return verts.at(index);
	}

	auto begin()->decltype(verts.begin())
	{
		return verts.begin();
	}

	auto end()->decltype(verts.end())
	{
		return verts.end();
	}
};

#endif