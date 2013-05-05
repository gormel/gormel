#ifndef my_mouse_state
#define my_mouse_state

#include <gl/GLAux.h>
#include <Windows.h>

class MouseState
{
private:
public:
	int X;
	int Y;

	bool Left;
	bool Right;
	bool Middle;

	MouseState()
	{
		X = Y = -1;
		Left = Right = Middle = false;
	}

	static MouseState Current()
	{
		int x, y;
		auxGetMouseLoc(&x, &y);
		MouseState current;

		if ((GetKeyState(VK_LBUTTON) & 0x80) != 0)
			current.Left = true;

		if ((GetKeyState(VK_MBUTTON) & 0x80) != 0)
			current.Middle = true;

		if ((GetKeyState(VK_RBUTTON) & 0x80) != 0)
			current.Right = true;

		current.X = x;
		current.Y = y;
		return current;
	}
};
#endif