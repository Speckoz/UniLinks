using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class LessonBusiness : ILessonBusiness
	{
		private readonly ILessonRepository _classRepository;

		public LessonBusiness(ILessonRepository classRepository)
		{
			_classRepository = classRepository;
		}

		public async Task<LessonModel> AddTaskAsync(LessonModel lesson)
		{
			if (await _classRepository.FindByURITaskAsync(lesson.URI) is LessonModel)
				return null;

			return await _classRepository.AddTaskAsync(lesson);
		}

		public async Task<bool> DeleteTaskAsync(Guid classId)
		{
			if (await _classRepository.FindByIdTaskAsync(classId) is LessonModel lesson)
			{
				await _classRepository.DeleteTaskAsync(lesson);
				return true;
			}

			return default;
		}

		public async Task<LessonModel> FindByCourseTaskAsync(string course, byte period) =>
			await _classRepository.FindByCourseTaskAsync(course, period);

		public async Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum classShift) =>
			await _classRepository.FindByDateTaskAsync(dateTime, classShift);

		public async Task<LessonModel> FindByIdTaskAsync(Guid classId) =>
			await _classRepository.FindByIdTaskAsync(classId);

		public async Task<LessonModel> FindByURITaskAsync(string uri) =>
			await _classRepository.FindByURITaskAsync(uri);

		public async Task<LessonModel> UpdateTaskAsync(LessonModel newLesson)
		{
			if (await _classRepository.FindByIdTaskAsync(newLesson.LessonId) is LessonModel oldLesson)
				return await _classRepository.UpdateTaskAsync(oldLesson, newLesson);

			return default;
		}
	}
}