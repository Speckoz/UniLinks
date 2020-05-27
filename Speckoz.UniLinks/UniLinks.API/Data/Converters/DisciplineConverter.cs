using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Data.Converters
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

		public List<DisciplineModel> ParseList(List<DisciplineVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public List<DisciplineVO> ParseList(List<DisciplineModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}