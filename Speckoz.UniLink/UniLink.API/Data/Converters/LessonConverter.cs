using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Data.Converter;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.Converters
{
    public class LessonConverter : IParser<LessonVO, LessonModel>, IParser<LessonModel, LessonVO>
    {
        public LessonModel Parse(LessonVO origin)
        {
            if (origin == null) return new LessonModel();
            return new LessonModel
            {
                Date = origin.Date,
                Discipline = origin.Discipline,
                LessonId = origin.LessonId,
                LessonSubject = origin.LessonSubject,
                URI = origin.URI
            };
        }

        public LessonVO Parse(LessonModel origin)
        {
            if (origin == null) return new LessonVO();
            return new LessonVO
            {
                Date = origin.Date,
                Discipline = origin.Discipline,
                LessonId = origin.LessonId,
                LessonSubject = origin.LessonSubject,
                URI = origin.URI
            };
        }

        public List<LessonModel> ParseList(List<LessonVO> origin)
        {
            if (origin == null) return new List<LessonModel>();
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<LessonVO> ParseList(List<LessonModel> origin)
        {
            if (origin == null) return new List<LessonVO>();
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
