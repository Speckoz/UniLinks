using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UniLinks.API.Data;
using UniLinks.API.Models;
using UniLinks.API.Repository.Interfaces;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.API.Repository
{
    public class CoordinatorRepository : BaseRepository, ICoordinatorRepository
    {
        public CoordinatorRepository(DataContext context) : base(context)
        {
        }

        public async Task<CoordinatorModel> FindUserByLoginTaskAsync(LoginRequestModel login) =>
            await _context.Coordinators.SingleOrDefaultAsync(x => x.Email.ToLower() == login.Email.ToLower() && x.Password == login.Password);
    }
}