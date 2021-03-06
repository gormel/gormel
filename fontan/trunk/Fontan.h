#ifndef my_font
#define my_font

#include "gl\GL.H"
#include <vector>
#include <stdlib.h>
#include <algorithm>
#include <assert.h>
#include "BaseObject.h"
#include "Geosphere.h"
#include "Cube.h"
#include "part.h"

class Fontan : public BaseObject
{
private:
	std::vector<Part> parts;
	int partCount;
	long lastCreation;
	float partSize;
	mutable Geosphere model;
protected:
	void draw(long timeSpend)
	{
		for (auto it = parts.begin(); it != parts.end(); it++)
		{
			model.Position = Rotations.Transform(it->Position) + Position;
			model.Rotations = Rotations;
			glColor4d(it->Color.X, it->Color.Y, it->Color.Z, (double)Transparency / 255);
			model.Draw(timeSpend);
		}
	}

	void update(long timeSpend)
	{
		if (Height <= 0 || Radius <= 0)
			return;
		lastCreation += timeSpend;
		if ((int)parts.size() < partCount && lastCreation > 1000 / CreationSpeed)
		{
			lastCreation = 0;
			Part p(Gravity);
			float y = sqrt(2 * Gravity * Height);
			float x = Gravity * Radius / (2 * y);

			x = x * (float)rand() / RAND_MAX / 2 + x / 2;
			//y = (float)rand() / RAND_MAX * y / 2 + y / 2;
			float angle = (float)rand() / RAND_MAX * 360;
			
			p.Velocity = Rotation(0, 1, 0, angle).ToQuternion().Transform(Vector3(x, y, 0));
			p.Position = Vector3(0, 0, 0);
			
			float r = 0.2;//(float)rand() / RAND_MAX;
			float g = 0.2;//(float)rand() / RAND_MAX;
			float b = (float)rand() / RAND_MAX / 2 + 0.5;
			p.Color = Vector3(r, g, b);
			parts.push_back(p);
		}

		for (auto it = parts.begin(); it != parts.end(); it++)
			it->Update(timeSpend);

		std::sort(parts.begin(), parts.end(), [](Part &a, Part &b) { return a.Position.Y < b.Position.Y; });
		while (parts.size() > 0 && parts.front().Position.Y < 0)
			parts.erase(parts.begin());
	}
public:
	static const float Gravity;
	float Radius;
	float Height;
	int CreationSpeed;
	short Transparency;

	Fontan(int pCount, float pSize, int creationSpeed, float radius, float height)
		: partCount(pCount), partSize(pSize), model(2), Radius(radius), Height(height), 
		lastCreation(0), CreationSpeed(creationSpeed), Transparency(128)
	{
		model.Scale = Vector3(pSize);
	}
};

const float Fontan::Gravity = 9.8;

#endif