#ifndef my_phone
#define my_phone

class Call;

#include "stdafx.h"

class Phone
{
private:
	std::vector<Call> calls;
	std::wstring number;
	std::wstring family;
	int category;
protected:
	Phone(int category)
		: category(category)
	{

	}
public:
	virtual ~Phone() {}
	int GetCategory()
	{
		return category;
	}

	void SetCategory(int value)
	{
		category = value;
	}

	std::wstring GetNumber()
	{
		return number;
	}

	void SetNumber(const std::wstring &value)
	{
		number = value;
	}

	std::wstring GetFamily()
	{
		return family;
	}

	void SetFamily(const std::wstring &value)
	{
		family = value;
	}

	std::vector<Call> GetCalls()
	{
		return calls;
	}

	void AddCall(Call &value)
	{
		calls.push_back(value);
		value.SetContainer(this);
	}
};

#endif