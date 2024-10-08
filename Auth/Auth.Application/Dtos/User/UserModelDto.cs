﻿using Shared.Application.Filters.Attributes;
using Shared.Domain.Aggregates.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace Auth.Application.Dtos.User
{
    public class UserModelDto
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


        public UserModelDto UserModelDtoMapper(AppUser appUser)
        {
            username = appUser.Username;
            phoneNumber = appUser.PhoneNumber;
            email = appUser.Email;

            return this;
        }

    }
}
