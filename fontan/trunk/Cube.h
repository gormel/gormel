#ifndef my_cube
#define my_cube

#include "VertexObject.h"

class Cube : public VertexObject
{
public:
	Cube()
	{
		Vector3 verts[] = {
			Vector3(-0.5, -0.5, -0.5), //left
			Vector3(-0.5,  0.5, -0.5),
			Vector3(-0.5,  0.5,  0.5),
			Vector3(-0.5, -0.5,  0.5),
			
			Vector3(0.5, -0.5, -0.5), //right
			Vector3(0.5, -0.5,  0.5),
			Vector3(0.5,  0.5,  0.5),
			Vector3(0.5,  0.5, -0.5),

			Vector3(-0.5, -0.5, -0.5),//bottom
			Vector3( 0.5, -0.5, -0.5),
			Vector3( 0.5, -0.5,  0.5), 
			Vector3(-0.5, -0.5,  0.5),

			Vector3(-0.5, 0.5, -0.5), //top
			Vector3(-0.5, 0.5,  0.5),
			Vector3( 0.5, 0.5,  0.5),
			Vector3( 0.5, 0.5, -0.5),

			Vector3(-0.5, -0.5, -0.5),//back
			Vector3(-0.5,  0.5, -0.5),
			Vector3( 0.5,  0.5, -0.5), 
			Vector3( 0.5, -0.5, -0.5),

			Vector3(-0.5, -0.5, 0.5), //front
			Vector3( 0.5, -0.5, 0.5),
			Vector3( 0.5,  0.5, 0.5),
			Vector3(-0.5,  0.5, 0.5),
		};

		Vector3 normals[24];
		for (int i = 0; i < 4; i++)
			normals[i] = Vector3(-1, 0, 0);
		for (int i = 4; i < 8; i++)
			normals[i] = Vector3(1, 0, 0);
		for (int i = 8; i < 12; i++)
			normals[i] = Vector3(0, -1, 0);
		for (int i = 12; i < 16; i++)
			normals[i] = Vector3(0, 1, 0);
		for (int i = 16; i < 20; i++)
			normals[i] = Vector3(0, 0, -1);
		for (int i = 20; i < 24; i++)
			normals[i] = Vector3(0, 0, 1);

		initList(verts, normals, nullptr, 24, GL_FILL, GL_FRONT, GL_QUADS);
	}

	virtual void update( long timeSpend )
	{
	}

};

#endif