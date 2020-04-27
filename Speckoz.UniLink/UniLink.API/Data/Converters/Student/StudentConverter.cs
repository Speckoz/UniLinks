using System;
using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters.Student
{
	public class StudentConverter : IParser<StudentVO, StudentModel>, IParser<StudentModel, StudentVO>
	{
		public StudentModel Parse(StudentVO origin)
		{
			if (origin == null)
				return new StudentModel();

			return new StudentModel
			{
				StudentId = origin.StudentId,
				Name = origin.Name,
				Email = origin.Email,
				CourseId = origin.CourseId,
				Disciplines = string.Join(";", origin.Disciplines.Select(x => x.DisciplineId.ToString()).ToArray())
			};
		}

		public StudentVO Parse(StudentModel origin)
		{
			if (origin == null)
				return new StudentVO();

			IList<DisciplineVO> disciplines = new List<DisciplineVO>();
			origin.Disciplines.Split(';').ToList().ForEach(x => disciplines.Add(new DisciplineVO { DisciplineId = Guid.Parse(x) }));

			return new StudentVO
			{
				StudentId = origin.StudentId,
				Name = origin.Name,
				Email = origin.Email,
				CourseId = origin.CourseId,
				Disciplines = disciplines
			};
		}

		public IList<StudentModel> ParseList(IList<StudentVO> origin)
		{
			if (origin == null)
				return new List<StudentModel>();

			return origin.Select(item => Parse(item)).ToList();
		}

		public IList<StudentVO> ParseList(IList<StudentModel> origin)
		{
			if (origin == null)
				return new List<StudentVO>();

			return origin.Select(item => Parse(item)).ToList();
		}
	}
}