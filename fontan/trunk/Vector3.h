#ifndef my_vector3
#define my_vector3

#include <cmath>
#include <limits>

class Vector3
{
private:
public:
	double X;
	double Y;
	double Z;

	Vector3()
	{
		X = Y = Z = 0;
	}

	Vector3(double xyz)
	{
		X = Y = Z = xyz;
	}

	Vector3(double x, double y, double z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	Vector3 operator +() const
	{
		return Vector3(X, Y, Z);
	}

	Vector3 operator +(const Vector3 &obj) const
	{
		return Vector3(X + obj.X, Y + obj.Y, Z + obj.Z);
	}

	void operator +=(const Vector3 &obj)
	{
		X += obj.X;
		Y += obj.Y;
		Z += obj.Z;
	}

	Vector3 operator -() const
	{
		return Vector3(-X, -Y, -Z);
	}

	Vector3 operator -(const Vector3 &obj) const
	{
		return Vector3(X - obj.X, Y - obj.Y, Z - obj.Z);
	}

	void operator -=(const Vector3 &obj)
	{
		X -= obj.X;
		Y -= obj.Y;
		Z -= obj.Z;
	}

	Vector3 operator *(double value) const
	{
		return Vector3(X * value, Y * value, Z * value);
	}

	/*static Vector3 operator *(double a, const Vector3 &b)
	{
		return b * a;
	}*/

	Vector3 operator /(double value) const
	{
		return *this * (1 / value);
	}

	/*static Vector3 operator /(double a, const Vector3 &b)
	{
		return b / a;
	}
*/
	void operator *=(double value)
	{
		X *= value;
		Y *= value;
		Z *= value;
	}

	void operator /=(double value)
	{
		return *this *= (1 / value);
	}

	bool operator ==(const Vector3 &obj) const
	{
		return ((X - obj.X < std::numeric_limits<double>::epsilon()) &&
			    (Y - obj.Y < std::numeric_limits<double>::epsilon()) &&
				(Z - obj.Z < std::numeric_limits<double>::epsilon()));
	}

	bool operator !=(const Vector3 &obj) const
	{
		return !(*this == obj);
	}

	double Lenght() const
	{
		return powf(X * X + Y * Y + Z * Z, 0.5);
	}

	double LenghtSquared() const
	{
		return X * X + Y * Y + Z * Z;
	}

	Vector3 Normalize() const
	{
		double len = Lenght();
		return Vector3(X / len, Y / len, Z / len);
	}

	double Dot(const Vector3 &obj) const
	{
		return X * obj.X + Y * obj.Y + Z * obj.Z;
	}

	Vector3 Cross(const Vector3 &obj) const
	{
		return Vector3(Y * obj.Z - Z * obj.Y, Z * obj.X - X * obj.Z, X * obj.Y - Y * obj.X);
	}

	static Vector3 Left()
	{
		return Vector3(-1, 0, 0);
	}

	static Vector3 Right()
	{
		return Vector3(1, 0, 0);
	}

	static Vector3 Up()
	{
		return Vector3(0, 1, 0);
	}

	static Vector3 Down()
	{
		return Vector3(0, -1, 0);
	}

	static Vector3 Front()
	{
		return Vector3(0, 0, 1);
	}

	static Vector3 Back()
	{
		return Vector3(0, 0, -1);
	}
};

#endif