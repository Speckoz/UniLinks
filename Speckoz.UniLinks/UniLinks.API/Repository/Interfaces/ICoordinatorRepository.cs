using System.Threading.Tasks;

using UniLinks.API.Models;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.API.Repository.Interfaces
{
    public interface ICoordinatorRepository
    {
        Task<CoordinatorModel> FindUserByLoginTaskAsync(LoginRequestModel login);
    }
}