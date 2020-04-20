using System;
using System.Threading.Tasks;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface ILessonBusiness
	{
		Task<LessonVO> AddTaskAsync(LessonVO lesson);

		Task<LessonVO> FindByIdTaskAsync(Guid lessonId);

		Task<LessonVO> FindByURITaskAsync(string uri);

		Task<LessonVO> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum LessonShift);

		Task<LessonVO> FindByCourseTaskAsync(Guid courseId, byte period);

		Task<LessonVO> UpdateTaskAsync(LessonVO newLesson);

		Task<bool> DeleteTaskAsync(Guid lessonId);
	}
}