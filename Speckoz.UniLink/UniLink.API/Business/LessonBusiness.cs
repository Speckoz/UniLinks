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
		private readonly ILessonRepository _lessonRepository;

		public LessonBusiness(ILessonRepository lessonRepository)
		{
			_lessonRepository = lessonRepository;
		}

		public async Task<LessonModel> AddTaskAsync(LessonModel lesson)
		{
			if (await _lessonRepository.FindByURITaskAsync(lesson.URI) is LessonModel)
				return null;

			return await _lessonRepository.AddTaskAsync(lesson);
		}

		public async Task<LessonModel> FindByCourseTaskAsync(Guid courseId, byte period) =>
			await _lessonRepository.FindByCourseTaskAsync(courseId, period);

		public async Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum lessonShift) =>
			await _lessonRepository.FindByDateTaskAsync(dateTime, lessonShift);

		public async Task<LessonModel> FindByIdTaskAsync(Guid lessonId) =>
			await _lessonRepository.FindByIdTaskAsync(lessonId);

		public async Task<LessonModel> FindByURITaskAsync(string uri) =>
			await _lessonRepository.FindByURITaskAsync(uri);

		public async Task<LessonModel> UpdateTaskAsync(LessonModel newLesson)
		{
			if (await _lessonRepository.FindByIdTaskAsync(newLesson.LessonId) is LessonModel oldLesson)
				return await _lessonRepository.UpdateTaskAsync(oldLesson, newLesson);

			return default;
		}

		public async Task<bool> DeleteTaskAsync(Guid lessonId)
		{
			if (await _lessonRepository.FindByIdTaskAsync(lessonId) is LessonModel lesson)
			{
				await _lessonRepository.DeleteTaskAsync(lesson);
				return true;
			}

			return default;
		}
	}
}