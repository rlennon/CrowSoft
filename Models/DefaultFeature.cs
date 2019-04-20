using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crowsoftmvc.Models
{
    public partial class DefaultFeature
    {
        public DefaultFeature()
        {
            BuildingFeatures = new HashSet<BuildingFeatures>();
        }

        [Key]
        public int IdDefaultFeature { get; set; }

        [Display(Name = "Description (E.g. Concrete Walls, Kitchen, Tiled Roof, etc.)")]
        [StringLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Description { get; set; }

        [Display(Name = "Select a Measurement")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Measurement { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> MeasureMentsList
        {
            get
            {
                return Measurements.GetMeasurements();
            }

            set => Measurements.GetMeasurements();
        }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} is required.")]
        public decimal UnitPrice { get; set; }

        [UIHint("ByteCheckbox")]
        public sbyte? IsDefaultFeature { get; set; }
        [NotMapped] //this is key to prevent EntityFramework from trying to match against a database field
        public bool ActiveBool
        {
            get { return IsDefaultFeature > 0; }
            //store values in the Active value
            set { IsDefaultFeature = value ? Convert.ToSByte(1) : Convert.ToSByte(0); }
        }

        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        public virtual ICollection<BuildingFeatures> BuildingFeatures { get; set; }
    }
}
