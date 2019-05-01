using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crowsoftmvc.Models
{
    public class InputModelBuidlingFeatures
    {
        public string buildingQouteId { get; set; }
        public IEnumerable<BuildingFeatures> buildingFeaters { get; set; }
    }

    public partial class BuildingFeatures
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBuildingFeatures { get; set; }

        [Display(Name = "Description of feature")]
        [StringLength(100)]
        [Required(ErrorMessage = "Please provide a description of the feature")]
        public string FeatureDescription { get; set; }

        [Display(Name = "Comments?")]
        [StringLength(150)]
        public string Comments { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please provide a quantity")]
        [DisplayFormat(DataFormatString = "{0:n}")]
        public decimal Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Please provide the unit price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? TotalCost { get; set; }

        [ForeignKey("idBuildingQuote")]
        public int BuildingQuoteIdBuildingQuote { get; set; }

        [ForeignKey("idDefaultFeature")]
        public int DefaultFeatureIdDefaultFeature { get; set; }

        public virtual BuildingQuote BuildingQuoteIdBuildingQuoteNavigation { get; set; }
        public virtual DefaultFeature DefaultFeatureIdDefaultFeatureNavigation { get; set; }
    }
}
