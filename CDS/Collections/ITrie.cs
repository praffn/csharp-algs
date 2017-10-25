using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
    public interface ITrie<Value>
    {
        Value Get(string key);
        void Put(string key, Value val);
    }
}
