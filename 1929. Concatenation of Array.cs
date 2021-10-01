using System;

namespace LeetCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 1 };
            var ans = GetConcatenation(nums);
            for (int i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i]);
                Console.Write(" ");
            }

        }
        public static int[] GetConcatenation(int[] nums)
        {
            var l = (nums.Length);
            int[] ans = new int[l * 2];
            for (int i = 0; i < l * 2; i++)
            {
                ans[i] = nums[i % l];
            }
            return ans;
        }
    }
}
