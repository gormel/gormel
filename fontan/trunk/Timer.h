#ifndef my_timer
#define my_timer

class Timer
{
private:
	long counted;
	bool ok;
public:
	long Interval;
	Timer(long interval)
		: Interval(interval)
	{
	}
	
	bool IsOk()
	{
		if (ok)
		{
			ok = false;
			return true;
		}
		return false;
	}

	void Update(long timeSpend)
	{
		counted += timeSpend;
		if (counted >= Interval)
		{
			ok = true;
			counted = 0;
		}
	}
};

#endif