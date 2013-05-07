#ifndef my_curvecanvas
#define my_curvecanvas

#include "DrawingArea.h"
#include "Multiline.h"

class CurveCanvas : public DrawingArea
{
protected:
	virtual void update(long timeSpend)
	{
	}

	virtual void clipDraw(Multiline &poly)
	{
		double eps = std::numeric_limits<double>::epsilon();
		glColor3d(1, 1, 1);
		glBegin(GL_LINES);
		for (int j = 0; j < poly.Count(); j++)
		{
			auto v1 = poly[j] + InnerPosition;
			auto v2 = poly[(j + 1) % poly.Count()] + InnerPosition;

			auto edge = v2 - v1;

			bool visible = true;
			double CB_t0 = 0;
			double CB_t1 = 1;

			for (int i = 0; i < Corners.size(); i++)
			{
				auto c1 = Corners.at(i);
				auto c2 = Corners.at((i + 1) % Corners.size());

				auto side = c2 - c1;
				auto normal = Vector3(-side.Y, side.X, 0).Normalize();

				double Pn = edge.Dot(normal);
				double Qn = normal.Dot(v1 - c1);

				if (abs(Pn) < eps)
				{
					if (Qn < 0)
					{
						visible = false;
						break;
					}
				}
				else
				{
					double r = -Qn / Pn;
					if (Pn < 0)
					{
						if (r < CB_t0)
						{
							visible = false;
							break;
						}
						if (r < CB_t1)
							CB_t1 = r;
					}
					else
					{
						if (r > CB_t1)
						{
							visible = false;
							break;
						}
						if (r > CB_t0)
							CB_t0 = r;
					}
				}
			}

			if (visible)
			{
				if (CB_t0 < CB_t1)
				{
					if (CB_t0 > 0)
						v1 += edge * CB_t0;
					if (CB_t1 < 1)
						v2 = v1 + edge * CB_t1;
					glVertex2d(v1.X, v1.Y);
					glVertex2d(v2.X, v2.Y);
				}
			}
		}
		glEnd();
	}
};

#endif