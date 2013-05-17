template<class T>
SingleListNode<T>::SingleListNode(const T &value)
	: value(value), next(0) 
{
#ifdef TALKY
	cout << "SingleListNode created!" << endl;
#endif
}

template<class T>
SingleList<T>::SingleList()
	: first(0), last(0), count(0) 
{
#ifdef TALKY
	cout << "SingleList created!" << endl;
#endif
}

template<class T>
SingleList<T>::SingleList(const SingleList<T> &obj)
	: count(0), first(0), last(0)
{
#ifdef TALKY
	cout << "SingleList created!" << endl;
#endif

	SingleListNode<T> *p = obj.first;
	while (p)
	{
		Add(p->value);
		p = p->next;
	}
}

template<class T>
SingleList<T> &SingleList<T>::operator =(const SingleList<T> &obj)
{
	Clear();
	SingleListNode<T> *current = obj.first;
	for (int i = 0; i < obj.count; ++i)
	{
		Add(current->value);
		current = current->next;
	}
	return *this;
}

template<class T>
SingleList<T>::~SingleList()
{
#ifdef TALKY
	cout << "SingleList deleted!" << endl;
#endif
	while (first)
	{
		SingleListNode<T> *deleting = first;
		first = first->next;
		delete deleting;
	}
}

template<class T>
void SingleList<T>::Add(const T &value)
{
	count++;
	if (!first)
	{
		first = last = new SingleListNode<T>(value);
		return;
	}

	SingleListNode<T> *newNode = new SingleListNode<T>(value);
	last->next = newNode;
	last = last->next;
}

template<class T>
void SingleList<T>::Remove(const T &value)
{
	if (!first)
		return;

	if (first->value == value)
	{
		SingleListNode<T> *deleting = first;
		first = first->next;
		delete deleting;
		count--;
		return;
	}

	SingleListNode<T> *c = first;
	while (c->next)
	{
		if (c->next->value == value)
		{
			SingleListNode<T> *deleting = c->next;
			c->next = deleting->next;
			delete deleting;
			count--;
			return;
		}
	}
}

template<class T>
int SingleList<T>::Find(const T &value) const
{
	int position = 0;
	SingleListNode<T> *c = first;
	while (c)
	{
		if (c->value == value)
		{
			return position;
		}
		position++;
	}
	return -1;
}

template<class T>
int SingleList<T>::Count() const
{
	return count;
}

template<class T>
T &SingleList<T>::Get(int position)
{
	assert(position >= 0 && position < count);
	int currentPosition = 0;
	SingleListNode<T> *currentNode = first;
	while (currentPosition++ < position)
	{
		currentNode = currentNode->next;
	}
	return currentNode->value;
}

template<class T>
void SingleList<T>::RemoveAt(int position)
{
	assert(position >= 0 && position < count);
	int currentPos = 0;
	SingleListNode<T> *currentNode = first;
	while (currentPos++ < position - 1)
	{
		 currentNode = currentNode->next;
	}
	SingleListNode<T> *deleting = currentNode->next;
	currentNode->next = deleting->next;
	delete deleting;
}

template<class T>
void SingleList<T>::Clear()
{
	while (first)
	{
		SingleListNode<T> *deleting = first;
		first = first->next;
		delete deleting;
	}
	count = 0;
}