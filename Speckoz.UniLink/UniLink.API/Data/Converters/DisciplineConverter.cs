using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data.Converter;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
    public class DisciplineConverter : IParser<DisciplineVO, DisciplineModel>, IParser<DisciplineModel, DisciplineVO>
    {
        public DisciplineModel Parse(DisciplineVO origin)
        {
            if (origin == null) return new DisciplineModel();
            return new DisciplineModel
            {
                DisciplineId = origin.DisciplineId,
                Name = origin.Name,
                CourseId = origin.CourseId,
                Period = origin.Period,
                Teacher = origin.Teacher
            };
        }

        public DisciplineVO Parse(DisciplineModel origin)
        {
            if (origin == null) return new DisciplineVO();
            return new DisciplineVO
            {
                DisciplineId = origin.DisciplineId,
                Name = origin.Name,
                CourseId = origin.CourseId,
                Period = origin.Period,
                Teacher = origin.Teacher
            };
        }

        public List<DisciplineModel> ParseList(List<DisciplineVO> origin)
        {
            if (origin == null) return new List<DisciplineModel>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DisciplineVO> ParseList(List<DisciplineModel> origin)
        {
            if (origin == null) return new List<DisciplineVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
