#ifndef my_keyboard_state
#define my_keyboard_state

#include <Windows.h>

class KeyboardState
{
private:
	SHORT keys[256];
public:
	KeyboardState()
	{
	}
	static KeyboardState Current()
	{
		KeyboardState kb;

		for (int i = 0; i < 256; i++)
		{
			kb.keys[i] = GetAsyncKeyState(i);
		}
		
		return kb;
	}

	bool IsKeyDown(int VK)
	{
		return keys[VK] != 0;
	}
};

#endif