using System;
using System.Collections;
using System.Collections.Generic;

namespace CDS.Collections
{
	public class Queue<T> : IQueue<T>
	{
		public int Count { get; private set; } = 0;
		public bool Empty => _first == null;

		private Node _first;
		private Node _last;

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
			_last = null;
		}

		public bool Contains(T value)
		{
			var current = _first;
			while (current != null)
			{
				if (current.Value.Equals(value))
				{
					return true;
				}
				current = current.Next;
			}
			return false;
		}

		public T Dequeue()
		{
			if (Empty)
			{
				throw new InvalidOperationException("Cannot dequeue an empty queue");
			}
			var value = _first.Value;
			_first = _first.Next;
			Count--;
			if (Empty)
			{
				_last = null;
			}
			return value;
		}

		public void Enqueue(T value)
		{
			var oldLast = _last;
			_last = new Node()
			{
				Value = value,
				Next = null,
			};
			if (Empty)
			{
				_first = _last;
			}
			else
			{
				oldLast.Next = _last;
			}
			Count++;
		}

		public T Peek()
		{
			if (Empty)
			{
				throw new InvalidOperationException("Cannot peek an empty queue");
			}
			return _first.Value;
		}

		private class Node
		{
			public T Value;
			public Node Next;
		}
	}
}
