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

	bool ContainsKeyword(const Call &call, const std::wstring &word)
	{
		if (std::to_wstring(call.GetCost()).find(word, 0) != std::wstring::npos)
			return true;
		if (std::to_wstring(call.GetTime()).find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetDateString().find(word, 0) != std::wstring::npos)
			return true;
		if (std::to_wstring(call.GetContainer().GetCategory()).find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetContainer().GetFamily().find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetContainer().GetNumber().find(word, 0) != std::wstring::npos)
			return true;
		return false;
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

	std::vector<Call> GetAllCalls()
	{
		std::vector<Call> result;
		for (auto p : phones)
		{
			for (auto c : p->GetCalls())
			{
				result.push_back(c);
			}
		}
		std::sort(result.begin(), result.end(), [&](const Call &a, const Call &b) { return a.GetDate() < b.GetDate(); });
		return result;
	}

	std::vector<Call> FindByKeyword(const std::wstring &value)
	{
		std::vector<Call> result;
		for (auto c : GetAllCalls())
		{
			if (ContainsKeyword(c, value))
				result.push_back(c);
		}
		return result;
	}
};

#endif