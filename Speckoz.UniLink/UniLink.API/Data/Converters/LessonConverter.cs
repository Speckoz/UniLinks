using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
	public class LessonConverter : IParser<LessonVO, LessonModel>, IParser<LessonModel, LessonVO>
	{
		public LessonModel Parse(LessonVO origin)
		{
			if (origin == null)
				return new LessonModel();

			return new LessonModel
			{
				Date = origin.Date,
				DisciplineId = origin.DisciplineId,
				LessonId = origin.LessonId,
				LessonSubject = origin.LessonSubject,
				URI = origin.URI
			};
		}

		public LessonVO Parse(LessonModel origin)
		{
			if (origin == null)
				return new LessonVO();

			return new LessonVO
			{
				Date = origin.Date,
				DisciplineId = origin.DisciplineId,
				LessonId = origin.LessonId,
				LessonSubject = origin.LessonSubject,
				URI = origin.URI
			};
		}

		public IList<LessonModel> ParseList(IList<LessonVO> origin)
		{
			if (origin == null)
				return new List<LessonModel>();

			return origin.Select(item => Parse(item)).ToList();
		}

		public IList<LessonVO> ParseList(IList<LessonModel> origin)
		{
			if (origin == null)
				return new List<LessonVO>();

			return origin.Select(item => Parse(item)).ToList();
		}
	}
}