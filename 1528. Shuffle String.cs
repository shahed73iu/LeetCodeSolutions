using System;
using System.Linq;

namespace LeetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var s1 = "codeleet";
            var s = "aaiougrt";
            int[] indices1 = { 4, 5, 6, 7, 0, 2, 1, 3 };
            int[] indices = { 4, 0, 2, 6, 7, 3, 1, 5 };

            var ans = RestoreString(s, indices);
            Console.WriteLine(ans);
        }
        //1528. Shuffle String
        public static string RestoreString(string s, int[] indices)
        {
            var res = "";
            var she = new char[s.Length];

            for (int i = 0, j = 0; i < indices.Length; i++)
            {
                if (j == indices[i])
                {
                    char item = s[i];
                    var sa = item.ToString();
                    she[j] = item;
                    j++;
                    i = -1;
                }
            }
            res = String.Concat(she);
            return res;
        }
    }
}
