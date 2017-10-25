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
            if (node == null) return default(Value);
            return node.val;
        }

        private Node Get(Node node, string key, int depth)
        {
            if (node == null) return default(Node); // If the node is null, return null
            char c = key[depth]; // Set current character in local variable

            if (c < node.character) return Get(node.left, key, depth);
            else if (c > node.character) return Get(node.right, key, depth);
            else if (depth < key.Length - 1) return Get(node.mid, key, depth + 1);
            else return node;
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
            internal char character;
            internal Node left, mid, right;
            internal Value val;
        }
    }
}
