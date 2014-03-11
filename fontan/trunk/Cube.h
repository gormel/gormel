#ifndef my_cube
#define my_cube

#include "VertexObject.h"

class Cube : public BaseObject
{
private:
protected:
	Vector3 *vertices;
	Vector3 *normals;
	static const int vertCount = 24;

	virtual void update(long timeSpend )
	{
	}

	virtual void draw(long timeSpend)
	{
		Color *colors = GetColors();
		glBegin(GL_QUADS);
		for (int i = 0; i < vertCount; i++)
		{
			glNormal3d(normals[i].X, normals[i].Y, normals[i].Z);
			if (colors)
				glColor4d(colors[i].GetG(), colors[i].GetG(), colors[i].GetB(), colors[i].GetA());
			glVertex3d(vertices[i].X, vertices[i].Y, vertices[i].Z);
		}
		glEnd();
	}

	virtual Color *GetColors()
	{
		return nullptr;
	}
public:
	Cube()
	{
		int t = 0;
		vertices = new Vector3[vertCount];
		vertices[t++] = Vector3(-0.5, -0.5, -0.5); //left
		vertices[t++] = Vector3(-0.5,  0.5, -0.5);
		vertices[t++] = Vector3(-0.5,  0.5,  0.5);
		vertices[t++] = Vector3(-0.5, -0.5,  0.5);

		vertices[t++] = Vector3(0.5, -0.5, -0.5);//right
		vertices[t++] = Vector3(0.5, -0.5,  0.5);
		vertices[t++] = Vector3(0.5,  0.5,  0.5);
		vertices[t++] = Vector3(0.5,  0.5, -0.5);

		vertices[t++] = Vector3(-0.5, -0.5, -0.5);//bottom
		vertices[t++] = Vector3( 0.5, -0.5, -0.5);
		vertices[t++] = Vector3( 0.5, -0.5,  0.5); 
		vertices[t++] = Vector3(-0.5, -0.5,  0.5);

		vertices[t++] = Vector3(-0.5, 0.5, -0.5);//top
		vertices[t++] = Vector3(-0.5, 0.5,  0.5);
		vertices[t++] = Vector3( 0.5, 0.5,  0.5);
		vertices[t++] = Vector3( 0.5, 0.5, -0.5);

		vertices[t++] = Vector3(-0.5, -0.5, -0.5);//back
		vertices[t++] = Vector3(-0.5,  0.5, -0.5);
		vertices[t++] = Vector3( 0.5,  0.5, -0.5); 
		vertices[t++] = Vector3( 0.5, -0.5, -0.5);

		vertices[t++] = Vector3(-0.5, -0.5, 0.5);//front
		vertices[t++] = Vector3( 0.5, -0.5, 0.5);
		vertices[t++] = Vector3( 0.5,  0.5, 0.5);
		vertices[t++] = Vector3(-0.5,  0.5, 0.5);

		normals = new Vector3[vertCount];
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
	}


	virtual ~Cube() {}

};

#endif