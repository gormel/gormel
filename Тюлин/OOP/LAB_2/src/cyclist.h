//file: cyclist.h
//cycled list class header
//autor: Tyulin Roman
//date: 20.04.2013

#ifndef my_cyclelist
#define my_cyclelist

#include <assert.h>
#ifdef BORLAND
	#include <iostream.h>
#else
	#include <iostream>
#endif

template<class T>
class CycleListNode
{
public:
	T value;
	CycleListNode<T> *next;
	CycleListNode(const T &value);
};

template<class T>
class CycleList
{
private:

	CycleListNode<T> *last;
	int count;
public:
	CycleList();

	CycleList(const CycleList<T> &obj);
	CycleList<T> &operator=(const CycleList<T> &obj);
	~CycleList();

	void Add(const T &value);
	T &Get(int position);
	int Find(const T &value) const;
	void Remove(const T &value);
	void RemoveAt(int position);
	int Count() const;
	void Clear();
};

#include "cyclist.hpp"

#endif