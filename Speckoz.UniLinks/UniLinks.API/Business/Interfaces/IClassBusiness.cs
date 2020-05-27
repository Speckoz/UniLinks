using System;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Business.Interfaces
{
    public interface IClassBusiness
    {
        Task<ClassVO> AddTasAsync(ClassVO @class);

        Task<ClassVO> FindByClassIdTaskAsync(Guid classId);

        Task<ClassVO> FindByURITaskAsync(string uri);

        Task RemoveAsync(ClassVO @class);
    }
}