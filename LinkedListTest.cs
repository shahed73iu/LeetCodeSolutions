using System;

namespace LeetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] nums = { 0, 1, 0, 12, 3 };
            ListNode third = null;
            ListNode third1 = null;
            var head = new ListNode();
            var second = new ListNode();
            third = new ListNode();
            head.val = 1;
            head.next = second;
            second.val = 3;
            second.next = third;
            third.val = 4;

            var head1 = new ListNode();
            var second1 = new ListNode();
            third1 = new ListNode();
            head1.val = 1;
            head1.next = second;
            second1.val = 2;
            second1.next = third;
            third1.val = 4;

            MergeTwoLists(head, head1);

        }

        // Definition for singly-linked list.
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }
        //21. Merge Two Sorted Lists
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null) return l2;
            if (l2 == null) return l1;


            var newHead = new ListNode(0); // Creating this dummy node ease the logic
            var runnerHead = newHead;        // This is the runner node

            while (l1 != null && l2 != null)
            {
                if (l1.val >= l2.val)
                {
                    runnerHead.next = l2;
                    l2 = l2.next;
                }
                else
                {
                    runnerHead.next = l1;
                    l1 = l1.next;
                }

                runnerHead = runnerHead.next;
            }

            // Simply add the 'leftover' from the while loop at the end 
            if (l1 != null) runnerHead.next = l1;
            if (l2 != null) runnerHead.next = l2;

            printList(newHead);
            return newHead.next;

        }
        static void printList(ListNode n)
        {
            // Iterate till n reaches null
            while (n != null)
            {
                // Print the data
                Console.Write(n.val + " ");
                n = n.next;
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
