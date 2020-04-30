using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Models;
using UniLink.API.Repository.Interfaces;

namespace UniLink.API.Repository
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

		public async Task<ClassModel> FindByURITaskAsync(string uri) => 
			await _context.Classes.SingleOrDefaultAsync(x => x.URI == uri);

		public async Task RemoveAsync(ClassModel @class)
		{
			_context.Classes.Remove(@class);
			await _context.SaveChangesAsync();
		}
	}
}