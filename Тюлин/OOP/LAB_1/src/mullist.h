#ifndef my_multilist
#define my_multilist

const int SUBLIST_COUNT = 3;

class MultiList
{
private:
	const static int MAIN_SUBLIST;
	class Node
	{
	public:
		Node *next[SUBLIST_COUNT];
		int value;
		Node(int value);
	};

	Node *last[SUBLIST_COUNT];
	int count[SUBLIST_COUNT];
public:
	const static int POSITIVE_SUBLIST;
	const static int NONPOSITIVE_SUBLIST;

	MultiList();
	MultiList(const MultiList &obj);
	MultiList &operator =(const MultiList &obj);
	~MultiList();

	void Add(int sublist, int element);
	void Remove(int element);
	void Remove(int sublist, int element);
	void RemoveAt(int sublist, int position);
	int Get(int sublist, int index);
	int Find(int element);

	int Count(int sublist);
};

#endif