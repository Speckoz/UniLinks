using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Models;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business
{
	public class ClassBusiness : IClassBusiness
	{
		private readonly IClassRepository _classRepository;
		private readonly ClassConverter _classConverter;

		public ClassBusiness(IClassRepository classRepository)
		{
			_classRepository = classRepository;
			_classConverter = new ClassConverter();
		}

		public async Task<ClassVO> AddTasAsync(ClassVO @class)
		{
			if (await _classRepository.FindByURITaskAsync(@class.URI) is null)
			{
				if (await _classRepository.AddTasAsync(_classConverter.Parse(@class)) is ClassModel addedClass)
					return _classConverter.Parse(addedClass);
			}

			return null;
		}

		public async Task<ClassVO> FindByClassIdTaskAsync(Guid classId) =>
			_classConverter.Parse(await _classRepository.FindByClassIdTaskAsync(classId));

		public async Task<ClassVO> FindByURITaskAsync(string uri) =>
			_classConverter.Parse(await _classRepository.FindByURITaskAsync(uri));

		public async Task RemoveAsync(ClassVO @class) =>
			await _classRepository.RemoveAsync(_classConverter.Parse(@class));
	}
}