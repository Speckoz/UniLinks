using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Utils;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class LessonBusiness : ILessonBusiness
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly LessonConverter _converter;

		public LessonBusiness(ILessonRepository lessonRepository)
		{
			_lessonRepository = lessonRepository;
			_converter = new LessonConverter();
		}

		public async Task<LessonVO> AddTaskAsync(LessonVO lesson)
		{
			if (await _lessonRepository.FindByURITaskAsync(lesson.URI) is LessonModel)
				return null;

			LessonModel lessonEntity = _converter.Parse(lesson);
			lessonEntity = await _lessonRepository.AddTaskAsync(lessonEntity);
			return _converter.Parse(lessonEntity);
		}

		public async Task<LessonVO> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum lessonShift) =>
			_converter.Parse(await _lessonRepository.FindByDateTaskAsync(dateTime, lessonShift));

		public async Task<IList<LessonVO>> FindAllByDisciplinesIdTaskASync(string disciplines)
		{
			if (GuidFormat.TryParseList(disciplines, ';', out IList<Guid> result))
				if (await _lessonRepository.FindAllByDisciplinesIdTaskASync(result) is IList<LessonModel> lessons)
					return _converter.ParseList(lessons);

			return default;
		}

		public async Task<LessonVO> FindByIdTaskAsync(Guid lessonId) =>
			_converter.Parse(await _lessonRepository.FindByIdTaskAsync(lessonId));

		public async Task<LessonVO> FindByURITaskAsync(string uri) =>
			_converter.Parse(await _lessonRepository.FindByURITaskAsync(uri));

		public async Task<LessonVO> UpdateTaskAsync(LessonVO newLesson)
		{
			if (await _lessonRepository.FindByIdTaskAsync(newLesson.LessonId) is LessonModel oldLesson)
			{
				var lessonEntity = await _lessonRepository.UpdateTaskAsync(oldLesson, _converter.Parse(newLesson));
				return _converter.Parse(lessonEntity);
			}
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