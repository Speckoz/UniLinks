using System;
using System.Collections.Generic;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.API.Models;
using UniLink.Dependencies.Data.VO.Coordinator;

namespace UniLink.API.Data.Converters.Coordinator
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
				Name = origin.Name
			};
		}

		public List<AuthCoordinatorVO> ParseList(List<CoordinatorModel> origin) => throw new NotImplementedException();
	}
}