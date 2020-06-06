using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Data.Converters.Student
{
	public class StudentConverter : IParser<StudentVO, StudentModel>, IParser<StudentModel, StudentVO>
	{
		public StudentModel Parse(StudentVO origin)
		{
			if (origin is null)
				return null;

			return new StudentModel
			{
				StudentId = origin.StudentId,
				Name = origin.Name,
				Email = origin.Email,
				CourseId = origin.CourseId,
				Disciplines = origin.Disciplines
			};
		}

		public StudentVO Parse(StudentModel origin)
		{
			if (origin is null)
				return null;

			return new StudentVO
			{
				StudentId = origin.StudentId,
				Name = origin.Name,
				Email = origin.Email,
				CourseId = origin.CourseId,
				Disciplines = origin.Disciplines
			};
		}

		public List<StudentModel> ParseList(List<StudentVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public List<StudentVO> ParseList(List<StudentModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}