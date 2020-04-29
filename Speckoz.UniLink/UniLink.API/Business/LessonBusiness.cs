using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters.Lesson;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Utils;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Lesson;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class LessonBusiness : ILessonBusiness
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly IDisciplineRepository _disciplineRepository;
		private readonly LessonConverter _lessonConverter;
		private readonly LessonDisciplineConverter _lessonDisciplineConverter;

		public LessonBusiness(ILessonRepository lessonRepository, IDisciplineRepository disciplineRepository)
		{
			_lessonRepository = lessonRepository;
			_disciplineRepository = disciplineRepository;
			_lessonConverter = new LessonConverter();
			_lessonDisciplineConverter = new LessonDisciplineConverter();
		}

		public async Task<LessonVO> AddTaskAsync(LessonVO lesson)
		{
			if (await _lessonRepository.FindByURITaskAsync(lesson.URI) is LessonModel)
				return null;

			LessonModel lessonEntity = _lessonConverter.Parse(lesson);
			lessonEntity = await _lessonRepository.AddTaskAsync(lessonEntity);
			return _lessonConverter.Parse(lessonEntity);
		}

		public async Task<LessonVO> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum lessonShift) =>
			_lessonConverter.Parse(await _lessonRepository.FindByDateTaskAsync(dateTime, lessonShift));

		public async Task<IList<LessonDisciplineVO>> FindAllByDisciplinesIdTaskASync(string disciplines)
		{
			if (GuidFormat.TryParseList(disciplines, ';', out IList<Guid> result))
			{
				IList<DisciplineModel> discipline = await _disciplineRepository.FindByRangeIdTaskAsync(result);
				IList<LessonModel> lesson = await _lessonRepository.FindAllByDisciplinesIdTaskASync(result);

				var lessonDisciplines = new List<(LessonModel, DisciplineModel)>();

				foreach (LessonModel l in lesson)
					lessonDisciplines.Add((l, discipline.Where(x => x.DisciplineId == l.DisciplineId).SingleOrDefault()));

				return _lessonDisciplineConverter.ParseList(lessonDisciplines);
			}

			return default;
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