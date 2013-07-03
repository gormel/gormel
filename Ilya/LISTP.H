//Fail listp.h
//class spisok
//avtor:Obmanov Ilja
//data: 10.04.2013


#include <iostream.h>
#include <stdlib.h>
#include <elementp.cpp>
 template<class T>
 class List
 {
      Elem<T> *Head;
      Elem<T> *Tail;
      int size;
  public:
      List();
      List(const List<T> &obj);
      List<T> &operator=(const List<T> &obj);
     ~List();
      void Add(const T &elem);
      void Show();
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
