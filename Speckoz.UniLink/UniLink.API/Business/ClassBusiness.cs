using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
    public class ClassBusiness : IClassBusiness
    {
        private readonly IClassRepository _classRepository;
        public ClassBusiness(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<ClassModel> AddTaskAsync(ClassModel @class) =>
            await _classRepository.AddTaskAsync(@class);

        public async Task DeleteTaskAsync(Guid classId) =>
            await _classRepository.DeleteTaskAsync(classId);

        public async Task<ClassModel> FindByCourseTaskAsync(string course, byte period) =>
            await _classRepository.FindByCourseTaskAsync(course, period);

        public async Task<ClassModel> FindByDateTaskAsync(DateTime dateTime, ClassShiftEnum classShift) =>
            await _classRepository.FindByDateTaskAsync(dateTime, classShift);

        public async Task<ClassModel> FindByIdTaskAsync(Guid classId) =>
            await _classRepository.FindByIdTaskAsync(classId);

        public async Task<ClassModel> FindByURITaskAsync(string uri) =>
            await _classRepository.FindByURITaskAsync(uri);

        public async Task<ClassModel> UpdateTaskAsync(ClassModel @class) =>
            await _classRepository.UpdateTaskAsync(@class);
    }
}
