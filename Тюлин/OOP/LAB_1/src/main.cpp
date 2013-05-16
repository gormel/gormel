#include <stdlib.h>
#ifdef BORLAND
	#include <iostream.h>
#else
	#include <iostream>
#endif
#include <time.h>
#include <string.h>
#include "intlist.h"
#include "mullist.h"

#ifndef BORLAND
	using namespace std;
#endif

const int ADD_TO_LIST = 0;
const int REMOVE_FROM_LIST = 1;
const int SHOW_LIST = 2;
const int CREATE_MULTILIST = 3;
const int SHOW_MULTILIST = 4;
const int EXIT = 5;
const int HELP = 6;

IntSingleList list;
MultiList multilist;

int ProcessCommand(int command, int *args, int arglLen);
void ShowList(ostream &os);
void CreateMultilist();
void ShowMultilist(ostream &os);
void ShowSubList(ostream &os, int sublist);
void ShowHelp(ostream &os);

int main()
{
	srand(time(0));
	ShowHelp(cout);
	int *args = new int[4];
	int argsSize = 4;
	int lastArg = 0;
	while (1)
	{
		char line[255];
		cin.getline(line, 255);
		int command = atoi(line);
		char *tLine = line;
		lastArg = 0;
		tLine = strchr(tLine, ' ') + 1;
		while (*tLine && tLine)
		{
			int arg = atoi(tLine);
			if (lastArg == argsSize)
			{
				int *newArgs = new int[argsSize * 2];
				memcpy(newArgs, args, argsSize);
				argsSize *= 2;
				delete[] args;
				args = newArgs;
			}
			args[lastArg++] = arg;
			tLine = strchr(tLine, ' ') + 1;
		}
		if (!ProcessCommand(command, args, lastArg))
			break;
	}

	delete[] args;
	return 0;
}

int ProcessCommand(int command, int *args, int arglLen)
{
	int i;
	switch(command)
	{
	case ADD_TO_LIST:
		for (i = 0; i < arglLen; ++i)
		{
			list.Add(args[i]);
		}
		cout << arglLen << " elements successfully added." << endl;
		break;
	case REMOVE_FROM_LIST:
		for (i = 0; i < arglLen; ++i)
		{
			list.Remove(args[i]);
		}
		cout << arglLen << " elements successfully removed" << endl;
		break;
	case SHOW_LIST:
		ShowList(cout);
		break;
	case CREATE_MULTILIST:
		CreateMultilist();
		cout << "multilist successfully created" << endl;
		break;
	case SHOW_MULTILIST:
		ShowMultilist(cout);
		break;
	case EXIT:
		return 0;
	case HELP:
		ShowHelp(cout);
		break;
	default:
		cout << "Wrong command, try '6'." << endl;
		break;
	};
	return 1;
}

void ShowList(ostream &os)
{
	os << "{ ";
	for (int i = 0; i < list.Count(); ++i)
	{
		os << list.Get(i) << " ";
	}
	os << "}" << endl;
}

void CreateMultilist()
{
	multilist.Clear();
	for (int i = 0; i < list.Count(); ++i)
	{
		int value = list.Get(i);
		multilist.Add(value);
		if (value > 0)
			multilist.Add(MultiList::POSITIVE_SUBLIST, value);
		else
			multilist.Add(MultiList::NONPOSITIVE_SUBLIST, value);
	}
}

void ShowMultilist(ostream &os)
{
	os << "{ " << endl;
	os << "positive: ";
	ShowSubList(os, MultiList::POSITIVE_SUBLIST);
	os << "non-positive: ";
	ShowSubList(os, MultiList::NONPOSITIVE_SUBLIST);
	os << "}" << endl;
}

void ShowSubList(ostream &os, int sublist)
{
	os << "{ ";
	for (int i = 0; i < multilist.Count(sublist); ++i)
	{
		os << multilist.Get(sublist, i) << " ";
	}
	os << "}" << endl;
}

void ShowHelp(ostream &os)
{
	os << "0 - add elements to list" << endl
	   << "1 - remove elements from list" << endl
	   << "2 - show list" << endl
	   << "3 - create multilist" << endl
	   << "4 - show mutilist" << endl
	   << "5 - exit" << endl
	   << "6 - show this message" << endl;
}