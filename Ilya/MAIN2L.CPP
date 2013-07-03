#ifndef _main_
#define _main_
#include <skullcap.h>
#include <assert.h>
#include <strstream.h>

const int HELP = 0;
const int EXIT = 1;
const int ADD = 2;
const int REMOVE = 3;
const int MOVE = 4;
const int SHOW = 5;

int ProcessCommand(int command, istream &args);
void Add(istream &args);
void RemoveM(istream &args);
void Move(istream &args);
void Show(istream &args);
void Show(const SkullCaps &skullcap);
void Help();
template<class T>
T Read(istream &is, T *fake);

List<SkullCaps> skullcaps;

int main()
{
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
				RemoveM(args);
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
	Points p;
	args >> p;
	if (args.eof())
		return;
	float w = Read(args, (float *)0);
	if (args.eof())
		return;
	float h = Read(args, (float *)0);
	if (args.eof())
		return;
	SkullCaps skullcap(p, w, h);
	skullcaps.Add(skullcap);
}

void RemoveM(istream &args)
{
	int index = Read(args, (int *)0);

	if (args.eof())
		return;

	if (index == -1)
	{
		skullcaps.Clear();
		return;
	}
      //	assert(index >= 0 && index < caps.Count());
     	skullcaps.RemoveAt(index);
}

void Move(istream &args)
{
	int index = Read(args, (int *)0);
	if (args.eof())
		return;

	Points dxdy;
	args >> dxdy;
	float dx=dxdy.getX();
        float dy=dxdy.getY();

	if (args.eof())
		return;

	if (index == -1)
	{
		for (int i = 0; i < skullcaps.Count(); ++i)
		{
			skullcaps.Get(i).MoveBy(dx,dy);
		}
		return;
	}

    //	assert(index >=0 && index < caps.Count());
	skullcaps.Get(index).MoveBy(dx,dy);
}

void Show(istream &args)
{
	int index = Read(args, (int *)0);

	if (args.eof())
		return;

	if (index == -1)
	{
		for (int i = 0; i < skullcaps.Count(); ++i)
		{
			cout << "[" << i << "]";
			Show(skullcaps.Get(i));
			cout << endl;
		}
		return;
	}

	//assert(index >= 0 && index < caps.Count());
	cout << "[" << index << "]";
	Show(skullcaps.Get(index));
	cout << endl;
}

void Show(const SkullCaps &skullcap)
{
	List<Points> skullcapPoints = skullcap.GetPoints();
	cout << "( ";
	for (int i = 0; i < skullcapPoints.Count() - 1; ++i)
	{
		cout << skullcapPoints.Get(i) << ", ";
	}
	cout << skullcapPoints.Get(skullcapPoints.Count() - 1) << " )";
}

void Help()
{
	cout << "0 - Show this message." << endl
		 << "1 - Exit program." << endl
		 << "2 - Add new caps in list (pos, w1, w2, h1, h2; w1 > w2)." << endl
		 << "3 - Remove caps by indices (-1 to clear)." << endl
		 << "4 - Move caps by indices (-1 to move all)." << endl
		 << "5 - Show caps by indices (-1 to show all)." << endl;
}

template<class T>
T Read(istream &is, T *fake)
{
	fake = fake;
	//assert(!is.eof());
	T var;
	is >> var;
	return var;
}

#endif