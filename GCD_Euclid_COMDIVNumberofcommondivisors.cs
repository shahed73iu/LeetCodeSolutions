#include <bits/stdc++.h>
using namespace std;

class Solution
{
    public:
	int GcdCalculation(long a, long b)
    {
        long x, y;
        long gcd = 0;
        x = max(a, b);
        y = min(a, b);
        while (true)
        {
            if (x % y == 0)
            {
                gcd = y;
                break;
            }
            else
            {
                y = x % y;
                x = x;
            }
        }
        return gcd;
    }
};
int main()
{
    Solution objectS;
    long ans = objectS.GcdCalculation(100000, 100000);
    long cnt = 0;
    for (long i = 1; i * i <= ans; i++)
    {
        if (ans % i == 0)
        {
            if (ans / i == i)
            {
                cnt++;
            }
            else
            {
                cnt += 2;
            }
        }
    }
    cout << "Maximum common divisor is : " << cnt;
    return 0;
}