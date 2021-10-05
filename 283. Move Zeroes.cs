using System;

namespace LeetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] nums = { 0, 1, 0, 12, 3 };
            MoveZeroes(nums);
            //Console.Write(ans);
        }
        //283. Move Zeroes
        public static void MoveZeroes(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return;

            int end = nums.Length;

            for (int i = 0; i < end; i++)
            {
                if (nums[i] == 0)
                {
                    for (int j = i; j < end - 1; j++)
                        nums[j] = nums[j + 1];

                    nums[end - 1] = 0;
                    end--;
                    i--;
                }
            }
        }
    }
}


//public int NumIdenticalPairs(int[] nums)
//{
//    var cnt = 0;
//    for (int i = 0; i < nums.Length; i++)
//    {
//        for (int j = i + 1; j < nums.Length; j++)
//        {
//            if (nums[i] == nums[j])
//            {
//                cnt++;
//            }
//        }
//    }
//    return cnt;
//}

//public int MaximumWealth(int[][] accounts)
//{
//    int highestSum = 0;

//    foreach (int[] account in accounts)
//    {
//        int accountSum = account.Sum();
//        if (accountSum > highestSum)
//        {
//            highestSum = accountSum;
//        }
//    }

//    return highestSum;
//}