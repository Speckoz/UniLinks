using Microsoft.EntityFrameworkCore.Internal;

using System;
using System.Linq;

using UniLink.API.Models;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data
{
	public class DataSeeder : Repository.BaseRepository
	{
		public DataSeeder(DataContext context) : base(context)
		{
		}

		public void Init()
		{
			SeedUsers();
			SeedCourse();
			//SeedDisciplines();
			//SeedLeassons();
			//SeedStudents();
		}

		private void SeedUsers()
		{
			if (!_context.Users.Any())
			{
				_context.Users.Add(new UserLoginModel
				{
					UserId = Guid.Parse("DA418561-E1B0-41C9-955D-2B41F35F0C4F"),
					Name = "Coord Eletrica",
					Email = "coord.ee@unilink.com",
					Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
					UserType = UserTypeEnum.Coordinator
				});
				_context.Users.Add(new UserLoginModel
				{
					UserId = Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088"),
					Name = "Coord Sistemas",
					Email = "coord.si@unilink.com",
					Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
					UserType = UserTypeEnum.Coordinator
				});

				_context.Users.Add(new UserLoginModel
				{
					UserId = Guid.Parse("74E8B9D9-310D-431B-B118-D386B85F8B8A"),
					Name = "Ruan Carlos",
					Email = "logikoz@unilink.com",
					UserType = UserTypeEnum.Student
				});

				_context.Users.Add(new UserLoginModel
				{
					UserId = Guid.Parse("4E4F780A-0313-45F6-8897-3BFF7D930778"),
					Name = "Marco Pandolfo",
					Email = "specko@unilink.com",
					UserType = UserTypeEnum.Student
				});

				_context.SaveChanges();
			}
		}

		private void SeedCourse()
		{
			if (!_context.Courses.Any())
			{
				_context.Courses.Add(new CourseModel
				{
					CourseId = Guid.Parse("8E6BDCE0-6B3C-4847-8EB7-0B5C4BC6549E"),
					Name = "Engenharia Eletrica",
					Periods = 10,
					CoordinatorId = Guid.Parse("DA418561-E1B0-41C9-955D-2B41F35F0C4F")
				});
				_context.Courses.Add(new CourseModel
				{
					CourseId = Guid.Parse("498880FD-A708-4BD4-AAC7-4AD859DC1FD8"),
					Name = "Sistemas de Informaçao",
					Periods = 8,
					CoordinatorId = Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088")
				});

				_context.SaveChanges();
			}
		}

		private void SeedDisciplines()
		{
			if (!_context.Disciplines.Any())
			{
				_context.Disciplines.Add(new DisciplineModel
				{
					DisciplineId = Guid.Parse("D02F5571-F056-4BFF-A5E0-A927306AE77D"),
					Name = "Principios de Eletronica",
					Teacher = "Son Goku",
					Period = 5,
					Course = _context.Courses.SingleOrDefault(x => x.CourseId == Guid.Parse("8E6BDCE0-6B3C-4847-8EB7-0B5C4BC6549E"))
				});
				//_context.Disciplines.Add(new DisciplineModel
				//{
				//	DisciplineId = Guid.Parse("10E2BABB-EB2A-4473-B9D9-499D9F595C43"),
				//	Name = "Fundamentos de programaçao",
				//	Teacher = "Naruto Uzumaki",
				//	Period = 2,
				//	Course = _context.Courses.SingleOrDefault(x => x.CourseId == Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088"))
				//});

				_context.SaveChanges();
			}
		}

		private void SeedLeassons()
		{
			if (!_context.Lessons.Any())
			{
				_context.Lessons.Add(new LessonModel
				{
					Date = DateTime.Now,
					LessonSubject = "Lição",
					URI = "https://logikoz.net",
					Discipline = _context.Disciplines.SingleOrDefault(x => x.DisciplineId == Guid.Parse("D02F5571-F056-4BFF-A5E0-A927306AE77D"))
				});

				_context.Lessons.Add(new LessonModel
				{
					Date = DateTime.Now,
					LessonSubject = "Prova",
					URI = "https://bit.ly/2RF0oFQ",
					Discipline = _context.Disciplines.SingleOrDefault(x => x.DisciplineId == Guid.Parse("10E2BABB-EB2A-4473-B9D9-499D9F595C43"))
				});

				_context.SaveChanges();
			}
		}

		private void SeedStudents()
		{
			if (!_context.Students.Any())
			{
				_context.Students.Add(new StudentModel
				{
					UserId = Guid.Parse("74E8B9D9-310D-431B-B118-D386B85F8B8A"),
					Course = _context.Courses.SingleOrDefault(x => x.CourseId == Guid.Parse("8E6BDCE0-6B3C-4847-8EB7-0B5C4BC6549E"))
				});
				_context.Students.Add(new StudentModel
				{
					UserId = Guid.Parse("4E4F780A-0313-45F6-8897-3BFF7D930778"),
					Course = _context.Courses.SingleOrDefault(x => x.CourseId == Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088"))
				});

				_context.SaveChanges();
			}
		}
	}
}