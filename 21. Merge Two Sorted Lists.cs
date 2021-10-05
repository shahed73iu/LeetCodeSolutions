using System;
namespace LeetCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var l1 = new LinkedList();
            var l2 = new LinkedList();
            l1.Add(new ListNode(1));
            l1.Add(new ListNode(3));
            l1.Add(new ListNode(4));

            l2.Add(new ListNode(1));
            l2.Add(new ListNode(2));
            l2.Add(new ListNode(7));
            l2.Add(new ListNode(8));
            l2.Add(new ListNode(9));
            l2.Add(new ListNode(10));
            var a = MergeTwoLists(l1.Head, l2.Head);
            printList(a);
        }
        // 21. Merge Two Sorted Lists
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null) return l2;
            if (l2 == null) return l1;
            var newHead = new ListNode(0);
            var runnerHead = newHead;

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
    public class LinkedList
    {
        ListNode head;
        ListNode current;
        public ListNode Head //Expose a public property to get to head of the list  
        {
            get { return head; }
        }
        public void Add(ListNode n)
        {
            if (head == null)
            {
                head = n; // point head to first added node  
                current = head; // set current to head  
            }
            else
            {
                current.next = n; //Set current next to newly added node.  
                current = current.next;  //Set new current to current next.  
            }
        }
    }
}
