using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
	public class DisciplineRepository : BaseRepository, IDisciplineRepository
	{
		public DisciplineRepository(DataContext context) : base(context)
		{
		}

		public async Task<DisciplineModel> AddTaskAsync(DisciplineModel discipline)
		{
			DisciplineModel addedDiscipline = (await _context.AddAsync(discipline)).Entity;
			await _context.SaveChangesAsync();
			return addedDiscipline;
		}

		public async Task<bool> ExistsByNameAndCourseIdTaskAsync(string name, Guid courseId) =>
			await _context.Disciplines.AnyAsync(x => x.Name == name && x.CourseId == courseId);

		public async Task<bool> ExistsByDisciplineIdTaskAsync(Guid disciplineId) =>
			await _context.Disciplines.AnyAsync(x => x.DisciplineId == disciplineId);

		public async Task<DisciplineModel> FindByDisciplineIdTaskAsync(Guid disciplineId) =>
			await _context.Disciplines.SingleOrDefaultAsync(d => d.DisciplineId == disciplineId);

		public async Task<IList<DisciplineModel>> FindByRangeIdTaskAsync(IList<Guid> disciplines)
		{
			IList<DisciplineModel> disciplinesAux = new List<DisciplineModel>();
			foreach (Guid discipline in disciplines)
				disciplinesAux.Add(await _context.Disciplines.SingleOrDefaultAsync(x => x.DisciplineId == discipline));

			return disciplinesAux;
		}

		public async Task<IList<DisciplineModel>> FindDisciplinesByCourseIdTaskAsync(Guid courseId) =>
			await _context.Disciplines.Where(x => x.CourseId == courseId).ToListAsync();

		public async Task<DisciplineModel> UpdateTaskAsync(DisciplineModel discipline, DisciplineModel newDiscipline)
		{
			_context.Entry(discipline).CurrentValues.SetValues(newDiscipline);
			await _context.SaveChangesAsync();
			return newDiscipline;
		}

		public async Task DeleteTaskAsync(DisciplineModel discipline)
		{
			_context.Remove(discipline);
			await _context.SaveChangesAsync();
		}
	}
}