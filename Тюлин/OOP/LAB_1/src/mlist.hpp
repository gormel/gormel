template<class T>
const int MultiList<T>::MAIN_SUBLIST = 0;
template<class T>
const int MultiList<T>::POSITIVE_SUBLIST = 1;
template<class T>
const int MultiList<T>::NONPOSITIVE_SUBLIST = 2;

template<class T>
MultiNode<T>::MultiNode(const T &value)
	: value(value)
{
#ifdef TALKY
	cout << "MultiNode created!" << endl;
#endif
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		next[i] = 0;
	}
}

template<class T>
MultiNode<T> *MultiList<T>::FindBefore(MultiNode<T> *from, int sublist, const T &element)
{
	MultiNode<T> *p1 = from;
	MultiNode<T> *p = p1->next[sublist];
	for (int i = 0; i < count[sublist]; ++i)
	{
		if (p->value == element)
			return p1;
		p1 = p;
		p = p->next[sublist];
	}
	return 0;
}

template<class T>
MultiNode<T> *MultiList<T>::RemoveNoDelete(int sublist, const T &element)
{
	MultiNode<T> *before = FindBefore(last[sublist], sublist, element);
	if (!before)
		return 0;
	MultiNode<T> *del = before->next[sublist];
	before->next[sublist] = del->next[sublist];
	if (del == last[sublist])
		last[sublist] = before;
	if (del->next[sublist] == del)
		last[sublist] = 0;
	count[sublist]--;
	return del;
}

template<class T>
MultiList<T>::MultiList()
{
#ifdef TALKY
	cout << "MultiList created!" << endl;
#endif
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		last[i] = 0;
		count[i] = 0;
	}
}

template<class T>
MultiList<T>::MultiList(const MultiList<T> &obj)
{
#ifdef TALKY
	cout << "MultiList created!" << endl;
#endif
	int i;
	for (i = 0; i < SUBLIST_COUNT; ++i)
	{
		last[i] = 0;
		count[i] = 0;
	}
	for (i = 0; i < SUBLIST_COUNT; ++i)
	{
		MultiNode<T> *pp = obj.last[i]->next[i];
		for (int j = 0; j < obj.Count(i); ++j)
		{
			Add(i, pp->value);
			pp = pp->next[i];
		}
	}
}

template<class T>
MultiList<T> &MultiList<T>::operator =(const MultiList<T> &obj)
{
	Clear();
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		MultiNode<T> *pp = obj.last[i]->next[i];
		for (int j = 0; j < obj.Count(i); ++j)
		{
			Add(i, pp->value);
			pp = pp->next[i];
		}
	}
	return *this;
}

template<class T>
MultiList<T>::~MultiList()
{
#ifdef TALKY
	cout << "MultiList deleted!" << endl;
#endif
	Clear();
}

template<class T>
void MultiList<T>::Add(const T &element)
{
	Add(MAIN_SUBLIST, element);
}

template<class T>
int MultiList<T>::Add(int sublist, const T &element)
{
	MultiNode<T> *newNode = 0;

	if (sublist != MAIN_SUBLIST)
	{
		newNode = last[MAIN_SUBLIST];
		do
		{
			newNode = FindBefore(newNode, MAIN_SUBLIST, element)->next[MAIN_SUBLIST];
		}
		while (newNode->next[sublist]);
	}
	else
		newNode = new MultiNode<T>(element);

	if (!newNode)
		return 0;
	count[sublist]++;

	if (!last[sublist])
	{
		last[sublist] = newNode;
		newNode->next[sublist] = newNode;
		return 1;
	}

	MultiNode<T> *first = last[sublist]->next[sublist];
	last[sublist]->next[sublist] = newNode;
	newNode->next[sublist] = first;
	last[sublist] = newNode;
	return 1;
}

template<class T>
int MultiList<T>::Remove(const T &element)
{
	int result = 0;
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		if (i == MAIN_SUBLIST)
			continue;
		if (!result)
			result = Remove(i, element);
		else
			Remove(i, element);
	}

	delete RemoveNoDelete(MAIN_SUBLIST, element);
	return result;
}

template<class T>
int MultiList<T>::Remove(int sublist, const T &element)
{
	return (int)RemoveNoDelete(sublist, element);
}

template<class T>
void MultiList<T>::RemoveAt(int sublist, int position)
{	
	assert(position >= 0 && position < count[sublist]);
	MultiNode<T> *p = last[sublist];
	for (int i = 0; i < position; ++i)
		p = p->next[sublist];
	MultiNode<T> *del = p->next[sublist];
	p->next[sublist] = del->next[sublist];
	if (del == last[sublist])
		last[sublist] = p;
	if (del->next[sublist] == del)
		last[sublist] = 0;
	count[sublist]--;
}

template<class T>
void MultiList<T>::Clear()
{
	for (int i = 0; i < SUBLIST_COUNT; ++i)
	{
		while (Count(i))
		{
			RemoveAt(i, 0);
		}
	}
}

template<class T>
T &MultiList<T>::Get(int sublist, int position)
{
	assert(position >= 0 && position < count[sublist]);
	MultiNode<T> *p = last[sublist]->next[sublist];
	for (int i = 0; i < position; ++i)
	{
		assert(p);
		p = p->next[sublist];
	}
	return p->value;
}

template<class T>
int MultiList<T>::Find(int sublist, const T &element) const
{
	MultiNode<T> *p = last[sublist]->next[sublist];
	for (int i = 0; i < count[sublist]; ++i)
	{
		if (p->value == element)
			return i;
		p = p->next[sublist];
	}
	return -1;
}

template<class T>
int MultiList<T>::Count(int sublist) const
{
	return count[sublist];
}