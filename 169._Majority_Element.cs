using System;
using System.Collections;

namespace _169._Majority_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 2, 2, 1, 1, 1, 2, 2 };
            Solution s = new Solution();
            var result = s.MajorityElement(nums);
            Console.WriteLine(result);
        }
    }
    public class Solution
    {
        public int MajorityElement(int[] nums)
        {
            Hashtable hash = new Hashtable();

            foreach (var item in nums)
                if (hash.ContainsKey(item))
                    hash[item] = (int)hash[item] + 1;
                else
                    hash.Add(item, 1);

            foreach (var key in hash.Keys)
                if ((int)hash[key] > nums.Length / 2)
                    return (int)key;

            return -1;
            ////  var lth = nums.Length;
            //int[] countArray = new int[nums.Length];
            //int maxInt = Int32.MinValue;
            //int maxIndex = -1;
            //for (int i = 0; i < nums.Length; i++)
            //{
            //    countArray[nums[i]]++;
            //}
            //for (int i = 0; i < nums.Length - 1; i++)
            //{
            //    int value = countArray[i];
            //    if (value > maxInt)
            //    {
            //        maxInt = value;
            //        maxIndex = i;
            //    }
            //}
            //if(maxIndex > nums.Length/2)
            //    return maxIndex;
            //return -1;

        }
    }
}
