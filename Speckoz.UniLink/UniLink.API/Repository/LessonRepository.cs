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
    public class LessonRepository : BaseRepository, ILessonRepository
    {
        public LessonRepository(DataContext context) : base(context)
        {
        }

        public async Task<LessonModel> AddTaskAsync(LessonModel lesson)
        {
            LessonModel lessonAdded = (await _context.AddAsync(lesson)).Entity;
            await _context.SaveChangesAsync();
            return lessonAdded;
        }

        public Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum lessonShift) =>
            throw new NotImplementedException();

        public async Task<IList<LessonModel>> FindAllByDisciplinesIdTaskASync(IList<Guid> disciplines)
        {
            var lessons = new List<LessonModel>();
            foreach (Guid discipline in disciplines)
                lessons.AddRange(await _context.Lessons.Where(x => x.DisciplineId == discipline).ToListAsync());

            return lessons;
        }

        public async Task<LessonModel> FindByIdTaskAsync(Guid lessonId) =>
            await _context.Lessons.Where(c => c.LessonId == lessonId).FirstOrDefaultAsync();

        public async Task<LessonModel> FindByURITaskAsync(string uri) =>
            await _context.Lessons.Where(l => l.URI == uri).FirstOrDefaultAsync();

        public async Task<LessonModel> UpdateTaskAsync(LessonModel lesson, LessonModel newLesson)
        {
            _context.Entry(lesson).CurrentValues.SetValues(newLesson);
            await _context.SaveChangesAsync();
            return newLesson;
        }

        public async Task DeleteTaskAsync(LessonModel lesson)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }
    }
}