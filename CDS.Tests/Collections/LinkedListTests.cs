using System;
using System.Linq;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
    public class LinkedListTests
    {
        private LinkedList<int> CreateLinkedListWithValues(params int[] values)
        {
            var list = new LinkedList<int>();
            foreach (var value in values)
                list.Add(value);
            return list;
        }
        
        [Fact]
        public void Empty_returns_true_on_empty_list()
        {
            Assert.True(new LinkedList<int>().Empty);
        }

        [Fact]
        public void Empty_returns_false_on_nonempty_list()
        {
            var list = new LinkedList<int> {2};
            Assert.False(list.Empty);
        }

        [Fact]
        public void Count_returns_0_on_empty_list()
        {
            Assert.Equal(0, new LinkedList<int>().Count);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 1, 1, 2)]
        public void Count_returns_expected(int expected, params int[] values)
        {
            var list = CreateLinkedListWithValues(values);
            Assert.Equal(values.Length, list.Count);
        }

        [Theory]
        [InlineData(false, 0)]
        [InlineData(true, 1, 1)]
        [InlineData(true, 1, 1, 1, 1, 1)]
        [InlineData(false, 1, 0, 2, 3, 4, 5)]
        public void Contains_return_expected(bool expected, int needle, params int[] values)
        {
            var list = CreateLinkedListWithValues(values);
            Assert.Equal(expected, list.Contains(needle));
        }

        [Fact]
        public void Clear_clears_list()
        {
            var list = CreateLinkedListWithValues(1, 2, 3, 4);
            Assert.False(list.Empty);
            list.Clear();
            Assert.True(list.Empty);
        }

        [Theory]
        [InlineData(-1, 0, 1, 2, 3)]
        [InlineData(0, 0, 0, 1, 2, 3)]
        [InlineData(1, 2, 1, 2, 3)]
        public void IndexOf_returns_index_of_element(int expected, int needle, params int[] values)
        {
            var list = CreateLinkedListWithValues(values);
            Assert.Equal(expected, list.IndexOf(needle));
        }

        [Fact]
        public void RemoveAt_removes_element_at_index()
        {
            var list = CreateLinkedListWithValues(1, 2, 3);
            list.RemoveAt(1);
            Assert.Equal(2, list.Count);
            Assert.False(list.Contains(2));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        public void RemoveAt_throws_if_argument_not_in_range(int index)
        {
            var list = CreateLinkedListWithValues(1, 2, 3);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(index));
        }

        [Fact]
        public void Remove_removes_element()
        {
            var list = CreateLinkedListWithValues(1, 2, 3, 4);
            list.Remove(2);
            Assert.Equal(3, list.Count);
            Assert.False(list.Contains(2));
        }

        [Fact]
        public void Remove_only_removes_first_occurence()
        {
            var list = CreateLinkedListWithValues(1, 2, 3, 4, 2);
            list.Remove(2);
            Assert.Equal(4, list.Count);
            Assert.True(list.Contains(2));
        }

        [Theory]
        [InlineData(2, 1, 1, 2, 3)]
        [InlineData(3, 0, 3)]
        public void Get_returns_expected(int expected, int index, params int[] values)
        {
            var list = CreateLinkedListWithValues(values);
            Assert.Equal(expected, list.Get(index));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        [InlineData(100)]
        public void Get_outside_range_throws(int index)
        {
            var list = CreateLinkedListWithValues(1, 2, 3);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(index));
        }

        [Fact]
        public void Indexer_get_works()
        {
            var list = CreateLinkedListWithValues(1, 2, 3);
            Assert.Equal(3, list[2]);
        }

        [Fact]
        public void Indexer_also_throws()
        {
            var list = CreateLinkedListWithValues(1, 2, 3);
            Assert.Throws<IndexOutOfRangeException>(() => list[100]);
        }

        [Fact]
        public void ArrayList_can_be_enumerated()
        {
            var list = CreateLinkedListWithValues(1, 2, 3, 4);
            var expected = new[] {1, 2, 3, 4};
            Assert.True(expected.SequenceEqual(list));
        }
    }
}