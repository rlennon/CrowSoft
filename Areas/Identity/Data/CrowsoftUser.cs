using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crowsoftmvc.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the crowsoftmvcUser class
    public class CrowsoftUser : IdentityUser
    {
        [Key]
        public override string Id { get => base.Id; set => base.Id = value; }

        [Display(Name = "User Name")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [HiddenInput(DisplayValue = false)]
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "{0} is required.")]
        public override string Email { get => base.Email; set => base.Email = value; }

        [HiddenInput(DisplayValue = false)]
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }

        [Display(Name = "Confirm Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "{0} is required.")]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        [HiddenInput(DisplayValue = false)]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [HiddenInput(DisplayValue = false)]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }

        [HiddenInput(DisplayValue = false)]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        [Display(Name = "Phone Number")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [HiddenInput(DisplayValue = false)]
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }

        [HiddenInput(DisplayValue = false)]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }

        [HiddenInput(DisplayValue = false)]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }

        [HiddenInput(DisplayValue = false)]
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }

        [HiddenInput(DisplayValue = false)]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
    }
}
