using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters.Student
{
	public class StudentDisciplineConverter : IParser<(StudentModel student, List<DisciplineModel> discipline), StudentDisciplineVO>
	{
		private readonly DisciplineConverter _disciplineConverter;

		public StudentDisciplineConverter()
		{
			_disciplineConverter = new DisciplineConverter();
		}

		public StudentDisciplineVO Parse((StudentModel student, List<DisciplineModel> discipline) origin)
		{
			if (origin.student is null || origin.discipline is null)
				return null;

			return new StudentDisciplineVO
			{
				StudentId = origin.student.StudentId,
				Name = origin.student.Name,
				Email = origin.student.Email,
				CourseId = origin.student.CourseId,
				Disciplines = _disciplineConverter.ParseList(origin.discipline)
			};
		}

		public List<StudentDisciplineVO> ParseList(List<(StudentModel, List<DisciplineModel>)> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}