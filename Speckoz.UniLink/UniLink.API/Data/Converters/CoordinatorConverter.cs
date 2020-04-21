using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.API.Models;
using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Data.Converters
{
	public class CoordinatorConverter : IParser<CoordinatorVO, CoordinatorModel>, IParser<CoordinatorModel, CoordinatorVO>
	{
		public CoordinatorModel Parse(CoordinatorVO origin)
		{
			if (origin == null)
				return new CoordinatorModel();

			return new CoordinatorModel
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				Email = origin.Email,
				Password = origin.Password
			};
		}

		public CoordinatorVO Parse(CoordinatorModel origin)
		{
			if (origin == null)
				return new CoordinatorVO();

			return new CoordinatorVO
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				Email = origin.Email,
				Password = origin.Password
			};
		}

		public IList<CoordinatorModel> ParseList(IList<CoordinatorVO> origin)
		{
			if (origin == null)
				return new List<CoordinatorModel>();

			return origin.Select(item => Parse(item)).ToList();
		}

		public IList<CoordinatorVO> ParseList(IList<CoordinatorModel> origin)
		{
			if (origin == null)
				return new List<CoordinatorVO>();

			return origin.Select(item => Parse(item)).ToList();
		}
	}
}