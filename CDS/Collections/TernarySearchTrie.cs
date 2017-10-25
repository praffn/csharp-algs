using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    public class TernarySearchTrie<Value> : ITrie<Value>
    {
        // Returns the number of 
        public int Count { get; private set;  } 
        public Value Get(string key)
        {
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
            char c;
            Node left, mid, right;
            Value val;
        }
    }
}
