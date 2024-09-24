using Shared.Application.Filters.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Dtos.User
{
    public class UserLoginModelDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [HashPassword]
        public string Password { get; set; }

        public string? CompanyName { get; set; }
    }
}
