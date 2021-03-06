//Fail listp.h
//class spisok
//avtor:Obmanov Ilja
//data: 10.04.2013
#ifndef my_listp_
#define my_listp_

#include <iostream.h>
#include <stdlib.h>
#include <elementp.cpp>
 template<class T>
 class List
 {
      Elem<T> *Tail;
      int size;
  public:
      List();
      List(const List<T> &obj);
      List<T> &operator=(const List<T> &obj);
     ~List();
      void Add(const T &elem);
      int Count();
      T &Get(int position);
	  T &operator[](int index);
     // template<class Func>
     // int Find(const Func &func);
      int ListNul();
      void Remove(const T &elem);
      void RemoveAt(int position);
      void Clear();
};

#endif