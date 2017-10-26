using System;
using System.Collections.Generic;
using System.Text;

namespace CDS.Collections
{
	public interface ITrie<TValue>
	{
		TValue Get(string key);
		void Put(string key, TValue val);
	}
}
