using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
	public class DisciplineModel
	{
		[Key]
		public Guid DisciplineId { get; set; }

		[Required]
		public string Teacher { get; set; }

		[Required]
		public string Period { get; set; }
	}
}