using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Models;

namespace UniLinks.API.Repository.Interfaces
{
	public interface ILessonRepository
	{
		Task<LessonModel> FindByIdTaskAsync(Guid lessonId);

		Task<LessonModel> FindByURITaskAsync(string uri);

		Task<int> FindCountByCourseIdTaskAsync(Guid courseId);

		Task<List<LessonModel>> FindFiveLastLessonsByCourseIdTaskAsync(Guid courseId);

		Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId);

		Task<List<LessonModel>> FindAllByRangeDisciplineIdsTaskASync(List<Guid> disciplines);

		Task<LessonModel> AddTaskAsync(LessonModel Lesson);

		Task<LessonModel> UpdateTaskAsync(LessonModel oldLesson, LessonModel newLesson);

		Task DeleteAsync(LessonModel lesson);
	}
}