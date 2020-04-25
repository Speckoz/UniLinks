using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Linq;
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
		private IList<LessonDisciplineVO> lessonOrigin;
		private byte filterSelected;

		private byte FilterSelected
		{
			get => filterSelected;
			set
			{
				filterSelected = value;
				lessons = lessonOrigin;
			}
		}

		[Inject]
		private ISessionStorageService SessionStorage { get; set; }

		protected async override Task OnInitializedAsync()
		{
			string token = await SessionStorage.GetItemAsync<string>("token");
			lessons = lessonOrigin = await new LessonService().GetAllLessonsTaskAync(token, "d02f5571-f056-4bff-a5e0-a927306ae77d;10e2babb-eb2a-4473-b9d9-499d9f595c43");
		}

		private void SelectLesson(LessonDisciplineVO lesson) => selected = lesson;

		private void FilterSearch(ChangeEventArgs e)
		{
			string value = e.Value.ToString();
			if (!string.IsNullOrEmpty(value))
			{
				switch (FilterSelected)
				{
					case 1:
						lessons = lessonOrigin.Where(x => x.Discipline.Name.ToLower().Contains(value.ToLower())).ToList();
						return;

					case 2:
						if (byte.TryParse(value, out byte result))
							lessons = lessonOrigin.Where(x => x.Discipline.Period.Equals(result)).ToList();
						return;

					case 3:
						//caso seja por data
						return;
				}
			}

			lessons = lessonOrigin;
		}
	}
}