using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.API.Models;
using UniLink.Dependencies.Data.VO.Coordinator;

namespace UniLink.API.Data.Converters
{
	public class CoordinatorConverter : IParser<CoordinatorVO, CoordinatorModel>, IParser<CoordinatorModel, CoordinatorVO>
	{
		public CoordinatorModel Parse(CoordinatorVO origin)
		{
			if (origin is null)
				return null;

			return new CoordinatorModel
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				Email = origin.Email,
				Password = origin.Password,
				CourseId = origin.CourseId
			};
		}

		public CoordinatorVO Parse(CoordinatorModel origin)
		{
			if (origin is null)
				return null;

			return new CoordinatorVO
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				Email = origin.Email,
				Password = origin.Password,
				CourseId = origin.CourseId
			};
		}

		public List<CoordinatorModel> ParseList(List<CoordinatorVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public List<CoordinatorVO> ParseList(List<CoordinatorModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}