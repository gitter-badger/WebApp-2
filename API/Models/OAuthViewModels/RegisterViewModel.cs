﻿using AiursoftBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.OAuthViewModels
{

    public class RegisterViewModel : IOAuthInfo
    {
        [Url]
        public virtual string ToRedirect { get; set; }
        public virtual string State { get; set; }
        public virtual string AppId { get; set; }
        public string ResponseType { get; set; }
        public string Scope { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string GetRegexRedirectUrl()
        {
            var url = new Uri(ToRedirect);
            string result = $@"{url.Scheme}://{url.Host}:{url.Port}{url.AbsolutePath}";
            return result;
        }
    }
}
