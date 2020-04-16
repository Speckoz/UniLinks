using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface ILessonBusiness
	{
		Task<LessonModel> AddTaskAsync(LessonModel lesson);

		Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

		Task<LessonModel> FindByURITaskAsync(string uri);

		Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum LessonShift);

		Task<LessonModel> FindByCourseTaskAsync(Guid courseId, byte period);

		Task<LessonModel> UpdateTaskAsync(LessonModel newLesson);

		Task<bool> DeleteTaskAsync(Guid lessonId);
	}
}