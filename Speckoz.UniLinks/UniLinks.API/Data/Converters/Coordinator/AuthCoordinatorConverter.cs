using System;
using System.Collections.Generic;

using UniLinks.API.Data.Converters.Interfaces;
using UniLinks.API.Models;
using UniLinks.Dependencies.Data.VO.Coordinator;

namespace UniLinks.API.Data.Converters.Coordinator
{
	public class AuthCoordinatorConverter : IParser<CoordinatorModel, AuthCoordinatorVO>
	{
		public AuthCoordinatorVO Parse(CoordinatorModel origin)
		{
			if (origin is null)
				return null;

			return new AuthCoordinatorVO
			{
				CoordinatorId = origin.CoordinatorId,
				Email = origin.Email,
				Name = origin.Name,
				CourseId = origin.CourseId
			};
		}

		public List<AuthCoordinatorVO> ParseList(List<CoordinatorModel> origin) => throw new NotImplementedException();
	}
}