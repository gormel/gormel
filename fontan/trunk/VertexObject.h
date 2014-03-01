#ifndef my_vertex_object
#define my_vertex_object

#include <Windows.h>
#include "gl/GL.H"
#include "BaseObject.h"

class VertexObject : public BaseObject
{
private:
protected:
	GLuint list;
	virtual void draw(long timeSpend)
	{
		glCallList(list);
	}

	virtual void initList(Vector3 *vertices, Vector3 *normals, Vector3 *colors, int vertCount, 
						  GLenum polygonFace, GLenum polygonMode, GLenum drawingPrimetives)
	{
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
				glColor3d(colors[i].X / 255, colors[i].Y / 255, colors[i].Z / 255);
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

	virtual ~VertexObject() { }
};
#endif