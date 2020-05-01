using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business.Interfaces
{
    public interface ICoordinatorBusiness
    {
        Task<CoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login);
    }
}