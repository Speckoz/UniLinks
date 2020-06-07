using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Models;

namespace UniLinks.API.Repository.Interfaces
{
	public interface IDisciplineRepository
	{
		Task<DisciplineModel> AddTaskAsync(DisciplineModel disciplines);

		Task<bool> ExistsByNameAndCourseIdTaskAsync(string name, Guid courseId);

		Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId);

		Task<DisciplineModel> FindByDisciplineIdTaskAsync(Guid disciplineId);

		Task<List<DisciplineModel>> FindDisciplinesByCourseIdTaskAsync(Guid courseId);

		Task<DisciplineModel> UpdateTaskAsync(DisciplineModel discipline, DisciplineModel newDiscipline);

		Task DeleteTaskAsync(DisciplineModel discipline);
	}
}