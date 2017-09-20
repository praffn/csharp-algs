using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
    public class HashMapTests
    {
        private HashMap<string, int> CreateMapWithKeys(params string[] keys)
        {
            var map = new HashMap<string, int>();
            foreach (var key in keys)
                map[key] = 0;
            return map;
        }
        
        [Fact]
        public void Empty_returns_true_on_empty_map()
        {
            Assert.True(new HashMap<string, int>().Empty);
        }

        [Fact]
        public void Empty_returns_false_on_nonempty_map()
        {
            var map = CreateMapWithKeys("hello", "world");
            Assert.False(map.Empty);
        }

        [Fact]
        public void Count_returns_0_on_empty_map()
        {
            Assert.Equal(0, new HashMap<string, int>().Count);
        }

        [Fact]
        public void Put_inserts_a_key_value_pair()
        {
            var map = new HashMap<string, int>();
            map["hello"] = 1;
            Assert.False(map.Empty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1, "hello")]
        [InlineData(1, "hello", "hello")]
        [InlineData(2, "hello", "world")]
        [InlineData(10, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j")]
        public void Count_returns_expected(int expected, params string[] keys)
        {
            var map = CreateMapWithKeys(keys);
            Assert.Equal(expected, map.Count);
        }

        [Fact]
        public void Get_throws_if_key_doesnt_exists()
        {
            Assert.Throws<KeyNotFoundException>(() => new HashMap<string, int>()["hello"]);
        }

        [Fact]
        public void Get_returns_expected_value()
        {
            var map = new HashMap<string, string> {["hello"] = "world"};
            Assert.Equal("world", map["hello"]);
        }

        [Fact]
        public void Settings_twice_overrides()
        {
            var map = new HashMap<string, string>();
            map["hello"] = "world";
            map["hello"] = "heaven";
            Assert.Equal("heaven", map["hello"]);
        }

        [Theory]
        [InlineData(false, "what is up")]
        [InlineData(true, "hello", "hello", "world")]
        [InlineData(false, "hello", "world")]
        public void ContainsKey_returns_expected(bool expected, string needle, params string[] keys)
        {
            var map = CreateMapWithKeys(keys);
            Assert.Equal(expected, map.ContainsKey(needle));
        }

        [Theory]
        [InlineData("hello", true)]
        [InlineData("heaven", false)]
        public void ContainsValue_returns_expected(string needle, bool expected)
        {
            var map = new HashMap<string, string> {["one"] = "hello", ["two"] = "world"};
            Assert.Equal(expected, map.ContainsValue(needle));
        }

        [Fact]
        public void Clear_clears_map()
        {
            var map = CreateMapWithKeys("hello", "world", "!");
            map.Clear();
            Assert.True(map.Empty);
        }
    }
}