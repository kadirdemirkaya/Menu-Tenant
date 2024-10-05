using Shared.Application.Filters.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Dtos.User
{
    public class UserUpdateModelDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [HashPassword]
        public string password { get; set; }

        [Required]
        [StringLength(10)]
        public string phoneNumber { get; set; }

    }
}
