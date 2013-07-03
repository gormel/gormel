//Fail elementp.cpp
//metodi elementa
//avtor:Obmanov Ilja
//data: 10.04.2013


#ifndef ELEM_CPP
#define ELEM_CPP

#include <iostream.h>

#include "elementp.h"

template<class T>
Elem<T>::~Elem()
{
}
template<class T>
Elem<T>::Elem(const T &data)
:data(data)
{
	next=NULL;

}

template<class T>
T &Elem<T>::getData()
{
	return data;
}

template<class T>
Elem<T>* Elem<T>::getNext()
{
	return next;
}

template<class T>
void Elem<T>::setData(const T &data)
{
	this->data=data;
}

template<class T>
void Elem<T>::setNext(Elem<T>* next)
{
	this->next=next;
}

#endif