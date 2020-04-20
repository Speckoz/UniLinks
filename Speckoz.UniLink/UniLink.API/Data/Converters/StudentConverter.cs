using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data.Converter;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
    public class StudentConverter : IParser<StudentVO, StudentModel>, IParser<StudentModel, StudentVO>
    {
        public StudentModel Parse(StudentVO origin)
        {
            if (origin == null) return new StudentModel();
            return new StudentModel
            {
                Course = origin.Course,
                Id = origin.Id,
                User = origin.User,
                UserId = origin.UserId
            };
        }

        public StudentVO Parse(StudentModel origin)
        {
            if (origin == null) return new StudentVO();
            return new StudentVO
            {
                Course = origin.Course,
                Id = origin.Id,
                User = origin.User,
                UserId = origin.UserId
            };
        }

        public List<StudentModel> ParseList(List<StudentVO> origin)
        {
            if (origin == null) return new List<StudentModel>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<StudentVO> ParseList(List<StudentModel> origin)
        {
            if (origin == null) return new List<StudentVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
