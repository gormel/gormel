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
  :Head(0),Tail(0),size(0)
{
}
template<class T>
List<T> &List<T>::operator = (const List<T> &obj)
	{
	Clear();
	Elem<T> *temp=obj.Tail->getNext();
	for (int i=0;i<obj.size;i++)
	{
		Add(temp->getData());

	}
	return *this;
	}
template<class T>
List<T> &operator=(const List<T> &obj)
{
Clear();
Elem<T> *curent=odj.Head;
int i;
for (i=0;i<obj.size;i++)
{
Add(curent->getData());
curent=curent->getNext();
}
}
template<class T>
List<T>::List(const List<T> &obj)
:size(obj.size),Head(obj.Head),Tail(obj.Tail)
{
Elem<T>*p=obj.Head;
int i;
for(i=0;i<size;i++)
{
Add(p->getData());
p=p->getNext();
}
}
template<class T>
int List<T>::ListNul()
{int a;
a=0;
if (Head==0)
	{a=1;
	}
return(a);
}
template<class T>
List<T>::~List()
 {
   while (size!=0)
    {
     Elem<T> *temp=Head->getNext();
     delete Head;
     Head=temp;
     size--;
    }
 }
template<class T>
int List<T>::Count()
 {
   return size;
 }
template<class T>
void List<T>::Add(const T &elem)
{
//cout << "added: " << elem << endl;
 size++;
 Elem<T> *temp=new Elem<T>(elem);
if (Head!=0)
 {
  Tail->setNext(temp);
  Tail=temp;
   }
else Head=Tail=temp;
temp->setNext(Head);
}
template <class T>
T &List<T>::Get(int position)
	{
	Elem<T> *p=Tail->getNext();
	for (int i=0;i<position;i++)
	p=p->getNext();
	return p->getData();
	}

template <class T>
void List<T>::RemoveAt(int position)
	{
	Elem<T> *p=Tail;
	for(int i=0;i<position;i++)
		{
		p=p->getNext();
		}
	Elem<T> *deleting=p->getNext();
	p->setNext(deleting->getNext());
	if (deleting==Tail)
		Tail=p;
	delete deleting;
	size--;
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
  ///	cout << "list[] call!!!" << endl;
	int i;
	assert(index > -1 && index < size);
	Elem<T> *p = Head;
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
	Elem<T> *deleting=Head;
	int i;
	for(i=0;i<size;i++)
		{
		Head=Head->getNext();
		delete deleting;
		}
	Tail=0;
	size=0;
	}

#endif


 //int comp(const Point &p)
 //{
//	return p.X > 5 && p.Y < 100500;
// }

// List<Point> l;
// int in = l.Find(comp);
// l[in] = ....;



