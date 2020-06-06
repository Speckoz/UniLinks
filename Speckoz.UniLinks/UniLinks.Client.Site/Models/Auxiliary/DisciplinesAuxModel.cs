using System;
using System.Collections.Generic;

namespace UniLinks.Client.Site.Models.Auxiliary
{
	public class AuxDiscipline
	{
		public Guid DisciplineId { get; set; }
		public string Discipline { get; set; }
	}

	public class AuxDisciplines
	{
		public int Period { get; set; }
		public List<AuxDiscipline> Disciplines { get; set; }
	}
}