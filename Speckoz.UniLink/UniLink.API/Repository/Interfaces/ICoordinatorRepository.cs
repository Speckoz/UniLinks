using System.Threading.Tasks;

using UniLink.API.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Repository.Interfaces
{
    public interface ICoordinatorRepository
    {
        Task<CoordinatorModel> FindUserByLoginTaskAsync(LoginRequestModel login);
    }
}