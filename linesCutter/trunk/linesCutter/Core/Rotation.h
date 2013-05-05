#ifndef my_rotation
#define my_rotation

#define _USE_MATH_DEFINES
#include <math.h>
#include "Quatarnion.h"

#define sign(x) (((x) < 0) ? -1 : (((x) > 0) ? 1 : 0))

class Rotation
{
private:
	inline double A(double v)
	{
		if (abs(v) > 1)
			return sign(v);
		return v;
	}
public:
	double X;
	double Y;
	double Z;
	double Angle;

	Rotation()
	{
		X = Y = Z = Angle = 0;
	}

	Rotation(double x, double y, double z, double angle)
	{
		X = x;
		Y = y;
		Z = z;
		Angle = angle;
	}

	Rotation(const Vector3 &direction, double angle)
	{
		X = direction.X;
		Y = direction.Y;
		Z = direction.Z;
		Angle = angle;
	}

	Rotation(const Quternion &q)
	{
		double size = sqrt(q.X * q.X + q.Y * q.Y + q.Z * q.Z);
		if (size != 0)
		{
			X = q.X / size;
			Y = q.Y / size;
			Z = q.Z / size;
		}
		else
		{
			X = Y = Z = 0;
		}

		Angle = 2.0 * acos(A(q.W)) * 180.0 / M_PI;
	}

	Quternion ToQuternion() const
	{
		Vector3 vn = Vector3(X, Y, Z).Normalize();
		return Quternion(vn * sin(Angle * M_PI / 360), cos(Angle * M_PI / 360));
	}
};

#endif