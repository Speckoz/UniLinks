using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.Client.Site.Models.Auxiliary;
using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services
{
	public class DisciplinesPeriodsService
	{
		private readonly DisciplineService _disciplineService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public DisciplinesPeriodsService(DisciplineService disciplineService, IHttpContextAccessor httpContextAccessor)
		{
			_disciplineService = disciplineService;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<List<AuxDisciplines>> GetDisciplinesAsync()
		{
			string token = _httpContextAccessor.HttpContext.User.FindFirst("Token").Value;
			ResultModel<List<DisciplineVO>> disciplinesInCourse = await _disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

			var showedDisciplines = new List<AuxDisciplines>();
			var periods = new HashSet<int>();

			disciplinesInCourse.Object.ForEach(i => periods.Add(i.Period));

			foreach (int period in periods)
			{
				showedDisciplines.Add(new AuxDisciplines
				{
					Period = period,
					Disciplines = disciplinesInCourse.Object.Where(x => x.Period == period).Select(x => new AuxDiscipline()
					{
						DisciplineId = x.DisciplineId,
						Discipline = x.Name
					}).ToList()
				});
			}

			return showedDisciplines;
		}
	}
}