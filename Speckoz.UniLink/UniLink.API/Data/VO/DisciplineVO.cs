﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.VO
{
    public class DisciplineVO
    {
		public Guid DisciplineId { get; set; }
		public string Name { get; set; }
		public string Teacher { get; set; }
		public byte Period { get; set; }
		public Guid CourseId { get; set; }
	}
}
