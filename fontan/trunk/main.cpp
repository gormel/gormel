#pragma comment(lib, "glu32.lib")
#pragma comment(lib, "glaux.lib")
#pragma comment(lib, "opengl32.lib")

#include <Windows.h>
#include "gl\glut.h"
#include "gl\GL.h"
#include "gl\GLU.h"
#include "gl\GLAux.h"
#include <iostream>

#include "Program.h"
#include "MouseState.h"
#include "Vector3.h"
#include "Quatarnion.h"
#include "Rotation.h"

void CALLBACK resize(int width, int height)
{
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	if (height != 0)
		gluPerspective(45, (double)width / height, 1, 1200);
		//glFrustum(-width / 2, width / 2, -height / 2, height / 2, 1, 1200);
	else
		glOrtho(-400, 400, -300, 300, 1, 1200);

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

	while (spendTime < 1000 / fps) 
	{
		long time = GetTickCount();
		spendTime += time - lastTime;
		Program::Instance()->Update(time - lastTime);
		lastTime = time;
	}
	Program::Instance()->Draw(spendTime);
	spendTime = 0;

	auxSwapBuffers();
}

int main()
{
	float pos[4] = {300, 300, 300, 1};
	float dir[3] = {-1, -1, -1};

	GLfloat mat_specular[] = { 1, 1, 1, 1 };
	GLfloat mat_diffuse[] = { 1, 1, 1, 1 };
	GLfloat mat_ambient[] = { 0.5, 0.5, 0.5, 1 };

	auxInitPosition( 50, 10, 800, 600);
	auxInitDisplayMode( AUX_RGBA | AUX_DEPTH24 | AUX_DOUBLE );
	auxInitWindow( "Graphics" );
	auxIdleFunc(display);
	auxReshapeFunc(resize);
	glEnable(GL_DEPTH_TEST);
	glEnable(GL_COLOR_MATERIAL);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0);
	glEnable(GL_AUTO_NORMAL);
	glEnable(GL_NORMALIZE);
	glLightfv(GL_LIGHT0, GL_POSITION, pos);
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, dir);

	glMaterialfv(GL_FRONT, GL_SPECULAR, mat_specular);
	//glMaterialfv(GL_FRONT, GL_DIFFUSE, mat_diffuse);
	//glMaterialfv(GL_FRONT, GL_AMBIENT, mat_ambient);
	glMaterialf(GL_FRONT, GL_SHININESS, 127);

	auxMainLoop(display);
	return 0;
}