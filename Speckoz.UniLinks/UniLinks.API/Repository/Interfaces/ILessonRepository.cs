using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Repository.Interfaces
{
    public interface ILessonRepository
    {
        Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

        Task<LessonModel> FindByURITaskAsync(string uri);

        Task<List<LessonModel>> FindAllByDisciplinesIdTaskASync(List<Guid> disciplines);

        Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum lessonShift);

        Task<LessonModel> AddTaskAsync(LessonModel Lesson);

        Task<LessonModel> UpdateTaskAsync(LessonModel oldLesson, LessonModel newLesson);

        Task DeleteAsync(LessonModel lesson);
    }
}