#ifndef my_vrt
#define my_vrt

#include <GdiPlus.h>
#include <Windows.h>
#include "Vector3.h"

class Vertex
{
public:
	Vector3 Position;
};

class VertexNormal : public Vertex
{
public:
	Vector3 Normal;
};

class VertexColor : public Vertex
{
public:
	Vector3 Color;
};

class VertexNormalColor : public VertexNormal
{
public:
	Vector3 Color;
};

class VertexNormalTexture : public VertexNormal
{
public:
	Vector3 TexCoords;
	Gdiplus::Bitmap *Texture;
};
#endif