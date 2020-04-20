using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
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
				Disciplines = origin.Disciplines
			};
		}

		public StudentVO Parse(StudentModel origin)
		{
			if (origin == null)
				return new StudentVO();
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
			if (origin == null)
				return new List<StudentModel>();
			return origin.Select(item => Parse(item)).ToList();
		}

		public List<StudentVO> ParseList(List<StudentModel> origin)
		{
			if (origin == null)
				return new List<StudentVO>();
			return origin.Select(item => Parse(item)).ToList();
		}
	}
}