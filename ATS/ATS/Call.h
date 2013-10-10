#ifndef my_call
#define my_call

class Station;

#include "stdafx.h"
#include "Phone.h"

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
	friend class Station;

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

	void SetDate(const CTime &value)
	{
		date = value;
	}
	
	std::wstring GetDateString() const
	{
		return std::wstring(GetDate().Format("%B %d %Y"));
	}

	void SetTime(const CTime &value)
	{
		date = value;
	}

	std::vector<std::wstring> GetInfoStrings();

	Phone *GetContainer() const;
};

#endif