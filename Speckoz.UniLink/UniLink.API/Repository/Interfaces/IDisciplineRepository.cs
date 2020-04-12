using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	internal interface IDisciplineRepository
	{
		Task<DisciplineModel> FindByIdTaskAsync(Guid disciplineId);

		Task<DisciplineModel> FindByCourseTaskAsync(string course, byte period);

		Task<DisciplineModel> AddTaskAsync(DisciplineModel discipline);

		Task<DisciplineModel> UpdateTaskAsync(DisciplineModel discipline);

		Task<bool> DeleteTaskAsync(DisciplineModel discipline);
	}
}