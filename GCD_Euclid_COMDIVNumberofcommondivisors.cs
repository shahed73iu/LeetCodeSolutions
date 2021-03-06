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
    long a, b, n;
    cin >> n;
    for (int g = 0; g < n; g++)
    {
        cin >> a >> b;
        long ans = objectS.GcdCalculation(a, b);
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
        cout << cnt << "\n";

    }
    return 0;
}