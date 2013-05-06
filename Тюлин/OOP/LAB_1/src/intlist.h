#ifndef my_list
#define my_list

class IntSingleList
{
private:
	class Node
	{
    	public:
		int value;
        Node *next;
		Node(int value);
	};

	Node *first;
	Node *last;
	int count;
public:
	IntSingleList();

	IntSingleList(const IntSingleList &obj);
	IntSingleList &operator =(const IntSingleList &obj);
	~IntSingleList();

	void Add(int value);
	int Get(int position);
    int Find(int value);
	void Remove(int value);
	void RemoveAt(int position);
	int Count();
	void Clear();
};

#endif