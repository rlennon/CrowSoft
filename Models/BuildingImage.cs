using System;
using System.Collections.Generic;

namespace crowsoftmvc.Models
{
    public partial class BuildingImage
    {
        public int IdBuildingImage { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int BuildingQuoteIdBuildingQuote { get; set; }

        public virtual BuildingQuote BuildingQuoteIdBuildingQuoteNavigation { get; set; }
    }
}
