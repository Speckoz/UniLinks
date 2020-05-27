using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Enums;

namespace UniLinks.API.Business.Interfaces
{
	public interface ILessonBusiness
	{
		Task<LessonVO> AddTaskAsync(LessonVO lesson);

		Task<LessonVO> FindByIdTaskAsync(Guid lessonId);

		Task<LessonVO> FindByURITaskAsync(string uri);

		Task<LessonVO> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum LessonShift);

		Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson);

		Task<List<LessonDisciplineVO>> FindAllByDisciplinesIdTaskASync(string disciplines);

		Task<LessonVO> UpdateTaskAsync(LessonVO newLesson);

		Task DeleteAsync(Guid lessonId);
	}
}