#ifndef my_alphascreen
#define my_alphascreen

#include "GL\GL.H"
#include "AlphaBlendScreen.h"

class AlphaScreen : public AlphaBlendScreen
{
protected:
	virtual void draw(long ellapsedTime)
	{
		glEnable(GL_ALPHA_TEST);
		glAlphaFunc(alphaFunc, 0.5);
		AlphaBlendScreen::draw(ellapsedTime);
		glDisable(GL_ALPHA_TEST);
	}
private:
	GLenum alphaFunc;
public:
	AlphaScreen()
		: alphaFunc(GL_ALWAYS)
	{
	}

	void SetAlphaFunc(GLenum func)
	{
		alphaFunc = func;
	}

	virtual ~AlphaScreen() {}
};

#endif