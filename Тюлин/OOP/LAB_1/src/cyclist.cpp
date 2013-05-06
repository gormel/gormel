#include <assert.h>
#include <iostream.h>
#include "cyclist.h"

IntCycleList::Node::Node(int value)
	: value(value) {}

IntCycleList::IntCycleList()
	: count(0), last(0) {}

IntCycleList::IntCycleList(const IntCycleList &obj)
	: count(0), last(0)
{
	Node *objLast = obj.last->next;
	for (int i = 0; i < obj.count; i++)
	{
		Add(objLast->value);
		objLast = objLast->next;
	}
}

IntCycleList &IntCycleList::operator =(const IntCycleList &obj)
{
	Clear();
	Node *current = obj.last->next;
	for (int i = 0; i < obj.count; ++i)
	{
		Add(current->value);
		current = current->next;
	}
	return *this;
}

IntCycleList::~IntCycleList()
{
	Clear();
}

void IntCycleList::Add(int value)
{
	Node *newNode = new Node(value);
    count++;
	if (!last)
	{
		last = newNode;
		last->next = last;
		return;		        	
	}

	Node *first = last->next;
	last->next = newNode;
	newNode->next = first;
	last = newNode;
}

int &IntCycleList::Get(int position)
{
	assert(position >= 0 && position < count);
	Node *p = last->next;
	for (int i = 0; i < position; i++)
		p = p->next;
	return p->value;
}   

int IntCycleList::Find(int value) const
{
	Node *p = last->next;
	for (int i = 0; i < count; i++)
		if (p->value == value)
			return i;
	return -1;
} 

void IntCycleList::Remove(int value)
{
	int watched = 0;
	Node *p = last;
	while (p->next->value != value)
	{
		if (watched >= count)
			return;
		p = p->next;
		watched++;
	}
	
	Node *deleting = p->next;
	p->next = p->next->next;
	delete deleting;
	last = p;
	count--;
}

void IntCycleList::RemoveAt(int position)
{
	assert(position >= 0 && position < count);
	Node *p = last;
	for (int i = 0; i < position; i++)
		p = p->next;
	Node *deleting = p->next;
	p->next = deleting->next;
	if (deleting == last)
		last = p;
	delete deleting;
	count--;
}

int IntCycleList::Count() const
{
	return count;
}

void IntCycleList::Clear()
{
	Node *first = last->next;
	for (int i = 0; i < count; ++i)
	{
		Node *deleting = first;
		first = first->next;
			delete deleting;
		
	}
	last = 0;
	count = 0;
}