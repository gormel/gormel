#ifndef my_call
#define my_call

class Phone;

#include "stdafx.h"

class Call
{
private:
	CTime date;
	int timespan;
	double cost;
	Phone *container;

	void SetContainer(Phone *value)
	{
		container = value;
	}
public:
	friend class Phone;

	Call()
	{
	}

	int GetTime() const
	{
		return timespan;
	}

	void SetTime(int timespan)
	{
		this->timespan = timespan;
	}

	double GetCost() const
	{
		return cost;
	}

	void SetCost(double value)
	{
		cost = value;
	}

	CTime GetDate() const
	{
		return date;
	}
	
	std::wstring GetDateString() const
	{
		return std::wstring(GetDate().Format("%B %d %Y"));
	}

	void SetTime(const CTime &value)
	{
		date = value;
	}

	Phone GetContainer() const;
};

#endif