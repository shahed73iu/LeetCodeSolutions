using System;
using System.Linq;

namespace LeetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] nums2 = { 2, 5, 1, 3, 4, 7 };
            int[] nums = { 1, 2, 3, 4, 4, 3, 2, 1 };
            int n1 = 3;
            int n = 4;

            var ans = Shuffle(nums, n);
            for (int i = 0; i < ans.Length; i++)
            {
                Console.Write(ans[i]);
                Console.Write(" ");
            }
        }
        //1470. Shuffle the Array
        public static int[] Shuffle(int[] nums, int n)
        {
            int[] a = new int[nums.Length];
            for (int i = 0, j=0; i < (nums.Length/2); i++)
            {
                a[j] = nums[i];
                a[j+1] = nums[n];
                n++;
                j += 2;
            }
            return a;
        }
    }
}
