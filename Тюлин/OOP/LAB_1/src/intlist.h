//file: intlist.h
//list class header
//autor: Tyulin Roman
//date: 20.04.2013

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
	int &Get(int position);
    int Find(int value) const;
	void Remove(int value);
	void RemoveAt(int position);
	void Clear();
	int Count() const;
};

#endif