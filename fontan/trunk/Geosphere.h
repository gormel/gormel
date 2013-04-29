#ifndef my_geosphere
#define my_geosphere

#include <Windows.h>
#include <gl/GL.H>
#include "VertexObject.h"

class Geosphere : public VertexObject
{
private:
	//size - размер входного массива, заменяется на размер выходного массива
	Vector3 *lvlUp(Vector3 *prev, int &size)
	{
		int a = size * 4;
		Vector3 *rv = new Vector3[a];

		for (int i = 0; i < size / 3; i++)
		{
			int j = i * 3;
			Vector3 v1 = prev[j];
			Vector3 v2 = prev[j + 1];
			Vector3 v3 = prev[j + 2];
			Vector3 v1_ = (v1 + v2) / 2;
			Vector3 v2_ = (v2 + v3) / 2;
			Vector3 v3_ = (v3 + v1) / 2;

			int t = j * 4;
			(rv[t++]) = v1.Normalize();
			(rv[t++]) = v1_.Normalize();
			(rv[t++]) = v3_.Normalize();

			(rv[t++]) = v1_.Normalize();
			(rv[t++]) = v2.Normalize();
			(rv[t++]) = v2_.Normalize();

			(rv[t++]) = v3_.Normalize();
			(rv[t++]) = v1_.Normalize();
			(rv[t++]) = v2_.Normalize();

			(rv[t++]) = v3_.Normalize();
			(rv[t++]) = v2_.Normalize();
			(rv[t++]) = v3.Normalize();
		}
		size *= 4;
		return rv;
	}

	Vector3 *vert;
	int vertCount;
protected:
	virtual void update(long timeSpend)
	{
	}
public:
	Geosphere(int level)
	{
		Vector3 v[] = 
		{
			Vector3(-1,  1,  1),
			Vector3(-1,  1, -1),
			Vector3( 1,  1, -1),
			Vector3( 1,  1,  1),

			Vector3(-1, -1,  1),
			Vector3(-1, -1, -1),
			Vector3( 1, -1, -1),
			Vector3( 1, -1,  1),
		};
		
		Vector3 *newLvl = new Vector3[36];
		newLvl[0] = v[0];
		newLvl[1] = v[1];
		newLvl[2] = v[2];
		newLvl[3] = v[0];
		newLvl[4] = v[2];
		newLvl[5] = v[3];

		newLvl[6] = v[1];
		newLvl[7] = v[0];
		newLvl[8] = v[4];
		newLvl[9] = v[1];
		newLvl[10] = v[4];
		newLvl[11] = v[5];

		newLvl[12] = v[6];
		newLvl[13] = v[2];
		newLvl[14] = v[1];
		newLvl[15] = v[6];
		newLvl[16] = v[1];
		newLvl[17] = v[5];

		newLvl[18] = v[3];
		newLvl[19] = v[2];
		newLvl[20] = v[7];
		newLvl[21] = v[7];
		newLvl[22] = v[2];
		newLvl[23] = v[6];

		newLvl[24] = v[0];
		newLvl[25] = v[3];
		newLvl[26] = v[7];
		newLvl[27] = v[4];
		newLvl[28] = v[0];
		newLvl[29] = v[7];

		newLvl[30] = v[4];
		newLvl[31] = v[7];
		newLvl[32] = v[5];
		newLvl[33] = v[7];
		newLvl[34] = v[6];
		newLvl[35] = v[5];

		int size = 36;

		for (int i = 0; i < level; i++)
		{
			Vector3 *a = newLvl;
			newLvl = lvlUp(a, size);
			delete[] a;
		}

		vert = newLvl;
		vertCount = size;
		initList(newLvl, newLvl, nullptr, size, 
			GL_FRONT_AND_BACK, GL_FILL, GL_TRIANGLES);
	}

	Geosphere(const Geosphere &obj)
	{
		vertCount = obj.vertCount;
		vert = new Vector3[vertCount];
		for (int i = 0; i < vertCount; i++)
			vert[i] = obj.vert[i];
		initList(vert, vert, nullptr, vertCount, 
			GL_FRONT_AND_BACK, GL_FILL, GL_TRIANGLES);
	}

	Geosphere &operator =(const Geosphere &obj)
	{
		delete[] vert;
		vertCount = obj.vertCount;
		vert = new Vector3[vertCount];
		for (int i = 0; i < vertCount; i++)
			vert[i] = obj.vert[i];
		initList(vert, vert, nullptr, vertCount, 
			GL_FRONT_AND_BACK, GL_FILL, GL_TRIANGLES);
		return *this;
	}

	void LvlUp()
	{
		auto a = vert;
		vert = lvlUp(vert, vertCount);
		delete[] a;
		initList(vert, vert, nullptr, vertCount, 
				 GL_FRONT_AND_BACK, GL_LINE, GL_TRIANGLES);
	}

	virtual ~Geosphere()
	{
		delete[] vert;
	}
};

#endif
