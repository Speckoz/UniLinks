using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
    public interface ILessonRepository
    {
        Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

        Task<LessonModel> FindByURITaskAsync(string uri);

        Task<IList<LessonModel>> FindAllByDisciplinesIdTaskASync(IList<Guid> disciplines);

        Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum lessonShift);

        Task<LessonModel> AddTaskAsync(LessonModel Lesson);

        Task<LessonModel> UpdateTaskAsync(LessonModel oldLesson, LessonModel newLesson);

        Task DeleteAsync(LessonModel lesson);
    }
}