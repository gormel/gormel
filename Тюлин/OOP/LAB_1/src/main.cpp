//#define PART_ONE

#include <stdlib.h>
#ifdef BORLAND
	#include <iostream.h>
	#include <strstream.h>
	typedef istrstream istringstream;
#else
	#include <iostream>
	#include <sstream>
#endif
#include <time.h>
#include <string.h>
#ifdef PART_ONE
	#include "intlist.h"
	#include "mullist.h"
	typedef int Type;
#else
	#include "point.h"
	#include "slist.h"
	#include "mlist.h"
	typedef Point Type;
#endif

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
const int SELECT_TYPE = 7;
const int CLEAN = 8;

#ifdef PART_ONE
	IntSingleList list;
	MultiList multilist;
	typedef MultiList MultiListType;
#else
	SingleList<int> iList;
	MultiList<int> iMultilist;
	SingleList<Point> pList;
	MultiList<Point> pMultilist;
	typedef MultiList<int> MultiListType;

	int selectedType = 0;
	const int INT = 0;
	const int POINT = 1;
#endif

int ProcessCommand(int command, istream &args);
void ShowList(ostream &os);
void CreateMultilist();
void ShowMultilist(ostream &os);
void ShowSubList(ostream &os, int sublist);
void ShowHelp(ostream &os);
void AddToList(istream &is);
void RemoveFromList(istream &is);


int main()
{
	srand(time(0));
	ShowHelp(cout);
	while (1)
	{
		char line[255];
		cin.getline(line, 255);
		istringstream str(line);
		int command = 0;
		str >> command;

		if (!ProcessCommand(command, str))
			break;
	}
	return 0;
}

int ProcessCommand(int command, istream &args)
{
	int i;
	switch(command)
	{
	case ADD_TO_LIST:
		AddToList(args);
		cout << "elements successfully added." << endl;
		break;
	case REMOVE_FROM_LIST:
		RemoveFromList(args);
		cout << "elements successfully removed." << endl;
		break;
	case SHOW_LIST:
		ShowList(cout);
		break;
	case CREATE_MULTILIST:
		CreateMultilist();
		cout << "multilist successfully created." << endl;
		break;
	case SHOW_MULTILIST:
		ShowMultilist(cout);
		break;
	case EXIT:
		return 0;
	case HELP:
		ShowHelp(cout);
		break;
#ifndef PART_ONE
	case SELECT_TYPE:
		int type = -1;
		args >> type;
		selectedType = type;
		cout << " type successfully selected." << endl;
		break;
#endif
	case CLEAN:
#ifdef PART_ONE
		list.Clear();
#else
		iList.Clear();
		pList.Clear();
#endif
		cout << "list successfully cleaned." << endl;
		break;
	default:
		cout << "wrong command, try '6'." << endl;
		break;
	};
	return 1;
}

void ShowList(ostream &os)
{
	os << "{ ";
	int count = -1;

#ifdef PART_ONE	
	count = list.Count();
#else
	switch (selectedType)
	{
	case INT:
		count = iList.Count();
		break;
	case POINT:
		count = pList.Count();
		break;
	}
#endif

	for (int i = 0; i < count; ++i)
	{
#ifdef PART_ONE	
		os << list.Get(i) << " ";
#else
		switch (selectedType)
		{
		case INT:
			os << iList.Get(i) << " ";
			break;
		case POINT:
			os << pList.Get(i) << " ";
			break;
		}
#endif
	}
	os << "}" << endl;
}

void CreateMultilist()
{
#ifdef PART_ONE	
	multilist.Clear();
	for (int i = 0; i < list.Count(); ++i)
	{
		int value = list.Get(i);
		multilist.Add(value);
		if (value > 0)
			multilist.Add(MultiListType::POSITIVE_SUBLIST, value);
		else
			multilist.Add(MultiListType::NONPOSITIVE_SUBLIST, value);
	}
#else
	int i = -1;
	switch (selectedType)
	{
	case INT:
		iMultilist.Clear();
		for (i = 0; i < iList.Count(); ++i)
		{
			int value = iList.Get(i);
			iMultilist.Add(value);
			if (value > 0)
				iMultilist.Add(MultiListType::POSITIVE_SUBLIST, value);
			else
				iMultilist.Add(MultiListType::NONPOSITIVE_SUBLIST, value);
		}
		break;
	case POINT:
		pMultilist.Clear();
		for (i = 0; i < pList.Count(); ++i)
		{
			Point value = pList.Get(i);
			pMultilist.Add(value);
			if (value > 0)
				pMultilist.Add(MultiListType::POSITIVE_SUBLIST, value);
			else
				pMultilist.Add(MultiListType::NONPOSITIVE_SUBLIST, value);
		}
		break;
	}
#endif
}

void ShowMultilist(ostream &os)
{
	os << "{ " << endl;
	os << "positive: ";
	ShowSubList(os, MultiListType::POSITIVE_SUBLIST);
	os << "non-positive: ";
	ShowSubList(os, MultiListType::NONPOSITIVE_SUBLIST);
	os << "}" << endl;
}

void ShowSubList(ostream &os, int sublist)
{
	os << "{ ";
	int count = -1;

#ifdef PART_ONE	
		count = multilist.Count(sublist);
#else
		switch (selectedType)
		{
		case INT:
			count = iMultilist.Count(sublist);
			break;
		case POINT:
			count = pMultilist.Count(sublist);
			break;
		}
#endif

	for (int i = 0; i < count; ++i)
	{
#ifdef PART_ONE	
		os << multilist.Get(sublist, i) << " ";
#else
		switch (selectedType)
		{
		case INT:
			os << iMultilist.Get(sublist, i) << " ";
			break;
		case POINT:
			os << pMultilist.Get(sublist, i) << " ";
			break;
		}
#endif
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
	   << "6 - show this message" << endl
#ifndef PART_ONE
	   << "7 - select working type (0 - int, 1 - point)" << endl
#endif
	   << "8 - clean list" << endl;
}

void AddToList(istream &is)
{
	while (!is.eof())
	{
#ifdef PART_ONE
		int arg;
		is >> arg;
		if (is.eof())
			return;
		list.Add(arg);
#else
		switch (selectedType)
		{
		case INT:
			int iArg;
			is >> iArg;
			if (is.eof())
				return;
			iList.Add(iArg);
			break;
		case POINT:
			Point pArg;
			is >> pArg;
			if (is.eof())
				return;
			//cout << pArg << " added.";
			pList.Add(pArg);
			break;
		}
#endif
	}
}

void RemoveFromList(istream &is)
{
	while (!is.eof())
	{
#ifdef PART_ONE
		int arg;
		if (!(is >> arg))
			return;
		list.Remove(arg);
#else
		switch (selectedType)
		{
		case INT:
			int iArg;
			if (!(is >> iArg))
				return;
			iList.Remove(iArg);
			break;
		case POINT:
			Point pArg;
			if (!(is >> pArg))
				return;
			pList.Remove(pArg);
			break;
		}
#endif
	}
}