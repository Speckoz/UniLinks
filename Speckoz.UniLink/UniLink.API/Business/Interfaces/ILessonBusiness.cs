using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Lesson;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Business.Interfaces
{
    public interface ILessonBusiness
    {
        Task<LessonVO> AddTaskAsync(LessonVO lesson);

        Task<LessonVO> FindByIdTaskAsync(Guid lessonId);

        Task<LessonVO> FindByURITaskAsync(string uri);

        Task<LessonVO> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum LessonShift);

        Task<IList<LessonDisciplineVO>> FindAllByDisciplinesIdTaskASync(string disciplines);

        Task<LessonVO> UpdateTaskAsync(LessonVO newLesson);

        Task DeleteAsync(Guid lessonId);
    }
}