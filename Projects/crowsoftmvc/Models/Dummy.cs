using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace crowsoftmvc.Models
{
    public partial class Dummy
    {
        [Key]
        public byte PersonId { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Address")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string City { get; set; }
    }
}
