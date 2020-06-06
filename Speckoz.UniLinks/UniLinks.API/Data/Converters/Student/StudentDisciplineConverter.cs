using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Data.Converters.Student
{
	public class StudentDisciplineConverter : IParser<(StudentModel student, List<DisciplineModel> discipline), StudentVO>
	{
		private readonly DisciplineConverter _disciplineConverter;

		public StudentDisciplineConverter()
		{
			_disciplineConverter = new DisciplineConverter();
		}

		public StudentVO Parse((StudentModel student, List<DisciplineModel> discipline) origin)
		{
			if (origin.student is null || origin.discipline is null)
				return null;

			return new StudentVO
			{
				StudentId = origin.student.StudentId,
				Name = origin.student.Name,
				Email = origin.student.Email,
				CourseId = origin.student.CourseId,
				Disciplines = _disciplineConverter.ParseList(origin.discipline)
			};
		}

		public List<StudentVO> ParseList(List<(StudentModel, List<DisciplineModel>)> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}