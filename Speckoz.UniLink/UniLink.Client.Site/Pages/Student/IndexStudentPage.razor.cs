﻿using Microsoft.AspNetCore.Components;

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
        private byte filterSelected = 1;

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
        private LessonService LessonService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            lessons = lessonOrigin = (await LessonService.GetAllLessonsTaskAync()).OrderBy(x => x.Lesson.Date).ToList();
            selected = lessonOrigin[0];
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