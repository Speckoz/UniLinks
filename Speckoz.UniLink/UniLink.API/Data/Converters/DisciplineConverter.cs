using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
	public class DisciplineConverter : IParser<DisciplineVO, DisciplineModel>, IParser<DisciplineModel, DisciplineVO>
	{
		public DisciplineModel Parse(DisciplineVO origin)
		{
			if (origin is null)
				return null;

			return new DisciplineModel
			{
				DisciplineId = origin.DisciplineId,
				Name = origin.Name,
				CourseId = origin.CourseId,
				Period = origin.Period,
				Teacher = origin.Teacher,
				ClassId = origin.ClassId
			};
		}

		public DisciplineVO Parse(DisciplineModel origin)
		{
			if (origin is null)
				return null;

			return new DisciplineVO
			{
				DisciplineId = origin.DisciplineId,
				Name = origin.Name,
				CourseId = origin.CourseId,
				Period = origin.Period,
				Teacher = origin.Teacher,
				ClassId = origin.ClassId
			};
		}

		public IList<DisciplineModel> ParseList(IList<DisciplineVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public IList<DisciplineVO> ParseList(IList<DisciplineModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}