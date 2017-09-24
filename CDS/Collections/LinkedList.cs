using System;
using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
    public class LinkedList<T> : IList<T>
    {
        private Node _head;
        private Node _tail;

        public int Count { get; private set; } = 0;
        public bool Empty => Count == 0;
        
        public IEnumerator<T> GetEnumerator()
        {
            var node = _head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public void Add(T element)
        {
            var node = new Node {Value = element};
            if (_head == null)
            {
                _head = node;
            }
            else
            {
                _tail.Next = node;
            }
            _tail = node;
            Count++;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            var node = _head;
            for (var i = 0; i < index; i++)
                node = node.Next;
            return node.Value;
        }

        public bool Contains(T element)
        {
            var node = _head;
            while (node != null)
            {
                if (node.Value.Equals(element))
                    return true;
                node = node.Next;
            }
            return false;
        }

        public void Remove(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                _head = _head.Next;
            }
            else
            {
                var node = _head;
                for (var i = 0; i < index - 1; i++)
                    node = node.Next;
                node.Next = node.Next.Next;
            }
            Count--;
        }

        public int IndexOf(T element)
        {
            var node = _head;
            var i = 0;
            while (node != null)
            {
                if (node.Value.Equals(element))
                    return i;
                i++;
                node = node.Next;
            }
            return -1;
        }

        public T this[int index] => Get(index);

        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
        }
    }
}