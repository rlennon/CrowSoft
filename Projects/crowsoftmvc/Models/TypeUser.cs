using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crowsoftmvc.Models
{
    public class TypeUser
    {
        [Display(Name = "Type User")]
        public string SelectedCounty { get; set; }
        public List<SelectListItem> CountiesList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Admin", Text = "Admin" },
            new SelectListItem { Value = "Client", Text = "Client" },
            new SelectListItem { Value = "User", Text = "User" },
        };
    }
}
