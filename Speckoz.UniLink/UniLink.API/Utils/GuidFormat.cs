using System;
using System.Collections.Generic;

namespace UniLink.API.Utils
{
	public class GuidFormat
	{
		public static bool TryParseList(string guids, char separator, out IList<Guid> result)
		{
			//chegando se os guids estao no formato correto.
			var aux = new List<Guid>();
			foreach (string discipline in guids.Split(separator))
			{
				if (Guid.TryParse(discipline, out Guid guid))
					aux.Add(guid);
				else
				{
					result = null;
					return false;
				}
			}

			result = aux;
			return true;
		}
	}
}