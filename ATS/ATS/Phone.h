#ifndef my_phone
#define my_phone

#include "stdafx.h"

class Phone
{
private:
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
};

#endif