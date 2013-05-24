//file: mullist.cpp
//multilist class realisation
//autor: Tyulin Roman
//date: 20.04.2013

#include <assert.h>
#ifdef BORLAND
	#include <iostream.h>
#else
	#include <iostream>
#endif
#include "mullist.h"

#ifndef BORLAND
	using namespace std;
#endif

const int MultiList::MAIN_SUBLIST = 0;
const int MultiList::POSITIVE_SUBLIST = 1;
const int MultiList::NONPOSITIVE_SUBLIST = 2;

MultiList::Node::Node(int value)
	: value(value)
{
#ifdef TALKY
	cout << "MultiList::Node created!" << endl;
#endif
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		next[i] = 0;
	}
}

MultiList::Node *MultiList::FindBefore(Node *from, int sublist, int element)
{
	//cout << "FindBefore(" << sublist << ", " << element << ");" << endl;
	Node *p1 = from;
	Node *p = p1->next[sublist];
	//Node *toRet = 0;
	for (int i = 0; i < count[sublist]; ++i)
	{
		if (p->value == element)
			return p1;
		p1 = p;
		p = p->next[sublist];
		//cout << "    step!" << endl;
	}
	return 0;
}

MultiList::Node *MultiList::RemoveNoDelete(int sublist, int element)
{
	Node *before = FindBefore(last[sublist], sublist, element);
	if (!before)
		return 0;
	Node *del = before->next[sublist];
	before->next[sublist] = del->next[sublist];
	if (del == last[sublist])
		last[sublist] = before;
	if (del->next[sublist] == del)
		last[sublist] = 0;
	count[sublist]--;
	return del;
}

MultiList::MultiList()
{
#ifdef TALKY
	cout << "MultiList created!" << endl;
#endif
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		last[i] = 0;
		count[i] = 0;
	}
}

MultiList::MultiList(const MultiList &obj)
{
#ifdef TALKY
	cout << "MultiList created!" << endl;
#endif
	int i;
	for (i = 0; i < SUBLIST_COUNT; ++i)
	{
		last[i] = 0;
		count[i] = 0;
	}
	for (i = 0; i < SUBLIST_COUNT; ++i)
	{
		Node *pp = obj.last[i]->next[i];
		for (int j = 0; j < obj.Count(i); ++j)
		{
			Add(i, pp->value);
			pp = pp->next[i];
		}
	}
}

MultiList &MultiList::operator =(const MultiList &obj)
{
	Clear();
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		Node *pp = obj.last[i]->next[i];
		for (int j = 0; j < obj.Count(i); ++j)
		{
			Add(i, pp->value);
			pp = pp->next[i];
		}
	}
	return *this;
}

MultiList::~MultiList()
{
#ifdef TALKY
	cout << "MultiList deleted!" << endl;
#endif
	Clear();
}

void MultiList::Add(int element)
{
	Add(MAIN_SUBLIST, element);
}

int MultiList::Add(int sublist, int element)
{
	Node *newNode = 0;

	if (sublist != MAIN_SUBLIST)
	{
		newNode = last[MAIN_SUBLIST];
		do
		{
			newNode = FindBefore(newNode, MAIN_SUBLIST, element)->next[MAIN_SUBLIST];
		}
		while (newNode->next[sublist]);
	}
	else
		newNode = new Node(element);

	if (!newNode)
		return 0;
	count[sublist]++;

	if (!last[sublist])
	{
		last[sublist] = newNode;
		newNode->next[sublist] = newNode;
		return 1;
	}

	Node *first = last[sublist]->next[sublist];
	last[sublist]->next[sublist] = newNode;
	newNode->next[sublist] = first;
	last[sublist] = newNode;
	return 1;
}

int MultiList::Remove(int element)
{
	int result = 0;
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		if (i == MAIN_SUBLIST)
			continue;
		if (!result)
			result = Remove(i, element);
		else
			Remove(i, element);
	}

	delete RemoveNoDelete(MAIN_SUBLIST, element);
	return result;
}

int MultiList::Remove(int sublist, int element)
{
	return (int)RemoveNoDelete(sublist, element);
}

void MultiList::RemoveAt(int sublist, int position)
{	
	assert(position >= 0 && position < count[sublist]);
	Node *p = last[sublist];
	for (int i = 0; i < position; ++i)
		p = p->next[sublist];
	Node *del = p->next[sublist];
	p->next[sublist] = del->next[sublist];
	if (del == last[sublist])
		last[sublist] = p;
	if (del->next[sublist] == del)
		last[sublist] = 0;
	count[sublist]--;
}

void MultiList::Clear()
{
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		while (Count(i))
		{
			RemoveAt(i, 0);
		}
	}
}

int &MultiList::Get(int sublist, int position)
{
	assert(position >= 0 && position < count[sublist]);
	Node *p = last[sublist]->next[sublist];
	for (int i = 0; i < position; ++i)
	{
		assert(p);
		p = p->next[sublist];
	}
	return p->value;
}

int MultiList::Find(int sublist, int element) const
{
	Node *p = last[sublist]->next[sublist];
	for (int i = 0; i < count[sublist]; ++i)
	{
		if (p->value == element)
			return i;
		p = p->next[sublist];
	}
	return -1;
}

int MultiList::Count(int sublist) const
{
	return count[sublist];
}