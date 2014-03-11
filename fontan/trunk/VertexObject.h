#ifndef my_vertex_object
#define my_vertex_object

#include <Windows.h>
#include "GL\GL.H"
#include "BaseObject.h"
#include "Color.h"

class VertexObject : public BaseObject
{
private:
	int vertCount;
protected:
	GLuint list;
	int GetVertCount()
	{
		return vertCount;
	}

	virtual void draw(long timeSpend)
	{
		glCallList(list);
	}

	virtual void initList(Vector3 *vertices, Vector3 *normals, Color *colors, int vertCount, 
						  GLenum polygonFace, GLenum polygonMode, GLenum drawingPrimetives)
	{
		this->vertCount = vertCount;
		if (list != -1)
			glDeleteLists(list, 1);
		list = glGenLists(1);
		glNewList(list, GL_COMPILE);
		glPolygonMode(polygonFace, polygonMode);
		glBegin(drawingPrimetives);
		for (int i = 0; i < vertCount; i++)
		{
			if (normals)
				glNormal3d(normals[i].X, normals[i].Y, normals[i].Z);
			if (colors)
				glColor4d(colors[i].GetR(), colors[i].GetG(), colors[i].GetB(), colors[i].GetA());
			glVertex3d(vertices[i].X, vertices[i].Y, vertices[i].Z);
		}
		glEnd();
		glEndList();
	}
public:
	VertexObject()
	{
		list = -1;
	}

	virtual ~VertexObject() 
	{
		if (list != -1)
			glDeleteLists(list, 1);
	}
};
#endif