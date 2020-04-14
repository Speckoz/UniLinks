using System.Linq;

using UniLink.API.Models;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Data
{
	public class DevData : Repository.Repository
	{
		public DevData(DataContext context) : base(context)
		{
		}

		public void Init()
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
	}
}