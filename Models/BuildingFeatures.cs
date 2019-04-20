using System;
using System.Collections.Generic;

namespace crowsoftmvc.Models
{
    public partial class BuildingFeatures
    {
        public int IdBuildingFeatures { get; set; }
        public string FeatureDescription { get; set; }
        public string Comments { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalCost { get; set; }
        public int BuildingQuoteIdBuildingQuote { get; set; }
        public int DefaultFeatureIdDefaultFeature { get; set; }

        public virtual BuildingQuote BuildingQuoteIdBuildingQuoteNavigation { get; set; }
        public virtual DefaultFeature DefaultFeatureIdDefaultFeatureNavigation { get; set; }
    }
}
