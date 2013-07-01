template<class T>
CycleListNode<T>::CycleListNode(const T &value)
	: value(value), next(0) {}

template<class T>
CycleList<T>::CycleList()
	: count(0), last(0) {}

template<class T>
CycleList<T>::CycleList(const CycleList<T> &obj)
	: count(0), last(0)
{
	CycleListNode<T> *objLast = obj.last->next;
	for (int i = 0; i < obj.count; i++)
	{
		Add(objLast->value);
		objLast = objLast->next;
	}
}

template<class T>
CycleList<T> &CycleList<T>::operator =(const CycleList<T> &obj)
{
	Clear();
	CycleListNode<T> *current = obj.last->next;
	for (int i = 0; i < obj.count; ++i)
	{
		Add(current->value);
		current = current->next;
	}
	return *this;
}

template<class T>
CycleList<T>::~CycleList()
{
	Clear();
}

template<class T>
void CycleList<T>::Add(const T &value)
{
	CycleListNode<T> *newNode = new CycleListNode<T>(value);
	count++;
	if (!last)
	{
		last = newNode;
		last->next = last;
		return;		        	
	}

	CycleListNode<T> *first = last->next;
	last->next = newNode;
	newNode->next = first;
	last = newNode;
}

template<class T>
T &CycleList<T>::Get(int position)
{
	assert(position >= 0 && position < count);
	CycleListNode<T> *p = last->next;
	for (int i = 0; i < position; i++)
		p = p->next;
	return p->value;
}   

template<class T>
int CycleList<T>::Find(const T &value) const
{
	CycleListNode<T> *p = last->next;
	for (int i = 0; i < count; i++)
		if (p->value == value)
			return i;
	return -1;
} 

template<class T>
void CycleList<T>::Remove(const T &value)
{
	int watched = 0;
	CycleListNode<T> *p = last;
	while (p->next->value != value)
	{
		if (watched >= count)
			return;
		p = p->next;
		watched++;
	}
	
	CycleListNode<T> *deleting = p->next;
	p->next = p->next->next;
	delete deleting;
	last = p;
	count--;
}

template<class T>
void CycleList<T>::RemoveAt(int position)
{
	assert(position >= 0 && position < count);
	CycleListNode<T> *p = last;
	for (int i = 0; i < position; i++)
		p = p->next;
	CycleListNode<T> *deleting = p->next;
	p->next = deleting->next;
	if (deleting == last)
		last = p;
	delete deleting;
	count--;
}

template<class T>
int CycleList<T>::Count() const
{
	return count;
}

template<class T>
void CycleList<T>::Clear()
{
	CycleListNode<T> *first = last->next;
	for (int i = 0; i < count; ++i)
	{
		CycleListNode<T> *deleting = first;
		first = first->next;
			delete deleting;
		
	}
	last = 0;
	count = 0;
}