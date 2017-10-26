using CDS.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CDS.Tests.Collections
{
    public class TernarySearchTrieTests
    {
        [Fact]
        public void Test_Put_Given_One_Element_Trie_Count_Equals_One()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val");

            Assert.Equal(1, trie.Count);
        }

        [Fact]
        public void Test_Put_Given_Five_Elements_Trie_Count_Equals_Five()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val1");
            trie.Put("pest", "val2");
            trie.Put("quest", "val3");
            trie.Put("best", "val4");
            trie.Put("nest", "val5");

            Assert.Equal(5, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Count_Does_Not_Increase()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal(1, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Value_Updates()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal("val2", trie.Get("test"));
        }

        [Fact]
        public void Test_Get_With_Empty_Trie_Returns_Null()
        {
            var trie = new TernarySearchTrie<String>();
            Assert.Null(trie.Get("test"));
        }

        [Fact]
        public void Test_Get_With_Element_Returns_Correct_value()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val");

            Assert.Equal("val", trie.Get("test"));
        }

        [Fact]
        public void Test_Get_Returns_Correct_Value_After_Put_With_Same_Key()
        {
            var trie = new TernarySearchTrie<String>();

            trie.Put("test", "val1");
            trie.Put("test", "val2");

            Assert.Equal("val2", trie.Get("test"));
        }

        [Fact]
        public void Test_KeyExists_Given_Put_Key_Returns_True()
        {
            var trie = new TernarySearchTrie<String>();
            
            trie.Put("test", "val");
            
            Assert.True(trie.KeyExists("test"));
        }
        
        [Fact]
        public void Test_KeyExists_Given_Not_Put_Key_Returns_False()
        {
            var trie = new TernarySearchTrie<String>();
            
            trie.Put("test", "val");
            
            Assert.False(trie.KeyExists("quest"));
        }



    }
}
