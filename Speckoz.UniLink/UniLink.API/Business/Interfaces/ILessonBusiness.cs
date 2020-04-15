using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface ILessonBusiness
	{
		Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

		Task<LessonModel> FindByURITaskAsync(string uri);

		Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum LessonShift);

		Task<LessonModel> FindByCourseTaskAsync(string course, byte period);

		Task<LessonModel> AddTaskAsync(LessonModel lesson);

		Task<LessonModel> UpdateTaskAsync(LessonModel newLesson);

		Task<bool> DeleteTaskAsync(Guid lessonId);
	}
}