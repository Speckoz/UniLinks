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
			SeedClasses();
		}

		private void SeedUsers()
		{
			if (!_context.Users.Any())
			{
				_context.Users.Add(new UserLoginModel
				{
					Name = "Administrador",
					Email = "admin@unilink.com",
					Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
					UserType = UserTypeEnum.Coordinator
				});

				_context.Users.Add(new UserLoginModel
				{
					Name = "Aluno Teste",
					Email = "Student@unilink.com",
					UserType = UserTypeEnum.Student
				});

				_context.SaveChanges();
			}
		}

		private void SeedClasses()
		{
			if (!_context.Lessons.Any())
			{
				_context.Lessons.Add(new LessonModel
				{
					Date = DateTime.Now,
					LessonSubject = "Lição",
					URI = "https://logikoz.net",
					Discipline = new DisciplineModel { Teacher = "Ruan", Period = 5, Name = "Principios de Eletronica", Course = "Eng Eletrica" },
				});

				_context.Lessons.Add(new LessonModel
				{
					Date = DateTime.Now,
					LessonSubject = "Prova",
					URI = "https://bit.ly/2RF0oFQ",
					Discipline = new DisciplineModel { Teacher = "Marco Cornolfo", Period = 2, Name = "Fundamentos de programaçao", Course = "Sistemas de Informaçao" },
				});

				_context.SaveChanges();
			}
		}
	}
}