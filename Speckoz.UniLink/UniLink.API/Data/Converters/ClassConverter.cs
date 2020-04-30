using System.Collections.Generic;
using System.Linq;

using UniLink.API.Data.Converters.Interfaces;
using UniLink.API.Models;
using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Data.Converters
{
	public class ClassConverter : IParser<ClassModel, ClassVO>, IParser<ClassVO, ClassModel>
	{
		public ClassModel Parse(ClassVO origin)
		{
			if (origin is null)
				return new ClassModel();

			return new ClassModel
			{
				ClassId = origin.ClassId,
				CourseId = origin.CourseId,
				Period = origin.Period,
				URI = origin.URI
			};
		}

		public ClassVO Parse(ClassModel origin)
		{
			if (origin is null)
				return new ClassVO();

			return new ClassVO
			{
				ClassId = origin.ClassId,
				CourseId = origin.CourseId,
				Period = origin.Period,
				URI = origin.URI
			};
		}

		public IList<ClassModel> ParseList(IList<ClassVO> origin)
		{
			if (origin is null)
				return new List<ClassModel>();

			return origin.Select(x => Parse(x)).ToList();
		}

		public IList<ClassVO> ParseList(IList<ClassModel> origin)
		{
			if (origin is null)
				return new List<ClassVO>();

			return origin.Select(x => Parse(x)).ToList();
		}
	}
}