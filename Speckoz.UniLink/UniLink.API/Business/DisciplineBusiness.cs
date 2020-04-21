using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class DisciplineBusiness : IDisciplineBusiness
	{
		private readonly DisciplineConverter _disciplineConverter;
		private readonly IDisciplineRepository _disciplineRepository;

		public DisciplineBusiness(IDisciplineRepository disciplineRepository)
		{
			_disciplineRepository = disciplineRepository;
			_disciplineConverter = new DisciplineConverter();
		}

		public async Task<IList<DisciplineVO>> FindDisciplinesTaskAsync(string disciplines)
		{
			//chegando se os guids estao no formato correto.
			var aux = new List<Guid>();
			foreach (string discipline in disciplines.Split(';'))
			{
				if (Guid.TryParse(discipline, out Guid result))
					aux.Add(result);
				else
					return default;
			}

			if (await _disciplineRepository.FindByRangeIdTaskAsync(aux) is IList<DisciplineModel> disc)
				return _disciplineConverter.ParseList(disc);

			return default;
		}
	}
}