using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters;
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
		private readonly IDisciplineBusiness _disciplineBusiness;
		private readonly CollabAPIService _collabAPIService;
		private readonly LessonConverter _lessonConverter;
		private readonly LessonDisciplineConverter _lessonDisciplineConverter;
		private readonly DisciplineConverter _disciplineConverter;

		public LessonBusiness(ILessonRepository lessonRepository, IDisciplineBusiness disciplineBusiness, CollabAPIService collabAPIService)
		{
			_lessonRepository = lessonRepository;
			_disciplineBusiness = disciplineBusiness;
			_collabAPIService = collabAPIService;
			_lessonConverter = new LessonConverter();
			_lessonDisciplineConverter = new LessonDisciplineConverter();
			_disciplineConverter = new DisciplineConverter();
		}

		public async Task<LessonVO> AddTaskAsync(LessonVO lessonCollab)
		{
			if (!(await _lessonRepository.AddTaskAsync(_lessonConverter.Parse(lessonCollab)) is LessonModel lessonModel))
				return null;

			return _lessonConverter.Parse(lessonModel);
		}

		public async Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson) =>
			await _collabAPIService.GetRecordingInfoTaskAsync(lesson);

		public async Task<List<LessonDisciplineVO>> FindAllByRangeDisciplinesIdTaskASync(List<Guid> disciplines)
		{
			if (!(await _disciplineBusiness.FindAllByDisciplineIdsTaskAsync(disciplines) is List<DisciplineVO> listDisciplines))
				return null;
			if (!(await _lessonRepository.FindAllByRangeDisciplinesIdTaskASync(disciplines) is List<LessonModel> listLessons))
				return null;

			List<DisciplineModel> disciplineModels = _disciplineConverter.ParseList(listDisciplines);

			var lessonDisciplines = new List<(LessonModel, DisciplineModel)>();

			foreach (LessonModel l in listLessons)
				lessonDisciplines.Add((l, disciplineModels.Where(x => x.DisciplineId == l.DisciplineId).SingleOrDefault()));

			return _lessonDisciplineConverter.ParseList(lessonDisciplines);
		}

		public async Task<LessonVO> FindByIdTaskAsync(Guid lessonId) =>
			_lessonConverter.Parse(await _lessonRepository.FindByIdTaskAsync(lessonId));

		public async Task<LessonVO> FindByURITaskAsync(string uri) =>
			_lessonConverter.Parse(await _lessonRepository.FindByURITaskAsync(uri));

		public async Task<LessonVO> UpdateTaskAsync(LessonVO newLesson)
		{
			if (!(await _lessonRepository.FindByIdTaskAsync(newLesson.LessonId) is LessonModel oldLesson))
				return null;

			return _lessonConverter.Parse(await _lessonRepository.UpdateTaskAsync(oldLesson, _lessonConverter.Parse(newLesson)));
		}

		public async Task DeleteAsync(Guid lessonId)
		{
			if (await _lessonRepository.FindByIdTaskAsync(lessonId) is LessonModel lesson)
				await _lessonRepository.DeleteAsync(lesson);
		}
	}
}