using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;

namespace UniLinks.API.Business.Interfaces
{
	public interface ILessonBusiness
	{
		Task<LessonVO> AddTaskAsync(LessonVO lesson);

		Task<LessonVO> FindByIdTaskAsync(Guid lessonId);

		Task<int> FindCountByCourseIdTaskAsync(Guid courseId);

		Task<List<LessonDisciplineVO>> FindFiveLastLessonsByCourseIdTaskAsync(Guid courseId);

		Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId);

		Task<LessonVO> FindByURITaskAsync(string uri);

		Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson);

		Task<List<LessonDisciplineVO>> FindAllByRangeDisciplinesIdTaskASync(List<Guid> disciplines);

		Task<LessonVO> UpdateTaskAsync(LessonVO newLesson);

		Task DeleteAsync(Guid lessonId);
	}
}