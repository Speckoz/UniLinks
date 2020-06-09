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

		Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId);

		Task<List<LessonModel>> FindAllByRangeDisciplinesIdTaskASync(List<Guid> disciplines);

		Task<LessonModel> AddTaskAsync(LessonModel Lesson);

		Task<LessonModel> UpdateTaskAsync(LessonModel oldLesson, LessonModel newLesson);

		Task DeleteAsync(LessonModel lesson);
	}
}