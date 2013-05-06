#include "intlist.h"
#include <assert.h>

IntSingleList::Node::Node(int value)
	: value(value), next(0) {}

IntSingleList::IntSingleList()
	: first(0), last(0), count(0) {}

IntSingleList::IntSingleList(const IntSingleList &obj)
	: count(0), first(0), last(0)
{
	Node *p = obj.first;
	while (p)
	{
		Add(p->value);
		p = p->next;
    }
}

IntSingleList &IntSingleList::operator =(const IntSingleList &obj)
{
	Clear();
	Node *current = obj.first;
	for (int i = 0; i < obj.count; ++i)
	{
		Add(current->value);
		current = current->next;
	}
	return *this;
}

IntSingleList::~IntSingleList()
{
	while (first)
	{
		Node *deleting = first;
		first = first->next;
        delete deleting;
    }
}

void IntSingleList::Add(int value)
{
	count++;
	if (!first)
	{
		first = last = new Node(value);
        return;
	}

	Node *newNode = new Node(value);
	last->next = newNode;
    last = last->next;
}

void IntSingleList::Remove(int value)
{
	if (!first)
		return;

	if (first->value == value)
	{
    	Node *deleting = first;
		first = first->next;
		delete deleting;
        count--;
        return;
	}

	Node *c = first;
	while (c->next)
	{
		if (c->next->value == value)
		{
			Node *deleting = c->next;
			c->next = deleting->next;
			delete deleting;
            count--;
            return;
        }
    }
}

int IntSingleList::Find(int value)
{
	int position = 0;
	Node *c = first;
	while (c)
	{
		if (c->value == value)
		{
        	return position;
		}
        position++;
	}
    return -1;
}

int IntSingleList::Count()
{
	return count;
}

int IntSingleList::Get(int position)
{
	assert(position >= 0 && position < count);
	int currentPosition = 0;
	Node *currentNode = first;
	while (currentPosition++ < position)
	{
		currentNode = currentNode->next;
	}
    return currentNode->value;
}

void IntSingleList::RemoveAt(int position)
{
	assert(position >= 0 && position < count);
	int currentPos = 0;
    Node *currentNode = first;
	while (currentPos++ < position - 1)
	{
    	 currentNode = currentNode->next;
	}
	Node *deleting = currentNode->next;
	currentNode->next = deleting->next;
    delete deleting;
}

void IntSingleList::Clear()
{
	while (first)
	{
		Node *deleting = first;
		first = first->next;
        delete deleting;
	}
    count = 0;
}