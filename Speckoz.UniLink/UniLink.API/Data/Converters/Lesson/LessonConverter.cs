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
				LessonId = origin.LessonId,
				URI = origin.URI,
				LessonSubject = origin.LessonSubject,
				DisciplineId = origin.DisciplineId,
				CourseId = origin.CourseId,
				Date = origin.Date,
				RecordName = origin.RecordName,
				Duration = origin.Duration
			};
		}

		public LessonVO Parse(LessonModel origin)
		{
			if (origin is null)
				return null;

			return new LessonVO
			{
				LessonId = origin.LessonId,
				URI = origin.URI,
				LessonSubject = origin.LessonSubject,
				DisciplineId = origin.DisciplineId,
				CourseId = origin.CourseId,
				Date = origin.Date,
				RecordName = origin.RecordName,
				Duration = origin.Duration
			};
		}

		public List<LessonModel> ParseList(List<LessonVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public List<LessonVO> ParseList(List<LessonModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}