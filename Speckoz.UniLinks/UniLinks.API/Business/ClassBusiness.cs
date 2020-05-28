using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters;
using UniLinks.API.Models;
using UniLinks.API.Repository.Interfaces;
using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Business
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

		public async Task<List<ClassVO>> FindAllByCourseIdAndPeriodTaskAsync(Guid courseId, int period) =>
			_classConverter.ParseList(await _classRepository.FindAllByCourseIdAndPeriodTaskAsync(courseId, period));

		public async Task<List<ClassVO>> FindAllByCourseIdTaskAsync(Guid courseId) =>
			_classConverter.ParseList(await _classRepository.FindAllByCourseIdTaskAsync(courseId));

		public async Task<ClassVO> FindByURITaskAsync(string uri) =>
			_classConverter.Parse(await _classRepository.FindByURITaskAsync(uri));

		public async Task<ClassVO> UpdateTaskAsync(ClassVO newClass)
		{
			if (await _classRepository.FindByClassIdTaskAsync(newClass.ClassId) is ClassModel currentClass)
				return _classConverter.Parse(await _classRepository.UpdateTaskAsync(currentClass, _classConverter.Parse(newClass)));

			return null;
		}

		public async Task RemoveAsync(Guid classId)
		{
			if (await _classRepository.FindByClassIdTaskAsync(classId) is ClassModel classModel)
				await _classRepository.RemoveAsync(classModel);
		}
	}
}