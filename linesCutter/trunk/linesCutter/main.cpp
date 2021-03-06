#pragma comment(lib, "glu32.lib")
#pragma comment(lib, "glaux.lib")
#pragma comment(lib, "Gdiplus.lib")

#include <Windows.h>
#include <gl\glut.h>
#include <gl\GL.h>
#include <gl\GLU.h>
#include <gl\GLAux.h>
#include <iostream>

#include "Program.h"
#include "IO\MouseState.h"
#include "Core\Vector3.h"
#include "Core\Quatarnion.h"
#include "Core\Rotation.h"

void CALLBACK resize(int width, int height)
{
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	//if (height != 0)
	//	gluPerspective(45, width / height, 1, 1200);
	//else
	//	glOrtho(-500, 500, -500, 500, 1, 1200);
	glOrtho(0, width, height, 0, 1, 1200);

	gluLookAt(0, 0, 1, 
			  0, 0, 0,
			  0, 1, 0);
	glMatrixMode(GL_MODELVIEW);
}

long spendTime = 0;
long lastTime = 0;
const long fps = 30;

void CALLBACK display(void)
{
	glClear( GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT );

	//system("cls");

	while (spendTime < 1000 / fps) 
	{
		long time = GetTickCount();
		spendTime += time - lastTime;
		Program::Instance()->Update(time - lastTime);
		lastTime = time;
		//std::cout << "Update" << std::endl;
	}
	Program::Instance()->Draw(spendTime);
	//std::cout << "Draw" << std::endl;
	spendTime = 0;

	auxSwapBuffers();
}

int main()
{
	float pos[4] = {300, 300, 300, 1};
	float dir[3] = {-1, -1, -1};

	GLfloat mat_specular[] = {1,1,1,1};

	auxInitPosition( 50, 10, 500, 500);
	auxInitDisplayMode( AUX_RGB | AUX_DEPTH24 | AUX_DOUBLE );
	auxInitWindow( "Window" );
	auxIdleFunc(display);
	auxReshapeFunc(resize);
	
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_COLOR_MATERIAL);
	//glEnable(GL_LIGHTING);
	//glEnable(GL_LIGHT0);
	//glEnable(GL_AUTO_NORMAL);

	//glLightfv(GL_LIGHT0, GL_POSITION, pos);
	//glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, dir);

	//glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	//glMaterialf(GL_FRONT, GL_SHININESS, 128.0);


	auxMainLoop(display);
	return 0;
}