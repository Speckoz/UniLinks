using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
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
        private List<Guid> selectedDisciplines;
        private List<StudentDisciplineVO> students;
        private List<DisciplineVO> disciplines;
        private StudentVO newStudent = new StudentVO();
        private string show = "collapse";

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
            newStudent = new StudentVO() { Disciplines = new List<DisciplineVO>() };
        }

        private async Task AddStudentAsync()
        {
            newStudent.Disciplines = selectedDisciplines.Select(x => new DisciplineVO() { DisciplineId = x }).ToList();
            if (await StudentService.AddStudentTaskAsync(newStudent) is StudentDisciplineVO student)
            {
                students.Add(student);
                await JSRuntime.InvokeVoidAsync("HideModal", "modalNewStudent");
                newStudent = new StudentVO() { Disciplines = new List<DisciplineVO>() };
                show = nameof(show);
            }
        }

        private async Task ViewDisciplinesAsync(IList<DisciplineVO> disciplines)
        {
            selectedStudent = students.IndexOf(students.Where(x => x.Disciplines.Equals(disciplines)).SingleOrDefault());
            await JSRuntime.InvokeVoidAsync("ShowModal", "modalStudentDisciplines");
        }

        private async Task RemoveStudentAsync(Guid studentId)
        {
            if (await StudentService.RemoveStudentTaskAsync(studentId))
                if (students.SingleOrDefault(x => x.StudentId == studentId) is StudentDisciplineVO student)
                    students.Remove(student);
        }

        private async Task EditStudentAsync(string nome)
        {
            await JSRuntime.InvokeVoidAsync("SendAlert", $"Voce editou {nome}\n\nMintira");
        }

        private void HideAlert() => show = "collapse";
    }
}