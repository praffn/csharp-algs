using System.Collections.Immutable;
using CDS.Collections;
using Xunit;
using System.Linq;

namespace CDS.Tests.Collections
{
    public class HashSetTests
    {
        private HashSet<string> CreateSetWithValues(params string[] values)
        {
            var set = new HashSet<string>();
            foreach (var value in values)
                set.Add(value);
            return set;
        }
        
        [Fact]
        public void Empty_returns_true_on_empty_set()
        {
            Assert.True(new HashSet<int>().Empty);
        }

        [Fact]
        public void Count_returns_0_on_empty_set()
        {
            Assert.Equal(0, new HashSet<int>().Count);
        }
        
        [Fact]
        public void Add_adds_item()
        {
            var set = new HashSet<string>();
            set.Add("Alice");
            Assert.False(set.Empty);
        }

        [Theory]
        [InlineData(1, "a")]
        [InlineData(2, "a", "b")]
        [InlineData(1, "a", "a")]
        [InlineData(3, "a", "b", "c")]
        public void Count_returns_expected(int expected, params string[] values)
        {
            var set = CreateSetWithValues(values);
            Assert.Equal(expected, set.Count);
        }

        [Theory]
        [InlineData(true, "alice")]
        [InlineData(false, "cooper")]
        public void Contains_returns_expected(bool expected, string needle)
        {
            var set = CreateSetWithValues("alice", "in", "wonderland");
            Assert.Equal(expected, set.Contains(needle));
        }

        [Fact]
        public void Remove_removes_value_and_decrements_count()
        {
            var set = CreateSetWithValues("hello", "world");
            set.Remove("hello");
            Assert.Equal(1, set.Count);
            Assert.False(set.Contains("hello"));
        }

        [Fact]
        public void Clear_clears_set()
        {
            var set = CreateSetWithValues("alice", "bob", "carl", "deedee");
            set.Clear();
            Assert.True(set.Empty);
        }

        [Fact]
        public void Set_can_be_enumerated()
        {
            string[] values = {"eric", "freddy", "george", "hilda"};
            var set = CreateSetWithValues(values);

            var expected = values.OrderBy(n => n);
            var actual = set.OrderBy(n => n);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Union_creates_a_unioned_set()
        {
            var setA = new HashSet<string> { "A", "B", "C" };
            var setB = new HashSet<string> { "D", "E", "F" };
            var setU = setA.Union(setB);
            
            var expected = new HashSet<string> { "A", "B", "C", "D", "E", "F" };
            Assert.Equal(expected.OrderBy(n => n), setU.OrderBy(n => n));
        }

        [Fact]
        public void Intersect_creates_a_set_of_intersection()
        {
            var setA = new HashSet<string> { "A", "B", "C", "D" };
            var setB = new HashSet<string> { "C", "D", "E", "F" };
            var setI = setA.Intersect(setB);

            var expected = new HashSet<string> {"C", "D"};
            Assert.Equal(expected.OrderBy(n => n), setI.OrderBy(n => n));
        }

        [Fact]
        public void Except_returns_new_set_with_values_in_a_not_in_b()
        {
            var setA = new HashSet<string> { "A", "B", "C", "D" };
            var setB = new HashSet<string> { "C", "D", "E", "F" };
            var setE = setA.Except(setB);
            
            var expected = new HashSet<string> { "A", "B" };
            Assert.Equal(expected.OrderBy(n => n), setE.OrderBy(n => n));
        }
    }
}