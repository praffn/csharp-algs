using System;
using System.Linq;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
    public class PriorityQueueTests
    {
        private PriorityQueue<int> CreatePQWithInserts(params int[] inserts)
        {
            var pq = new PriorityQueue<int>();
            foreach (var insert in inserts)
                pq.Enqueue(insert);
            return pq;
        }
        
        [Fact]
        public void Empty_returns_true_on_empty_queue()
        {
            Assert.True(new PriorityQueue<int>().Empty);
        }

        [Fact]
        public void Count_returns_0_on_empty_queue()
        {
            Assert.Equal(0, new PriorityQueue<int>().Count);
        }

        [Fact]
        public void Value_can_be_enqueued()
        {
            var queue = new PriorityQueue<int>();
            queue.Enqueue(1);
            Assert.False(queue.Empty);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 1, 2, 3)]
        [InlineData(9, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        public void Count_returns_expected(int expected, params int[] inserts)
        {
            var pq = CreatePQWithInserts(inserts);
            Assert.Equal(expected, pq.Count);
        }

        [Fact]
        public void Peek_throws_on_empty_queue()
        {
            Assert.Throws<InvalidOperationException>(() => new PriorityQueue<int>().Peek());
        }

        [Theory]
        [InlineData(1, 8, 4, 1, 3)]
        [InlineData(100, 1000, 100, 100000)]
        public void Peek_returns_minimum_inserted_value_without_mutating(int expected, params int[] inserts)
        {
            var pq = CreatePQWithInserts(inserts);
            Assert.Equal(expected, pq.Peek());
            Assert.Equal(inserts.Length, pq.Count);
        }
        
        [Fact]
        public void Dequeue_throws_on_empty_queue()
        {
            Assert.Throws<InvalidOperationException>(() => new PriorityQueue<int>().Dequeue());
        }

        [Theory]
        [InlineData(1, 8, 4, 1, 3)]
        [InlineData(100, 1000, 100, 100000)]
        public void Dequeue_removes_and_returns_minimum(int expected, params int[] inserts)
        {
            var pq = CreatePQWithInserts(inserts);
            Assert.Equal(expected, pq.Dequeue());
            Assert.Equal(inserts.Length - 1, pq.Count);
        }

        [Theory]
        [InlineData(false, 1)]
        [InlineData(true, 1, 1)]
        [InlineData(true, 1, 0, 2, 3, 1, 2)]
        [InlineData(true, 1, 0, 2, 3, 4, 5, 1)]
        [InlineData(false, 1, 20, 30, 28, 19, 200, 399)]
        public void Contains_return_expected(bool expected, int needle, params int[] inserts)
        {
            var pq = CreatePQWithInserts(inserts);
            Assert.Equal(expected, pq.Contains(needle));
        }

        [Fact]
        public void Clear_clears_queue()
        {
            var pq = CreatePQWithInserts(1, 2, 3, 4, 5);
            pq.Clear();
            Assert.True(pq.Empty);
        }
        
        [Theory]
        [InlineData()]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [InlineData(1)]
        [InlineData(1, 7, 9, 4, 2, 8)]
        public void GetEnumerator_returns_ordered_sequential_enumerable(params int[] inserts)
        {
            var queue = CreatePQWithInserts(inserts);
            var expected = inserts.ToList();
            expected.Sort();
            Assert.True(queue.SequenceEqual(expected));
        }
    }
}