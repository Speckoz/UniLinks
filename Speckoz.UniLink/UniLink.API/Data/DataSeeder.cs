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
			UserLoginModel u1 = null, u2 = null, u3 = null, u4 = null, u5 = null, u6 = null;
			CourseModel c1 = null, c2 = null;
			DisciplineModel d1 = null, d2 = null;
			LessonModel l1 = null, l2 = null;
			StudentModel s1 = null, s2 = null, s3 = null, s4 = null;

			SeedUsers();
			SeedCourse();
			SeedDisciplines();
			SeedLeassons();
			SeedStudents();

			_context.SaveChanges();

			void SeedUsers()
			{
				if (!_context.Users.Any())
				{
					// Coords
					u1 = new UserLoginModel
					{
						UserId = Guid.Parse("DA418561-E1B0-41C9-955D-2B41F35F0C4F"),
						Name = "Coord Eletrica",
						Email = "coord.ee@unilink.com",
						Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
						UserType = UserTypeEnum.Coordinator
					};

					u2 = new UserLoginModel
					{
						UserId = Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088"),
						Name = "Coord Sistemas",
						Email = "coord.si@unilink.com",
						Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
						UserType = UserTypeEnum.Coordinator
					};

					//Students
					u3 = new UserLoginModel
					{
						UserId = Guid.Parse("74E8B9D9-310D-431B-B118-D386B85F8B8A"),
						Name = "Ruan Carlos",
						Email = "logikoz@unilink.com",
						UserType = UserTypeEnum.Student
					};
					u4 = new UserLoginModel
					{
						UserId = Guid.Parse("94D21EF7-7F3B-4DA5-8B81-1A382BC235A3"),
						Name = "Joao Figueredo",
						Email = "joaof@unilink.com",
						UserType = UserTypeEnum.Student
					};

					u5 = new UserLoginModel
					{
						UserId = Guid.Parse("900BBD93-EE4F-4938-93A2-CF20FD49673E"),
						Name = "Carlos Eduardo",
						Email = "carlose@unilink.com",
						UserType = UserTypeEnum.Student
					};

					u6 = new UserLoginModel
					{
						UserId = Guid.Parse("4E4F780A-0313-45F6-8897-3BFF7D930778"),
						Name = "Marco Pandolfo",
						Email = "specko@unilink.com",
						UserType = UserTypeEnum.Student
					};

					_context.Users.AddRange(u1, u2, u3, u4, u5, u6);
				}
			}

			void SeedCourse()
			{
				if (!_context.Courses.Any())
				{
					c1 = new CourseModel
					{
						CourseId = Guid.Parse("8E6BDCE0-6B3C-4847-8EB7-0B5C4BC6549E"),
						Name = "Engenharia Eletrica",
						Periods = 10,
						CoordinatorId = Guid.Parse("DA418561-E1B0-41C9-955D-2B41F35F0C4F")
					};
					c2 = new CourseModel
					{
						CourseId = Guid.Parse("498880FD-A708-4BD4-AAC7-4AD859DC1FD8"),
						Name = "Sistemas de Informaçao",
						Periods = 8,
						CoordinatorId = Guid.Parse("BBB85201-C532-43F8-9F53-FC1696F49088")
					};

					_context.Courses.AddRange(c1, c2);
				}
			}

			void SeedDisciplines()
			{
				if (!_context.Disciplines.Any())
				{
					d1 = new DisciplineModel
					{
						DisciplineId = Guid.Parse("D02F5571-F056-4BFF-A5E0-A927306AE77D"),
						Name = "Principios de Eletronica",
						Teacher = "Son Goku",
						Period = 5,
						CourseId = c1.CourseId
					};
					d2 = new DisciplineModel
					{
						DisciplineId = Guid.Parse("10E2BABB-EB2A-4473-B9D9-499D9F595C43"),
						Name = "Fundamentos de programaçao",
						Teacher = "Naruto Uzumaki",
						Period = 2,
						CourseId = c2.CourseId
					};

					_context.Disciplines.AddRange(d1, d2);
				}
			}

			void SeedLeassons()
			{
				if (!_context.Lessons.Any())
				{
					l1 = new LessonModel
					{
						Date = DateTime.Now,
						LessonSubject = "Lição",
						URI = "https://logikoz.net",
						DisciplineId = d1.DisciplineId
					};

					l2 = new LessonModel
					{
						Date = DateTime.Now,
						LessonSubject = "Prova",
						URI = "https://bit.ly/2RF0oFQ",
						DisciplineId = d2.DisciplineId
					};

					_context.Lessons.AddRange(l1, l2);
				}
			}

			void SeedStudents()
			{
				if (!_context.Students.Any())
				{
					s1 = new StudentModel
					{
						StudentId = Guid.Parse("74E8B9D9-310D-431B-B118-D386B85F8B8A"),
						CourseId = c1.CourseId
					};
					s2 = new StudentModel
					{
						StudentId = Guid.Parse("94D21EF7-7F3B-4DA5-8B81-1A382BC235A3"),
						CourseId = c1.CourseId
					};
					s3 = new StudentModel
					{
						StudentId = Guid.Parse("900BBD93-EE4F-4938-93A2-CF20FD49673E"),
						CourseId = c2.CourseId
					};
					s4 = new StudentModel
					{
						StudentId = Guid.Parse("4E4F780A-0313-45F6-8897-3BFF7D930778"),
						CourseId = c2.CourseId
					};

					_context.Students.AddRange(s1, s2, s3, s4);
				}
			}
		}
	}
}