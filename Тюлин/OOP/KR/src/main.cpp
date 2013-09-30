//file: main.cpp
//entry point
//author: Tyulin Roman
//date: 20.04.2013

#include <iostream.h>
#include <strstream.h>
#include <assert.h>
#include <conio.h>
#include "slist.h"
#include "cap.h"
#include "point.h"
#include "showview.h"

const int HELP = 0;
const int EXIT = 1;
const int ADD = 2;
const int ADD_ = 3;
const int REMOVE = 4;
const int MOVE = 5;
const int SHOW = 6;

int ProcessCommand(int command, istream &args);
void Add(istream &args);
void AddSticker(istream &args);
void Remove(istream &args);
void Move(istream &args);
void Move(Cap *cap, Point dxdy);
void Show(istream &args);
void Show(const Cap *cap);
void Help();
void GenerateShowcaseView();
template<class T>
T Read(istream &is, T *fake);

ShowcaseView *view;

int main()
{
	clrscr();
	cout << "Caps manipulator." << endl
		 << "created by Tyulin Roman(c)" << endl;
	GenerateShowcaseView();
	Help();
	while (1)
	{
		char line[255];
		cin.getline(line, 255);
		istrstream str(line);
		int command;
		str >> command;
		if (!ProcessCommand(command, str))
			break;
	}
	return 0;
}

int ProcessCommand(int command, istream &args)
{
	while (!args.eof())
	{
		switch(command)
		{
			case HELP:
				Help();
				return 1;
			case EXIT:
				return 0;
			case ADD:
				Add(args);
				break;
			case REMOVE:
				Remove(args);
				break;
			case MOVE:
				Move(args);
				break;
			case SHOW:
				Show(args);
				break;
			default:
				cout << "Wrong command, try 0." << endl;
				return 1;
		}
	}
	cout << "Command done." << endl;
	return 1;
}

void Add(istream &args)
{
	Point p;
	args >> p;
	if (args.eof())
		return;
	double w1 = Read(args, (double *)0);
	if (args.eof())
		return;
	double w2 = Read(args, (double *)0);
	if (args.eof())
		return;
	double h1 = Read(args, (double *)0);
	if (args.eof())
		return;
	double h2 = Read(args, (double *)0);

	if (args.eof())
		return;

	Cap *cap = new Cap(p, w1, w2, h1, h2);
	if (!view->Validate(*cap))
	{
		cout << "Wrong arguments. Cap is out of range." << endl;
		return;
	}
	view->Add(cap);
}

void AddSticker(istream &args)
{
	Point p;
	args >> p;
	if (args.eof())
		return;
	double w1 = Read(args, (double *)0);
	if (args.eof())
		return;
	double w2 = Read(args, (double *)0);
	if (args.eof())
		return;
	double h1 = Read(args, (double *)0);
	if (args.eof())
		return;
	double h2 = Read(args, (double *)0);

	if (args.eof())
		return;

	Goodcap *cap = new Goodcap(p, w1, w2, h1, h2);
	if (!view->Validate(*cap))
	{
		cout << "Wrong arguments. Cap is out of range." << endl;
		return;
	}
	view->Add(cap);
}

void Remove(istream &args)
{
	int index = Read(args, (int *)0);

	if (args.eof())
		return;

	if (index == -1)
	{
		view->Clear();
		return;
	}
	assert(index >= 0 && index < view->Count());
	Cap *toDelete = view->Get(index);
	delete toDelete;
	view->RemoveAt(index);
}

void Move(istream &args)//{10, 10} 10 5 2 5 {10, 10} 10 5 2 5 {11, 12} 10 6 2 5
{
	int index = Read(args, (int *)0);
	if (args.eof())
		return;

	Point dxdy;
	args >> dxdy;

	if (args.eof())
		return;

	if (index == -1)
	{
		for (int i = 0; i < view->Count(); ++i)
		{
			Move(view->Get(i), dxdy);
		}
		return;
	}

	assert(index >=0 && index < view->Count());
	Move(view->Get(index), dxdy);
}

void Move(Cap *cap, Point dxdy)
{
	cap->MoveBy(dxdy);
	if (!view->Validate(*cap))
	{
		cout << "Wrong parameters. Cap is out of range." << endl;
		cap->MoveBy(Point(-dxdy.GetX(), -dxdy.GetY()));
		return;
	}
}

void Show(istream &args)
{
	int index = Read(args, (int *)0);

	if (args.eof())
		return;

	if (index == -1)
	{
		for (int i = 0; i < view->Count(); ++i)
		{
			cout << "[" << i << "]";
			Show(view->Get(i));
			cout << endl;
		}
		return;
	}

	assert(index >= 0 && index < view->Count());
	cout << "[" << index << "]";
	Show(view->Get(index));
	cout << endl;
}

void Show(const Cap *cap)
{
	CycleList<Point> capPoints = cap->GetPoints();
	cout << "( ";
	for (int i = 0; i < capPoints.Count() - 1; ++i)
	{
		cout << capPoints.Get(i) << ", ";
	}
	cout << capPoints.Get(capPoints.Count() - 1) << " )";
}

void Help()
{
	cout << HELP << " - Show this message." << endl
		 << EXIT << " - Exit program." << endl
		 << ADD << " - Add new caps in list (pos, w1, w2, h1, h2; w1 > w2)." << endl
		 << ADD_ << " - Add new sticker caps in list (pos, w1, w2, h1, h2; w1 > w2)." << endl
		 << REMOVE << " - Remove caps by indices (-1 to clear)." << endl
		 << MOVE << " - Move caps by indices (-1 to move all)." << endl
		 << SHOW << " - Show caps by indices (-1 to show all)." << endl;
}

void GenerateShowcaseView()
{
	cout << "input showcase view parameters: (position, height, down side, top side)" << endl;
	Point pos;
	cin >> pos;
	double h;
	cin >> h;
	double d;
	cin >> d;
	double t;
	cin >> t;
	view = new ShowcaseView(pos, h, d, t);
}

template<class T>
T Read(istream &is, T *fake)
{
	fake = fake; //не стирать, руки оторву!
	//assert(!is.eof());
	T var;
	is >> var;
	return var;
}