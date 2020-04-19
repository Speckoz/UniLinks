using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using RestSharp;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Attributes;
using UniLink.Client.Site.Services.Admin;
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
			IRestResponse resp = await new CourseService().GetCourseByCoordIdTaskAsync(token);
			var course = JsonSerializer.Deserialize<CourseModel>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			IRestResponse response = await new StudentService().GetStudentsTaskAsync(token, course.CourseId);

			if (response.StatusCode == HttpStatusCode.OK)
				students = JsonSerializer.Deserialize<List<StudentModel>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else if (response.StatusCode == HttpStatusCode.NotFound)
				students = new List<StudentModel>();
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