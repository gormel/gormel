#ifndef my_call
#define my_call

#include "stdafx.h"

class Call
{
private:
	int time;
	double cost;
public:
	Call(int time, double cost)
		: time(time), cost(cost)
	{

	}
	int GetTime()
	{
		return time;
	}

	void SetTime(int time)
	{
		this->time = time;
	}

	double GetCost()
	{
		return cost;
	}

	void SetCost(double value)
	{
		cost = value;
	}
};

#endif