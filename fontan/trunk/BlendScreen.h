#ifndef my_blend
#define my_blend

#include "AlphaBlendScreen.h"

class BlendScreen : public AlphaBlendScreen
{
protected:
	virtual void draw(long timeSpend)
	{
		glEnable(GL_BLEND);
		glBlendFunc(sfactor, dfactor);
		AlphaBlendScreen::draw(timeSpend);
		glDisable(GL_BLEND);
	}
private:
	GLenum sfactor;
	GLenum dfactor;
public:
	BlendScreen()
		: sfactor(GL_SRC_ALPHA), dfactor(GL_ONE_MINUS_SRC_ALPHA)
	{
	}

	void SetSfactor(GLenum value)
	{
		sfactor = value;
	}

	void SetDfactor(GLenum value)
	{
		dfactor = value;
	}
};

#endif