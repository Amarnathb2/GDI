using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDI.Components.Widgets.Location
{
    public class LocationViewModel
    {
        public bool Visible { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ZipCode { get; set; }
        public string? Phone { get; set; }

        public string? Email { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

    }
}
