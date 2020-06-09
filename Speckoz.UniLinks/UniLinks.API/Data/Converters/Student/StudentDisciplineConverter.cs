using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Data.Converters.Student
{
	public class StudentDisciplineConverter : IParser<(StudentModel student, List<DisciplineModel> discipline), StudentDisciplineVO>
	{
		private readonly DisciplineConverter _disciplineConverter;
		private readonly StudentConverter _studentConverter;

		public StudentDisciplineConverter()
		{
			_disciplineConverter = new DisciplineConverter();
			_studentConverter = new StudentConverter();
		}

		public StudentDisciplineVO Parse((StudentModel student, List<DisciplineModel> discipline) origin)
		{
			if (origin.student is null || origin.discipline is null)
				return null;

			return new StudentDisciplineVO
			{
				Student = _studentConverter.Parse(origin.student),
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