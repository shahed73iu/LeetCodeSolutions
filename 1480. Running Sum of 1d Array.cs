using System;

namespace LeetCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4 };
            var ans = RunningSum(nums);
            for (int i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i]);
                Console.Write(" ");
            }
        }
        //1480. Running Sum of 1d Array
        public static int[] RunningSum(int[] nums)
        {
            var l = nums.Length;
            int[] ans = new int[l];
            for (int i = 1; i < l; i++)
            {
                var a = 0;
                ans[0] = nums[0];
                for (int j = 0; j <= i; j++)
                {
                    a += nums[j];
                }
                ans[i] = a;
            }
            return ans;
        }
    }
}
