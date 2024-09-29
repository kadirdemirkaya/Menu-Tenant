using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Models.Menu;
using System.ComponentModel.DataAnnotations;

namespace Tenant.Application.Dtos
{
    public class CreateMenuModelDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
