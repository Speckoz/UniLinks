using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters.Student
{
	public class StudentDisciplineConverter : IParser<(StudentModel student, IList<DisciplineModel> discipline), StudentDisciplineVO>
	{
		private readonly DisciplineConverter _disciplineConverter;
		private readonly StudentConverter _studentConverter;

		public StudentDisciplineConverter()
		{
			_disciplineConverter = new DisciplineConverter();
			_studentConverter = new StudentConverter();
		}

		public StudentDisciplineVO Parse((StudentModel student, IList<DisciplineModel> discipline) origin)
		{
			if (origin.student is null || origin.discipline is null)
				return null;

			return new StudentDisciplineVO
			{
				Disciplines = _disciplineConverter.ParseList(origin.discipline),
				Student = _studentConverter.Parse(origin.student)
			};
		}

		public IList<StudentDisciplineVO> ParseList(IList<(StudentModel, IList<DisciplineModel>)> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}