using System;
using System.Collections.Generic;

namespace crowsoftmvc.Models
{
    public partial class BuildingQuote
    {
        public BuildingQuote()
        {
            BuildingFeatures = new HashSet<BuildingFeatures>();
            BuildingImage = new HashSet<BuildingImage>();
        }

        public int IdBuildingQuote { get; set; }
        public int UserAccountIdUserAccount { get; set; }
        public string Description { get; set; }
        public string MeasurementType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public string PurposeOfBuilding { get; set; }
        public int? BuildingSize { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Status { get; set; }
        public int? TimeFrame { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int? UpdatedById { get; set; }

        public virtual UserAccount UserAccountIdUserAccountNavigation { get; set; }
        public virtual ICollection<BuildingFeatures> BuildingFeatures { get; set; }
        public virtual ICollection<BuildingImage> BuildingImage { get; set; }
    }
}
