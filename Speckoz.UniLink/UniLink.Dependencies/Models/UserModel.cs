using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using UniLink.Dependencies.Enums;

namespace UniLink.Dependencies.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [NotNull]
        public string Nome { get; set; }

        [NotNull]
        public UserTypeEnum UserType { get; set; }
    }
}