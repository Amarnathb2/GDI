using GDI.Business.Models;

namespace GDI.Components.Widgets.ProductRequestForm
{
    public class ProductRequestFormViewModel
    {
        /// <summary>
        /// Declaring the title field to fetch the Title Data
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Declaring the Subtitle field to fetch the Title Data
        /// </summary>
        public string? Subtitle { get; set; }
        /// <summary>
        ///  IsVisibility Feature to Editor
        /// </summary>
        public bool? IsVisible { get; set; } = true;
        /// <summary>
        /// LolDairyUrl field to fetch the link Text
        /// </summary>
        public IEnumerable<ContactFormStatesData>? Getstates { get; set; }

        public IEnumerable<ContactFormCountriesData>? GetCountries { get; set; }

    }
}
