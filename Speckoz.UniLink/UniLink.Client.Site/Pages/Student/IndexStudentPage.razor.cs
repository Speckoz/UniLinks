using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Client.Site.Services.Student;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO.Lesson;
using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Pages.Student
{
	[Authorizes(UserTypeEnum.Student)]
	public partial class IndexStudentPage
	{
		private LessonDisciplineVO selected;
		private IList<LessonDisciplineVO> lessons;

		[Inject]
		private ISessionStorageService SessionStorage { get; set; }

		protected async override Task OnInitializedAsync()
		{
			string token = await SessionStorage.GetItemAsync<string>("token");
			lessons = await new LessonService().GetAllLessonsTaskAync(token, "d02f5571-f056-4bff-a5e0-a927306ae77d;10e2babb-eb2a-4473-b9d9-499d9f595c43");
		}

		private void SelectLesson(LessonDisciplineVO lesson) => selected = lesson;
	}
}