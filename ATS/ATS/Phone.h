#ifndef my_phone
#define my_phone

#include "stdafx.h"

class Phone
{
private:
	std::vector<Call> calls;
	std::string number;
	std::string family;
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

	std::string GetNumber()
	{
		return number;
	}

	void SetNumber(const std::string &value)
	{
		number = value;
	}

	std::string GetFamily()
	{
		return family;
	}

	void SetFamily(const std::string &value)
	{
		family = value;
	}

	std::vector<Call> &GetCalls()
	{
		return calls;
	}
};

#endif