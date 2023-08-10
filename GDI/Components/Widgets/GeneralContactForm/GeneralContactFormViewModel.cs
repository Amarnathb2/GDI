using GDI.Business.Models;

namespace GDI.Components.Widgets.GeneralContactForm
{
    public class GeneralContactFormViewModel
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
        /// Get State list
        /// </summary>
        public IEnumerable<ContactFormStatesData>? Getstates { get; set; }

        /// <summary>
        /// Get Countries List
        /// </summary>
        public IEnumerable<ContactFormCountriesData>? GetCountries { get; set; }
    }

    public class ContactUsForm
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? OptIn { get; set; }
        public string? Comments { get; set; }
    }
}
