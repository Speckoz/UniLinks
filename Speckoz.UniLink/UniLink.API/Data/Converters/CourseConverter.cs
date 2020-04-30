using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
	public class CourseConverter : IParser<CourseVO, CourseModel>, IParser<CourseModel, CourseVO>
	{
		public CourseModel Parse(CourseVO origin)
		{
			if (origin is null)
				return null;

			return new CourseModel
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				CourseId = origin.CourseId,
				Periods = origin.Periods
			};
		}

		public CourseVO Parse(CourseModel origin)
		{
			if (origin is null)
				return null;

			return new CourseVO
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				CourseId = origin.CourseId,
				Periods = origin.Periods
			};
		}

		public IList<CourseModel> ParseList(IList<CourseVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public IList<CourseVO> ParseList(IList<CourseModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}