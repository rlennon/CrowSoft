using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crowsoftmvc.Models
{
    public partial class BuildingQuote
    {
        public BuildingQuote()
        {
            BuildingFeatures = new HashSet<BuildingFeatures>();
            BuildingImage = new HashSet<BuildingImage>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBuildingQuote { get; set; }

        [Column("UserAccountIdUserAccount")]
        [ForeignKey("UserAccount")]
        [HiddenInput(DisplayValue = false)]
        public int UserAccountIdUserAccount { get; set; }

        [Display(Name = "Buidling Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Description { get; set; }

        [Display(Name = "Select a Measurement (Meters Only")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} is required.")]
        public string MeasurementType { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> MeasureMentsList
        {
            get
            {
                return Measurements.GetMeasurements();
            }

            set => Measurements.GetMeasurements();
        }

        [Display(Name = "Height (Mtrs)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Height { get; set; }

        [Display(Name = "Width (Mtrs)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Width { get; set; }

        [Display(Name = "Depth (Mtrs)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Depth { get; set; }

        [Display(Name = "Purpose of the Building")]
        [StringLength(150)]
        [Required(ErrorMessage = "{0} is required.")]
        public string PurposeOfBuilding { get; set; }

        [Display(Name = "Building Size (Sqr Meters)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? BuildingSize { get; set; }

        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal? TotalCost { get; set; }

        public DateTime? DateCreated { get; set; }

        public string Status { get; set; }

        [Display(Name = "Timeframe of Project (Months)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? TimeFrame { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int? UpdatedById { get; set; }

        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        public virtual UserAccount UserAccountIdUserAccountNavigation { get; set; }

       // [ForeignKey("BuildingQuote_idBuildingQuote")]
        public virtual ICollection<BuildingFeatures> BuildingFeatures { get; set; }

       // [ForeignKey("BuildingQuote_idBuildingQuote")]
        public virtual ICollection<BuildingImage> BuildingImage { get; set; }
    }
}
