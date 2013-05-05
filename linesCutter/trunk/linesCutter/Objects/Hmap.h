#ifndef my_hmap
#define my_hmap

#include <GdiPlus.h>
#include <Windows.h>
#include "..\Core\VertexObject.h"

class HMap : public VertexObject
{
protected:
	virtual void update(long timeSpend)
	{
	}
private:
	int width;
	int height;
	VertexNormalColor *vertices;
public:

	HMap(Gdiplus::Bitmap *map)
	{
		width = map->GetWidth();
		height = map->GetHeight();

		vertices = new VertexNormalColor[width * height];

		Gdiplus::Color pixel;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				map->GetPixel(x, y, &pixel);
				double h = (double)pixel.GetR() / 255;
				vertices[x + width * y].Position = Vector3((double)x / width - 0.5, h, (double)y / height - 0.5);
				vertices[x + width * y].Color = Vector3(255, 255, 255);
			}
		}
		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Vector3 upper = vertices[x + width * y].Position;
				Vector3 downer = vertices[x + width * y].Position;
				Vector3 lefter = vertices[x + width * y].Position;
				Vector3 righter = vertices[x + width * y].Position;

				if (x > 0)
					lefter = vertices[x - 1 + width * y].Position;
				if (y > 0)
					upper = vertices[x + width * (y - 1)].Position;
				if (x < width - 1)
					righter = vertices[x + 1 + width * y].Position;
				if (y < height - 1)
					downer = vertices[x + width * (y + 1)].Position;

				Vector3 cur = vertices[x + width * y].Position;

				vertices[x + width * y].Normal = (upper - cur).Cross(lefter - cur).Normalize() * 2;
			}
		}

		UpdateList();
	}

	virtual ~HMap()
	{
		delete[] vertices;
	}

	void UpdateList()
	{
		int *indices = new int[(width - 1) * (height - 1) * 6];
		int t = 0;
		for (int x = 0; x < width - 1; x++)
		{
			for (int y = 0; y < height - 1; y++)
			{
				indices[t++] = x + width * y;
				indices[t++] = x + width * (y + 1);
				indices[t++] = x + 1 + width * y;
				
				indices[t++] = x + width * (y + 1);
				indices[t++] = x + 1 + width * (y + 1);
				indices[t++] = x + 1 + width * y;
			}
		}
		initList(vertices, indices, (width - 1) * (height - 1) * 6, GL_FRONT, GL_FILL, GL_TRIANGLES);
		delete[] indices;
	}

	double Height(double x, double y)
	{
		double x_ = x / Scale.X;
		double y_ = y / Scale.Y;

		if (abs(x_) > 0.5 || abs(y_) > 0.5)
			return 0;

		x_ += 0.5;
		y_ += 0.5;

		x_ *= width;
		y_ *= height;
	}
};

#endif