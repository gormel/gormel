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
	Node *FindBefore(Node *from, int sublist, int element);
	Node *RemoveNoDelete(int sublist, int element);
public:
	const static int POSITIVE_SUBLIST;
	const static int NONPOSITIVE_SUBLIST;

	MultiList();
	MultiList(const MultiList &obj);
	MultiList &operator =(const MultiList &obj);
	~MultiList();

	void Add(int element);
	int Add(int sublist, int element);

	int Remove(int element);
	int Remove(int sublist, int element);
	void RemoveAt(int sublist, int position);
	void Clear();
	
	int &Get(int sublist, int position);
	int Find(int sublist, int element) const;

	int Count(int sublist) const;
};

#endif