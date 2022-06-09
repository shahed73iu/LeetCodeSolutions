using System;

namespace Leetcode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            var abc = "MCMXCIV";
            var h = s.RomanToInt(abc);
            Console.WriteLine(h);
        }
    }
    public class Solution
    {
        public int RomanToInt(string s)
        {
            char[] chars = s.ToCharArray();
            var dicts = new Dictionary<char, int>();
            dicts.Add('I', 1);
            dicts.Add('V', 5);
            dicts.Add('X', 10);
            dicts.Add('L', 50);
            dicts.Add('C', 100);
            dicts.Add('D', 500);
            dicts.Add('M', 1000);

            int ans = 0;


            for (int i = 0; i < s.Length; i++)
            {

                if (i > 0 && dicts[s[i]] > dicts[s[i - 1]])
                {
                    ans += dicts[s[i]] - 2 * dicts[s[i - 1]];
                }
                else
                {
                    ans += dicts[s[i]];
                }
            }
            return ans;
        }
    }
}
