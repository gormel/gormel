#ifndef my_color
#define my_color

class Color
{
private:
public:
	unsigned char R;
	unsigned char G;
	unsigned char B;
	unsigned char A;
	Color(char r, char g, char b, char a)
		: R(r), G(g), B(b), A(a)
	{
	}

	Color(char r, char g, char b)
		: R(r), G(g), B(b), A(255)
	{
	}

	Color(double r, double g, double b, double a)
		: R(r * 255), G(g * 255), B(b * 255), A(a * 255)
	{
	}

	Color(double r, double g, double b)
		: R(r * 255), G(g * 255), B(b * 255), A(255)
	{
	}

	Color()
		: R(0), G(0), B(0), A(255)
	{
	}

	double GetR()
	{
		return (double)R / 255;
	}

	double GetG()
	{
		return (double)G / 255;
	}

	double GetB()
	{
		return (double)B / 255;
	}

	double GetA()
	{
		return (double)A / 255;
	}
};

#endif