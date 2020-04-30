using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Models;
using UniLink.API.Repository.Interfaces;

namespace UniLink.API.Business
{
	public class ClassBusiness : IClassBusiness
	{
		private readonly IClassRepository _classRepository;

		public ClassBusiness(IClassRepository classRepository)
		{
			_classRepository = classRepository;
		}

		public async Task<ClassModel> AddTasAsync(ClassModel @class)
		{
			if (await _classRepository.FindByURITaskAsync(@class.URI) is null)
			{
				if (await _classRepository.AddTasAsync(@class) is ClassModel addedClass)
					return addedClass;
			}

			return null;
		}

		public async Task<ClassModel> FindByClassIdTaskAsync(Guid classId) =>
			await _classRepository.FindByClassIdTaskAsync(classId);

		public async Task RemoveAsync(ClassModel @class) =>
			await _classRepository.RemoveAsync(@class);
	}
}