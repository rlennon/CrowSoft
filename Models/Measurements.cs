using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crowsoftmvc.Models
{
    public class Measurements
    {
        public static IEnumerable<SelectListItem> GetMeasurements()
        {
            IEnumerable<SelectListItem> MeasurementList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Qty", Text = "Qty" },
                new SelectListItem { Value = "Time", Text = "Time" },
                new SelectListItem { Value = "SqrMeters", Text = "Sqr Meters" },
                new SelectListItem { Value = "Meter", Text = "Meters" },
                new SelectListItem { Value = "Hour", Text = "Per Hour" },
                new SelectListItem { Value = "Kg", Text = "Kg" },
            };
            return MeasurementList;
        }
    }
}
