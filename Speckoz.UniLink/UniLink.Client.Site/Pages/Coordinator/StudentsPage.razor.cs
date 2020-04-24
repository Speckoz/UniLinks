using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Services.Coordinator;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Pages.Coordinator
{
	[Authorizes(UserTypeEnum.Coordinator)]
	public partial class StudentsPage
	{
		private int selectedStudent = -1;
		private IList<StudentVO> students;

		[Inject]
		private ISessionStorageService SessionStorage { get; set; }

		[Inject]
		private IJSRuntime JSRuntime { get; set; }

		protected override async Task OnInitializedAsync()
		{
			string token = await SessionStorage.GetItemAsync<string>("token");

			CourseVO course = await new CourseService().GetCourseByCoordIdTaskAsync(token);

			students = await new StudentService().GetStudentsTaskAsync(token, course.CourseId);
			foreach (StudentVO student in students)
				student.DisciplinesList = await new DisciplineService().GetDisciplinesByRangeTaskAsync(token, student.Disciplines);
		}

		private async Task ViewDisciplines(IList<DisciplineVO> disciplines)
		{
			selectedStudent = students.IndexOf(students.Where(x => x.DisciplinesList.Equals(disciplines)).SingleOrDefault());
			await JSRuntime.InvokeVoidAsync("ShowModal", "modalStudentDisciplines");
		}

		private async Task RemoveStudent(string nome)
		{
			await JSRuntime.InvokeVoidAsync("SendAlert", $"Voce removeu {nome}\n\nMintira");
		}

		private async Task EditStudent(string nome)
		{
			await JSRuntime.InvokeVoidAsync("SendAlert", $"Voce editou {nome}\n\nMintira");
		}
	}
}