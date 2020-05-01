using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
    public interface IDisciplineRepository
    {
        Task<IList<DisciplineModel>> FindByRangeIdTaskAsync(IList<Guid> disciplines);

        Task<DisciplineModel> FindByIdTaskAsync(Guid disciplineId);

        Task<IList<DisciplineModel>> FindDisciplinesByCourseIdTaskAsync(Guid courseId);
    }
}