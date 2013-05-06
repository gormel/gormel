#include <stdlib.h>
#include <iostream.h>
#include <time.h>
#include "intlist.h"
#include "cyclist.h"
#include "mullist.h"

int main()
{
	srand(time(0));

	MultiList list;

	int elementCount = 20;

	for (int i = 0; i < elementCount; ++i)
	{
		int value = rand() % 20 - 10;
		cout << value << ", ";
		list.Add(value);
		if (value > 0)
			list.Add(MultiList::POSITIVE_SUBLIST, value);
		else
			list.Add(MultiList::NONPOSITIVE_SUBLIST, value);
	}
	cout << endl;

	cout << "positive:" << endl;
	for (i = 0; i < list.Count(MultiList::POSITIVE_SUBLIST); ++i)
	{
		cout << list.Get(MultiList::POSITIVE_SUBLIST, i) << " ";
	}
	cout << endl;

	cout << "non-positive:" << endl;
	for (i = 0; i < list.Count(MultiList::NONPOSITIVE_SUBLIST); ++i)
	{
		cout << list.Get(MultiList::NONPOSITIVE_SUBLIST, i) << " ";
	}
	cout << endl;

    cout << "lol!!!";

    return 0;
}