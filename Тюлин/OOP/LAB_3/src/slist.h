//file: slist.h
//template list class header
//autor: Tyulin Roman
//date: 20.04.2013

#ifndef my_slist
#define my_slist

#include <assert.h>

#ifdef TALKY
	#include <iostream.h>
#endif

#ifndef BORLAND
	using namespace std;
#endif

template<class T>
class SingleListNode
{
public:
	T value;
	SingleListNode<T> *next;
	SingleListNode(const T &value);
};

template<class T>
class SingleList
{
private:

	SingleListNode<T> *first;
	SingleListNode<T> *last;
	int count;
public:
	SingleList();

	SingleList(const SingleList<T> &obj);
	SingleList<T> &operator =(const SingleList<T> &obj);
	~SingleList();

	void Add(const T &value);
	T &Get(int position);
	int Find(const T &value) const;
	void Remove(const T &value);
	void RemoveAt(int position);
	void Clear();
	int Count() const;
};

#include "slist.hpp"

#endif