using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
	public class StudentModel
	{
		// o UserId nao pode ser PK, pois um usuario pode estar fazendo mais de um curso.
		public int Id { get; set; }

		[Required]
		public Guid UserId { get; set; }

		[Required]
		public CourseModel Course { get; set; }

		[Required]
		public string Email { get; set; }
	}
}