using System;

namespace ProblemSolving
{
    class Program
    {
        public unsafe void SwapTwoNumbersUsingPointers(int* p, int* q)
        {
            int temp = *p;
            *p = *q;
            *q = temp;
        }
        static unsafe void Main(string[] args)
        {
            Program p1 = new Program();
            int a = 10;
            int b = 20;

            int* x = &a;
            int* y = &b;
            Console.WriteLine("Before Swap a={0}, b={1}", a, b);
            p1.SwapTwoNumbersUsingPointers(x, y);
            Console.WriteLine("After Swap a={0}, b={1}", a, b);
        }
    }
}



