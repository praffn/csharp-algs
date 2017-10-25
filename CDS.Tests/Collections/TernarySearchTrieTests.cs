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
            var trie = new TernarySearchTrie<int>();

            trie.Put("test", 8);

            Assert.Equal(1, trie.Count);
        }

        [Fact]
        public void Test_Put_Given_Five_Elements_Trie_Count_Equals_Five()
        {
            var trie = new TernarySearchTrie<int>();

            trie.Put("test", 1);
            trie.Put("pest", 3);
            trie.Put("quest", 5);
            trie.Put("best", 7);
            trie.Put("nest", 9);

            Assert.Equal(5, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Count_Does_Not_Increase()
        {
            var trie = new TernarySearchTrie<int>();

            trie.Put("test", 1);
            trie.Put("test", 2);

            Assert.Equal(1, trie.Count);
        }

        [Fact]
        public void Test_Put_With_Identic_Key_Value_Updates()
        {
            var trie = new TernarySearchTrie<int>();

            trie.Put("test", 1);
            trie.Put("test", 2);

            Assert.Equal(2, trie.Get("test"));
        }



    }
}
