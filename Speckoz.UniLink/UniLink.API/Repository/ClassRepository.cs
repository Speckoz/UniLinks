using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
    public class ClassRepository : BaseRepository, IClassRepository
    {
        public ClassRepository(DataContext context) : base(context)
        {
        }

        public async Task<ClassModel> AddTaskAsync(ClassModel @class)
        {
            ClassModel newClass = (await _context.AddAsync(@class)).Entity;
            await _context.SaveChangesAsync();
            return newClass;
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            ClassModel @class = await _context.Classes.Where(c => c.ClassId == id).FirstAsync();
            if (@class != null)
            {
                _context.Classes.Remove(@class);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ClassModel> FindByCourseTaskAsync(string course, byte period) =>
            await _context.Classes
            .Include(c => c.Discipline)
            .Where(c => c.Discipline.Course == course && c.Discipline.Period == period).FirstAsync();

        public Task<ClassModel> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum classShift) =>
            throw new NotImplementedException();

        public async Task<ClassModel> FindByIdTaskAsync(Guid classId) =>
            await _context.Classes
            .Include(c => c.Discipline)
            .Where(c => c.ClassId == classId).FirstAsync();

        public async Task<ClassModel> FindByURITaskAsync(string uri) =>
            await _context.Classes
            .Include(c => c.Discipline)
            .Where(c => c.URI == uri).FirstAsync();

        public async Task<ClassModel> UpdateTaskAsync(ClassModel @class)
        {
            if (!await ExistsTaskAsync(@class.ClassId))
                return null;

            _context.Entry(await _context.Classes.Where(c => c.ClassId == @class.ClassId).FirstAsync())
                .CurrentValues.SetValues(@class);

            await _context.SaveChangesAsync();
            return @class;
        }

        private async Task<bool> ExistsTaskAsync(Guid id) =>
            await _context.Classes.AnyAsync(c => c.ClassId == id);
    }
}
