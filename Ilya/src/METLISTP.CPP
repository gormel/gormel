//Fail metlistp.cpp
//metodi spiska
//avtor:Obmanov Ilja
//data: 10.04.2013


#ifndef listp
#define listp
#include <iostream.h>
#include <stdlib.h>
#include <assert.h>
#include <listp.h>
template<class T>
List<T>::List()
  :Tail(0),size(0)
{
//cout << "List::ctor;" << endl;
}
template<class T>
List<T> &List<T>::operator =(const List<T> &obj)
{
//cout << "List::=();" << endl;
	Clear();
	Elem<T> *temp=obj.Tail->getNext();
	for (int i=0; i < obj.size; i++)
	{
		Add(temp->getData());
		temp = temp->getNext();
	}
	return *this;
}
	
template<class T>
List<T>::List(const List<T> &obj)
	: size(0), Tail(0)
{
//cout << "List::copy ctor" << endl;
	Elem<T> *p = obj.Tail->getNext();
	int i;
	for(i = 0; i < obj.size; i++)
	{
		Add(p->getData());
		p = p->getNext();
	}
}

template<class T>
int List<T>::ListNul()
{int a;
a=0;
if (Tail==0)
	{a=1;
	}
return(a);
}

template<class T>
List<T>::~List()
 {
// cout << "List::destr;" << endl;
   while (size > 0)
    {
     Elem<T> *temp = Tail->getNext();
     delete Tail;
     Tail = temp;
     size--;
    }
 }
template<class T>
int List<T>::Count()
 {
// cout << "List::Cout();" << size << endl;
   return size;
 }
template<class T>
void List<T>::Add(const T &elem)
{
//cout << "List::Add();" << endl;
	Elem<T> *newNode = new Elem<T>(elem);
	size++;
	if (!Tail)
	{
		Tail = newNode;
		Tail->setNext(Tail);
		return;		        	
	}

	Elem<T> *first = Tail->getNext();
	Tail->setNext(newNode);
	newNode->setNext(first);
	Tail = newNode;
}
template <class T>
T &List<T>::Get(int position)
{
	return this->operator[](position);
}

template <class T>
void List<T>::RemoveAt(int position)
{
//cout << "List::RemoveAt();" << endl;
	Elem<T> *p = Tail;
	for(int i=0; i < position; i++)
	{
		p = p->getNext();
	}
	Elem<T> *deleting = p->getNext();
	p->setNext(deleting->getNext());
	if (deleting==Tail)
		Tail=p;
	delete deleting;
	size--;
	if (size == 0)
		Tail = 0;
}

// template <class T>
//void List<T>::Remove(const T &elem){
//	Elem<T> *tempHead;
//	tempHead=Head;
//	Elem<T> *before = Tail;
//	int i;
//	for (i = 0; i < size; i++)
//	{
//		if (tempHead->getData() == elem)
//		{     //	cout<<"debug"<<tempHead->getData()<<endl;
//			Elem<T> *del = tempHead;
//			before->setNext(del->getNext());
//		      //	cout<<"before"<<before->getData()<<endl;
//			if (del == Head)
//				Head = del->getNext();
//			if (del == Tail)
//				Tail = before;
//			if (Head == Tail)
//				Head = Tail = NULL;
		      //	cout<<"Head"<<Head<<endl;
		      //	cout<<"Tail"<<Tail<<endl;
//			delete del;
//			size--;
//		}
//		before = tempHead;
//		tempHead = tempHead->getNext();
//}
//size--;
//}

template<class T>
T &List<T>::operator[](int index)
{
//cout << "List::operator[];" << endl;
  //	cout << "list[] call!!!" << endl;
	int i;
	assert(index > -1 && index < size);
	Elem<T> *p = Tail->getNext();
	for (i = 0; i < index; ++i)
	{
     //		cout << p << " " << p->getNext() << ": " << p->getData() << endl;
		p = p->getNext();
	}
	return p->getData();
}
template<class T>
void List<T>::Clear()
{
//cout << "List::Clear();" << endl;
	while (size > 0)
	{
		RemoveAt(0);
	}
}

#endif



