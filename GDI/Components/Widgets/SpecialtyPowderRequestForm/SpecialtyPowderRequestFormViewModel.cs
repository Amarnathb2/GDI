using GDI.Business.Models;

namespace GDI.Components.Widgets.SpecialtyPowderRequestForm
{
    public class SpecialtyPowderRequestFormViewModel
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
        public IEnumerable<ContactFormStatesData>? Getstates { get; set; }

        public IEnumerable<ContactFormCountriesData>? GetCountries { get; set; }

    }
}
