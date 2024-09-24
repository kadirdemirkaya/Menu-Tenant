using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Dtos.User
{
    public class CompanyModelDto
    {
        [Required]
        public string name { get; set; }
        
        //[Required]
        //public string? databaseName { get; set; }
    }
}
