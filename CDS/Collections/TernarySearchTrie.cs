using System;

namespace CDS.Collections
{
    /**
     * Ternary Search Trie (TST) based on Sedwick and Wayne, Algorithms Fourth Edition's TST.
     * Note that this implementation MUST be used with non-primitive types. You may not use int, char, string etc.
     * Use the Wrapper classes (That returns null as default value! Int32 does not return null) if you need to use them.
     **/
    public class TernarySearchTrie<TValue> : ITrie<TValue>
    {
        private HashSet<string> _keys;
        private Node root;

        public TernarySearchTrie()
        {
            _keys = new HashSet<string>();
        }

        public bool KeyExists(string key)
        {
            return _keys.Contains(key);
        }

        // Returns the number of 
        public int Count { get; private set;  } 

        public TValue Get(String key)
        {
            Node node = Get(root, key, 0);
            // If the node is null, return null
            if (node == null) return default(TValue); 
            return node.val;
        }

        private Node Get(Node node, string key, int depth)
        {
            // If the node is null, return null
            if (node == null) return default(Node);
            // Set current character in local variable
            char c = key[depth]; 

            if (c < node.character) return Get(node.left, key, depth);
            else if (c > node.character) return Get(node.right, key, depth);
            else if (depth < key.Length - 1) return Get(node.mid, key, depth + 1);
            else return node;
        }

        public void Put(String key, TValue val)
        {
            if (key == null) throw new ArgumentException("Invalid key");
            if (!KeyExists(key)) Count++;
            root = Put(root, key, val, 0);
            _keys.Add(key);
        }

        private Node Put(Node node, String key, TValue val, int depth)
        {
            // Set the current character in local variable
            char c = key[depth]; 
            // If the node is null, create a new node and set its character.
            if (node == null)
            {
                node = new Node();
                node.character = c;
            }

            if (c < node.character) node.left = Put(node.left, key, val, depth);
            else if (c > node.character) node.right = Put(node.right, key, val, depth);
            else if (depth < key.Length - 1) node.mid = Put(node.mid, key, val, depth + 1);
            else node.val = val;

            return node;

        }

        private class Node
        {
            internal char character;
            internal Node left, mid, right;
            internal TValue val;
        }
    }
}
