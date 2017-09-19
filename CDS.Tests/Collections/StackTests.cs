using System;
using System.Linq;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
    public class StackTests
    {
        private Stack<int> CreateStackWithInserts(params int[] inserts)
        {
            var stack = new Stack<int>();
            foreach (var insert in inserts)
                stack.Push(insert);
            return stack;
        }
        
        [Fact]
        public void Stack_can_be_instantiated()
        {
            new Stack<int>();
        }

        [Fact]
        public void Push_adds_item_to_stack()
        {
            var stack = new Stack<int>();
            Assert.True(stack.Empty);
            stack.Push(1);
            Assert.False(stack.Empty);
        }

        [Fact]
        public void Empty_returns_true_on_empty_stack()
        {
            Assert.True(new Stack<int>().Empty);
        }

        [Fact]
        public void Count_returns_0_on_empty_stack()
        {
            Assert.Equal(0, new Stack<int>().Count);
        }

        [Fact]
        public void Count_returns_amount_of_items_in_stack()
        {
            var stack = CreateStackWithInserts(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            Assert.Equal(10, stack.Count);
        }

        [Fact]
        public void Peek_throws_on_empty_stack()
        {
            Assert.Throws<InvalidOperationException>(() => new Stack<int>().Peek());
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 1, 2, 3)]
        [InlineData(5, 1, 8, 3, 9, 0, 2, 5)]
        public void Peek_returns_most_recently_added_item_without_mutating(int expected, params int[] inserts)
        {
            var stack = CreateStackWithInserts(inserts);
            Assert.Equal(expected, stack.Peek());
            Assert.Equal(inserts.Length, stack.Count);
        }
        
        [Fact]
        public void Pop_throws_on_empty_stack()
        {
            Assert.Throws<InvalidOperationException>(() => new Stack<int>().Pop());
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 1, 2, 3)]
        [InlineData(5, 1, 8, 3, 9, 0, 2, 5)]
        public void Pop_removes_and_returns_most_recently_added_item_without_mutating(int expected, params int[] inserts)
        {
            var stack = CreateStackWithInserts(inserts);
            Assert.Equal(inserts.Length, stack.Count);
            Assert.Equal(expected, stack.Pop());
            Assert.Equal(inserts.Length - 1, stack.Count);
        }

        [Theory]
        [InlineData(false, 2)]
        [InlineData(true, 2, 2)]
        [InlineData(false, 2, 1, 3, 4, 5, 6, 7, 8, 10)]
        [InlineData(true, 8, 2, 39, 949, 8, 299)]
        public void Contains_returns_expected(bool expected, int needle, params int[] inserts)
        {
            var stack = CreateStackWithInserts(inserts);
            Assert.Equal(expected, stack.Contains(needle));
        }
        
        [Theory]
        [InlineData()]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [InlineData(1)]
        [InlineData(1, 7, 9, 4, 2, 8)]
        public void GetEnumerator_returns_fifo_sequential_enumerable(params int[] inserts)
        {
            var queue = CreateStackWithInserts(inserts);
            var expected = inserts.Reverse();
            Assert.True(queue.SequenceEqual(expected));
        }

        [Fact]
        public void Clear_removes_all_elements_from_stack()
        {
            var stack = CreateStackWithInserts(1, 2, 3, 4, 7, 9, 19, 100);
            stack.Clear();
            Assert.True(stack.Empty);
        }
    }
}