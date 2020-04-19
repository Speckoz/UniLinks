﻿using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface ICourseRepository
	{
		Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId);
	}
}