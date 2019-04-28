using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crowsoftmvc.Models
{
    public partial class BuildingImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBuildingImage { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Description { get; set; }

        [Display(Name = "Select Image Folder")]
        [StringLength(200)]
        [Required(ErrorMessage = "{0} is required.")]
        public string ImagePath { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Column("BuildingQuoteIdBuildingQuote")]
        [ForeignKey("BuildingQuote")]
        public int BuildingQuoteIdBuildingQuote { get; set; }

        [NotMapped]
        public virtual BuildingQuote BuildingQuoteIdBuildingQuoteNavigation { get; set; }
    }
}
