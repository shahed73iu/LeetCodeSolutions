using System;

namespace LeetCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string[] operations = { "--X", "X++", "X++" };
            string[] operations = { "++X", "++X", "X++" };
            var ans = FinalValueAfterOperations(operations);
            Console.WriteLine(ans);
        }
        //2011. Final Value of Variable After Performing Operations
        public static int FinalValueAfterOperations(string[] operations)
        {
            int ans = 0;
            for (int i = 0; i < operations.Length; i++)
            {
                if ((operations[i] == "X++") || (operations[i] == "++X"))
                {
                    ans++;
                }
                else
                {
                    ans--;
                }
            }
            return ans;
        }
    }
}
