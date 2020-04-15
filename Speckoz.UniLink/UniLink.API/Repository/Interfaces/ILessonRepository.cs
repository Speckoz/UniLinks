using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface ILessonRepository
	{
		Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

		Task<LessonModel> FindByURITaskAsync(string uri);

		Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum lessonShift);

		Task<LessonModel> FindByCourseTaskAsync(string course, byte period);

		Task<LessonModel> AddTaskAsync(LessonModel Lesson);

		Task<LessonModel> UpdateTaskAsync(LessonModel oldLesson, LessonModel newLesson);

		Task DeleteTaskAsync(LessonModel lesson);
	}
}