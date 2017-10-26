using System;
using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
	public class Stack<T> : IStack<T>
	{
		public int Count { get; private set; } = 0;
		public bool Empty => _first == null;

		private Node _first;

		public IEnumerator<T> GetEnumerator()
		{
			var current = _first;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		public void Clear()
		{
			Count = 0;
			_first = null;
		}

		public T Peek()
		{
			if (Empty)
			{
				throw new InvalidOperationException("Cannot peek an empty stack");
			}
			return _first.Value;
		}

		public T Pop()
		{
			if (Empty)
			{
				throw new InvalidOperationException("Cannot pop an empty stack");
			}
			var value = _first.Value;
			_first = _first.Next;
			Count--;
			return value;
		}

		public void Push(T value)
		{
			_first = new Node
			{
				Value = value,
				Next = _first,
			};
			Count++;
		}

		public bool Contains(T value)
		{
			foreach (var item in this)
			{
				if (item.Equals(value))
					return true;
			}
			return false;
		}

		private class Node
		{
			public T Value;
			public Node Next;
		}
	}
}
