using System;
using System.Linq;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
	public class QueueTests
	{
		private Queue<int> CreateQueueWithInserts(params int[] inserts)
		{
			var queue = new Queue<int>();
			foreach (var insert in inserts)
				queue.Enqueue(insert);
			return queue;
		}

		[Fact]
		public void Queue_can_be_instantiated()
		{
			new Queue<int>();
		}

		[Fact]
		public void Count_returns_0_on_empty_queue()
		{
			Assert.Equal(0, new Queue<int>().Count);
		}

		[Fact]
		public void Empty_is_true_on_empty_queue()
		{
			Assert.True(new Queue<int>().Empty);
		}

		[Fact]
		public void Value_can_be_enqueued()
		{
			var queue = new Queue<int>();
			queue.Enqueue(1);
			Assert.False(queue.Empty);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1, 0)]
		[InlineData(2, 1, 1)]
		[InlineData(3, 1, 1, 0)]
		[InlineData(10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0)]
		public void Count_returns_count_of_queue(int expected, params int[] inserts)
		{
			var queue = CreateQueueWithInserts(inserts);
			Assert.Equal(expected, queue.Count);
		}

		[Fact]
		public void Clear_clears_queue()
		{
			var queue = new Queue<int>();
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Clear();
			Assert.True(queue.Empty);
		}

		[Fact]
		public void Peek_throws_on_empty_queue()
		{
			Assert.Throws<InvalidOperationException>(() => new CDS.Collections.Queue<int>().Peek());
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(1, 1, 2)]
		[InlineData(10, 10, 29, 39, 2881, 28838, 9328)]
		public void Peek_returns_first_enqueued_item(int expected, params int[] inserts)
		{
			var queue = CreateQueueWithInserts(inserts);
			Assert.Equal(expected, queue.Peek());
		}

		[Fact]
		public void Dequeue_throws_on_empty_queue()
		{
			Assert.Throws<InvalidOperationException>(() => new CDS.Collections.Queue<int>().Dequeue());
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(1, 1, 2)]
		[InlineData(10, 10, 29, 39, 2881, 28838, 9328)]
		public void Dequeue_removes_and_returns_first_enqueued_item(int expected, params int[] inserts)
		{
			var queue = CreateQueueWithInserts(inserts);
			Assert.Equal(inserts.Length, queue.Count);
			Assert.Equal(expected, queue.Dequeue());
			Assert.Equal(inserts.Length - 1, queue.Count);
		}

		[Theory]
		[InlineData(false, 1)]
		[InlineData(true, 1, 1)]
		[InlineData(true, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
		[InlineData(false, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
		public void Contains_returns_expected(bool expected, int needle, params int[] inserts)
		{
			var queue = CreateQueueWithInserts(inserts);
			Assert.Equal(expected, queue.Contains(needle));
		}

		[Theory]
		[InlineData()]
		[InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9)]
		[InlineData(1)]
		[InlineData(1, 7, 9, 4, 2, 8)]
		public void GetEnumerator_returns_fifo_sequential_enumerable(params int[] inserts)
		{
			var queue = CreateQueueWithInserts(inserts);
			Assert.True(queue.SequenceEqual(inserts));
		}
	}
}
