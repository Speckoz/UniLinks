﻿using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class CourseBusiness : ICourseBusiness
	{
		private readonly ICourseRepository _courseRepository;
		private readonly CourseConverter _courseConverter;

		public CourseBusiness(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
			_courseConverter = new CourseConverter();
		}

		public async Task<CourseVO> AddTaskAsync(CourseVO course)
		{
			if (await _courseRepository.AddTaskAsync(_courseConverter.Parse(course)) is CourseModel courseModel)
				return _courseConverter.Parse(courseModel);

			return null;
		}

		public async Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId) =>
			_courseConverter.Parse(await _courseRepository.FindByCoordIdTaskAsync(coordId));

		public async Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId) =>
			await _courseRepository.ExistsCoordInCourseTaskAsync(coordId, courseId);

		public async Task<CourseVO> FindByCourseIdTaskAsync(Guid courseId)
		{
			if (await _courseRepository.FindByCourseIdTaskAsync(courseId) is CourseModel course)
				return _courseConverter.Parse(course);

			return null;
		}

		public async Task<CourseVO> UpdateTaskAsync(CourseVO course, CourseVO newCourse)
		{
			if (await _courseRepository.UpdateTaskAsync(_courseConverter.Parse(course), _courseConverter.Parse(newCourse)) is CourseModel courseModel)
				return _courseConverter.Parse(courseModel);

			return null;
		}

		public async Task DeleteAsync(Guid courseId)
		{
			if (await _courseRepository.FindByCourseIdTaskAsync(courseId) is CourseModel courseModel)
				await _courseRepository.DeleteAsync(courseModel);
		}
	}
}