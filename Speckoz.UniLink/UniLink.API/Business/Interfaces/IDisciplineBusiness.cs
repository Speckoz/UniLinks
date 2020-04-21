using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business.Interfaces
{
	public interface IDisciplineBusiness
	{
		Task<IList<DisciplineVO>> FindDisciplinesTaskAsync(string disciplines);
	}
}