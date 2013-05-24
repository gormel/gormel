//file: mlist.h
//template class multilist header
//autor: Tyulin Roman
//date: 20.04.2013

#ifndef my_multilist
#define my_multilist
#include <assert.h>
#ifdef BORLAND
	#include <iostream.h>
#else
	#include <iostream>
	using namespace std;
#endif

const int SUBLIST_COUNT = 3;

template<class T>
class MultiNode
{
	public:
	MultiNode<T> *next[SUBLIST_COUNT];
	T value;
	MultiNode(const T &value);
};

template<class T>
class MultiList
{
private:
	const static int MAIN_SUBLIST;

	MultiNode<T> *last[SUBLIST_COUNT];
	int count[SUBLIST_COUNT];
	MultiNode<T> *FindBefore(MultiNode<T> *from, int sublist, const T &element);
	MultiNode<T> *RemoveNoDelete(int sublist, const T &element);
public:
	const static int POSITIVE_SUBLIST;
	const static int NONPOSITIVE_SUBLIST;

	MultiList();
	MultiList(const MultiList<T> &obj);
	MultiList<T> &operator =(const MultiList<T> &obj);
	~MultiList();

	void Add(const T &element);
	int Add(int sublist, const T &element);

	int Remove(const T &element);
	int Remove(int sublist, const T &element);
	void RemoveAt(int sublist, int position);
	void Clear();
	
	T &Get(int sublist, int position);
	int Find(int sublist, const T &element) const;

	int Count(int sublist) const;
};

#include "mlist.hpp"

#endif