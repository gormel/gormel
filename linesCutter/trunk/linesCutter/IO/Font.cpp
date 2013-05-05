#include "Font.h"

Font::Font(Gdiplus::Bitmap *font)
{
	int w = font->GetWidth() / 16;
	int h = font->GetHeight() / 16;

	sybolWidth = w;
	symbolHeight = h;

	symbols = glGenLists(256);

	for (unsigned char c = 0; c < 255; c++)
	{
		int x = c % 16;
		int y = c / 16;

		int x_ = x * w;
		int y_ = y * h;

		Gdiplus::Bitmap *p = font->Clone(x_ + 1, y_ + 1, w - 1, h - 1, font->GetPixelFormat());
		createSymbol(p, symbols + (unsigned char)c);
		delete p;
	}
}

void Font::createSymbol(Gdiplus::Bitmap *bmp, GLuint list)
{
	glNewList(list, GL_COMPILE_AND_EXECUTE);
	glBegin(GL_QUADS);

	std::vector<std::pair<int, int>> was;

	for (int x = 0; x < bmp->GetWidth(); x++)
	{
		for (int y = 0; y < bmp->GetHeight(); y++)
		{
			Gdiplus::Color c;
			bmp->GetPixel(x, y, &c);
			if (c.GetR() == 0 && c.GetG() == 0 && c.GetB() == 0)
			{
				glVertex2d(x - 0.5 - sybolWidth / 2, -y + 0.5 + symbolHeight / 2);
				glVertex2d(x + 0.5 - sybolWidth / 2, -y + 0.5 + symbolHeight / 2);
				glVertex2d(x + 0.5 - sybolWidth / 2, -y - 0.5 + symbolHeight / 2);
				glVertex2d(x - 0.5 - sybolWidth / 2, -y - 0.5 + symbolHeight / 2);
			}
		}
	}

	glEnd();
	glEndList();
}

void Font::DrawString(std::string str, int x, int y, float scaleX, float scaleY, Vector3 color)
{
	glColor3d(color.X / 255, color.Y / 255, color.Z / 255);
	int i = 0;
	int x_ = 0;
	int y_ = 0;
	while (i != str.length())
	{
		unsigned char c = str.at(i);
		if (c == '\n')
		{
			x_ = 0;
			y_++;
		}
		else
		{
			glPushMatrix();

			//glLineWidth((scaleX + scaleY) / 2);
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
			glTranslated(x + x_ * scaleX * sybolWidth + sybolWidth / 2, 
						-y - y_ * scaleY * symbolHeight - symbolHeight / 2, 0);
			glScaled(scaleX, scaleY, 1);
			glCallList(symbols + c);

			glPopMatrix();
			x_++;
		}
		i++;
	}
}

Vector3 Font::TextBounds(std::string str)
{
	int i = 0;
	float w = 0;
	float maxW = 0;
	float h = 0;

	while (i < str.length())
	{
		if (str.at(i) == '\n' || i == str.length() - 1)
		{
			h += symbolHeight;
			w = 0;
		}
		w += sybolWidth;
		if (w > maxW)
			maxW = w;
		i++;
	}

	return Vector3(maxW, h, 0);
}

//☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬☻↑↓→←∟↔▲▼ !"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~☺2•АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийкльноп