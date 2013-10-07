#ifndef my_station
#define my_station

#include "stdafx.h"

class Station
{
private:
	std::vector<Phone *> phones;

	void Initialize()
	{
		for (int i = 0; i < 8; i++)
		{
			phones.push_back(new Phone3());
		}
		for (int i = 0; i < 4; i++)
		{
			phones.push_back(new Phone2());
			phones.push_back(new Phone1());
		}
	}
public:
	Station()
	{
		Initialize();
	}
	~Station()
	{
		for (auto p : phones)
		{
			delete p;
		}
	}

	Phone *GetPhone(int index)
	{
		return phones.at(index);
	}

	std::vector<Phone *> GetPhonesByCaegory(int category)
	{
		std::vector<Phone *> result;
		for (int i = 0; i < phones.size(); i++)
		{
			if (phones.at(i)->GetCategory() == category)
				result.push_back(phones.at(i));
		}
		return result;
	}
};

#endif