using System;

namespace UniLink.Dependencies.Data.VO
{
    public class ClassVO
    {
        public Guid ClassId { get; set; }

        public Guid CourseId { get; set; }

        public string URI { get; set; }

        public byte Period { get; set; }
    }
}