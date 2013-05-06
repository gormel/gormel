#include <stdlib.h>
#include <iostream.h>
#include <time.h>
#include "intlist.h"
#include "cyclist.h"

int main()
{
	srand(time(0));
	IntSingleList list;
    int elementCount = 20;
	for (int i = 0; i < elementCount; i++)
		list.Add(rand() % 60);

	for (i = 0; i < elementCount; i++)
		cout << list.Get(i) << " ";
    cout << endl;

    IntSingleList list1;
    IntSingleList list2(list1 = list);

	for (i = 0; i < elementCount; i++)
		cout << list1.Get(i) << " ";
    cout << endl;

    for (i = 0; i < elementCount; i++)
		cout << list2.Get(i) << " ";
    cout << endl;

    cout << "lol!!!";

    return 0;
}