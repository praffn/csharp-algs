using System.Collections;

namespace CDS.Collections
{
	public interface ICollection : IEnumerable
	{
		int Count { get; }
		bool Empty { get; }
		void Clear();
	}
}
