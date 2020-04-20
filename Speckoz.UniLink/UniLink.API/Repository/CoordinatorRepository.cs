using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Models;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Repository
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