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

		public async Task<DisciplineModel> FindByIdTaskAsync(Guid disciplineId) =>
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
	}
}