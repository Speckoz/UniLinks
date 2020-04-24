using System;
using System.Collections.Generic;
using System.Text;

namespace UniLink.Dependencies.Data.VO.Student
{
    public class StudentDisciplineVO
    {
        public StudentVO Student { get; set; }
        public IList<DisciplineVO> Disciplines { get; set; }
    }
}
