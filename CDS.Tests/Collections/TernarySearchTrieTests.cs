using CDS.Collections;
using System;
using Xunit;

namespace CDS.Tests.Collections
{
    public class TernarySearchTrieTests
    {
        [Theory]
        [InlineData(1, "test")]
        [InlineData(3, "test", "pest", "quest")]
        [InlineData(5, "test", "pest", "quest", "nest", "best")]
        public void Test_Put_Given_Elements_Trie_Count_Equals_Expected(int expected, params string[] inserts)
        {
            var trie = new TernarySearchTrie<string>();

            for (var i = 0; i < inserts.Length; i++)
            {
                trie.Put(inserts[i], "val" + i);
            }
            Assert.Equal(expected, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Count_Does_Not_Increase()
        {
            var trie = new TernarySearchTrie<string>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal(1, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Value_Updates()
        {
            var trie = new TernarySearchTrie<string>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal("val2", trie.Get("test"));
        }

        [Fact]
        public void Test_Get_With_Empty_Trie_Returns_Null()
        {
            var trie = new TernarySearchTrie<string>();
            Assert.Null(trie.Get("test"));
        }

        [Fact]
        public void Test_Get_With_Element_Returns_Correct_value()
        {
            var trie = new TernarySearchTrie<string>();

            trie.Put("test", "val");

            Assert.Equal("val", trie.Get("test"));
        }

        [Fact]
        public void Test_Get_Returns_Correct_Value_After_Put_With_Same_Key()
        {
            var trie = new TernarySearchTrie<string>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal("val2", trie.Get("test"));
        }

        [Fact]
        public void Test_KeyExists_Given_Put_Key_Returns_True()
        {
            var trie = new TernarySearchTrie<string>();
            
            trie.Put("test", "val");
            
            Assert.True(trie.KeyExists("test"));
        }
        
        [Fact]
        public void Test_KeyExists_Given_Not_Put_Key_Returns_False()
        {
            var trie = new TernarySearchTrie<string>();
            
            trie.Put("test", "val");
            
            Assert.False(trie.KeyExists("quest"));
        }

        [Fact]
        public void Test_Put_Given_Null_Key_Throws_ArgumentException()
        {
            var trie = new TernarySearchTrie<string>();
            
            Assert.Throws<ArgumentException>(() => trie.Put(null, "val"));
        }
    }
}
