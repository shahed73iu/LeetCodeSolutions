using System;

namespace LeetCode
{
    public class Program
    {
        static void Main(string[] args)
        {

            //int[] nums = { 0, 2, 1, 5, 3, 4 };
            int[] nums = { 5, 0, 1, 2, 3, 4 };
            var ans = BuildArray(nums);

            for (int i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i]);
                Console.Write(" ");
            }
        }
        //1920. Build Array from Permutation
        public static int[] BuildArray(int[] nums)
        {
            var l = nums.Length;

            int[] ans = new int[l];
            for (int i = 0; i < l; i++)
            {
                ans[i] = nums[nums[i]];
            }
            return ans;
        }
    }
}
