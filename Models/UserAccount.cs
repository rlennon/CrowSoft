using crowsoftmvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace crowsoftmvc.Models
{
    public class UserAccount
    {
        [Key]
        public int idUserAccount { get; set; }
        
        [Display(Name = "Email Address")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "{0} is required.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(150)]
        public string CompanyName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(60)]
        [Required(ErrorMessage = "{0} is required.")]
        public string LastName { get; set; }

        [Display(Name = "Telephone No")]
        [StringLength(45)]
        [Required(ErrorMessage = "{0} is required.")]
        public string TelephoneNo { get; set; }

        [Display(Name = "Address Line")]
        [StringLength(150)]
        public string AddressLine { get; set; }

        [Display(Name = "Select County")]
        [StringLength(150)]
        public string County { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> CountiesList
        {
            get
            {
                return Counties.GetCounties();
            }

            set =>Counties.GetCounties();
        }

        [Display(Name = "Country")]
        [StringLength(150)]
        public string Country { get; set; } = "Ireland";

        [Display(Name = "Eir Code")]
        [StringLength(8)]
        public string EirCode { get; set; }

        public string AspNetUserID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string TypeUser { get; set; } = "Client";

        [HiddenInput(DisplayValue = false)]
        public Nullable<DateTime> DateCreated { get; set; } = System.DateTime.Now;
    }



}
