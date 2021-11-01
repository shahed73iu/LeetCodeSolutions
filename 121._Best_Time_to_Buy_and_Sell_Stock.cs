using System;

namespace _121._Best_Time_to_Buy_and_Sell_Stock
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] prices = { 7, 1, 5, 3, 6, 4 };
            Solution s = new Solution();
            var result = s.MaxProfit(prices);
            Console.WriteLine(result);
        }
    }
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            if (prices.Length == 1)
                return 0;

            int tempMax = prices[0];
            int tempMin = prices[0];
            int res = 0;

            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (prices[i] > prices[i + 1])
                {
                    tempMax = 0;
                }

                tempMax = Math.Max(tempMax, prices[i + 1]);
                tempMin = Math.Min(tempMin, prices[i + 1]);

                if (res < tempMax - tempMin)
                {
                    res = tempMax - tempMin;
                }
            }
            return res;
        }
    }
}
