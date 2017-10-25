using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    public class TernarySearchTrie<Value> : ITrie<Value>
    {
        public Value Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Put(string key, Value val)
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
