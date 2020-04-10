using System;
using System.ComponentModel.DataAnnotations;
using UniLink.API.Models.Enums;

namespace UniLink.API.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }
    }
}