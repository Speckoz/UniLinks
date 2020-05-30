using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters.Lesson;
using UniLinks.API.Repository.Interfaces;
using UniLinks.API.Services;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Business
{
	public class LessonBusiness : ILessonBusiness
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly IDisciplineRepository _disciplineRepository;
		private readonly CollabAPIService _collabAPIService;
		private readonly LessonConverter _lessonConverter;
		private readonly LessonDisciplineConverter _lessonDisciplineConverter;

		public LessonBusiness(ILessonRepository lessonRepository, IDisciplineRepository disciplineRepository, CollabAPIService collabAPIService)
		{
			_lessonRepository = lessonRepository;
			_disciplineRepository = disciplineRepository;
			_collabAPIService = collabAPIService;
			_lessonConverter = new LessonConverter();
			_lessonDisciplineConverter = new LessonDisciplineConverter();
		}

		public async Task<LessonVO> AddTaskAsync(LessonVO lessonCollab)
		{
			LessonModel lessonEntity = _lessonConverter.Parse(lessonCollab);
			lessonEntity = await _lessonRepository.AddTaskAsync(lessonEntity);

			return _lessonConverter.Parse(lessonEntity);
		}

		public async Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson) =>
			await _collabAPIService.GetRecordingInfoTaskAsync(lesson);

		public async Task<List<LessonDisciplineVO>> FindAllByRangeDisciplinesIdTaskASync(List<Guid> disciplines)
		{
			if (!(await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(disciplines) is List<DisciplineModel> listDisciplines))
				return null;
			if (!(await _lessonRepository.FindAllByRangeDisciplinesIdTaskASync(disciplines) is List<LessonModel> listLessons))
				return null;

			var lessonDisciplines = new List<(LessonModel, DisciplineModel)>();

			foreach (LessonModel l in listLessons)
				lessonDisciplines.Add((l, listDisciplines.Where(x => x.DisciplineId == l.DisciplineId).SingleOrDefault()));

			return _lessonDisciplineConverter.ParseList(lessonDisciplines);
		}

		public async Task<LessonVO> FindByIdTaskAsync(Guid lessonId) =>
			_lessonConverter.Parse(await _lessonRepository.FindByIdTaskAsync(lessonId));

		public async Task<LessonVO> FindByURITaskAsync(string uri) =>
			_lessonConverter.Parse(await _lessonRepository.FindByURITaskAsync(uri));

		public async Task<LessonVO> UpdateTaskAsync(LessonVO newLesson)
		{
			if (await _lessonRepository.FindByIdTaskAsync(newLesson.LessonId) is LessonModel oldLesson)
			{
				var lessonEntity = await _lessonRepository.UpdateTaskAsync(oldLesson, _lessonConverter.Parse(newLesson));
				return _lessonConverter.Parse(lessonEntity);
			}
			return default;
		}

		public async Task DeleteAsync(Guid lessonId)
		{
			if (await _lessonRepository.FindByIdTaskAsync(lessonId) is LessonModel lesson)
			{
				await _lessonRepository.DeleteAsync(lesson);
			}
		}
	}
}