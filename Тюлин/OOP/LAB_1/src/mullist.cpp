#include <assert.h>
#include <iostream.h>
#include "mullist.h"

const int MultiList::MAIN_SUBLIST = 0;
const int MultiList::POSITIVE_SUBLIST = 1;
const int MultiList::NONPOSITIVE_SUBLIST = 2;

MultiList::Node::Node(int value)
	: value(value)
{
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		next[i] = 0;
	}
}

MultiList::Node *MultiList::FindBefore(int sublist, int element)
{
	//cout << "FindBefore(" << sublist << ", " << element << ");" << endl;
	Node *p1 = last[sublist];
	Node *p = p1->next[sublist];
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

MultiList::MultiList()
{
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		last[i] = 0;
		count[i] = 0;
	}
}

MultiList::MultiList(const MultiList &obj)
{
	Node *p = obj.last[MAIN_SUBLIST]->next[MAIN_SUBLIST];
	for (int i = 0; i < obj.Count(MAIN_SUBLIST); ++i)
	{
		Add(p->value);
		p = p->next[MAIN_SUBLIST];
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
	Node *p = obj.last[MAIN_SUBLIST]->next[MAIN_SUBLIST];
	for (int i = 0; i < obj.Count(MAIN_SUBLIST); ++i)
	{
		Add(p->value);
		p = p->next[MAIN_SUBLIST];
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
	return *this;
}

MultiList::~MultiList()
{
	Clear();
}

void MultiList::Add(int element)
{
	Add(MAIN_SUBLIST, element);
}

int MultiList::Add(int sublist, int element)
{
	cout << "Add(" << sublist << ", " << element << ");" << endl;
	Node *newNode = 0;

	if (sublist != MAIN_SUBLIST)
	{
		newNode = FindBefore(MAIN_SUBLIST, element)->next[MAIN_SUBLIST];
		//cout << "Found: " << newNode->value;
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
		result = Remove(i, element);
	}
	return result;
}

int MultiList::Remove(int sublist, int element)
{
	Node *before = FindBefore(sublist, element);
	if (!before)
		return 0;
	Node *del = before->next[sublist];
	before->next[sublist] = del->next[sublist];
	if (del == last[sublist])
		last[sublist] = before;
	delete del;
	count[sublist]--;
	return 1;
}

void MultiList::RemoveAt(int sublist, int position)
{	
	assert(position >= 0 && position < count[sublist]);
	Node *p = last[sublist];
	for (int i = 0; i < position; ++i)
		p = p->next[sublist];
	Node *del = p->next[sublist];
	p->next[sublist] = del->next[sublist];
	delete del;
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
		p = p->next[sublist];
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