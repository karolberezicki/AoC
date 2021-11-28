using System.Collections.Generic;

namespace AoC._2018.Day09
{
    public static class LinkedListExtensions
    {
        public static LinkedListNode<T> CircleForward<T>(this LinkedListNode<T> node, LinkedList<T> list)
        {
            return node.Next ?? list.First;
        }

        private static LinkedListNode<T> CircleBackward<T>(this LinkedListNode<T> node, LinkedList<T> list)
        {
            return node.Previous ?? list.Last;
        }

        public static LinkedListNode<T> CircleBackward<T>(this LinkedListNode<T> node, LinkedList<T> list, int times)
        {
            for (var i = 0; i < times; i++)
            {
                node = node.CircleBackward(list);
            }

            return node;
        }
    }
}
