#ifndef my_station
#define my_station

#include "stdafx.h"
#include "Call.h"
#include "Phone.h"
#include "Phone1.h"
#include "Phone2.h"
#include "Phone3.h"
	
class Station
{
private:
	std::vector<Phone *> phones;
	std::vector<Call *> calls;

	void Initialize()
	{
		std::ifstream ifs(L"phones.txt");
		while (!ifs.eof())
		{
			int _class;
			std::string phone;
			std::string family;

			std::string cl;
			std::getline(ifs, cl);
			if (ifs.eof())
				break;
			std::getline(ifs, phone);
			if (ifs.eof())
				break;
			std::getline(ifs, family);
			_class = std::stoi(cl);

			Phone *p;
			switch (_class)
			{
			case 1:
				p = new Phone1();
				p->SetNumber(stows(phone));
				p->SetFamily(stows(family));
				break;
			case 2:
				p = new Phone2();
				p->SetNumber(stows(phone));
				p->SetFamily(stows(family));
				break;
			case 3:
				p = new Phone3();
				p->SetNumber(stows(phone));
				p->SetFamily(stows(family));
				break;
			default:
				break;
			}
			phones.push_back(p);
		}
	}

	std::wstring stows(std::string value)
	{
		int bufferSize = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, value.c_str(), -1, nullptr, 0);
		wchar_t *arr = new wchar_t[bufferSize];
		MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, value.c_str(), -1, arr, bufferSize);
		return arr;
	}

	bool ContainsKeyword(const Call &call, const std::wstring &word)
	{
		if (std::to_wstring(call.GetCost()).find(word, 0) != std::wstring::npos)
			return true;
		if (std::to_wstring(call.GetTime()).find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetDateString().find(word, 0) != std::wstring::npos)
			return true;
		if (std::to_wstring(call.GetContainer()->GetCategory()).find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetContainer()->GetFamily().find(word, 0) != std::wstring::npos)
			return true;
		if (call.GetContainer()->GetNumber().find(word, 0) != std::wstring::npos)
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

	std::vector<Phone *> GetPhones()
	{
		return phones;
	}

	std::vector<Phone *> GetPhonesByCaegory(int category)
	{
		std::vector<Phone *> result;
		for (size_t i = 0; i < phones.size(); i++)
		{
			if (phones.at(i)->GetCategory() == category)
				result.push_back(phones.at(i));
		}
		return result;
	}

	std::vector<Call *> GetCalls()
	{
		return calls;
	}

	void AddCall(Phone *p, Call *call)
	{
		call->SetContainer(p);
		calls.push_back(call);
		std::sort(calls.begin(), calls.end(), [&](const Call *a, const Call *b){ return a->GetDate() < b->GetDate(); });
	}

	void RemoveCall(int index)
	{
		calls.erase(calls.begin() + index);
	}

	std::vector<Call *> FindByKeyword(const std::wstring &value)
	{
		std::vector<Call *> result;
		for (auto c : GetCalls())
		{
			if (ContainsKeyword(*c, value))
				result.push_back(c);
		}
		return result;
	}
};

#endif

