using System.ComponentModel.DataAnnotations;

namespace Shared.Application.Models.Dtos.User
{
    public class CompanyModelDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string? databaseName { get; set; }
    }
}
