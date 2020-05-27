using System.ComponentModel.DataAnnotations;

namespace UniLinks.API.Models.Auxiliary
{
    public class EmailFromBody
    {
        [Required]
        public string Email { get; set; }
    }
}