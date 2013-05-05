#ifndef my_font
#define my_font

#include <Windows.h>
#include <GdiPlus.h>
#include <gl/GL.H>
#include <hash_map>
#include <algorithm>
#include "..\Core\Vector3.h"

class Font
{
private:
	GLuint symbols;
	void createSymbol(Gdiplus::Bitmap *bmp, GLuint list);
	double sybolWidth;
	double symbolHeight;
public:
	Font(Gdiplus::Bitmap *font);
	~Font();
	void DrawString(std::string str, int x, int y, float scaleX, float scaleY, Vector3 color);
	Vector3 TextBounds(std::string str);
};

#endif