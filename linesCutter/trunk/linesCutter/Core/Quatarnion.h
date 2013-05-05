#ifndef my_quat
#define my_quat

#include "Vector3.h"

class Quternion
{
private:
public:
	double X;
	double Y;
	double Z;
	double W;
	Quternion()
	{
		X = Y = Z = 0;
		W = 1;
	}

	Quternion(Vector3 xyz, double w)
	{
		X = xyz.X;
		Y = xyz.Y;
		Z = xyz.Z;
		W = w;
	}

	Quternion(double x, double y, double z, double w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	Quternion operator +() const
	{
		return Quternion(X, Y, Z, W);
	}

	Quternion operator +(const Quternion &obj) const
	{
		return Quternion(X + obj.X, Y + obj.Y, Z + obj.Z, W + obj.W);
	}

	Quternion operator -() const
	{
		return Quternion(-X, -Y, -Z, -W);
	}

	Quternion operator -(const Quternion &obj) const
	{
		return *this + (-obj);
	}

	Quternion operator *(double value) const
	{
		return Quternion(X * value, Y * value, Z * value, W * value);
	}
	
	Quternion operator /(double value) const
	{
		return Quternion(X / value, Y / value, Z / value, W / value);
	}
	/*
	static Quternion operator /(double a, const Quternion &b)
	{
		return b / a;
	}

	static Quternion operator *(double a, const Quternion &b)
	{
		return b * a;
	}
	*/
	Quternion operator *(const Quternion &obj) const
	{
		return Quternion(
			X * obj.W + Y * obj.Z - Z * obj.Y + W * obj.X,
		  - X * obj.Z + Y * obj.W + Z * obj.X + W * obj.Y,
			X * obj.Y - Y * obj.X + Z * obj.W + W * obj.Z,
		  - X * obj.X - Y * obj.Y - Z * obj.Z + W * obj.W);
	}

	void operator +=(const Quternion &obj)
	{
		*this = *this + obj;
	}

	void operator -=(const Quternion &obj)
	{
		*this = *this - obj;
	}

	void operator *=(const Quternion &obj)
	{
		*this = *this * obj;
	}

	void operator *=(double value)
	{
		*this = *this * value;
	}

	void operator /=(double value)
	{
		*this = *this / value;
	}

	Quternion Normalize() const
	{
		Quternion r(*this);
		double a = X * X + Y * Y + Z * Z + W * W;
		//if (fabs(a) > 0.00001 && fabs(a - 1.0) > 0.00001)
		{
			double b = sqrt(a);
			r.X /= b;
			r.Y /= b;
			r.Z /= b;
			r.W /= b;
		}
		return r;
	}

	Quternion Conjugate() const
	{
		return Quternion(-X, -Y, -Z, W);
	}

	Vector3 Transform(const Vector3 &value) const
	{
		Quternion q(value, 0);
		Quternion r = *this * q * Conjugate();
		return Vector3(r.X, r.Y, r.Z);
	}

	//void O_o()
	//{
	//	O_o();
	//}
};

#endif