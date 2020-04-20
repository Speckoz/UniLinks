using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Client.Site.Services.Admin;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.Client.Site.Pages.Admin
{
	[Authorizes(UserTypeEnum.Coordinator)]
	public partial class StudentsPage
	{
		private IList<StudentModel> students;

		[Inject]
		private ISessionStorageService SessionStorage { get; set; }

		[Inject]
		private IJSRuntime JSRuntime { get; set; }

		protected override async Task OnInitializedAsync()
		{
			string token = await SessionStorage.GetItemAsync<string>("token");

			CourseModel course = await new CourseService().GetCourseByCoordIdTaskAsync(token);

			students = await new StudentService().GetStudentsTaskAsync(token, course.CourseId);
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