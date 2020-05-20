using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters.Lesson
{
	public class LessonConverter : IParser<LessonVO, LessonModel>, IParser<LessonModel, LessonVO>
	{
		public LessonModel Parse(LessonVO origin)
		{
			if (origin is null)
				return null;

			return new LessonModel
			{
				Date = origin.Date,
				DisciplineId = origin.DisciplineId,
				LessonId = origin.LessonId,
				LessonSubject = origin.LessonSubject,
				URI = origin.URI,
				CourseId = origin.CourseId
			};
		}

		public LessonVO Parse(LessonModel origin)
		{
			if (origin is null)
				return null;

			return new LessonVO
			{
				Date = origin.Date,
				DisciplineId = origin.DisciplineId,
				LessonId = origin.LessonId,
				LessonSubject = origin.LessonSubject,
				URI = origin.URI,
				CourseId = origin.CourseId
			};
		}

		public IList<LessonModel> ParseList(IList<LessonVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public IList<LessonVO> ParseList(IList<LessonModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}