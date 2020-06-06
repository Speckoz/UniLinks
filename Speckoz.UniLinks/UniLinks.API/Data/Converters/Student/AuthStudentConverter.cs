using System;
using System.Collections.Generic;
using System.Linq;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Data.Converters.Student
{
	public class AuthStudentConverter : IParser<AuthStudentVO, StudentModel>, IParser<StudentModel, AuthStudentVO>
	{
		public StudentModel Parse(AuthStudentVO origin)
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

		public AuthStudentVO Parse(StudentModel origin)
		{
			if (origin is null)
				return null;

			return new AuthStudentVO
			{
				StudentId = origin.StudentId,
				Name = origin.Name,
				Email = origin.Email,
				CourseId = origin.CourseId,
				Disciplines = origin.Disciplines
			};
		}

		public List<StudentModel> ParseList(List<AuthStudentVO> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}

		public List<AuthStudentVO> ParseList(List<StudentModel> origin)
		{
			return origin switch
			{
				null => null,
				_ => origin.Select(item => Parse(item)).ToList()
			};
		}
	}
}