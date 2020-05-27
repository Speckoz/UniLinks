using System;
using System.Threading.Tasks;

using UniLinks.API.Models;

namespace UniLinks.API.Repository.Interfaces
{
    public interface IClassRepository
    {
        Task<ClassModel> AddTasAsync(ClassModel @class);

        Task<ClassModel> FindByClassIdTaskAsync(Guid classId);

        Task<ClassModel> FindByURITaskAsync(string uri);

        Task RemoveAsync(ClassModel @class);
    }
}