#include "stdafx.h"
#include "Call.h"

Phone *Call::GetContainer() const
{
	return container;
}

std::vector<std::wstring> Call::GetInfoStrings()
{
	std::vector<std::wstring> result;
	result.push_back(std::to_wstring(GetContainer()->GetCategory()));
	result.push_back(GetContainer()->GetNumber());
	result.push_back(GetContainer()->GetFamily());
	result.push_back(GetDateString());
	result.push_back(std::to_wstring(GetTime()));
	result.push_back(std::to_wstring(GetCost()));
	return result;
}