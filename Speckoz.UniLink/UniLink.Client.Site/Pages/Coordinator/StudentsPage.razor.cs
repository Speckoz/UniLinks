using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLink.Client.Site.Services.Coordinator;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Pages.Coordinator
{
	[Authorizes(UserTypeEnum.Coordinator)]
	public partial class StudentsPage
	{
		private int selectedStudent = -1;
		private IList<StudentDisciplineVO> students;
		private IList<DisciplineVO> disciplines;
		private StudentVO newStudent = new StudentVO();

		[Inject]
		private StudentService StudentService { get; set; }

		[Inject]
		private CourseService CourseService { get; set; }

		[Inject]
		private DisciplineService DisciplineService { get; set; }

		[Inject]
		private IJSRuntime JSRuntime { get; set; }

		protected override async Task OnInitializedAsync()
		{
			students = await StudentService.GetStudentsTaskAsync((await CourseService.GetCourseByCoordIdTaskAsync()).CourseId);
			disciplines = await DisciplineService.GetDisciplinesByCoordIdTaskAsync();
		}

		private async Task AddStudentAsync()
		{
			if (await StudentService.AddStudentTaskAsync(newStudent) is StudentDisciplineVO student)
			{
				students.Add(student);
				await JSRuntime.InvokeVoidAsync("HideModal", "modalNewStudent");
				newStudent = new StudentVO();
			}
		}

		private async Task ViewDisciplinesAsync(IList<DisciplineVO> disciplines)
		{
			selectedStudent = students.IndexOf(students.Where(x => x.Disciplines.Equals(disciplines)).SingleOrDefault());
			await JSRuntime.InvokeVoidAsync("ShowModal", "modalStudentDisciplines");
		}

		private async Task RemoveStudentAsync(string nome)
		{
			await JSRuntime.InvokeVoidAsync("SendAlert", $"Voce removeu {nome}\n\nMintira");
		}

		private async Task EditStudentAsync(string nome)
		{
			await JSRuntime.InvokeVoidAsync("SendAlert", $"Voce editou {nome}\n\nMintira");
		}
	}
}