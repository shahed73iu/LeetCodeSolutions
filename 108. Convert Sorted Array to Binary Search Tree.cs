using System;

namespace SortedArrayToBalancedBST
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            int[] arr = new int[] { -10, -3, 0, 5, 9 };
            s.SortedArrayToBST(arr);
        }
    }
    public class BinaryTree
    {

        public static TreeNode root;


        /* A function that constructs Balanced Binary Search Tree
        from a sorted array */
        public virtual TreeNode sortedArrayToBST(int[] arr, int start, int end)
        {

            /* Base Case */
            if (start > end)
            {
                return null;
            }

            /* Get the middle element and make it root */
            int mid = (start + end) / 2;
            TreeNode node = new TreeNode(arr[mid]);

            /* Recursively construct the left subtree and make it
            left child of root */
            node.left = sortedArrayToBST(arr, start, mid - 1);

            /* Recursively construct the right subtree and make it
            right child of root */
            node.right = sortedArrayToBST(arr, mid + 1, end);

            return node;
        }

        /* A utility function to print preorder traversal of BST */
        public virtual void preOrder(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write(node.val + " ");
            preOrder(node.left);
            preOrder(node.right);
        }
    }

    //Definition for a binary tree node.
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution
    {
        public static TreeNode root;
        public TreeNode SortedArrayToBST(int[] nums)
        {
            BinaryTree tree = new BinaryTree();
           
            int n = nums.Length;
            root = tree.sortedArrayToBST(nums, 0, n - 1);
            return root;
        }
    }
}
