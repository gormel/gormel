//Fail elementp.cpp
//class element
//avtor:Obmanov Ilja
//data: 10.04.2013

#ifndef ELEM_H
#define ELEM_H
#include "metpoint.cpp"
template<class T>
class Elem
{
private:
	T data;
	Elem* next;
public:
	Elem();
	Elem(const T &data);
	T &getData();
	Elem<T>* getNext();
	void setData(const T &data);
	void setNext(Elem*);
	~Elem();
};

#endif ELEM_H