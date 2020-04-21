using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
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

		public async Task<IList<DisciplineModel>> FindByRangeIdTaskAsync(IList<Guid> disciplines)
		{
			IList<DisciplineModel> disciplinesAux = new List<DisciplineModel>();
			foreach (Guid discipline in disciplines)
				disciplinesAux.Add(await _context.Disciplines.SingleOrDefaultAsync(x => x.DisciplineId == discipline));

			return disciplinesAux;
		}
	}
}