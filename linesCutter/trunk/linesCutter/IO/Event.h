#ifndef my_event
#define my_event

#include <vector>
#include <functional>
#include <algorithm>


template<class T1 = void, class T2 = void, class T3 = void, class T4 = void>
class Event
{
public:
	typedef std::function<void(T1, T2, T3, T4)> func;
private:
	std::vector<func> toCall;
public:
	void operator +=(func &f)
	{
		toCall.push_back(f);
	}

	void operator ()(const T1 &a1, const T2 &a2, const T3 &a3, const T4 &a4)
	{
		std::for_each(toCall.begin(), toCall.end(), [&](func &f)
		{
			f(a1, a2, a3, a4);
		});
	}
};

template<>
class Event<void, void, void, void>
{
public:
	typedef std::function<void()> func;
private:
	std::vector<std::function<void()>> toCall;
public:
	void operator +=(func &f)
	{
		toCall.push_back(f);
	}

	void operator ()()
	{
		std::for_each(toCall.begin(), toCall.end(), [&](func &f)
		{
			f();
		});
	}
};

template<class T1>
class Event<T1, void, void, void>
{
public:
	typedef std::function<void(T1)> func;
private:
	std::vector<func> toCall;
public:
	void operator +=(func f)
	{
		toCall.push_back(f);
	}

	void operator ()(const T1 &a1)
	{
		std::for_each(toCall.begin(), toCall.end(), [&](func &f)
		{
			f(a1);
		});
	}
};

template<class T1, class T2>
class Event<T1, T2, void, void>
{
public:
	typedef std::function<void(T1, T2)> func;
private:
	std::vector<func> toCall;
public:
	void operator +=(func f)
	{
		toCall.push_back(f);
	}

	void operator ()(const T1 &a1, const T2 &a2)
	{
		std::for_each(toCall.begin(), toCall.end(), [&](func &f)
		{
			f(a1, a2);
		});
	}
};

template<class T1, class T2, class T3>
class Event<T1, T2, T3, void>
{
public:
	typedef std::function<void(T1, T2, T3)> func;
private:
	std::vector<func> toCall;
public:
	void operator +=(func f)
	{
		toCall.push_back(f);
	}

	void operator ()(const T1 &a1, const T2 &a2, const T3 &a3)
	{
		std::for_each(toCall.begin(), toCall.end(), [&](func &f)
		{
			f(a1, a2, a3);
		});
	}
};
#endif