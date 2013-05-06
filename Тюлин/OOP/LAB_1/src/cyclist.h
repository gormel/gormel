#ifndef my_cyclelist
#define my_cyclelist

class IntCycleList
{
private:
	class Node
	{
	public:
		int value;
		Node *next;
		Node(int value);
	};

	Node *last;
	int count;
public:
	IntCycleList();

	IntCycleList(const IntCycleList &obj);
	IntCycleList &operator=(const IntCycleList &obj);
	~IntCycleList();

	void Add(int value);
	int Get(int position);
	int Find(int value);
	void Remove(int value);
	void RemoveAt(int position);
	int Count();
	void Clear();
};


#endif