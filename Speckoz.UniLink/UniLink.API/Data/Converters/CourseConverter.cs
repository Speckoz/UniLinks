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
			if (origin == null)
				return new CourseModel();
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
			if (origin == null)
				return new CourseVO();
			return new CourseVO
			{
				CoordinatorId = origin.CoordinatorId,
				Name = origin.Name,
				CourseId = origin.CourseId,
				Periods = origin.Periods
			};
		}

		public List<CourseModel> ParseList(List<CourseVO> origin)
		{
			if (origin == null)
				return new List<CourseModel>();
			return origin.Select(item => Parse(item)).ToList();
		}

		public List<CourseVO> ParseList(List<CourseModel> origin)
		{
			if (origin == null)
				return new List<CourseVO>();
			return origin.Select(item => Parse(item)).ToList();
		}
	}
}