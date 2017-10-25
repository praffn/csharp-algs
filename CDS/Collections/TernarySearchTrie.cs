using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    /**
     * Ternary Search Trie (TST) based on Sedwick and Wayne, Algorithms Fourth Edition's TST.
     **/
    public class TernarySearchTrie<Value> : ITrie<Value>
    {
        private Node root;

        // Returns the number of 
        public int Count { get; private set;  } 

        public Value Get(string key)
        {
            Node node = Get(root, key, 0);
            if (node == null)
            {
                return default(Value);
            }
            return node.val;

            throw new NotImplementedException();
        }

        private Node Get(Node node, string key, int depth)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, Value val)
        {
            throw new NotImplementedException();
            Count++;
        }

        private Node Put(Node node, string key, Value val, int depth)
        {
            throw new NotImplementedException();
        }

        private class Node
        {
            internal char c;
            internal Node left, mid, right;
            internal Value val;
        }
    }
}
