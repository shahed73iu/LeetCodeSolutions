using System;
namespace ProblemSolving
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] arrays = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            int[] arrays = { 5, 4, -1, 7, 8 };
            var ans = MaxSubArray(arrays);
            Console.WriteLine(ans);
        }
        public static int MaxSubArray(int[] nums)
        {
            var maxSub = nums[0];
            var curSum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if(curSum < 0)
                {
                    curSum = 0;
                }
                curSum += nums[i];
                maxSub = Math.Max(maxSub, curSum);
            }
            return maxSub;
        }
    }
}