using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.API.Data;
using UniLinks.API.Models;
using UniLinks.API.Repository.Interfaces;

namespace UniLinks.API.Repository
{
	public class ClassRepository : BaseRepository, IClassRepository
	{
		public ClassRepository(DataContext context) : base(context)
		{
		}

		public async Task<ClassModel> AddTasAsync(ClassModel @class)
		{
			ClassModel addedClass = (await _context.Classes.AddAsync(@class)).Entity;
			await _context.SaveChangesAsync();
			return addedClass;
		}

		public async Task<ClassModel> FindByClassIdTaskAsync(Guid classId) =>
			await _context.Classes.SingleOrDefaultAsync(x => x.ClassId == classId);

		public async Task<List<ClassModel>> FindAllByCourseIdAndPeriodTaskAsync(Guid courseId, int period) =>
			await _context.Classes.Where(x => x.CourseId == courseId && x.Period == period).ToListAsync();

		public async Task<ClassModel> FindByURITaskAsync(string uri) =>
			await _context.Classes.SingleOrDefaultAsync(x => x.URI == uri);

		public async Task RemoveAsync(ClassModel @class)
		{
			_context.Classes.Remove(@class);
			await _context.SaveChangesAsync();
		}
	}
}