using System.Linq;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
	public class ChainMapTests
	{
		private ChainMap<string, int> CreateMapWithKeys(params string[] keys)
		{
			var map = new ChainMap<string, int>();
			foreach (var key in keys)
				map[key] = 0;
			return map;
		}

		[Fact]
		public void Empty_returns_true_on_empty_map()
		{
			Assert.True(new ChainMap<string, int>().Empty);
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
			Assert.Equal(0, new ChainMap<string, int>().Count);
		}

		[Fact]
		public void Put_inserts_a_key_value_pair()
		{
			var map = new ChainMap<string, int>();
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
			Assert.Throws<KeyNotFoundException>(() => new ChainMap<string, int>()["hello"]);
		}

		[Fact]
		public void Get_returns_expected_value()
		{
			var map = new ChainMap<string, string> {["hello"] = "world"};
			Assert.Equal("world", map["hello"]);
		}

		[Fact]
		public void TryGet_sets_out_param()
		{
			var map = new ChainMap<string, string> {["hello"] = "world"};
			var ok = map.TryGet("hello", out var value);
			Assert.True(ok);
			Assert.Equal("world", value);
		}

		[Fact]
		public void TryGet_returns_false_on_nonexisting_key()
		{
			var map = new ChainMap<string, string>();
			var ok = map.TryGet("hello", out var value);
			Assert.False(ok);
			Assert.Equal(default(string), value);
		}

		[Fact]
		public void Settings_twice_overrides()
		{
			var map = new ChainMap<string, string>();
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
			var map = new ChainMap<string, string> {["one"] = "hello", ["two"] = "world"};
			Assert.Equal(expected, map.ContainsValue(needle));
		}

		[Fact]
		public void Clear_clears_map()
		{
			var map = CreateMapWithKeys("hello", "world", "!");
			map.Clear();
			Assert.True(map.Empty);
		}

		[Fact]
		public void Remove_removes_key_and_value_and_decrements_count_and_returns_value_associated_with_key()
		{
			var map = new ChainMap<string, string>();
			map["hello"] = "world";
			map["goodbye"] = "sky";

			var val = map.Remove("hello");

			Assert.Equal(1, map.Count);
			Assert.False(map.ContainsKey("hello"));
			Assert.False(map.ContainsValue("world"));
			Assert.Equal("world", val);
		}

		[Fact]
		public void KeySet_returns_set_of_keys()
		{
			var map = new ChainMap<string, string> { ["a"] = "A", ["b"] = "B" };
			var keySet = map.KeySet();
			Assert.True(keySet.OrderBy(k => k).SequenceEqual(new []{ "a", "b" }));
		}

		[Fact]
		public void Values_returns_list_of_values()
		{
			var map = new ChainMap<string, string>
			{
				["hello"] = "world",
				["goodbye"] = "world",
				["hola"] = "mundo",
			};
			var expected = new[] {"mundo", "world", "world"};
			Assert.True(map.Values().OrderBy(v => v).SequenceEqual(expected));
		}

		[Fact]
		public void ChainMap_can_be_enumerated()
		{
			var map = new ChainMap<string, string>
			{
				["hello"] = "world",
				["goodbye"] = "heaven"
			};

			var expected = new[] {("hello", "world"), ("goodbye", "heaven")}.OrderBy(t => t);
			var actual = map.Select(kvp => (kvp.Key, kvp.Value)).OrderBy(t => t);
			Assert.Equal(expected, actual);
		}
	}
}
