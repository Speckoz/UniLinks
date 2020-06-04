using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.API.Models;
using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Data.Converters
{
	public class ClassConverter : IParser<ClassModel, ClassVO>, IParser<ClassVO, ClassModel>
	{
		public ClassModel Parse(ClassVO origin)
		{
			if (origin is null)
				return null;

			return new ClassModel
			{
				ClassId = origin.ClassId,
				CourseId = origin.CourseId,
				Period = origin.Period,
				URI = origin.URI,
				WeekDays = origin.WeekDays
			};
		}

		public ClassVO Parse(ClassModel origin)
		{
			if (origin is null)
				return null;

			return new ClassVO
			{
				ClassId = origin.ClassId,
				CourseId = origin.CourseId,
				Period = origin.Period,
				URI = origin.URI,
				WeekDays = origin.WeekDays
			};
		}

		public List<ClassModel> ParseList(List<ClassVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}

		public List<ClassVO> ParseList(List<ClassModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(x => Parse(x)).ToList()
			};
		}
	}
}