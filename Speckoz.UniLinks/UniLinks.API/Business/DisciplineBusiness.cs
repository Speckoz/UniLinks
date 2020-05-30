using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters;
using UniLinks.API.Repository.Interfaces;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Business
{
	public class DisciplineBusiness : IDisciplineBusiness
	{
		private readonly DisciplineConverter _disciplineConverter;
		private readonly IDisciplineRepository _disciplineRepository;

		public DisciplineBusiness(IDisciplineRepository disciplineRepository)
		{
			_disciplineRepository = disciplineRepository;
			_disciplineConverter = new DisciplineConverter();
		}

		public async Task<DisciplineVO> AddTaskAsync(DisciplineVO discipline)
		{
			if (await _disciplineRepository.AddTaskAsync(_disciplineConverter.Parse(discipline)) is DisciplineModel addedDisci)
				return _disciplineConverter.Parse(addedDisci);

			return null;
		}

		public async Task<bool> ExistsByNameAndCourseIdTaskAsync(string name, Guid courseId) =>
			await _disciplineRepository.ExistsByNameAndCourseIdTaskAsync(name, courseId);

		public async Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId) =>
			await _disciplineRepository.ExistsByDisciplineIdTaskAsync(disciplineId);

		public async Task<DisciplineVO> FindByDisciplineIdTaskAsync(Guid disciplineId)
		{
			if (await _disciplineRepository.FindByDisciplineIdTaskAsync(disciplineId) is DisciplineModel discipline)
				return _disciplineConverter.Parse(discipline);

			return null;
		}

		public async Task<List<DisciplineVO>> FindByCourseIdTaskAsync(Guid courseId) =>
			_disciplineConverter.ParseList(await _disciplineRepository.FindDisciplinesByCourseIdTaskAsync(courseId));

		public async Task<List<DisciplineVO>> FindAllByDisciplineIdsTaskAsync(List<Guid> disciplines)
		{
			if (await _disciplineRepository.FindAllByRangeDisciplinesIdTaskASync(disciplines) is List<DisciplineModel> disc)
				if (!disc.Contains(null))
					return _disciplineConverter.ParseList(disc);

			return null;
		}

		public async Task<DisciplineVO> UpdateTaskAync(DisciplineVO newDiscipline)
		{
			if (await _disciplineRepository.FindByDisciplineIdTaskAsync(newDiscipline.DisciplineId) is DisciplineModel discipline)
				return _disciplineConverter.Parse(await _disciplineRepository.UpdateTaskAsync(discipline, _disciplineConverter.Parse(newDiscipline)));

			return null;
		}

		public async Task DeleteTaskAsync(Guid disciplineId)
		{
			if (await _disciplineRepository.FindByDisciplineIdTaskAsync(disciplineId) is DisciplineModel disc)
				await _disciplineRepository.DeleteTaskAsync(disc);
		}
	}
}