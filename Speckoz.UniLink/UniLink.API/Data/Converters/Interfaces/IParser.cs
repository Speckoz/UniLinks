using System.Collections.Generic;

namespace UniLink.API.Data.Converters.Interfaces
{
	public interface IParser<O, D>
	{
		D Parse(O origin);

		IList<D> ParseList(IList<O> origin);
	}
}