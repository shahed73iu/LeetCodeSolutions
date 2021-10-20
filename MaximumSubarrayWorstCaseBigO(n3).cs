using System;

namespace ProblemSolving
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum1 = 0;
            int[] number = new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
            var maxValue = 0;
            var maxValue2 = 0;
            bool flag1 = true;
            bool flag2 = true;

            for (int i = 0; i < number.Length - 1; i++)
            {
                for (int j = i; j < number.Length - 1; j++)
                {
                    int sum = 0;
                    int k = i;
                    while (k <= j)
                    {
                        sum += number[k];
                        k++;
                    }
                    if (flag1)
                    {
                        maxValue = sum;
                        flag1 = false;
                    }
                    maxValue = Math.Max(maxValue, sum);
                }
                if (flag2)
                {
                    maxValue2 = maxValue;
                    flag2 = false;
                }
                maxValue2 = Math.Max(maxValue, maxValue2);
            }
            Console.WriteLine(maxValue2);
        }
    }
}
