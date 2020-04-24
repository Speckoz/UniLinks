using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO.Lesson;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters.Lesson
{
	public class LessonDisciplineConverter : IParser<(LessonModel lesson, DisciplineModel discipline), LessonDisciplineVO>
	{
		private readonly LessonConverter _lessonConverter;
		private readonly DisciplineConverter _disciplineConverter;

		public LessonDisciplineConverter()
		{
			_lessonConverter = new LessonConverter();
			_disciplineConverter = new DisciplineConverter();
		}

		public LessonDisciplineVO Parse((LessonModel lesson, DisciplineModel discipline) origin)
		{
			if (origin.lesson is null || origin.discipline is null)
				return new LessonDisciplineVO();

			return new LessonDisciplineVO
			{
				Lesson = _lessonConverter.Parse(origin.lesson),
				Discipline = _disciplineConverter.Parse(origin.discipline)
			};
		}

		public IList<LessonDisciplineVO> ParseList(IList<(LessonModel, DisciplineModel)> origin)
		{
			if (origin == null)
				return new List<LessonDisciplineVO>();

			return origin.Select(item => Parse(item)).ToList();
		}
	}
}